using CKK.DB.Interfaces;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace CKK.DB.UOW
{
    public class DatabaseConnectionFactory : IConnectionFactory
    {
        public static string ConnectionValue(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        private readonly string connectionString = "Data Source = " +
            "(localdb)\\MSSQLLocalDB; Initial Catalog = StructuredProjectDB";

        public string ConnectionString { get;}

        public IDbConnection GetConnection
        {
            get
            {
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", 
                    System.Data.SqlClient.SqlClientFactory.Instance);
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
                //connection.Open();
                return connection;
            }
        }

        
    }
}
