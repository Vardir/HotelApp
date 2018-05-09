using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace HotelsApp.Core.DBTools
{
    public class SqlAdapter
    {
        string connectionString;
        SqlConnection dbConnection;

        SqlConnection DbConnection => dbConnection ?? (dbConnection = new SqlConnection());

        public bool IsOpened => dbConnection.State == ConnectionState.Open;
        public bool IsCreated => dbConnection != null;

        public SqlAdapter(ConnectionInfo connection)
        {
            connectionString = connection.GetConnectionString();
        }

        public DataSet ReadData(string sql, params DataTableMapping[] mappings)
        {
            DbConnection.ConnectionString = connectionString;
            
            DbConnection.Open();
            DataSet dataSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, dbConnection))
            {
                if (mappings.Length != 0)
                    adapter.TableMappings.AddRange(mappings);
                adapter.Fill(dataSet);
            }
            DbConnection.Close();
            return dataSet;
        }
    }
}