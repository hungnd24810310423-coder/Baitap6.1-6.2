using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.GUI
{
    public partial class frmBanHang : Form
    {
        private TextBox txtMaSP;
        private NumericUpDown numSoLuong;
        private Button btnThem;
        private Button btnXoa;
        private Button btnThanhToan;
        private Button btnDoanhThuNgay;
        private Button btnDoanhThuThang;
        private Button btnTop5;
        private ComboBox cboKhachHang;
        private DataGridView dgvGioHang;
        private DataGridView dgvTop5;
        private Label lblTongTien;
        private Label lblDiemTichLuy;
        private Label lblDoanhThuNgay;
        private Label lblDoanhThuThang;

        private readonly BanHangBUS banHangBUS = new BanHangBUS();
        private readonly SanPhamBUS sanPhamBUS = new SanPhamBUS();
        private readonly KhachHangBUS khachHangBUS = new KhachHangBUS();
        private readonly BaoCaoBUS baoCaoBUS = new BaoCaoBUS();
        private readonly List<GioHangItemDTO> gioHang = new List<GioHangItemDTO>();

        public frmBanHang()
        {
            InitializeComponent();
            TaoGiaoDien();

            this.Load += frmBanHang_Load;
            btnThem.Click += btnThem_Click;
            btnXoa.Click += btnXoa_Click;
            btnThanhToan.Click += btnThanhToan_Click;
            btnDoanhThuNgay.Click += btnDoanhThuNgay_Click;
            btnDoanhThuThang.Click += btnDoanhThuThang_Click;
            btnTop5.Click += btnTop5_Click;

            txtMaSP.KeyDown += txtMaSP_KeyDown;
            cboKhachHang.SelectedIndexChanged += cboKhachHang_SelectedIndexChanged;
        }

        private void TaoGiaoDien()
        {
            this.Text = "QUAN LY BAN HANG";
            this.Size = new Size(1300, 950);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.MinimumSize = new Size(1200, 850);

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "QUAN LY BAN HANG";
            lblTieuDe.Font = new Font("Arial", 22, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.DarkBlue;
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 70;
            this.Controls.Add(lblTieuDe);

            GroupBox grpSanPham = new GroupBox();
            grpSanPham.Text = "THONG TIN SAN PHAM";
            grpSanPham.Font = new Font("Arial", 10, FontStyle.Bold);
            grpSanPham.Location = new Point(20, 85);
            grpSanPham.Size = new Size(340, 250);
            this.Controls.Add(grpSanPham);

            Label lblMaSP = new Label();
            lblMaSP.Text = "Ma san pham:";
            lblMaSP.Location = new Point(20, 40);
            lblMaSP.AutoSize = true;
            grpSanPham.Controls.Add(lblMaSP);

            txtMaSP = new TextBox();
            txtMaSP.Location = new Point(20, 65);
            txtMaSP.Size = new Size(290, 30);
            grpSanPham.Controls.Add(txtMaSP);

            Label lblSoLuong = new Label();
            lblSoLuong.Text = "So luong:";
            lblSoLuong.Location = new Point(20, 110);
            lblSoLuong.AutoSize = true;
            grpSanPham.Controls.Add(lblSoLuong);

            numSoLuong = new NumericUpDown();
            numSoLuong.Location = new Point(20, 135);
            numSoLuong.Size = new Size(290, 30);
            numSoLuong.Minimum = 1;
            numSoLuong.Maximum = 1000;
            numSoLuong.Value = 1;
            grpSanPham.Controls.Add(numSoLuong);

            btnThem = new Button();
            btnThem.Text = "THEM VAO GIO";
            btnThem.Location = new Point(20, 185);
            btnThem.Size = new Size(135, 40);
            btnThem.BackColor = Color.LightGreen;
            grpSanPham.Controls.Add(btnThem);

            btnXoa = new Button();
            btnXoa.Text = "XOA SAN PHAM";
            btnXoa.Location = new Point(170, 185);
            btnXoa.Size = new Size(140, 40);
            btnXoa.BackColor = Color.LightCoral;
            grpSanPham.Controls.Add(btnXoa);

            GroupBox grpGioHang = new GroupBox();
            grpGioHang.Text = "GIO HANG";
            grpGioHang.Font = new Font("Arial", 10, FontStyle.Bold);
            grpGioHang.Location = new Point(380, 85);
            grpGioHang.Size = new Size(880, 430);
            this.Controls.Add(grpGioHang);

            dgvGioHang = new DataGridView();
            dgvGioHang.Location = new Point(15, 30);
            dgvGioHang.Size = new Size(850, 385);
            dgvGioHang.AllowUserToAddRows = false;
            dgvGioHang.ReadOnly = true;
            dgvGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGioHang.MultiSelect = false;
            dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGioHang.RowHeadersVisible = false;
            dgvGioHang.BackgroundColor = Color.White;

            dgvGioHang.Columns.Add("MaSP", "Ma SP");
            dgvGioHang.Columns.Add("TenSP", "Ten san pham");
            dgvGioHang.Columns.Add("SoLuong", "So luong");
            dgvGioHang.Columns.Add("DonGia", "Don gia");
            dgvGioHang.Columns.Add("ThanhTien", "Thanh tien");

            dgvGioHang.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            grpGioHang.Controls.Add(dgvGioHang);

            GroupBox grpThanhToan = new GroupBox();
            grpThanhToan.Text = "THONG TIN THANH TOAN";
            grpThanhToan.Font = new Font("Arial", 10, FontStyle.Bold);
            grpThanhToan.Location = new Point(20, 530);
            grpThanhToan.Size = new Size(1240, 145);
            this.Controls.Add(grpThanhToan);

            Label lblKH = new Label();
            lblKH.Text = "Khach hang:";
            lblKH.Location = new Point(20, 40);
            lblKH.AutoSize = true;
            grpThanhToan.Controls.Add(lblKH);

            cboKhachHang = new ComboBox();
            cboKhachHang.Location = new Point(120, 35);
            cboKhachHang.Size = new Size(250, 30);
            cboKhachHang.DropDownStyle = ComboBoxStyle.DropDownList;
            grpThanhToan.Controls.Add(cboKhachHang);

            Label lblTong = new Label();
            lblTong.Text = "Tong tien:";
            lblTong.Location = new Point(420, 40);
            lblTong.AutoSize = true;
            grpThanhToan.Controls.Add(lblTong);

            lblTongTien = new Label();
            lblTongTien.Text = "0 VND";
            lblTongTien.Location = new Point(510, 40);
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Arial", 12, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Red;
            grpThanhToan.Controls.Add(lblTongTien);

            Label lblDiem = new Label();
            lblDiem.Text = "Diem du kien:";
            lblDiem.Location = new Point(420, 85);
            lblDiem.AutoSize = true;
            grpThanhToan.Controls.Add(lblDiem);

            lblDiemTichLuy = new Label();
            lblDiemTichLuy.Text = "0";
            lblDiemTichLuy.Location = new Point(530, 85);
            lblDiemTichLuy.AutoSize = true;
            lblDiemTichLuy.Font = new Font("Arial", 11, FontStyle.Bold);
            grpThanhToan.Controls.Add(lblDiemTichLuy);

            btnThanhToan = new Button();
            btnThanhToan.Text = "THANH TOAN";
            btnThanhToan.Location = new Point(850, 40);
            btnThanhToan.Size = new Size(300, 70);
            btnThanhToan.Font = new Font("Arial", 14, FontStyle.Bold);
            btnThanhToan.BackColor = Color.Green;
            btnThanhToan.ForeColor = Color.White;
            grpThanhToan.Controls.Add(btnThanhToan);

            GroupBox grpThongKe = new GroupBox();
            grpThongKe.Text = "BAO CAO - THONG KE";
            grpThongKe.Font = new Font("Arial", 10, FontStyle.Bold);
            grpThongKe.Location = new Point(20, 690);
            grpThongKe.Size = new Size(1240, 200);
            this.Controls.Add(grpThongKe);

            btnDoanhThuNgay = new Button();
            btnDoanhThuNgay.Text = "DOANH THU NGAY";
            btnDoanhThuNgay.Location = new Point(20, 30);
            btnDoanhThuNgay.Size = new Size(160, 35);
            grpThongKe.Controls.Add(btnDoanhThuNgay);

            lblDoanhThuNgay = new Label();
            lblDoanhThuNgay.Text = "0 VND";
            lblDoanhThuNgay.Location = new Point(190, 38);
            lblDoanhThuNgay.AutoSize = true;
            grpThongKe.Controls.Add(lblDoanhThuNgay);

            btnDoanhThuThang = new Button();
            btnDoanhThuThang.Text = "DOANH THU THANG";
            btnDoanhThuThang.Location = new Point(420, 30);
            btnDoanhThuThang.Size = new Size(170, 35);
            grpThongKe.Controls.Add(btnDoanhThuThang);

            lblDoanhThuThang = new Label();
            lblDoanhThuThang.Text = "0 VND";
            lblDoanhThuThang.Location = new Point(600, 38);
            lblDoanhThuThang.AutoSize = true;
            grpThongKe.Controls.Add(lblDoanhThuThang);

            btnTop5 = new Button();
            btnTop5.Text = "TOP 5 SAN PHAM BAN CHAY";
            btnTop5.Location = new Point(850, 30);
            btnTop5.Size = new Size(300, 35);
            grpThongKe.Controls.Add(btnTop5);

            dgvTop5 = new DataGridView();
            dgvTop5.Location = new Point(20, 75);
            dgvTop5.Size = new Size(1190, 110);
            dgvTop5.ReadOnly = true;
            dgvTop5.AllowUserToAddRows = false;
            dgvTop5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTop5.RowHeadersVisible = false;
            dgvTop5.BackgroundColor = Color.White;
            grpThongKe.Controls.Add(dgvTop5);
        }

        private void frmBanHang_Load(object? sender, EventArgs e)
        {
            try
            {
                banHangBUS.KiemTraKetNoi();
                LoadKhachHang();
                CapNhatTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khoi tao: " + ex.Message);
            }
        }

        private void LoadKhachHang()
        {
            cboKhachHang.Items.Clear();

            foreach (KhachHangDTO kh in khachHangBUS.LayDanhSachKemKhachLe())
                cboKhachHang.Items.Add(kh);

            cboKhachHang.SelectedIndex = 0;
        }

        private void HienThiGioHang()
        {
            dgvGioHang.Rows.Clear();

            foreach (GioHangItemDTO item in gioHang)
            {
                dgvGioHang.Rows.Add(
                    item.MaSP,
                    item.TenSP,
                    item.SoLuong,
                    item.DonGia,
                    item.ThanhTien
                );
            }

            CapNhatTongTien();
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            ThemSanPhamVaoGio();
        }

        private void txtMaSP_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ThemSanPhamVaoGio();
                e.SuppressKeyPress = true;
            }
        }

        private void ThemSanPhamVaoGio()
        {
            try
            {
                SanPhamDTO sp = sanPhamBUS.TimTheoMa(txtMaSP.Text);
                banHangBUS.ThemVaoGio(gioHang, sp, Convert.ToInt32(numSoLuong.Value));

                HienThiGioHang();
                txtMaSP.Clear();
                numSoLuong.Value = 1;
                txtMaSP.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtMaSP.Focus();
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            if (dgvGioHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui long chon san pham can xoa!");
                return;
            }

            int maSP = Convert.ToInt32(dgvGioHang.SelectedRows[0].Cells["MaSP"].Value);
            banHangBUS.XoaKhoiGio(gioHang, maSP);
            HienThiGioHang();
        }

        private void CapNhatTongTien()
        {
            decimal tongTien = banHangBUS.TinhTongTien(gioHang);

            lblTongTien.Text = tongTien.ToString("N0") + " VND";
            lblDiemTichLuy.Text = banHangBUS.TinhDiem(tongTien).ToString();
        }

        private void cboKhachHang_SelectedIndexChanged(object? sender, EventArgs e)
        {
            CapNhatTongTien();
        }

        private void btnThanhToan_Click(object? sender, EventArgs e)
        {
            if (gioHang.Count == 0)
            {
                MessageBox.Show("Gio hang dang trong!");
                return;
            }

            KhachHangDTO? khachHang = cboKhachHang.SelectedItem as KhachHangDTO;
            int? maKH = khachHang?.MaKH;
            decimal tongTien = banHangBUS.TinhTongTien(gioHang);

            try
            {
                int maHD = banHangBUS.ThanhToan(maKH, gioHang);

                MessageBox.Show(
                    "Thanh toan thanh cong!\nMa hoa don: " + maHD +
                    "\nTong tien: " + tongTien.ToString("N0") + " VND",
                    "Thanh cong"
                );

                gioHang.Clear();
                HienThiGioHang();
                LoadKhachHang();
            }
            catch (Exception ex)
            {
                // DAL da goi trans.Rollback()
                MessageBox.Show(
                    "Thanh toan that bai!\nDu lieu da duoc huy bo.\n" + ex.Message,
                    "Loi thanh toan"
                );
            }
        }

        private void btnDoanhThuNgay_Click(object? sender, EventArgs e)
        {
            try
            {
                lblDoanhThuNgay.Text =
                    baoCaoBUS.DoanhThuNgay(DateTime.Today).ToString("N0") + " VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi thong ke ngay: " + ex.Message);
            }
        }

        private void btnDoanhThuThang_Click(object? sender, EventArgs e)
        {
            try
            {
                lblDoanhThuThang.Text =
                    baoCaoBUS.DoanhThuThang(DateTime.Today).ToString("N0") + " VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi thong ke thang: " + ex.Message);
            }
        }

        private void btnTop5_Click(object? sender, EventArgs e)
        {
            try
            {
                dgvTop5.DataSource = baoCaoBUS.Top5BanChay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi lay Top 5: " + ex.Message);
            }
        }
    }
}
