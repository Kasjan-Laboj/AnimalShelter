namespace AnimalShelter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            UserInterface userInterface = new UserInterface(database);

            userInterface.Program();

        }
    }
}
