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

        public Animal(int id, string type, string name, int age,string breed, string healthStatus)
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

        public Animal GetUpdatedAnimal(Animal newAnimal)
        {
            return new Animal
            {
                Id = this.Id,
                Type = string.IsNullOrEmpty(newAnimal.Type) ? this.Type : newAnimal.Type,
                Name = string.IsNullOrEmpty(newAnimal.Name) ? this.Name : newAnimal.Name,
                Age = newAnimal.Age ?? this.Age,
                Breed = string.IsNullOrEmpty(newAnimal.Breed) ? this.Breed : newAnimal.Breed,
                HealthStatus = string.IsNullOrEmpty(newAnimal.HealthStatus) ? this.HealthStatus : newAnimal.HealthStatus
            };
        }
    }
}
