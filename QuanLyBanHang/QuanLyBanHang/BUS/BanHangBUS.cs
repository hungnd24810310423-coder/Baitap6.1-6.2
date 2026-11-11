using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class BanHangBUS
    {
        private readonly HoaDonDAL hoaDonDAL = new HoaDonDAL();

        public void KiemTraKetNoi()
        {
            Database.KiemTraKetNoi();
        }

        // Them vao gio: gop dong neu trung ma, kiem tra ton kho
        public void ThemVaoGio(List<GioHangItemDTO> gio, SanPhamDTO sp, int soLuong)
        {
            if (soLuong <= 0)
                throw new Exception("So luong phai lon hon 0!");

            if (sp.SoLuongTon < soLuong)
                throw new Exception("So luong ton kho khong du!");

            GioHangItemDTO? dong = gio.FirstOrDefault(x => x.MaSP == sp.MaSP);
            int tongSoLuong = soLuong + (dong?.SoLuong ?? 0);

            if (tongSoLuong > sp.SoLuongTon)
                throw new Exception("Tong so luong trong gio vuot qua ton kho!");

            if (dong != null)
            {
                dong.SoLuong = tongSoLuong;
            }
            else
            {
                gio.Add(new GioHangItemDTO
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    SoLuong = soLuong,
                    DonGia = sp.DonGia
                });
            }
        }

        public void XoaKhoiGio(List<GioHangItemDTO> gio, int maSP)
        {
            gio.RemoveAll(x => x.MaSP == maSP);
        }

        public decimal TinhTongTien(List<GioHangItemDTO> gio)
        {
            return gio.Sum(x => x.ThanhTien);
        }

        // Diem tich luy = TongTien / 100000 (lam tron xuong)
        public int TinhDiem(decimal tongTien)
        {
            return (int)Math.Floor(tongTien / 100000m);
        }

        // Thanh toan (SqlTransaction nam o tang DAL). Tra ve ma hoa don.
        public int ThanhToan(int? maKH, List<GioHangItemDTO> gio)
        {
            if (gio.Count == 0)
                throw new Exception("Gio hang dang trong!");

            return hoaDonDAL.ThanhToan(maKH, gio, TinhTongTien(gio));
        }
    }
}
