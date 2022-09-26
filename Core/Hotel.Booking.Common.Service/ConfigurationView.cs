using Hotel.Booking.Common.Contract.Services;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Hotel.Booking.Common.Service
{
    public class ConfigurationView : IConfigurationView
    {
        private static Dictionary<string, string> _connectionStrings = new();

        private readonly IConfiguration _config;

        public ConfigurationView(IConfiguration config)
        {
            _config = config;
        }

        public string AppSettings(string name)
        {
            var value = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(value))
            {
                value = _config[name];
            }
            return value ?? ConfigurationManager.AppSettings[name];
        }

        public string ConnectionStrings(string name)
        {
            LoadConnectionStrings();

            _connectionStrings.TryGetValue(name, out var value);

            if (string.IsNullOrWhiteSpace(value))
            {
                value = _config.GetConnectionString(name);
            }
            return value ?? ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
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
                map.Add(name, value?.ToString());
            }
            _connectionStrings = map;
        }
    }
}
