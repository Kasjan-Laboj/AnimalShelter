namespace AnimalShelter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();

            // Create a new instance of AnimalOperations with the database instance
            AnimalOperations animalOperations = new AnimalOperations(database);

            // Retrieve the list of all animals from the database
            List<Animal> animals = animalOperations.GetAllAnimals();

            // Print details of each animal
            foreach (Animal animal in animals)
            {
                Console.WriteLine(animal);
            }


        }
    }
}
