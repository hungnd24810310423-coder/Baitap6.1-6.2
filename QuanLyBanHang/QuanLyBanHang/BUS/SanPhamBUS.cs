using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class SanPhamBUS
    {
        private readonly SanPhamDAL dal = new SanPhamDAL();

        // Nhan ma san pham dang chuoi (o nhap / may quet), kiem tra hop le roi tim trong CSDL
        public SanPhamDTO TimTheoMa(string maText)
        {
            if (!int.TryParse(maText.Trim(), out int maSP))
                throw new Exception("Vui long nhap ma san pham hop le!");

            SanPhamDTO? sp = dal.LayTheoMa(maSP);
            if (sp == null)
                throw new Exception("Khong tim thay san pham!");

            return sp;
        }
    }
}
