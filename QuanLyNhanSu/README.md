# Bài 6.2: Hệ thống quản lý nhân sự & tính lương (HRM & Payroll System)

## MỤC 1: THÔNG TIN SINH VIÊN
- **Họ và tên sinh viên:** [Điền họ tên]
- **Mã sinh viên:** [Điền MSV]
- **Lớp / Khóa:** [Điền tên lớp]

## MỤC 2: HÌNH ẢNH KẾT QUẢ CÁC MÀN HÌNH CHỨC NĂNG

1. Màn hình Chính / Tổng quan hệ thống:![Màn hình chính](images/01_main_screen.png)
2. Màn hình Quản lý Danh mục (CRUD - Thêm/Sửa/Xóa):![Màn hình CRUD](images/02_crud_screen.png)
3. Màn hình Thực thi Nghiệp vụ chính (Chấm công / Chốt lương):![Màn hình Nghiệp vụ](images/03_transaction_screen.png)
4. Màn hình Báo cáo - Thống kê:![Màn hình Thống kê](images/04_report_screen.png)
5. Minh họa An toàn Dữ liệu (Phòng chống SQL Injection / Báo lỗi Transaction Rollback):![Minh họa Xử lý lỗi](images/05_error_handling.png)

---

## Hướng dẫn chạy (Visual Studio 2026, .NET 10, WinForms)
1. Chạy `Database/ScriptDB.sql` trong SQL Server Management Studio (tạo `HRMDB` + dữ liệu mẫu).
2. Mở `QuanLyNhanSu.sln`. Nếu cần, sửa chuỗi kết nối trong `QuanLyNhanSu/App.config`.
3. Nhấn **F5**.

## Cấu trúc thư mục (3-Tier)
```
├── Database/ScriptDB.sql
├── QuanLyNhanSu/
│   ├── DTO/   NhanVienDTO
│   ├── DAL/   Database, NhanVienDAL
│   ├── BUS/   NhanVienBUS
│   ├── GUI/   frmNhanVien
│   └── App.config / Program.cs
├── images/
├── README.md
└── QuanLyNhanSu.sln
```
