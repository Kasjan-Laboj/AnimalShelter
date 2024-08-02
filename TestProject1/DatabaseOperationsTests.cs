using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalShelter;
using AnimalShelter.Interfaces;
using FluentAssertions;
using Npgsql;

namespace TestProject1
{
    public class DatabaseOperationsTests: IClassFixture<DatabaseTest>
    {
        private readonly IDatabase _database;
        public DatabaseOperationsTests(DatabaseTest databaseTest)
        {
            _database = databaseTest;
        }

        [Fact]
        public void Should_add_animal_to_database()
        {
            var animalOperation = new AnimalOperations(_database);
            var animal = new Animal(0, "wewe", "pepe", 1, "wewe", "gut");

            bool isAnimalAdded = animalOperation.AddAnimal(animal);

            isAnimalAdded.Should().BeTrue("Animal should be added to database");
            
        }

        [Fact]
        public void Delete_animal_with_given_id_in_database()
        {
            var animalOperation = new AnimalOperations(_database);
            var animal = new Animal(0, "wewe", "keke", 1, "asd", "dsa");

            bool isAnimalAdded = animalOperation.AddAnimal(animal);
            isAnimalAdded.Should().BeTrue("Animal should be added to database");

            int animalId = animalOperation.FindIdOfLastAddedAnimal();

            bool isAnimalDeleted = animalOperation.DeleteAnimal(animalId);

            isAnimalDeleted.Should().BeTrue("Animal should be deleted from test databse");
        }

        [Fact]
        public void Should_not_delete_animal_from_database_because_of_not_existing_Id()
        {
            var animalOperation = new AnimalOperations(_database);

            var animal = new Animal(0, "wewe", "keke", 1, "asd", "dsa");

            int animalId = 99999;

            bool isAnimalDeleted = animalOperation.DeleteAnimal(animalId);

            isAnimalDeleted.Should().BeFalse($"Animal shouldnt be deleted from database because animal with id{animalId} not exists");
        }

        [Fact]
        public void Should_update_animal_in_databaseTest()
        {
            var animalOperation = new AnimalOperations(_database);
            var animal = new Animal(0, "wewe", "pepe", 1, "wewe", "gut");

            bool isAnimalAdded = animalOperation.AddAnimal(animal);
            isAnimalAdded.Should().BeTrue("Animal should be added to database");

            int animalId = animalOperation.FindIdOfLastAddedAnimal();
            animalId.Should().BeGreaterThan(0, "Animal ID should be greater than 0");

            var updatedAnimal = new Animal
            {
                Type = "newType",
                Name = "newName"
            };

            bool isAnimalUpdated = animalOperation.UpdateInformationAboutAnimal(animalId, updatedAnimal);
            isAnimalUpdated.Should().BeTrue("Animal should be updated in the database");

            //bool isAnimalDeleted = animalOperation.DeleteAnimal(animalId);
            //isAnimalDeleted.Should().BeTrue("Animal should be deleted from test database");
        }

        [Fact]
        public void User_can_login_to_program()
        {
            var employeeOperation = new EmployeeOperations(_database);

            bool isLogged = employeeOperation.Login("abc","123");

            isLogged.Should().BeTrue();
        }
    }
}
