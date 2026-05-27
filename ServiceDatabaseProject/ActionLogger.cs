using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ServiceDatabaseProject
{
    internal static class ActionLogger
    {
        public static void LogUserAction(string table, string action, long userId, long? entityId)
        {
            string logCommand = @"
                INSERT INTO adminlog
                (adminlog_id, log_time, user_id, action, tablename, entity_id)
                VALUES
                (alogpk.NEXTVAL, :log_time, :user_id, :action, :tablename, :entity_id)";
            using (OracleConnection conn = new OracleConnection(ServiceDatabaseProject.HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(logCommand, conn))
                {

                    cmd.BindByName = true;

                    cmd.Parameters.Add(":log_time", OracleDbType.Date).Value = DateTime.Now;
                    cmd.Parameters.Add(":user_id", OracleDbType.Int64).Value = userId;
                    cmd.Parameters.Add(":action", OracleDbType.Varchar2).Value = action;
                    cmd.Parameters.Add(":tablename", OracleDbType.Varchar2).Value = table;
                    cmd.Parameters.Add(":entity_id", OracleDbType.Int64).Value = entityId;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void SetDeactivationTime(string tableName, long entityId)
        {
            string idColumn = $"{tableName}_ID";

            string updateCommand = $@"
                UPDATE {tableName}
                SET DEACTIVATED_AT = SYSDATE
                WHERE {idColumn} = :entity_id";

            using (OracleConnection conn = new OracleConnection(ServiceDatabaseProject.HomeScreen.connString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(updateCommand, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add(":entity_id", OracleDbType.Int64).Value = entityId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ClearDeactivationTime(string tableName, long entityId)
        {
            string idColumn = $"{tableName}_ID";

            string updateCommand = $@"
                UPDATE {tableName}
                SET DEACTIVATED_AT = NULL
                WHERE {idColumn} = :entity_id";

            using (OracleConnection conn = new OracleConnection(ServiceDatabaseProject.HomeScreen.connString))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand(updateCommand, conn))
                {
                    cmd.BindByName = true;
                    cmd.Parameters.Add(":entity_id", OracleDbType.Int64).Value = entityId;

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
