using System.Configuration;

namespace Hotel.Booking.Common.Utility
{
    public static class Configuration
    {
        private static IDictionary<string, string> _connectionStrings;

        public static string AppSettings(string name)
        {
            var value = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);            
            return value ?? ConfigurationManager.AppSettings[name] ?? throw new ArgumentException($"No app settings with name {name} was found");
        }

        public static string ConnectionStrings(string name)
        {
            LoadConnectionStrings();
            _connectionStrings.TryGetValue(name, out var value);
            return value ?? ConfigurationManager.ConnectionStrings[name]?.ConnectionString ?? throw new ArgumentException($"No connection string with name {name} was found");
        }

        private static void LoadConnectionStrings()
        {
            if (_connectionStrings != null)
                return;
            var variables = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);
            var map = new Dictionary<string, string>();
            foreach (var key in variables.Keys.Cast<string>())
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                var name = string.Empty;

                if (key.StartsWith("ConnectionStrings:"))
                {
                    var parts = key.Split(':');
                    name = parts[1];
                }
                else
                {
                    const string subtext = "CONNSTR_";

                    var index = key.IndexOf(subtext, StringComparison.OrdinalIgnoreCase);
                    if (index >= 0)
                    {
                        name = key[(index + subtext.Length)..];
                    }
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    continue;
                }

                var value = variables[key];

                map.Add(name, value?.ToString() ?? string.Empty);
            }
            _connectionStrings = map;
        }
    }
}