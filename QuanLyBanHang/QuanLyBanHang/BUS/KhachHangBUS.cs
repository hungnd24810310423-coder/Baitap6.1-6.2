using QuanLyBanHang.DAL;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class KhachHangBUS
    {
        private readonly KhachHangDAL dal = new KhachHangDAL();

        // Danh sach khach hang, phan tu dau tien la "Khach le" (MaKH = null)
        public List<KhachHangDTO> LayDanhSachKemKhachLe()
        {
            List<KhachHangDTO> ds = new List<KhachHangDTO>();
            ds.Add(new KhachHangDTO(null, "Khach le"));
            ds.AddRange(dal.LayDanhSach());
            return ds;
        }
    }
}
