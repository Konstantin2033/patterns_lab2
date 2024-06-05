using System.Data.SqlClient;
using Post.Classes;

namespace Post.Repositories
{
    public class ParcelRepository
    {
        private string connectionString;
        private CheckPointRepository checkPointRepository;

        public ParcelRepository(string connectionString, CheckPointRepository checkPointRepository)
        {
            this.connectionString = connectionString;
            this.checkPointRepository = checkPointRepository;
        }

        public void AddParcel(Parcel parcel)
        {
            string insertParcelQuery = "INSERT INTO Parcels (UserId, Number, Name, Description, Recipient, SendingTime, ReceivedTime, IsDelivered) " +
                "VALUES (@UserId, @Number, @Name, @Description, @Recipient, @SendingTime, @ReceivedTime, @IsDelivered)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error occurred while adding parcel: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public void UpdateParcel(Parcel parcel)
        {
            string updateParcelQuery = "UPDATE Parcels SET ReceivedTime = @ReceivedTime, IsDelivered = @IsDelivered " +
                                       "WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(updateParcelQuery, connection);
                    command.Parameters.AddWithValue("@ReceivedTime", parcel.ReceivedTime);
                    command.Parameters.AddWithValue("@IsDelivered", parcel.IsDelivered);
                    command.Parameters.AddWithValue("@Id", parcel.Id);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error occurred while updating parcel: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public List<Parcel>? GetAllParcels()
        {
            List<Parcel>? parcels = new List<Parcel>();
            string selectParcelsQuery = "SELECT * FROM Parcels";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(selectParcelsQuery, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        List<CheckPoint>? checkPoints = checkPointRepository.GetAllCheckPoints();
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
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error occurred while getting all parcels: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                throw;
            }

            return parcels;
        }
    }
}
