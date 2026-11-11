using Microsoft.Data.SqlClient;
using System.Data;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAL
{
    public class HoaDonDAL
    {
        // Lap hoa don trong MOT SqlTransaction (ACID). Loi bat ky -> Rollback + nem lai ngoai le.
        // Tra ve ma hoa don vua tao.
        public int ThanhToan(int? maKH, List<GioHangItemDTO> gioHang, decimal tongTien)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Chen HoaDon
                        string sqlHoaDon = @"
                            INSERT INTO HoaDon(MaKH, TongTien)
                            VALUES(@MaKH, @TongTien);

                            SELECT SCOPE_IDENTITY();";

                        int maHD;

                        using (SqlCommand cmdHD = new SqlCommand(sqlHoaDon, conn, trans))
                        {
                            cmdHD.Parameters.Add("@MaKH", SqlDbType.Int).Value =
                                maKH.HasValue ? (object)maKH.Value : DBNull.Value;

                            SqlParameter pTongTien = cmdHD.Parameters.Add("@TongTien", SqlDbType.Decimal);
                            pTongTien.Precision = 18;
                            pTongTien.Scale = 2;
                            pTongTien.Value = tongTien;

                            maHD = Convert.ToInt32(cmdHD.ExecuteScalar());
                        }

                        foreach (GioHangItemDTO item in gioHang)
                        {
                            // 2. Chen ChiTietHoaDon
                            string sqlChiTiet = @"
                                INSERT INTO ChiTietHoaDon
                                (MaHD, MaSP, SoLuong, DonGia, ThanhTien)
                                VALUES
                                (@MaHD, @MaSP, @SoLuong, @DonGia, @ThanhTien)";

                            using (SqlCommand cmdCT = new SqlCommand(sqlChiTiet, conn, trans))
                            {
                                cmdCT.Parameters.Add("@MaHD", SqlDbType.Int).Value = maHD;
                                cmdCT.Parameters.Add("@MaSP", SqlDbType.Int).Value = item.MaSP;
                                cmdCT.Parameters.Add("@SoLuong", SqlDbType.Int).Value = item.SoLuong;

                                SqlParameter pDonGia = cmdCT.Parameters.Add("@DonGia", SqlDbType.Decimal);
                                pDonGia.Precision = 18;
                                pDonGia.Scale = 2;
                                pDonGia.Value = item.DonGia;

                                SqlParameter pThanhTien = cmdCT.Parameters.Add("@ThanhTien", SqlDbType.Decimal);
                                pThanhTien.Precision = 18;
                                pThanhTien.Scale = 2;
                                pThanhTien.Value = item.ThanhTien;

                                cmdCT.ExecuteNonQuery();
                            }

                            // 3. Tru ton kho (thieu hang -> 0 dong bi anh huong -> loi -> Rollback)
                            string sqlTonKho = @"
                                UPDATE SanPham
                                SET SoLuongTon = SoLuongTon - @SoLuong
                                WHERE MaSP = @MaSP
                                AND SoLuongTon >= @SoLuong";

                            using (SqlCommand cmdTonKho = new SqlCommand(sqlTonKho, conn, trans))
                            {
                                cmdTonKho.Parameters.Add("@MaSP", SqlDbType.Int).Value = item.MaSP;
                                cmdTonKho.Parameters.Add("@SoLuong", SqlDbType.Int).Value = item.SoLuong;

                                int ketQua = cmdTonKho.ExecuteNonQuery();

                                if (ketQua != 1)
                                {
                                    throw new Exception(
                                        "San pham ma " + item.MaSP +
                                        " khong du so luong ton kho!");
                                }
                            }
                        }

                        // 4. Cong diem tich luy
                        if (maKH.HasValue)
                        {
                            string sqlDiem = @"
                                UPDATE KhachHang
                                SET DiemTichLuy = DiemTichLuy +
                                    CAST(FLOOR(@TongTien / 100000.0) AS INT)
                                WHERE MaKH = @MaKH";

                            using (SqlCommand cmdDiem = new SqlCommand(sqlDiem, conn, trans))
                            {
                                SqlParameter pTongTien = cmdDiem.Parameters.Add("@TongTien", SqlDbType.Decimal);
                                pTongTien.Precision = 18;
                                pTongTien.Scale = 2;
                                pTongTien.Value = tongTien;

                                cmdDiem.Parameters.Add("@MaKH", SqlDbType.Int).Value = maKH.Value;

                                if (cmdDiem.ExecuteNonQuery() != 1)
                                {
                                    throw new Exception("Khong tim thay khach hang!");
                                }
                            }
                        }

                        // 5. Hoan tat
                        trans.Commit();
                        return maHD;
                    }
                    catch
                    {
                        try
                        {
                            trans.Rollback();
                        }
                        catch
                        {
                        }

                        throw;
                    }
                }
            }
        }
    }
}
