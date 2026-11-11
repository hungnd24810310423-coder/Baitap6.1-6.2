using System.Configuration;
using Microsoft.Data.SqlClient;

namespace QuanLyBanHang.DAL
{
    public class Database
    {
        // Chuỗi kết nối nằm trong App.config
        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["BanHangDB"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static void KiemTraKetNoi()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
            }
        }
    }
}
