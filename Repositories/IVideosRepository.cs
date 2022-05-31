using Backendv2.Models.Videos;

namespace Backendv2.Repositories
{
    public interface IVideosRepository
    {
        IList<VideoModel> GetVideos(int courseId);
    }
}