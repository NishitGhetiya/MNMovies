using Microsoft.Data.SqlClient;
using MNMovies_Backend.Models;
using System.Data;

namespace MNMovies_Backend.Data
{
    public class SeasonRepository
    {

        #region Connection
        private readonly string _connectionString;

        public SeasonRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAll
        public IEnumerable<SeasonModel> SelectAll()
        {
            var seasons = new List<SeasonModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Season_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    seasons.Add(new SeasonModel
                    {
                        SeasonID = Convert.ToInt32(reader["SeasonID"]),
                        SeasonNumber = Convert.ToInt32(reader["SeasonNumber"]),
                        SeasonImage = reader["SeasonImage"].ToString(),
                        SeasonDescription = reader["SeasonDescription"].ToString(),
                        SeasonDate = Convert.ToDateTime(reader["SeasonDate"]),
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                    });
                }

            }

            return seasons;
        }
        #endregion

        #region SelectByPk
        public SeasonModel SelectByPK(int SeasonID)
        {
            SeasonModel season = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Season_SelectByPk", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SeasonID", SeasonID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    season = new SeasonModel
                    {
                        SeasonID = Convert.ToInt32(reader["SeasonID"]),
                        SeasonNumber = Convert.ToInt32(reader["SeasonNumber"]),
                        SeasonImage = reader["SeasonImage"].ToString(),
                        SeasonDescription = reader["SeasonDescription"].ToString(),
                        SeasonDate = Convert.ToDateTime(reader["SeasonDate"]),
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                    };
                }

            }

            return season;
        }
        #endregion

        #region SelectBySeriesID
        public IEnumerable<SeasonModel> SelectBySeriesID(int SeriesID)
        {
            var seasons = new List<SeasonModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Season_SelectBySeriesID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SeriesID", SeriesID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    seasons.Add(new SeasonModel
                    {
                        SeasonID = Convert.ToInt32(reader["SeasonID"]),
                        SeasonNumber = Convert.ToInt32(reader["SeasonNumber"]),
                        SeasonImage = reader["SeasonImage"].ToString(),
                        SeasonDescription = reader["SeasonDescription"].ToString(),
                        SeasonDate = Convert.ToDateTime(reader["SeasonDate"]),
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                    });
                }

            }

            return seasons;
        }
        #endregion

        #region Delete
        public bool Delete(int SeasonID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Season_Delete", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@SeasonID", SeasonID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception (e.g., foreign key constraint violation)
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Log general exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            return false; // Return false if an error occurs
        }
        #endregion

        #region Insert
        public bool Insert(SeasonModel seasonModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Season_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SeasonNumber", seasonModel.SeasonNumber);
                cmd.Parameters.AddWithValue("@SeasonImage", seasonModel.SeasonImage);
                cmd.Parameters.AddWithValue("@SeasonDescription", seasonModel.SeasonDescription);
                cmd.Parameters.AddWithValue("@SeriesID", seasonModel.SeriesID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(SeasonModel seasonModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Season_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SeasonID", seasonModel.SeasonID);
                cmd.Parameters.AddWithValue("@SeasonNumber", seasonModel.SeasonNumber);
                cmd.Parameters.AddWithValue("@SeasonImage", seasonModel.SeasonImage);
                cmd.Parameters.AddWithValue("@SeasonDescription", seasonModel.SeasonDescription);
                cmd.Parameters.AddWithValue("@SeriesID", seasonModel.SeriesID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Count
        public List<SeasonCountModel> Count()
        {
            var seasonCounts = new List<SeasonCountModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Season_SeriesWiseCount", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    seasonCounts.Add(new SeasonCountModel
                    {
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                        SeasonCount = Convert.ToInt32(reader["Seasons"])
                    });
                }
            }

            return seasonCounts;
        }
        #endregion

    }
}
