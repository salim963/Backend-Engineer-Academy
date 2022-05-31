using System.Data;

namespace Backendv2.Repositories
{
    public interface IDbConnectionRepository
    {
        IDbConnection Create();
        IDbConnection Delete();
    }
}