namespace QuanLyBanHang.DTO
{
    public class KhachHangDTO
    {
        public int? MaKH { get; set; }          // null = khach le
        public string TenKH { get; set; } = "";

        public KhachHangDTO() { }

        public KhachHangDTO(int? maKH, string tenKH)
        {
            MaKH = maKH;
            TenKH = tenKH;
        }

        public override string ToString()
        {
            return TenKH;
        }
    }
}
