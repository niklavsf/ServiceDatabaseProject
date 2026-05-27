using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ServiceDatabaseProject
{
    public static class NotificationStore
    {
        private static string FolderPath
        {
            get
            {
                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "ServiceDatabaseProject"
                );
                Directory.CreateDirectory(path);
                return path;
            }
        }

        private static string FilePath => Path.Combine(FolderPath, "notifications.json");

        public static List<NotificationItem> Load()
        {
            if (!File.Exists(FilePath))
                return new List<NotificationItem>();

            string json = File.ReadAllText(FilePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<NotificationItem>();

            List<NotificationItem> items = JsonSerializer.Deserialize<List<NotificationItem>>(json);
            return items ?? new List<NotificationItem>();
        }

        public static void Save(List<NotificationItem> items)
        {
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(items, opt);
            File.WriteAllText(FilePath, json);
        }
    }

    public class NotificationItem
    {
        public long ServiceId { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRead { get; set; }
        public int WeekNumber { get; set; }
    }
}
