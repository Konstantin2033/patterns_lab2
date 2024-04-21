using System.Data.SqlClient;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace Post.Classes
{
    internal class DataBaseAdapter
    {
        private string connectionString;

        public DataBaseAdapter(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateTables()
        {

            string createUsersTableQuery = @"
            IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
            BEGIN
                CREATE TABLE Users (
                    Id BIGINT PRIMARY KEY IDENTITY,
                    FirstName NVARCHAR(30),
                    LastName NVARCHAR(30),
                    Login NVARCHAR(30) UNIQUE,
                    Password NVARCHAR(30))
            END";

            string createParcelsTableQuery = @"
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
            END";

            string createParcelsCheckPointsTableQuery = @"
            IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CheckPoints')
            BEGIN
                CREATE TABLE CheckPoints (
                    Id BIGINT PRIMARY KEY IDENTITY,
                    ParcelId BIGINT,
                    Description NVARCHAR(MAX),
                    Time DATETIME,
                    FOREIGN KEY (ParcelId) REFERENCES Parcels(Id))
            END";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(createUsersTableQuery, connection);
                    command.ExecuteNonQuery();
                    command.CommandText = createParcelsTableQuery;
                    command.ExecuteNonQuery();
                    command.CommandText = createParcelsCheckPointsTableQuery;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex) { Warning.ShowMessage(ex.Message); }
                finally { connection.Close(); }
            }
        }

        /*----------------------------------------------------------------------------------------*/
        //Users
        public void AddUser(User user)
        {
            string insertUserQuery = "INSERT INTO Users (FirstName, LastName, Login, Password) VALUES (@FirstName, @LastName, @Login, @Password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertUserQuery, connection);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Login", user.Login);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex) { Warning.ShowMessage(ex.Message); }
                finally { connection.Close(); }
            }
        }

        public List<User>? GetAllUsers()
        {
            List<User>? users = new List<User>();
            string selectUsersQuery = "SELECT * FROM Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(selectUsersQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        List<Parcel>? parcels = GetAllParcels();
                        long userId = (long)reader["Id"];

                        User user = new User
                        (
                            userId,
                            reader["FirstName"].ToString(),
                            reader["LastName"].ToString(),
                            reader["Login"].ToString(),
                            reader["Password"].ToString(),
                            parcels.FindAll(parcel => parcel.UserId == userId)
                        );
                        users.Add(user);
                    }
                    reader.Close();
                }
                catch (Exception ex) { Warning.ShowMessage(ex.Message); }
                finally { connection.Close(); }
                return users;
            }
        }
        /*----------------------------------------------------------------------------------------*/
        //Parcels

        public void AddParsel(Parcel parcel)
        {
            string insertParcelQuery = "INSERT INTO Parcels (UserId, Number, Name, Description, Recipient, SendingTime, ReceivedTime, IsDelivered) " +
                "VALUES (@UserId, @Number, @Name, @Description, @Recipient, @SendingTime, @ReceivedTime, @IsDelivered)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertParcelQuery, connection);
                    command.Parameters.AddWithValue("@UserId", parcel.UserId);
                    command.Parameters.AddWithValue("@Number", parcel.Number);
                    command.Parameters.AddWithValue("@Name", parcel.Name);
                    command.Parameters.AddWithValue("@Description", parcel.Description);
                    command.Parameters.AddWithValue("@Recipient", parcel.Recipient);
                    command.Parameters.AddWithValue("@SendingTime", parcel.SendingTime);
                    command.Parameters.AddWithValue("@ReceivedTime", parcel.ReceivedTime == null ? DBNull.Value : parcel.ReceivedTime);
                    command.Parameters.AddWithValue("@IsDelivered", parcel.IsDelivered);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex) { Warning.ShowMessage(ex.Message); }
                finally { connection.Close(); }
            }
        }

        public void UpdateParcel(Parcel parcel)
        {
            string updateParcelQuery = "UPDATE Parcels SET ReceivedTime = @ReceivedTime, IsDelivered = @IsDelivered " +
                                       "WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(updateParcelQuery, connection);
                    command.Parameters.AddWithValue("@ReceivedTime", parcel.ReceivedTime);
                    command.Parameters.AddWithValue("@IsDelivered", parcel.IsDelivered);
                    command.Parameters.AddWithValue("@Id", parcel.Id);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex) { Warning.ShowMessage(ex.Message); }
                finally { connection.Close(); }
            }
        }

        public List<Parcel>? GetAllParcels()
        {

            List<Parcel>? parcels = new List<Parcel>();
            string selectParcelsQuery = "SELECT * FROM Parcels";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(selectParcelsQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        List<CheckPoint>? checkPoints = GetAllCheckPoints();
                        long parcelId = (long)reader["Id"];

                        Parcel parcel = new Parcel(
                            parcelId,
                            (long)reader["UserId"],
                            reader["Number"].ToString(),
                            reader["Name"].ToString(),
                            reader["Description"].ToString(),
                            reader["Recipient"].ToString(),
                            (DateTime)reader["SendingTime"],
                            reader["ReceivedTime"] == DBNull.Value ? null : (DateTime)reader["ReceivedTime"],
                            checkPoints.FindAll(point => point.ParcelId == parcelId),
                            (bool)reader["IsDelivered"]
                            );
                        parcels.Add(parcel);
                    }
                    reader.Close();
                }
                catch (Exception ex) { Warning.ShowMessage(ex.Message); }
                finally { connection.Close(); }
                return parcels;
            }
        }

        /*----------------------------------------------------------------------------------------*/
        //CheckPoint

        public void AddCheckPoint(CheckPoint checkPoint)
        {
            string insertCheckPointQuery = "INSERT INTO CheckPoints (ParcelId, Description, Time) " +
                "VALUES (@ParcelId, @Description, @Time)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertCheckPointQuery, connection);
                    command.Parameters.AddWithValue("@ParcelId", checkPoint.ParcelId);
                    command.Parameters.AddWithValue("@Description", checkPoint.Description);
                    command.Parameters.AddWithValue("@Time", checkPoint.Time);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex) { Warning.ShowMessage(ex.Message); }
                finally { connection.Close(); }
            }
        }

        public List<CheckPoint>? GetAllCheckPoints()
        {

            List<CheckPoint>? checkPoints = new List<CheckPoint>();
            string selectCheckPointsQuery = "SELECT * FROM CheckPoints";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(selectCheckPointsQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CheckPoint checkPoint = new CheckPoint
                        (
                            (long)reader["ParcelId"],
                            reader["Description"].ToString(),
                            (DateTime)reader["Time"]
                        );
                        checkPoints.Add(checkPoint);
                    }
                    reader.Close();
                }
                catch (Exception ex) { Warning.ShowMessage(ex.Message); }
                finally { connection.Close(); }
                return checkPoints;
            }
        }
    }
}
