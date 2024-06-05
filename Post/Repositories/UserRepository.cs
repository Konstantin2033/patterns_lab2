using System.Data.SqlClient;
using Post.Classes;

namespace Post.Repositories
{
    public class UserRepository
    {
        private string connectionString;
        private ParcelRepository parcelRepository;
        

        public UserRepository(string connectionString, ParcelRepository parcelRepository)
        {
            this.connectionString = connectionString;
            this.parcelRepository = parcelRepository;
        }

        public void AddUser(User user)
        {
            string insertUserQuery = "INSERT INTO Users (FirstName, LastName, Login, Password) VALUES (@FirstName, @LastName, @Login, @Password)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertUserQuery, connection);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Login", user.Login);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error occurred while adding user: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public List<User>? GetAllUsers()
        {
            List<User>? users = new List<User>();
            string selectUsersQuery = "SELECT * FROM Users";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(selectUsersQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        List<Parcel>? parcels = parcelRepository.GetAllParcels();
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
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error occurred while getting all users: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                throw;
            }

            return users;
        }
        
        private List<Parcel>? GetAllParcels()
        {
            return parcelRepository.GetAllParcels();
        }
    }
}
