using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class BaoCaoBUS
    {
        private readonly BaoCaoDAL dal = new BaoCaoDAL();

        public decimal DoanhThuNgay(DateTime ngay) => dal.DoanhThuNgay(ngay);
        public decimal DoanhThuThang(DateTime ngay) => dal.DoanhThuThang(ngay);
        public List<TopSanPhamDTO> Top5BanChay() => dal.Top5BanChay();
    }
}
