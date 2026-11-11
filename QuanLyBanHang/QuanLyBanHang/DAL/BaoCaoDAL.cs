using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAL
{
    public class BaoCaoDAL
    {
        // ExecuteScalar: doanh thu trong ngay
        public decimal DoanhThuNgay(DateTime ngay)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT ISNULL(SUM(TongTien), 0)
                    FROM HoaDon
                    WHERE NgayLap >= @Ngay
                    AND NgayLap < DATEADD(DAY, 1, @Ngay)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        // ExecuteScalar: doanh thu trong thang cua ngay duoc truyen vao
        public decimal DoanhThuThang(DateTime ngay)
        {
            DateTime ngayDau = new DateTime(ngay.Year, ngay.Month, 1);
            DateTime ngaySau = ngayDau.AddMonths(1);

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT ISNULL(SUM(TongTien), 0)
                    FROM HoaDon
                    WHERE NgayLap >= @NgayDau
                    AND NgayLap < @NgaySau";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@NgayDau", SqlDbType.DateTime).Value = ngayDau;
                    cmd.Parameters.Add("@NgaySau", SqlDbType.DateTime).Value = ngaySau;
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        // SqlDataReader: Top 5 san pham ban chay nhat
        public List<TopSanPhamDTO> Top5BanChay()
        {
            List<TopSanPhamDTO> ds = new List<TopSanPhamDTO>();

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT TOP 5
                        SP.MaSP,
                        SP.TenSP,
                        SUM(CT.SoLuong) AS TongSoLuong
                    FROM ChiTietHoaDon CT
                    INNER JOIN SanPham SP
                        ON CT.MaSP = SP.MaSP
                    GROUP BY SP.MaSP, SP.TenSP
                    ORDER BY SUM(CT.SoLuong) DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ds.Add(new TopSanPhamDTO
                        {
                            MaSP = Convert.ToInt32(reader["MaSP"]),
                            TenSP = reader["TenSP"].ToString() ?? "",
                            TongSoLuong = Convert.ToInt32(reader["TongSoLuong"])
                        });
                    }
                }
            }

            return ds;
        }
    }
}
