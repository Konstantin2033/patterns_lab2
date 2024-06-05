using System.Data.SqlClient;
using Post.Classes;

namespace Post.Repositories
{
    public class CheckPointRepository
    {
        private string connectionString;

        public CheckPointRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddCheckPoint(CheckPoint checkPoint)
        {
            string insertCheckPointQuery = "INSERT INTO CheckPoints (ParcelId, Description, Time) " +
                "VALUES (@ParcelId, @Description, @Time)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(insertCheckPointQuery, connection);
                    command.Parameters.AddWithValue("@ParcelId", checkPoint.ParcelId);
                    command.Parameters.AddWithValue("@Description", checkPoint.Description);
                    command.Parameters.AddWithValue("@Time", checkPoint.Time);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error occurred while adding check point: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public List<CheckPoint>? GetAllCheckPoints()
        {
            List<CheckPoint>? checkPoints = new List<CheckPoint>();
            string selectCheckPointsQuery = "SELECT * FROM CheckPoints";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error occurred while getting all check points: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                throw;
            }

            return checkPoints;
        }
    }
}
