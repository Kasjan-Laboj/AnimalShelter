using AnimalShelter.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter
{
    public class Database : IDatabase
    {
        private string _connectionString =
            "Host=localhost;" +
            "Username=postgres;" +
            "Password=1234;" +
            "Database=AnimalShelter";

        private NpgsqlConnection _connection;

        public Database()
        {
            _connection = new NpgsqlConnection(_connectionString);
        }

        public bool IsConnecting()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: ", ex);

                return false;
            }
        }

        public bool ExecuteNonQuery(string query, params NpgsqlParameter[] parameters)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddRange(parameters);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured", ex);
                return false;
            }
        }

        public int ExecuteScalar(string query, params NpgsqlParameter[] parameters)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddRange(parameters);
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: ", ex);
                return 0;
            }
        }


        public NpgsqlDataReader ExecuteQuery(string query, params NpgsqlParameter[] parameters)
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            try
            {
                var command = new NpgsqlCommand(query, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                connection.Close();
                return null;
            }
        }
    }
}
