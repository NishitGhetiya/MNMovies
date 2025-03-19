using Microsoft.Data.SqlClient;
using MNMovies_Backend.Models;
using System.Data;

namespace MNMovies_Backend.Data
{
    public class SeriesRepository
    {
        #region Connection
        private readonly string _connectionString;

        public SeriesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAll
        public IEnumerable<SeriesModel> SelectAll()
        {
            var series = new List<SeriesModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SeriesModel
                    {
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                        SeriesImage = reader["SeriesImage"].ToString(),
                        TypeOfSeries = reader["TypeOfSeries"].ToString(),
                        SeriesCategory = reader["SeriesCategory"].ToString(),
                        SeriesType = reader["SeriesType"].ToString(),
                        SeriesLanguage = reader["SeriesLanguage"].ToString(),
                        SeriesDescription = reader["SeriesDescription"].ToString(),
                        SeriesDate = Convert.ToDateTime(reader["SeriesDate"]),
                        SeriesView = reader["SeriesView"] != DBNull.Value ? Convert.ToInt32(reader["SeriesView"]) : 0
                    });
                }

            }

            return series;
        }
        #endregion

        #region SelectTopTen
        public IEnumerable<SeriesModel> SelectTopTen()
        {
            var series = new List<SeriesModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_SelectTopTen", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SeriesModel
                    {
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                        SeriesImage = reader["SeriesImage"].ToString(),
                        TypeOfSeries = reader["TypeOfSeries"].ToString(),
                        SeriesCategory = reader["SeriesCategory"].ToString(),
                        SeriesType = reader["SeriesType"].ToString(),
                        SeriesLanguage = reader["SeriesLanguage"].ToString(),
                        SeriesDescription = reader["SeriesDescription"].ToString(),
                        SeriesDate = Convert.ToDateTime(reader["SeriesDate"]),
                        SeriesView = reader["SeriesView"] != DBNull.Value ? Convert.ToInt32(reader["SeriesView"]) : 0
                    });
                }

            }

            return series;
        }
        #endregion

        #region SelectLatest
        public IEnumerable<SeriesModel> SelectLatest()
        {
            var series = new List<SeriesModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_SelectLatest", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SeriesModel
                    {
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                        SeriesImage = reader["SeriesImage"].ToString(),
                        TypeOfSeries = reader["TypeOfSeries"].ToString(),
                        SeriesCategory = reader["SeriesCategory"].ToString(),
                        SeriesType = reader["SeriesType"].ToString(),
                        SeriesLanguage = reader["SeriesLanguage"].ToString(),
                        SeriesDescription = reader["SeriesDescription"].ToString(),
                        SeriesDate = Convert.ToDateTime(reader["SeriesDate"]),
                        SeriesView = reader["SeriesView"] != DBNull.Value ? Convert.ToInt32(reader["SeriesView"]) : 0
                    });
                }

            }

            return series;
        }
        #endregion

        #region SelectByPk
        public SeriesModel SelectByPK(int SeriesID)
        {
            SeriesModel series = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_SelectByPk", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SeriesID", SeriesID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    series = new SeriesModel
                    {
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                        SeriesImage = reader["SeriesImage"].ToString(),
                        TypeOfSeries = reader["TypeOfSeries"].ToString(),
                        SeriesCategory = reader["SeriesCategory"].ToString(),
                        SeriesType = reader["SeriesType"].ToString(),
                        SeriesLanguage = reader["SeriesLanguage"].ToString(),
                        SeriesDescription = reader["SeriesDescription"].ToString(),
                        SeriesDate = Convert.ToDateTime(reader["SeriesDate"]),
                        SeriesView = reader["SeriesView"] != DBNull.Value ? Convert.ToInt32(reader["SeriesView"]) : 0
                    };
                }

            }

            return series;
        }
        #endregion

        #region Delete
        public bool Delete(int SeriesID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("PR_Series_Delete", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@SeriesID", SeriesID);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                // Log SQL exception (e.g., foreign key constraint failure)
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Log general exceptions
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            return false; // Return false in case of an error
        }
        #endregion

        #region Insert
        public bool Insert(SeriesModel seriesModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SeriesName", seriesModel.SeriesName);
                cmd.Parameters.AddWithValue("@SeriesImage", seriesModel.SeriesImage);
                cmd.Parameters.AddWithValue("@TypeOfSeries", seriesModel.TypeOfSeries);
                cmd.Parameters.AddWithValue("@SeriesCategory", seriesModel.SeriesCategory);
                cmd.Parameters.AddWithValue("@SeriesType", seriesModel.SeriesType);
                cmd.Parameters.AddWithValue("@SeriesLanguage", seriesModel.SeriesLanguage);
                cmd.Parameters.AddWithValue("@SeriesDescription", seriesModel.SeriesDescription);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(SeriesModel seriesModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@SeriesID", seriesModel.SeriesID);
                cmd.Parameters.AddWithValue("@SeriesName", seriesModel.SeriesName);
                cmd.Parameters.AddWithValue("@SeriesImage", seriesModel.SeriesImage);
                cmd.Parameters.AddWithValue("@TypeOfSeries", seriesModel.TypeOfSeries);
                cmd.Parameters.AddWithValue("@SeriesCategory", seriesModel.SeriesCategory);
                cmd.Parameters.AddWithValue("@SeriesType", seriesModel.SeriesType);
                cmd.Parameters.AddWithValue("@SeriesLanguage", seriesModel.SeriesLanguage);
                cmd.Parameters.AddWithValue("@SeriesDescription", seriesModel.SeriesDescription);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region IncrementViews
        public bool IncrementViews(int SeriesID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_IncrementViews", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SeriesID", SeriesID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region SelectLatestForHomeSlider
        public IEnumerable<SeriesModel> SelectLatestForHomeSlider()
        {
            var series = new List<SeriesModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_SelectLatestForHomeSlider", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SeriesModel
                    {
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                        SeriesImage = reader["SeriesImage"].ToString(),
                        TypeOfSeries = reader["TypeOfSeries"].ToString(),
                        SeriesCategory = reader["SeriesCategory"].ToString(),
                        SeriesType = reader["SeriesType"].ToString(),
                        SeriesLanguage = reader["SeriesLanguage"].ToString(),
                        SeriesDescription = reader["SeriesDescription"].ToString(),
                        SeriesDate = Convert.ToDateTime(reader["SeriesDate"]),
                        SeriesView = reader["SeriesView"] != DBNull.Value ? Convert.ToInt32(reader["SeriesView"]) : 0
                    });
                }

            }

            return series;
        }
        #endregion

        #region SelectTopForHomeSlider
        public IEnumerable<SeriesModel> SelectTopForHomeSlider()
        {
            var series = new List<SeriesModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_SelectTopForHomeSlider", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    series.Add(new SeriesModel
                    {
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                        SeriesImage = reader["SeriesImage"].ToString(),
                        TypeOfSeries = reader["TypeOfSeries"].ToString(),
                        SeriesCategory = reader["SeriesCategory"].ToString(),
                        SeriesType = reader["SeriesType"].ToString(),
                        SeriesLanguage = reader["SeriesLanguage"].ToString(),
                        SeriesDescription = reader["SeriesDescription"].ToString(),
                        SeriesDate = Convert.ToDateTime(reader["SeriesDate"]),
                        SeriesView = reader["SeriesView"] != DBNull.Value ? Convert.ToInt32(reader["SeriesView"]) : 0
                    });
                }

            }

            return series;
        }
        #endregion

        #region Count
        public int Count()
        {
            int totalSeries = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Series_Count", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    totalSeries = Convert.ToInt32(result);
                }
            }

            return totalSeries;
        }
        #endregion

    }
}
