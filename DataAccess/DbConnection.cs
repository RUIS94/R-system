using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace DataAccess
{
    public class DbConnection
    {
        private readonly string connectionString;

        /// <summary>
        /// Default constructor - loads config from config/database_config.json
        /// </summary>
        public DbConnection() : this(
            Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "config", "database_config.json"))
        {
        }
        public string ConnectionString => connectionString;
        /// <summary>
        /// Constructor with custom config path, useful for testing or advanced use cases
        /// </summary>
        /// <param name="configPath">Path to the configuration file</param>
        public DbConnection(string configPath)
        {
            if (string.IsNullOrWhiteSpace(configPath))
                throw new ArgumentException("Configuration file path cannot be null or empty", nameof(configPath));

            connectionString = LoadConnectionString(configPath);
        }

        /// <summary>
        /// Loads the database connection string from a JSON config file
        /// </summary>
        /// <param name="configPath">Full path to the config file</param>
        /// <returns>A valid MySQL connection string</returns>
        private string LoadConnectionString(string configPath)
        {
            if (!File.Exists(configPath))
                throw new FileNotFoundException("Configuration file not found", configPath);

            var json = File.ReadAllText(configPath);
            var config = JsonConvert.DeserializeObject<DatabaseConfig>(json);

            if (config?.ConnectionString == null)
                throw new Exception("Invalid or missing 'ConnectionString' section in config file");

            return $"server={config.ConnectionString.Server};" +
                   $"user={config.ConnectionString.User};" +
                   $"database={config.ConnectionString.Database};" +
                   $"port={config.ConnectionString.Port};" +
                   $"password={config.ConnectionString.Password};";
        }

        /// <summary>
        /// Creates and opens a connection to the database
        /// </summary>
        /// <returns>An open MySqlConnection</returns>
        public MySqlConnection ConnectToDatabase()
        {
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Database connection successful!");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
                throw;
            }
        }
    }

    public class DatabaseConfig
    {
        public ConnectionString? ConnectionString { get; set; }
    }

    public class ConnectionString
    {
        public string? Server { get; set; }
        public string? User { get; set; }
        public string? Database { get; set; }
        public string? Port { get; set; }
        public string? Password { get; set; }
    }
}