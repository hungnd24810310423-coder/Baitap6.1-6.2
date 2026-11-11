using Microsoft.Data.SqlClient;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAL
{
    public class KhachHangDAL
    {
        public List<KhachHangDTO> LayDanhSach()
        {
            List<KhachHangDTO> ds = new List<KhachHangDTO>();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "SELECT MaKH, TenKH FROM KhachHang";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ds.Add(new KhachHangDTO(
                            Convert.ToInt32(reader["MaKH"]),
                            reader["TenKH"].ToString() ?? ""));
                    }
                }
            }

            return ds;
        }
    }
}
