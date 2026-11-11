using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAL
{
    public class SanPhamDAL
    {
        // Tra ve null neu khong tim thay san pham
        public SanPhamDTO? LayTheoMa(int maSP)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT MaSP, TenSP, DonGia, SoLuongTon
                    FROM SanPham
                    WHERE MaSP = @MaSP";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@MaSP", SqlDbType.Int).Value = maSP;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new SanPhamDTO
                        {
                            MaSP = Convert.ToInt32(reader["MaSP"]),
                            TenSP = reader["TenSP"].ToString() ?? "",
                            DonGia = Convert.ToDecimal(reader["DonGia"]),
                            SoLuongTon = Convert.ToInt32(reader["SoLuongTon"])
                        };
                    }
                }
            }
        }
    }
}
