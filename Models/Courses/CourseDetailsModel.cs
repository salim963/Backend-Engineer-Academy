using Backendv2.Models.Articles;
using Backendv2.Models.Videos;

namespace Backendv2.Models.Courses
{
    public class CourseDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<VideoModel> Videos { get; set; } = new List<VideoModel>();
        public IList<ArticleModel> Articles { get; set; } = new List<ArticleModel>();
    }
}
