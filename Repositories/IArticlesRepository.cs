using Backendv2.Models.Articles;

namespace Backendv2.Repositories
{
    public interface IArticlesRepository
    {
        IList<ArticleModel> GetArticles();
    }
}
