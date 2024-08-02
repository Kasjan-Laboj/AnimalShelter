using AnimalShelter.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter
{
    public class EmployeeOperations
    {
        private readonly IDatabase _database;

        public EmployeeOperations(IDatabase database)
        {
            _database = database;
        }

        public bool Login(string login, string password)
        {
            string query = "SELECT COUNT(*) FROM employees WHERE login = @login AND password = @password";

            var parameters = new[]
            {
                new NpgsqlParameter("@login", login),
                new NpgsqlParameter("@password", password)
            };

            int result = _database.ExecuteScalar(query, parameters);

            return result > 0;
        }
    }
}
