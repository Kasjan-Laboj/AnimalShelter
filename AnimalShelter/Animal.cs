using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter
{
    public class Animal
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string Breed { get; set; }
        public string? HealthStatus { get; set; }

        public Animal(int id, string name, string type, int age,string breed, string healthStatus)
        {
            Id = id;
            Type = type;
            Name = name;
            Age = age;
            Breed = breed;
            HealthStatus = healthStatus;
        }
        public Animal()
        {
            
        }

        public override string ToString()
        {
            return $"Id: {Id} Type: {Type} Name: {Name} Age: {Age} Breed: {Breed} HealthStatus: {HealthStatus}";
        }
    }
}
