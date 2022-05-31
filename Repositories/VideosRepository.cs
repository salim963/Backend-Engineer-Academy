using Backendv2.Models.Videos;
using System.Data;
using Backendv2.Extensions;

namespace Backendv2.Repositories
{
    public class VideosRepository : IVideosRepository
    {
        private readonly IDbConnectionRepository dbConnectionService;

        public VideosRepository(IDbConnectionRepository dbConnectionService)
        {
            this.dbConnectionService = dbConnectionService;
        }
        public IList<VideoModel> GetVideos(int courseId)
        {
            var videos = new List<VideoModel>();
            using (var conn = dbConnectionService.Create())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from video where coursesId=@courseId";

                var courseIdParam = cmd.CreateParameter();
                courseIdParam.ParameterName = "@courseId";
                courseIdParam.Value = courseId;
                cmd.Parameters.Add(courseIdParam);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        videos.Add(GetVideos(reader));
                    }
                }
            }

            return videos;
        }

        private VideoModel GetVideos(IDataReader reader)
        {
            return new VideoModel()
            {
                Id = reader.GetIntValue("ID"),
                Title = reader.GetStringValue("videoTitle"),
                Description = reader.GetStringValue("videoDes"),
                VideoId = reader.GetStringValue("videoId"),
            };
        }
    }
}
