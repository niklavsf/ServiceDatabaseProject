using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceDatabaseProject
{
    public partial class UserAuthenticationForm : Form
    {
        public static long ActiveUserId { get; set; }
        public static bool isAdmin = false;
        private bool passwordChangeMode = false;
        private AppUser pendingUser = null;
        public UserAuthenticationForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ErrorMessageText.Text = "";

            PasswordTextbox.UseSystemPasswordChar = true;
            this.AcceptButton = LoginButton;
        }

        public class AppUser
        {
            public long UserId { get; set; }
            public string Username { get; set; }
            public bool IsDisabled { get; set; }
            public bool IsFirstLogin { get; set; } 
            public string UserType { get; set; }   
            public string Name { get; set; }
            public string Surname { get; set; }
            public int PasswordChange { get; set; }
        }

        public static class AuthService
        {
            public static AppUser TryLogin(string username, string password)
            {
                using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand(@"
                        SELECT
                            u.USER_ID,
                            u.USERNAME,
                            u.PASSWORD_HASH,
                            u.LAST_LOGIN,
                            u.DISABLED,
                            u.NAME,
                            u.SURNAME,
                            u.PASSWORD_CHANGE,
                            t.USER_TYPE
                        FROM USERS u
                        JOIN USERTYPE t ON t.USERTYPE_ID = u.USERTYPE_ID
                        WHERE UPPER(u.USERNAME) = UPPER(:username)
                    ", conn))
                    {
                        cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            string storedHash = reader["PASSWORD_HASH"] == DBNull.Value ? null : reader["PASSWORD_HASH"].ToString();
                            bool passOk = PasswordHasher.VerifyPassword(password, storedHash);

                            if (!passOk)
                                return null;

                            long userId = Convert.ToInt64(reader["USER_ID"]);
                            bool disabled = reader["DISABLED"] != DBNull.Value && Convert.ToInt64(reader["DISABLED"]) == 1;
                            bool firstLogin = reader["LAST_LOGIN"] == DBNull.Value;
                            int passwordChange = Convert.ToInt32(reader["PASSWORD_CHANGE"]);

                            AppUser user = new AppUser();
                            user.UserId = userId;
                            user.Username = reader["USERNAME"].ToString();
                            user.IsDisabled = disabled;
                            user.IsFirstLogin = firstLogin;
                            user.UserType = reader["USER_TYPE"].ToString();
                            user.Name = reader["NAME"] == DBNull.Value ? "" : reader["NAME"].ToString();
                            user.Surname = reader["SURNAME"] == DBNull.Value ? "" : reader["SURNAME"].ToString();
                            user.PasswordChange = passwordChange;   

                            ActiveUserId = userId;
                            if (user.UserType.ToUpper() == "ADMIN")
                            {
                                isAdmin = true;
                            }

                            return user;
                        }
                    }
                }
            }

            public static void UpdateLastLogin(long userId)
            {
                using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(
                        "UPDATE USERS SET LAST_LOGIN = SYSDATE WHERE USER_ID = :id", conn))
                    {
                        cmd.Parameters.Add("id", OracleDbType.Int64).Value = userId;
                        cmd.ExecuteNonQuery();
                    }
                }
                ActionLogger.LogUserAction("", "Log in", userId, null);
            }

            public static void SetNewPasswordAndEnable(long userId, string newPassword)
            {
                string newHash = PasswordHasher.HashPassword(newPassword);

                using (OracleConnection conn = new OracleConnection(HomeScreen.connString))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(@"
                        UPDATE USERS
                        SET PASSWORD_HASH = :hash,
                            PASSWORD_CHANGE = 0,
                            DISABLED = 0,
                            LAST_LOGIN = SYSDATE
                        WHERE USER_ID = :id
                    ", conn))
                    {
                        cmd.BindByName = true;
                        cmd.Parameters.Add(":hash", OracleDbType.Varchar2).Value = newHash;
                        cmd.Parameters.Add(":id", OracleDbType.Int64).Value = userId;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private bool PasswordValidation()
        {
            string first = UsernameTextbox.Text;
            string second = PasswordTextbox.Text;

            if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(second))
            {
                ErrorMessageText.Text = "Please fill both fields";
                return false;
            }

            if (first.Length < 8)
            {
                ErrorMessageText.Text = "The password must be at least 8 characters";
                return false;
            }

            if (first != second)
            {
                ErrorMessageText.Text = "Passwords do not match";
                return false;
            }

            ErrorMessageText.Text = "";
            return true;
        }

        // Helpers

        private void EnterPasswordChangeMode(AppUser user)
        {
            passwordChangeMode = true;
            pendingUser = user;

            FirstLabel.Text = "New password";
            SecondLabel.Text = "New password repeat";

            UsernameTextbox.Text = "";
            PasswordTextbox.Text = "";

            UsernameTextbox.UseSystemPasswordChar = true;
            PasswordTextbox.UseSystemPasswordChar = true;

            LoginButton.Text = "Set Password";

            ErrorMessageText.Text = "You must change your temporary password.";
        }


        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (passwordChangeMode == true)
            {
                bool validated = PasswordValidation();
                if (validated == false)
                {
                    return;
                }

                string newPassword = UsernameTextbox.Text;

                AuthService.SetNewPasswordAndEnable(pendingUser.UserId, newPassword);

                pendingUser.PasswordChange = 0;
                pendingUser.IsDisabled = false;

                AppSession.CurrentUser = pendingUser;

                this.Hide();
                using (HomeScreen home = new HomeScreen(isAdmin))
                {
                    home.ShowDialog(this);
                }
                this.Close();
                return;
            }

            // MODE 1: normal login (your original logic)
            string username = UsernameTextbox.Text.Trim();
            string password = PasswordTextbox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Enter username and password.");
                return;
            }

            AppUser user = AuthService.TryLogin(username, password);

            if (user == null)
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }

            if (user.PasswordChange == 1)
            {
                EnterPasswordChangeMode(user);
                return;
            }

            // For now: block disabled accounts (first-login logic later)
            if (user.IsDisabled)
            {
                MessageBox.Show("Account is disabled. Contact an administrator.");
                return;
            }

            // Normal successful login: update last login
            AuthService.UpdateLastLogin(user.UserId);

            // Store user globally if you want role checks later
            AppSession.CurrentUser = user;

            // Open main window
            this.Hide();
            using (HomeScreen home = new HomeScreen(isAdmin))
            {
                home.ShowDialog(this);
            }
            this.Close();
        }


        private void DebugHashButton_Click(object sender, EventArgs e)
        {
            string plain = PasswordTextbox.Text; // or a separate textbox
            string hash = PasswordHasher.HashPassword(plain);
            MessageBox.Show(hash);
        }
    }
}

