using AnimalShelter;
using FluentAssertions;

namespace TestProject1
{
    public class DatabaseConnectionTests
    {
        [Fact]
        public void Database_should_connect()
        {
            var databaseTest = new DatabaseTest();

            bool isConnected = databaseTest.IsConnecting();

            isConnected.Should().BeTrue("Database connection should be successful(True)");
        }
    }
}