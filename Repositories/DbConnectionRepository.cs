using MySql.Data.MySqlClient;
using System.Data;

namespace Backendv2.Repositories
{
    public class DbConnectionRepository : IDbConnectionRepository
    {
        //TODO this should not be in code but in appsettings.json
        private static string connectionString = "server=localhost;port=3306;database=backend;user=root;password=salim123";
        //
        //"server=130.225.170.79;uid=bangash;pwd=123;database=backend";
        //"server=130.225.170.79;database=backend;user=bangash;password=123";

        public IDbConnection Create()
        {
            return new MySqlConnection(connectionString);

        }


        public IDbConnection Delete()
        {
            return new MySqlConnection(connectionString);

        }
    }
}
