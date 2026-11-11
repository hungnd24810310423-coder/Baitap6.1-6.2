using QuanLyBanHang.GUI;

namespace QuanLyBanHang
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmBanHang());
        }
    }
}
