using Microsoft.Data.SqlClient;
using MNMovies_Backend.Models;
using System.Data;

namespace MNMovies_Backend.Data
{
    public class MovieRepository
    {
        #region Connection
        private readonly string _connectionString;

        public MovieRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAll
        public IEnumerable<MovieModel> SelectAll()
        {
            var movies = new List<MovieModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(new MovieModel
                    {
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        MovieName = reader["MovieName"].ToString(),
                        MovieImage = reader["MovieImage"].ToString(),
                        MovieVideo = reader["MovieVideo"].ToString(),
                        TypeOfMovie = reader["TypeOfMovie"].ToString(),
                        MovieCategory = reader["MovieCategory"].ToString(),
                        MovieType = reader["MovieType"].ToString(),
                        MovieLanguage = reader["MovieLanguage"].ToString(),
                        MovieDescription = reader["MovieDescription"].ToString(),
                        MovieLength = reader["MovieLength"].ToString(),
                        MovieDate = Convert.ToDateTime(reader["MovieDate"]),
                        MovieViews = reader["MovieViews"] != DBNull.Value ? Convert.ToInt32(reader["MovieViews"]) : 0
                    });
                }

            }

            return movies;
        }
        #endregion

        #region SelectTopTen
        public IEnumerable<MovieModel> SelectTopTen()
        {
            var movies = new List<MovieModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_SelectTopTen", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(new MovieModel
                    {
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        MovieName = reader["MovieName"].ToString(),
                        MovieImage = reader["MovieImage"].ToString(),
                        MovieVideo = reader["MovieVideo"].ToString(),
                        TypeOfMovie = reader["TypeOfMovie"].ToString(),
                        MovieCategory = reader["MovieCategory"].ToString(),
                        MovieType = reader["MovieType"].ToString(),
                        MovieLanguage = reader["MovieLanguage"].ToString(),
                        MovieDescription = reader["MovieDescription"].ToString(),
                        MovieLength = reader["MovieLength"].ToString(),
                        MovieDate = Convert.ToDateTime(reader["MovieDate"]),
                        MovieViews = reader["MovieViews"] != DBNull.Value ? Convert.ToInt32(reader["MovieViews"]) : 0
                    });
                }

            }

            return movies;
        }
        #endregion

        #region SelectLatest
        public IEnumerable<MovieModel> SelectLatest()
        {
            var movies = new List<MovieModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_SelectLatest", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(new MovieModel
                    {
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        MovieName = reader["MovieName"].ToString(),
                        MovieImage = reader["MovieImage"].ToString(),
                        MovieVideo = reader["MovieVideo"].ToString(),
                        TypeOfMovie = reader["TypeOfMovie"].ToString(),
                        MovieCategory = reader["MovieCategory"].ToString(),
                        MovieType = reader["MovieType"].ToString(),
                        MovieLanguage = reader["MovieLanguage"].ToString(),
                        MovieDescription = reader["MovieDescription"].ToString(),
                        MovieLength = reader["MovieLength"].ToString(),
                        MovieDate = Convert.ToDateTime(reader["MovieDate"]),
                        MovieViews = reader["MovieViews"] != DBNull.Value ? Convert.ToInt32(reader["MovieViews"]) : 0
                    });
                }

            }

            return movies;
        }
        #endregion

        #region SelectByPk
        public MovieModel SelectByPK(int MovieID)
        {
            MovieModel movie = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_SelectByPk", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@MovieID", MovieID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    movie = new MovieModel
                    {
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        MovieName = reader["MovieName"].ToString(),
                        MovieImage = reader["MovieImage"].ToString(),
                        MovieVideo = reader["MovieVideo"].ToString(),
                        TypeOfMovie = reader["TypeOfMovie"].ToString(),
                        MovieCategory = reader["MovieCategory"].ToString(),
                        MovieType = reader["MovieType"].ToString(),
                        MovieLanguage = reader["MovieLanguage"].ToString(),
                        MovieDescription = reader["MovieDescription"].ToString(),
                        MovieLength = reader["MovieLength"].ToString(),
                        MovieDate = Convert.ToDateTime(reader["MovieDate"]),
                        MovieViews = reader["MovieViews"] != DBNull.Value ? Convert.ToInt32(reader["MovieViews"]) : 0
                    };
                }

            }

            return movie;
        }
        #endregion

        #region Delete
        public bool Delete(int MovieID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@MovieID", MovieID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;

            }
        }
        #endregion

        #region Insert
        public bool Insert(MovieModel movieModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@MovieName", movieModel.MovieName);
                cmd.Parameters.AddWithValue("@MovieImage", movieModel.MovieImage);
                cmd.Parameters.AddWithValue("@MovieVideo", movieModel.MovieVideo);
                cmd.Parameters.AddWithValue("@TypeOfMovie", movieModel.TypeOfMovie);
                cmd.Parameters.AddWithValue("@MovieCategory", movieModel.MovieCategory);
                cmd.Parameters.AddWithValue("@MovieType", movieModel.MovieType);
                cmd.Parameters.AddWithValue("@MovieLanguage", movieModel.MovieLanguage);
                cmd.Parameters.AddWithValue("@MovieDescription", movieModel.MovieDescription);
                cmd.Parameters.AddWithValue("@MovieLength", movieModel.MovieLength);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(MovieModel movieModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@MovieID", movieModel.MovieID);
                cmd.Parameters.AddWithValue("@MovieName", movieModel.MovieName);
                cmd.Parameters.AddWithValue("@MovieImage", movieModel.MovieImage);
                cmd.Parameters.AddWithValue("@MovieVideo", movieModel.MovieVideo);
                cmd.Parameters.AddWithValue("@TypeOfMovie", movieModel.TypeOfMovie);
                cmd.Parameters.AddWithValue("@MovieCategory", movieModel.MovieCategory);
                cmd.Parameters.AddWithValue("@MovieType", movieModel.MovieType);
                cmd.Parameters.AddWithValue("@MovieLanguage", movieModel.MovieLanguage);
                cmd.Parameters.AddWithValue("@MovieDescription", movieModel.MovieDescription);
                cmd.Parameters.AddWithValue("@MovieLength", movieModel.MovieLength);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region IncrementViews
        public bool IncrementViews(int MovieID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_IncrementViews", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@MovieID", MovieID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region SelectLatestForHomeSlider
        public IEnumerable<MovieModel> SelectLatestForHomeSlider()
        {
            var movies = new List<MovieModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_SelectLatestForHomeSlider", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(new MovieModel
                    {
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        MovieName = reader["MovieName"].ToString(),
                        MovieImage = reader["MovieImage"].ToString(),
                        MovieVideo = reader["MovieVideo"].ToString(),
                        TypeOfMovie = reader["TypeOfMovie"].ToString(),
                        MovieCategory = reader["MovieCategory"].ToString(),
                        MovieType = reader["MovieType"].ToString(),
                        MovieLanguage = reader["MovieLanguage"].ToString(),
                        MovieDescription = reader["MovieDescription"].ToString(),
                        MovieLength = reader["MovieLength"].ToString(),
                        MovieDate = Convert.ToDateTime(reader["MovieDate"]),
                        MovieViews = reader["MovieViews"] != DBNull.Value ? Convert.ToInt32(reader["MovieViews"]) : 0
                    });
                }

            }

            return movies;
        }
        #endregion

        #region SelectTopForHomeSlider
        public IEnumerable<MovieModel> SelectTopForHomeSlider()
        {
            var movies = new List<MovieModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_SelectTopForHomeSlider", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movies.Add(new MovieModel
                    {
                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        MovieName = reader["MovieName"].ToString(),
                        MovieImage = reader["MovieImage"].ToString(),
                        MovieVideo = reader["MovieVideo"].ToString(),
                        TypeOfMovie = reader["TypeOfMovie"].ToString(),
                        MovieCategory = reader["MovieCategory"].ToString(),
                        MovieType = reader["MovieType"].ToString(),
                        MovieLanguage = reader["MovieLanguage"].ToString(),
                        MovieDescription = reader["MovieDescription"].ToString(),
                        MovieLength = reader["MovieLength"].ToString(),
                        MovieDate = Convert.ToDateTime(reader["MovieDate"]),
                        MovieViews = reader["MovieViews"] != DBNull.Value ? Convert.ToInt32(reader["MovieViews"]) : 0
                    });
                }

            }

            return movies;
        }
        #endregion

        #region Count
        public int Count()
        {
            int totalMovies = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Movie_Count", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    totalMovies = Convert.ToInt32(result);
                }
            }

            return totalMovies;
        }
        #endregion

    }
}
