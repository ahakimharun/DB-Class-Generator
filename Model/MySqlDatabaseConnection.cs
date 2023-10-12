using DB_Class_Generator.ViewModel;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;


namespace DB_Class_Generator.Model
{
    internal class MySqlDatabaseConnection
    {
        private MySqlConnection _connection;
        private readonly string _connectionstring;

        public MySqlDatabaseConnection (string ServerURL, uint? PortNumber, string DatabaseName, string UserName, string Password)
        {
            if (PortNumber == null)
            {
                _connectionstring = new MySqlConnectionStringBuilder
                {
                    Server = ServerURL,
                    Database = DatabaseName,
                    UserID = UserName,
                    Password = Password

                }.ToString();
            }
            else
            {
                _connectionstring = new MySqlConnectionStringBuilder
                {
                    Server = ServerURL,
                    Port = (uint) PortNumber,
                    Database = DatabaseName,
                    UserID = UserName,
                    Password = Password

                }.ToString();
            }
        }

        public MySqlDatabaseConnection (string connectionString)
        {
            _connectionstring = connectionString;
        }

        public async Task<IEnumerable<T>> ListTables<T> ()
        {
            using (_connection = new MySqlConnection(_connectionstring))
            {
                return await _connection.QueryAsync<T>("SHOW TABLES;", commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<T>> GetTableFields<T> (string TableName, string DatabaseName)
        {
            using (_connection = new MySqlConnection(_connectionstring))
            {
                return await _connection.QueryAsync<T>($"SHOW COLUMNS FROM {TableName} FROM {DatabaseName}", commandType: CommandType.Text);
            }
        }

    }
}
