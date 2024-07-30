using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter
{
    internal class UserInterface
    {
        EmployeeOperations ? employeeOperations;
        public void Program()
        {
            Console.WriteLine("Staff shelter access");
            Console.Write("Enter login: ");
            string login = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            employeeOperations.Login(login,password);
        }
    }
}
