using System.Text.Json;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Caching
{
    public class RedisHelper : IRedisHelper
    {
        private readonly IDatabase _db;
        private readonly RedisConfig _redisConfig;

        public RedisHelper(string configPath = "config/redis_config.json")
        {
            var configFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, configPath);

            _redisConfig = LoadRedisConfig(configFilePath);

            var configuration = ConfigurationOptions.Parse($"{_redisConfig.Host}:{_redisConfig.Port}");
            configuration.Password = _redisConfig.Password;
            configuration.DefaultDatabase = _redisConfig.Database;

            var connection = ConnectionMultiplexer.Connect(configuration);
            _db = connection.GetDatabase();
        }

        private RedisConfig LoadRedisConfig(string configPath)
        {
            if (!File.Exists(configPath))
                throw new FileNotFoundException("Configuration file not found", configPath);

            var json = File.ReadAllText(configPath);
            var config = JsonConvert.DeserializeObject<RedisConfig>(json);

            if (config == null)
                throw new Exception("Invalid or missing Redis configuration in config file");

            return config;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonConvert.SerializeObject(value);
            await _db.StringSetAsync(key, json, expiry);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            return !string.IsNullOrEmpty(value) ? JsonConvert.DeserializeObject<T>(value) : default;
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }

    public class RedisConfig
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Password { get; set; }
        public int Database { get; set; }
    }
}
