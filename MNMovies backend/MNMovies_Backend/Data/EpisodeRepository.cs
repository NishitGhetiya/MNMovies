using Microsoft.Data.SqlClient;
using MNMovies_Backend.Models;
using System.Data;

namespace MNMovies_Backend.Data
{
    public class EpisodeRepository
    {

        #region Connection
        private readonly string _connectionString;

        public EpisodeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAll
        public IEnumerable<EpisodeModel> SelectAll()
        {
            var episodes = new List<EpisodeModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Episode_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    episodes.Add(new EpisodeModel
                    {
                        EpisodeID = Convert.ToInt32(reader["EpisodeID"]),
                        EpisodeName = reader["EpisodeName"].ToString(),
                        EpisodeNumber = Convert.ToInt32(reader["EpisodeNumber"]),
                        EpisodeImage = reader["EpisodeImage"].ToString(),
                        EpisodeVideo = reader["EpisodeVideo"].ToString(),
                        EpisodeDescription = reader["EpisodeDescription"].ToString(),
                        EpisodeLength = reader["EpisodeLength"].ToString(),
                        EpisodeDate = Convert.ToDateTime(reader["EpisodeDate"]),
                        SeasonID = Convert.ToInt32(reader["SeasonID"]),
                        SeasonNumber = Convert.ToInt32(reader["SeasonNumber"]),
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                    });
                }

            }

            return episodes;
        }
        #endregion

        #region SelectByPk
        public EpisodeModel SelectByPK(int EpisodeID)
        {
            EpisodeModel episode = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Episode_SelectByPk", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EpisodeID", EpisodeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    episode = new EpisodeModel
                    {
                        EpisodeID = Convert.ToInt32(reader["EpisodeID"]),
                        EpisodeName = reader["EpisodeName"].ToString(),
                        EpisodeNumber = Convert.ToInt32(reader["EpisodeNumber"]),
                        EpisodeImage = reader["EpisodeImage"].ToString(),
                        EpisodeVideo = reader["EpisodeVideo"].ToString(),
                        EpisodeDescription = reader["EpisodeDescription"].ToString(),
                        EpisodeLength = reader["EpisodeLength"].ToString(),
                        EpisodeDate = Convert.ToDateTime(reader["EpisodeDate"]),
                        SeasonID = Convert.ToInt32(reader["SeasonID"]),
                        SeasonNumber = Convert.ToInt32(reader["SeasonNumber"]),
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                    };
                }

            }

            return episode;
        }
        #endregion

        #region SelectBySeasonID
        public IEnumerable<EpisodeModel> SelectBySeasonID(int SeasonID)
        {
            var episodes = new List<EpisodeModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Episode_SelectBySeasonID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@SeasonID", SeasonID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    episodes.Add(new EpisodeModel
                    {
                        EpisodeID = Convert.ToInt32(reader["EpisodeID"]),
                        EpisodeName = reader["EpisodeName"].ToString(),
                        EpisodeNumber = Convert.ToInt32(reader["EpisodeNumber"]),
                        EpisodeImage = reader["EpisodeImage"].ToString(),
                        EpisodeVideo = reader["EpisodeVideo"].ToString(),
                        EpisodeDescription = reader["EpisodeDescription"].ToString(),
                        EpisodeLength = reader["EpisodeLength"].ToString(),
                        EpisodeDate = Convert.ToDateTime(reader["EpisodeDate"]),
                        SeasonID = Convert.ToInt32(reader["SeasonID"]),
                        SeasonNumber = Convert.ToInt32(reader["SeasonNumber"]),
                        SeriesID = Convert.ToInt32(reader["SeriesID"]),
                        SeriesName = reader["SeriesName"].ToString(),
                    });
                }

            }

            return episodes;
        }
        #endregion

        #region Delete
        public bool Delete(int EpisodeID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Episode_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EpisodeID", EpisodeID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;

            }
        }
        #endregion

        #region Insert
        public bool Insert(EpisodeModel episodeModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Episode_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                //cmd.Parameters.AddWithValue("@EpisodeName", episodeModel.EpisodeName);
                cmd.Parameters.AddWithValue("@EpisodeName", episodeModel.EpisodeName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EpisodeNumber", episodeModel.EpisodeNumber);
                cmd.Parameters.AddWithValue("@EpisodeImage", episodeModel.EpisodeImage);
                cmd.Parameters.AddWithValue("@EpisodeVideo", episodeModel.EpisodeVideo);
                cmd.Parameters.AddWithValue("@EpisodeDescription", episodeModel.EpisodeDescription ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EpisodeLength", episodeModel.EpisodeLength);
                cmd.Parameters.AddWithValue("@SeasonID", episodeModel.SeasonID);
                cmd.Parameters.AddWithValue("@SeriesID", episodeModel.SeriesID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(EpisodeModel episodeModel)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Episode_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@EpisodeID", episodeModel.EpisodeID);
                //cmd.Parameters.AddWithValue("@EpisodeName", episodeModel.EpisodeName);
                cmd.Parameters.AddWithValue("@EpisodeName", episodeModel.EpisodeName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EpisodeNumber", episodeModel.EpisodeNumber);
                cmd.Parameters.AddWithValue("@EpisodeImage", episodeModel.EpisodeImage);
                cmd.Parameters.AddWithValue("@EpisodeVideo", episodeModel.EpisodeVideo);
                cmd.Parameters.AddWithValue("@EpisodeDescription", episodeModel.EpisodeDescription ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EpisodeLength", episodeModel.EpisodeLength);
                cmd.Parameters.AddWithValue("@SeasonID", episodeModel.SeasonID);
                cmd.Parameters.AddWithValue("@SeriesID", episodeModel.SeriesID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion
    }
}
