using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalShelter.Interfaces;
using System.Data;

namespace TestProject1
{
    public class DatabaseTest : IDatabase
    {
        private readonly string _connectionStringTest =
            "Host=localhost;" +
            "Username=postgres;" +
            "Password=1234;" +
            "Database=AnimalShelterTest";

        private NpgsqlConnection _connection;
        public DatabaseTest()
        {
            _connection = new NpgsqlConnection(_connectionStringTest);
        }

        public bool IsConnecting()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionStringTest))
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
                using (var connection = new NpgsqlConnection(_connectionStringTest))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(query, connection)) { 
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
                using (var connection = new NpgsqlConnection(_connectionStringTest))
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
            var connection = new NpgsqlConnection(_connectionStringTest);
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
