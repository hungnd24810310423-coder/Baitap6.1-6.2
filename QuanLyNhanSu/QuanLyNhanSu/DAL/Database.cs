using System.Configuration;
using Microsoft.Data.SqlClient;

namespace QuanLyNhanSu.DAL
{
    public class Database
    {
        // Chuỗi kết nối nằm trong App.config
        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["HRMDB"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
