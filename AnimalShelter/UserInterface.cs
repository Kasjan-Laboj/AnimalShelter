using AnimalShelter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter
{
    public class UserInterface
    {
        private readonly EmployeeOperations _employeeOperations;
        private readonly IDatabase _database;
        private readonly AnimalOperations _animalOperations;
        public UserInterface(IDatabase database)
        {
            _database = database;
            _employeeOperations = new EmployeeOperations(_database);
            _animalOperations = new AnimalOperations(_database);
        }


        public void Program()
        {
            Console.WriteLine("Staff shelter access");
            Console.Write("Enter login: ");
            string login = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            //bool isLogged = _employeeOperations.Login(login, password);

            if (_employeeOperations.Login(login, password))
            {
                Console.WriteLine("Welcome");

                Console.WriteLine("Choose operation");

                Console.WriteLine(
                    "1-Display all animals" +
                    "2-Add animal" +
                    "3-Delete animal" +
                    "4-Update animal" +
                    "5-Display id of newest animal" +
                    "6-Log out" +
                    "7-Close program");


                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Choose operation from 1-7");
                }

                int id = 0;
                int age, idToUpdate;
                string type, name, breed, healthStatus;

                switch (choice)
                {
                    case 1:
                        var animalList = new List<Animal>();
                        animalList = _animalOperations.GetAllAnimals();

                        foreach (var an in animalList)
                        {
                            Console.WriteLine(an);
                        }
                        break;
                    case 2:
                        //create method for getting all vars
                        Console.WriteLine("Enter type of animal");
                        type = Console.ReadLine();
                        Console.WriteLine("Enter name of animal");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter age of animal");
                        while (!int.TryParse(Console.ReadLine(), out age) || age > 0 || age < 15)
                        {
                            Console.WriteLine("Enter correct age");
                        }
                        Console.WriteLine("Enter breed of animal");
                        breed = Console.ReadLine();
                        Console.WriteLine("Enter health status of animal");
                        healthStatus = Console.ReadLine();

                        var animal = new Animal(id, type, name, age, breed, healthStatus);

                        _animalOperations.AddAnimal(animal);
                        if (_animalOperations.AddAnimal(animal))
                        {
                            Console.WriteLine("Operation successfully");
                        }
                        else
                        {
                            Console.WriteLine("Something goes wrong try again");
                        }

                        break;
                    case 3:
                        Console.WriteLine("Enter the ID of the animal to be deleted");
                        int idToDelete;
                        //add metod to check if animal exists with given id
                        while (!int.TryParse(Console.ReadLine(), out idToDelete) || idToDelete > 0)
                        {
                            Console.WriteLine("Id cannot be negative");
                        }

                        if (_animalOperations.DeleteAnimal(idToDelete))
                        {
                            Console.WriteLine("Operation successfully");
                        }
                        else
                        {
                            Console.WriteLine("Something goes wrong try again");
                        }

                        break;
                    case 4:
                        Console.WriteLine("Choose animal id to update");
                        //add metod to check if animal exists with given id
                        while (!int.TryParse(Console.ReadLine(), out idToUpdate) || idToUpdate > 0)
                        {
                            Console.WriteLine("Id cannot be negative");
                        }

                        Console.WriteLine("LEAVE BLANK (CLICK ENTER) TO SAVE PREVIOUS INFORMATION");
                        //create method for getting all vars
                        Console.WriteLine("Enter type of animal");
                        type = Console.ReadLine();
                        Console.WriteLine("Enter name of animal");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter age of animal");
                        while (!int.TryParse(Console.ReadLine(), out age) || age > 0 || age < 15)
                        {
                            Console.WriteLine("Enter correct age");
                        }
                        Console.WriteLine("Enter breed of animal");
                        breed = Console.ReadLine();
                        Console.WriteLine("Enter health status of animal");
                        healthStatus = Console.ReadLine();

                        animal = new Animal();

                        if (_animalOperations.UpdateInformationAboutAnimal(idToUpdate, animal))
                        {
                            Console.WriteLine("Operation successfully");
                        }
                        else
                        {
                            Console.WriteLine("Something goes wrong try again");
                        }

                        break;
                    case 5:
                        Console.WriteLine($"Last id of added animal is: {_animalOperations.FindIdOfLastAddedAnimal()}");
                        break;
                    case 6:
                        //isLogged = false;
                        break;
                    case 7:
                        Console.WriteLine("Closing the program...");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                }

            }
            else
            {
                Console.WriteLine("Try again");
            }



        }
    }
}
