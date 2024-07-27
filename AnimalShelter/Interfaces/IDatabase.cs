using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.Interfaces
{
    public interface IDatabase
    {
        bool IsConnecting();
        bool ExecuteNonQuery(string query, params NpgsqlParameter[] parameters);
        int ExecuteScalar(string query, params NpgsqlParameter[] parameters);
        NpgsqlDataReader ExecuteQuery(string query, params NpgsqlParameter[] parameters);
    }
}
