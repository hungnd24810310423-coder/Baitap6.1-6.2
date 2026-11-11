
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyNhanSu.GUI
{
    public partial class frmNhanVien : Form
    {
        TextBox txtMaNV, txtHoTen, txtCCCD, txtLuong, txtHeSo, txtTimKiem;
        DateTimePicker dtpNgaySinh;
        ComboBox cboPhongBan;
        DataGridView dgvNhanVien;

        Button btnThem, btnSua, btnXoa, btnLamMoi, btnTimKiem;

        public frmNhanVien()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            this.Controls.Clear();
            this.Text = "QUAN LY NHAN SU VA TIEN LUONG";
            this.Size = new Size(1200, 750);
            this.MinimumSize = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            Panel header = new Panel();
            header.Dock = DockStyle.Top;
            header.Height = 85;
            header.BackColor = Color.FromArgb(30, 65, 110);
            this.Controls.Add(header);

            Label lblTitle = new Label();
            lblTitle.Text = "HE THONG QUAN LY NHAN SU";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Arial", 21, FontStyle.Bold);
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            header.Controls.Add(lblTitle);

            Panel main = new Panel();
            main.Dock = DockStyle.Fill;
            main.Padding = new Padding(15);
            this.Controls.Add(main);

            GroupBox grpThongTin = new GroupBox();
            grpThongTin.Text = "THONG TIN NHAN VIEN";
            grpThongTin.Font = new Font("Arial", 11, FontStyle.Bold);
            grpThongTin.Dock = DockStyle.Top;
            grpThongTin.Height = 230;
            main.Controls.Add(grpThongTin);

            TaoLabel(grpThongTin, "Ma nhan vien:", 25, 35);
            txtMaNV = TaoTextBox(grpThongTin, 150, 32, 220);

            TaoLabel(grpThongTin, "Ho va ten:", 420, 35);
            txtHoTen = TaoTextBox(grpThongTin, 540, 32, 250);

            TaoLabel(grpThongTin, "Ngay sinh:", 25, 85);
            dtpNgaySinh = new DateTimePicker();
            dtpNgaySinh.Location = new Point(150, 82);
            dtpNgaySinh.Size = new Size(220, 30);
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            grpThongTin.Controls.Add(dtpNgaySinh);

            TaoLabel(grpThongTin, "CCCD:", 420, 85);
            txtCCCD = TaoTextBox(grpThongTin, 540, 82, 250);

            TaoLabel(grpThongTin, "Phong ban:", 25, 135);
            cboPhongBan = new ComboBox();
            cboPhongBan.Location = new Point(150, 132);
            cboPhongBan.Size = new Size(220, 30);
            cboPhongBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhongBan.Items.AddRange(new object[]
            {
                "Nhan su",
                "Ke toan",
                "Ky thuat",
                "Kinh doanh",
                "Hanh chinh"
            });
            grpThongTin.Controls.Add(cboPhongBan);

            TaoLabel(grpThongTin, "Luong co ban:", 420, 135);
            txtLuong = TaoTextBox(grpThongTin, 540, 132, 250);

            TaoLabel(grpThongTin, "He so luong:", 25, 180);
            txtHeSo = TaoTextBox(grpThongTin, 150, 177, 220);

            btnThem = TaoButton(grpThongTin, "THEM", 850, 30, Color.FromArgb(39, 174, 96));
            btnSua = TaoButton(grpThongTin, "SUA", 850, 75, Color.FromArgb(52, 152, 219));
            btnXoa = TaoButton(grpThongTin, "XOA", 850, 120, Color.FromArgb(231, 76, 60));
            btnLamMoi = TaoButton(grpThongTin, "LAM MOI", 850, 165, Color.FromArgb(127, 140, 141));

            btnThem.Click += (s, e) =>
                MessageBox.Show("Chuc nang them nhan vien se duoc ket noi o buoc tiep theo.");

            btnSua.Click += (s, e) =>
                MessageBox.Show("Chuc nang sua nhan vien se duoc ket noi o buoc tiep theo.");

            btnXoa.Click += (s, e) =>
                MessageBox.Show("Chuc nang xoa nhan vien se duoc ket noi o buoc tiep theo.");

            btnLamMoi.Click += (s, e) => LamMoi();

            Panel searchPanel = new Panel();
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Height = 65;
            searchPanel.Padding = new Padding(5, 15, 5, 5);
            main.Controls.Add(searchPanel);

            Label lblSearch = new Label();
            lblSearch.Text = "TIM KIEM:";
            lblSearch.Font = new Font("Arial", 10, FontStyle.Bold);
            lblSearch.Location = new Point(10, 19);
            lblSearch.AutoSize = true;
            searchPanel.Controls.Add(lblSearch);

            txtTimKiem = new TextBox();
            txtTimKiem.Location = new Point(110, 15);
            txtTimKiem.Size = new Size(350, 30);
            searchPanel.Controls.Add(txtTimKiem);

            btnTimKiem = TaoButton(searchPanel, "TIM KIEM", 480, 10, Color.FromArgb(41, 128, 185));
            btnTimKiem.Click += (s, e) =>
                MessageBox.Show("Chuc nang tim kiem se duoc ket noi o buoc tiep theo.");

            GroupBox grpDanhSach = new GroupBox();
            grpDanhSach.Text = "DANH SACH NHAN VIEN";
            grpDanhSach.Font = new Font("Arial", 11, FontStyle.Bold);
            grpDanhSach.Dock = DockStyle.Fill;
            main.Controls.Add(grpDanhSach);

            dgvNhanVien = new DataGridView();
            dgvNhanVien.Dock = DockStyle.Fill;
            dgvNhanVien.BackgroundColor = Color.White;
            dgvNhanVien.BorderStyle = BorderStyle.None;
            dgvNhanVien.AutoGenerateColumns = false;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;
            dgvNhanVien.RowHeadersVisible = false;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.ColumnHeadersHeight = 40;
            dgvNhanVien.RowTemplate.Height = 35;
            dgvNhanVien.EnableHeadersVisualStyles = false;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(30, 65, 110);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font =
                new Font("Arial", 10, FontStyle.Bold);
            dgvNhanVien.DefaultCellStyle.Font = new Font("Arial", 10);
            dgvNhanVien.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(190, 220, 245);

            dgvNhanVien.Columns.Add("MaNV", "Ma nhan vien");
            dgvNhanVien.Columns.Add("HoTen", "Ho va ten");
            dgvNhanVien.Columns.Add("NgaySinh", "Ngay sinh");
            dgvNhanVien.Columns.Add("CCCD", "CCCD");
            dgvNhanVien.Columns.Add("PhongBan", "Phong ban");
            dgvNhanVien.Columns.Add("Luong", "Luong co ban");
            dgvNhanVien.Columns.Add("HeSo", "He so luong");

            grpDanhSach.Controls.Add(dgvNhanVien);

            StatusStrip status = new StatusStrip();
            status.Dock = DockStyle.Bottom;
            status.Items.Add("HE THONG QUAN LY NHAN SU");
            status.Items.Add(new ToolStripStatusLabel()
            {
                Spring = true
            });
            status.Items.Add("Trang thai: San sang");
            this.Controls.Add(status);
            status.BringToFront();
        }

        private void TaoLabel(Control parent, string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.Size = new Size(120, 25);
            lbl.Font = new Font("Arial", 10);
            parent.Controls.Add(lbl);
        }

        private TextBox TaoTextBox(Control parent, int x, int y, int width)
        {
            TextBox txt = new TextBox();
            txt.Location = new Point(x, y);
            txt.Size = new Size(width, 28);
            txt.Font = new Font("Arial", 10);
            parent.Controls.Add(txt);
            return txt;
        }

        private Button TaoButton(Control parent, string text,
            int x, int y, Color color)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(115, 35);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Arial", 9, FontStyle.Bold);
            parent.Controls.Add(btn);
            return btn;
        }

        private void LamMoi()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtCCCD.Clear();
            txtLuong.Clear();
            txtHeSo.Clear();
            txtTimKiem.Clear();
            cboPhongBan.SelectedIndex = -1;
            dtpNgaySinh.Value = DateTime.Now;
            dgvNhanVien.ClearSelection();
        }
    }
}
