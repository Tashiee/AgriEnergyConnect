using System.Data;
using Microsoft.Data.SqlClient;

namespace AgriEnergyConnect.Infrastructure;

internal sealed class SqlConnectionFactory(string connectionString) 
{
    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(connectionString);
        connection.Open();

        return connection;
    }
} 