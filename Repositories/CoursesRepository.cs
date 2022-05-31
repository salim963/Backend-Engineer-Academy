using Backendv2.Extensions;
using Backendv2.Models.Articles;
using Backendv2.Models.Courses;
using Backendv2.Models.Videos;
using System.Data;

namespace Backendv2.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly IDbConnectionRepository dbConnectionService;

        public CoursesRepository(IDbConnectionRepository dbConnectionService)
        {
            this.dbConnectionService = dbConnectionService;
        }

        public void CreateCourse(string name, string description)
        {
            using (var conn = dbConnectionService.Create())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO courses (courseName, courseDescription) VALUES (@name, @description)";

                var nameParam = cmd.CreateParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = name;
                cmd.Parameters.Add(nameParam);

                var descriptionParam = cmd.CreateParameter();
                descriptionParam.ParameterName = "@description";
                descriptionParam.Value = description;
                cmd.Parameters.Add(descriptionParam);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCourse(int id, string name, string description)
        {
            using (var conn = dbConnectionService.Create())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE courses SET courseName=@name, courseDescription=@description WHERE coursesID=@id";

                var idParam = cmd.CreateParameter();
                idParam.ParameterName = "@id";
                idParam.Value = id;
                cmd.Parameters.Add(idParam);

                var nameParam = cmd.CreateParameter();
                nameParam.ParameterName = "@name";
                nameParam.Value = name;
                cmd.Parameters.Add(nameParam);

                var descriptionParam = cmd.CreateParameter();
                descriptionParam.ParameterName = "@description";
                descriptionParam.Value = description;
                cmd.Parameters.Add(descriptionParam);

                cmd.ExecuteNonQuery();
            }
        }

        public CourseDetailsModel GetById(int id)
        {
            CourseDetailsModel model = null;
            using (var conn = dbConnectionService.Create())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"
                  SELECT a.*, 
                    b.id as videoId, b.videoTitle, b.videoDes, b.videoId as youtubeVideoId, 
                    c.id as articleId, c.articleTitle, c.articleDes, c.articleLink
                    FROM courses a
                    LEFT OUTER JOIN video b
                    ON a.coursesID = b.coursesID
                    LEFT OUTER JOIN article c 
                    ON a.coursesID = c.coursesID
                    where a.coursesID = @courseId
                ";




                var idParam = cmd.CreateParameter();
                idParam.ParameterName = "@courseId";
                idParam.Value = id;
                cmd.Parameters.Add(idParam);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (model == null)
                        {
                            model = new CourseDetailsModel
                            {
                                Id = id,
                                Name = reader.GetStringValue("CourseName"),
                                Description = reader.GetStringValue("CourseDescription")
                            };
                        }

                        var hasVideo = !reader.IsNull("videoId");
                        if (hasVideo)
                        {
                            model.Videos.Add(GetVideo(reader));
                        }

                        var hasArticle = !reader.IsNull("articleId");
                        if (hasArticle)
                        {
                            model.Articles.Add(GetArticle(reader));
                        }
                    }
                }
            }

            return model;
        }

        public IList<CourseModel> GetCourses()
        {
            var courses = new List<CourseModel>();
            using (var conn = dbConnectionService.Create())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from courses";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(GetCourse(reader));
                    }
                }
            }

            return courses;
        }

        private CourseModel GetCourse(IDataReader reader)
        {
            return new CourseModel()
            {
                Id = Convert.ToInt32(reader["coursesID"]),
                Name = reader["CourseName"].ToString(),
                Description = reader["CourseDescription"].ToString(),
            };
        }

        public void DeleteCourse(int Id)
        {
            using (var conn = dbConnectionService.Delete())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM courses WHERE coursesID=@Id";

                var idParam = cmd.CreateParameter();
                idParam.ParameterName = "@Id";
                idParam.Value = Id;
                cmd.Parameters.Add(idParam);
                cmd.ExecuteNonQuery();
            }
        }

        private VideoModel GetVideo(IDataReader reader)
        {
            return new VideoModel
            {
                Id = reader.GetIntValue("videoId"),
                Title = reader.GetStringValue("videoTitle"),
                Description = reader.GetStringValue("videoDes"),
                VideoId = reader.GetStringValue("youtubeVideoId")
            };
        }

        private ArticleModel GetArticle(IDataReader reader)
        {
            return new ArticleModel
            {
                Id = reader.GetIntValue("articleId"),
                Title = reader.GetStringValue("articleTitle"),
                Description = reader.GetStringValue("articleDes"),
                Link = reader.GetStringValue("articleLink")
            };
        }
    }
}
