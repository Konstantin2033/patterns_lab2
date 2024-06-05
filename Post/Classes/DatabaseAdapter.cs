using System.Data.SqlClient;

namespace Post.Classes
{
    public class DatabaseAdapter
    {
        private string connectionString;

        public DatabaseAdapter(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateTables()
        {
            string[] createTableQueries = new string[]
            {
                @"
                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
                BEGIN
                    CREATE TABLE Users (
                        Id BIGINT PRIMARY KEY IDENTITY,
                        FirstName NVARCHAR(30),
                        LastName NVARCHAR(30),
                        Login NVARCHAR(30) UNIQUE,
                        Password NVARCHAR(30))
                END",
                @"
                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Parcels')
                BEGIN
                    CREATE TABLE Parcels (
                        Id BIGINT PRIMARY KEY IDENTITY,
                        UserId BIGINT,
                        Number NVARCHAR(25) UNIQUE,
                        Name NVARCHAR(50),
                        Description NVARCHAR(MAX) NULL,
                        Recipient NVARCHAR(MAX),
                        SendingTime DATETIME,
                        ReceivedTime DATETIME NULL,
                        IsDelivered BIT,
                        FOREIGN KEY (UserId) REFERENCES Users(Id))
                END",
                @"
                IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CheckPoints')
                BEGIN
                    CREATE TABLE CheckPoints (
                        Id BIGINT PRIMARY KEY IDENTITY,
                        ParcelId BIGINT,
                        Description NVARCHAR(MAX),
                        Time DATETIME,
                        FOREIGN KEY (ParcelId) REFERENCES Parcels(Id))
                END"
            };

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (string query in createTableQueries)
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error occurred while creating tables: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
