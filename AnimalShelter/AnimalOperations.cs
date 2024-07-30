using AnimalShelter.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter
{
    public class AnimalOperations
    {
        private readonly IDatabase _database;

        public AnimalOperations(IDatabase database)
        {
            _database = database;
        }
        public bool AddAnimal(Animal animal)
        {
            string query = "INSERT INTO ANIMALS (Type, Name, Age, Breed, HealthStatus) VALUES (@Type, @Name, @Age, @Breed, @HealthStatus)";

            var parameters = new[]
            {
                new NpgsqlParameter("@Type", animal.Type),
                new NpgsqlParameter("@Name", animal.Name),
                new NpgsqlParameter("@Age", animal.Age),
                new NpgsqlParameter("@Breed", animal.Breed),
                new NpgsqlParameter("@HealthStatus", animal.HealthStatus),
            };

            return _database.ExecuteNonQuery(query, parameters);
        }

        public bool DeleteAnimal(int animalIdToDelete)
        {
            string query = "DELETE FROM ANIMALS WHERE Id = @Id";
            //Animal animal = new Animal();

            var parameters = new[]
            {
                new NpgsqlParameter("@Id", animalIdToDelete),
            };

            bool isDeleted = _database.ExecuteNonQuery(query, parameters);

            if (!isDeleted)
            {
                Console.WriteLine($"Animal with ID {animalIdToDelete} does not exist and could not be deleted");
            }

            return isDeleted;
        }

        public int FindLastAddedAnimal()
        {
            string query = "SELECT MAX(ID) FROM ANIMALS";

            return _database.ExecuteScalar(query);
        }

        public List<Animal> GetAllAnimals()
        {
            string query = "SELECT * FROM ANIMALS";
            var animals = new List<Animal>();

            // Open connection and execute query
            using (var reader = _database.ExecuteQuery(query))
            {
                // Check if reader is not null and open
                if (reader != null)
                {
                    // Read all rows from the reader
                    while (reader.Read())
                    {
                        // Create a new Animal object and populate its properties
                        var animal = new Animal
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                            Age = reader.IsDBNull(reader.GetOrdinal("Age")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Age")),
                            Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            HealthStatus = reader.IsDBNull(reader.GetOrdinal("HealthStatus")) ? null : reader.GetString(reader.GetOrdinal("HealthStatus")),
                        };

                        // Add the Animal object to the list
                        animals.Add(animal);
                    }
                }
            }

            // Return the list of animals
            return animals;
        }

        public bool UpdateInformationAboutAnimal(int animalId, Animal newAnimal)
        {
            // Get the current animal data from the database
            string selectQuery = "SELECT * FROM ANIMALS WHERE Id = @Id";
            var selectParameters = new[]
            {
                new NpgsqlParameter("@Id", animalId),
            };

            Animal currentAnimal = null;
            using (var reader = _database.ExecuteQuery(selectQuery, selectParameters))
            {
                if (reader != null && reader.Read())
                {
                    currentAnimal = new Animal
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Type = reader.GetString(reader.GetOrdinal("Type")),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                        Age = reader.IsDBNull(reader.GetOrdinal("Age")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Age")),
                        Breed = reader.GetString(reader.GetOrdinal("Breed")),
                        HealthStatus = reader.IsDBNull(reader.GetOrdinal("HealthStatus")) ? null : reader.GetString(reader.GetOrdinal("HealthStatus")),
                    };
                }
            }

            if (currentAnimal == null)
            {
                Console.WriteLine($"Animal with ID {animalId} does not exist.");
                return false;
            }

            // Get the updated animal object
            var updatedAnimal = currentAnimal.GetUpdatedAnimal(newAnimal);

            // Update the database with the new values
            string updateQuery = "UPDATE ANIMALS SET Type = @Type, Name = @Name, Age = @Age, Breed = @Breed, HealthStatus = @HealthStatus WHERE Id = @Id";
            var updateParameters = new[]
            {
                new NpgsqlParameter("@Type", updatedAnimal.Type),
                new NpgsqlParameter("@Name", updatedAnimal.Name),
                new NpgsqlParameter("@Age", updatedAnimal.Age),
                new NpgsqlParameter("@Breed", updatedAnimal.Breed),
                new NpgsqlParameter("@HealthStatus", updatedAnimal.HealthStatus),
                new NpgsqlParameter("@Id", updatedAnimal.Id),
            };

            return _database.ExecuteNonQuery(updateQuery, updateParameters);
        }
    }
}
