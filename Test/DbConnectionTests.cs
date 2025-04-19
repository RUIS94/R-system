using NUnit.Framework;
using DataAccess;
using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace Test
{
    public class DbConnectionTests
    {
        private DbConnection dbConnection;
        private string configPath;

        [SetUp]
        public void Setup()
        {
            var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            configPath = Path.Combine(projectRoot, "config", "database_config.json");

            Console.WriteLine($"Config path£º{configPath}");

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("Database configuration file not found", configPath);
            }

            dbConnection = new DbConnection(configPath);
        }

        [Test]
        public void Test_LoadConnectionString_ShouldReturnValidConnectionString()
        {
            var connectionString = dbConnection.GetType().GetMethod("LoadConnectionString", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(dbConnection, new object[] { configPath }) as string;

            Assert.IsNotNull(connectionString);
            Assert.IsTrue(connectionString.Contains("server="));
            Assert.IsTrue(connectionString.Contains("user="));
            Assert.IsTrue(connectionString.Contains("password="));
        }

        [Test]
        public void Test_ConnectToDatabase_ShouldOpenConnection()
        {
            MySqlConnection connection = null;

            try
            {
                connection = dbConnection.ConnectToDatabase();

                Assert.IsNotNull(connection);
                Assert.AreEqual(System.Data.ConnectionState.Open, connection.State);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Database connection failed: {ex.Message}");
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}