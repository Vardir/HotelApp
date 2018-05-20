using System;
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

        public DataSet ReadData(string sql, out string error)
        {
            error = null;
            DbConnection.ConnectionString = connectionString;            
            DbConnection.Open();
            DataSet dataSet = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, dbConnection))
            {
                try
                {
                    adapter.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }
            DbConnection.Close();
            return dataSet;
        }
        public T ReadData<T>(string sql, out string error)
        {
            T result = default(T);
            DataSet dataSet = ReadData(sql, out error);
            if (error == null && dataSet.Tables.Count == 1)
            {
                var tableSet = dataSet.Tables[0];
                if (tableSet.Rows.Count == 1 && tableSet.Columns.Count == 1)
                {
                    if (tableSet.Rows[0][0] is T value)
                        result = value;
                }
            }
            return result;
        }
    }
}