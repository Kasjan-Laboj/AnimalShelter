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
            int id = 0;
            int age, idToOperate, choice;
            string type, name, breed, healthStatus, login, password;
            bool isLogged;

            do
            {
                Console.WriteLine("Staff shelter access");

                GetLoginAndPasswordFromUser(out login, out password);

                isLogged = _employeeOperations.Login(login, password);

                if (!isLogged)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect data");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }

                while (isLogged)
                {
                    DisplayOptions();

                    while (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Choose operation from 1-7");
                    }

                    switch (choice)
                    {
                        case 1:
                            var animalList = new List<Animal>();
                            animalList = _animalOperations.GetAllAnimals();

                            foreach (var an in animalList)
                            {
                                Console.WriteLine(an);
                            }

                            Console.WriteLine("Press enter to continue");
                            Console.ReadKey();
                            break;
                        case 2:
                            GetAllVariablesForOperationOnAnimal(out type, out name, out age, out breed, out healthStatus);

                            var animal = new Animal(id, type, name, age, breed, healthStatus);

                            if (_animalOperations.AddAnimal(animal))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Operation successfully");
                                Console.ForegroundColor = ConsoleColor.White;
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Something goes wrong try again");
                                Console.ForegroundColor = ConsoleColor.White;
                                Thread.Sleep(1000);
                            }

                            break;
                        case 3:
                            Console.WriteLine("Enter the ID of the animal to be deleted");

                            GetIdOfAnimalToOperate(out idToOperate);

                            if (_animalOperations.DeleteAnimal(idToOperate))
                            {
                                Console.WriteLine("Operation successfully");
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                Console.WriteLine("Press Enter to continue");
                                Console.ReadKey();
                            }
                            break;
                        case 4:
                            Console.WriteLine("Choose animal id to update");

                            GetIdOfAnimalToOperate(out idToOperate);

                            Console.WriteLine("Leave empty space to save old data");

                            GetAllVariablesForOperationOnAnimal(out type, out name, out age, out breed, out healthStatus);

                            animal = new Animal()
                            {
                                Type = type,
                                Name = name,
                                Age = age,
                                Breed = breed,
                                HealthStatus = healthStatus
                            };

                            if (_animalOperations.UpdateInformationAboutAnimal(idToOperate, animal))
                            {
                                DisplayInformation("Operation successfully",1000);
                            }
                            else
                            {
                                Console.WriteLine("Press Enter to continue");
                                Console.ReadKey();
                            }

                            break;
                        case 5:
                            Console.WriteLine($"Last id of added animal is: {_animalOperations.FindIdOfLastAddedAnimal()}");

                            Console.WriteLine("Press enter to continue");
                            Console.ReadKey();
                            break;
                        case 6:
                            DisplayInformation("Loggin out...",1000);
                            isLogged = false;
                            continue;
                        //break;
                        case 7:
                            DisplayInformation("Closing the program...", 1000);
                            Environment.Exit(0);
                            break;
                    }
                }

                Console.Clear();
            } while (true);
        }

        private void GetIdOfAnimalToOperate(out int idToOperate)
        {
            while (!int.TryParse(Console.ReadLine(), out idToOperate) || idToOperate < 0)
            {
                Console.WriteLine("Id cannot be negative");
            }
        }
        private void DisplayInformation(string text, int threadLength)
        {
            Console.WriteLine($"{text}");
            Thread.Sleep(threadLength);
        }
        private void GetAllVariablesForOperationOnAnimal(out string type, out string name, out int age, out string breed, out string healthStatus)
        {
            Console.WriteLine("Enter type of animal");
            type = Console.ReadLine();
            Console.WriteLine("Enter name of animal");
            name = Console.ReadLine();
            Console.WriteLine("Enter age of animal");
            //dry
            while (!int.TryParse(Console.ReadLine(), out age) || age < 0 || age > 15)
            {
                Console.WriteLine("Enter correct age");
            }
            Console.WriteLine("Enter breed of animal");
            breed = Console.ReadLine();
            Console.WriteLine("Enter health status of animal");
            healthStatus = Console.ReadLine();
        }
        private void GetLoginAndPasswordFromUser(out string login, out string password)
        {
            Console.Write("Enter login: ");
            login = Console.ReadLine();
            Console.Write("Enter password: ");
            password = Console.ReadLine();
        }

        private void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("Welcome\n-----------------");

            Console.WriteLine("Choose operation:\n");

            Console.WriteLine(
                "1-Display all animals\n" +
                "2-Add animal\n" +
                "3-Delete animal\n" +
                "4-Update animal\n" +
                "5-Display id of newest animal\n" +
                "6-Log out\n" +
                "7-Close program");
        }
    }
}
