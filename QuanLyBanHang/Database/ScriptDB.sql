/* ===== Bài 6.1 - BanHangDB: Hệ thống quản lý bán hàng & siêu thị mini ===== */
USE master;
GO
IF DB_ID(N'BanHangDB') IS NOT NULL
BEGIN
    ALTER DATABASE BanHangDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BanHangDB;
END
GO
CREATE DATABASE BanHangDB;
GO
USE BanHangDB;
GO

CREATE TABLE NhanVien (
    MaNV        INT IDENTITY(1,1) PRIMARY KEY,
    TenNV       NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15)   NULL,
    ChucVu      NVARCHAR(50)  NULL
);
CREATE UNIQUE INDEX UQ_NhanVien_SDT ON NhanVien(SoDienThoai) WHERE SoDienThoai IS NOT NULL;

CREATE TABLE KhachHang (
    MaKH        INT IDENTITY(1,1) PRIMARY KEY,
    TenKH       NVARCHAR(100) NOT NULL,
    DienThoai   VARCHAR(15)   NULL,
    DiemTichLuy INT NOT NULL DEFAULT 0
);
CREATE UNIQUE INDEX UQ_KhachHang_DT ON KhachHang(DienThoai) WHERE DienThoai IS NOT NULL;

CREATE TABLE SanPham (
    MaSP       INT IDENTITY(1,1) PRIMARY KEY,
    TenSP      NVARCHAR(150) NOT NULL,
    DonGia     DECIMAL(18,2) NOT NULL CHECK (DonGia > 0),
    SoLuongTon INT NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0)
);

CREATE TABLE HoaDon (
    MaHD     INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap  DATETIME NOT NULL DEFAULT GETDATE(),
    MaNV     INT NULL REFERENCES NhanVien(MaNV),
    MaKH     INT NULL REFERENCES KhachHang(MaKH),   -- NULL = khách lẻ
    TongTien DECIMAL(18,2) NOT NULL DEFAULT 0
);

CREATE TABLE ChiTietHoaDon (
    MaHD      INT NOT NULL REFERENCES HoaDon(MaHD),
    MaSP      INT NOT NULL REFERENCES SanPham(MaSP),
    SoLuong   INT NOT NULL CHECK (SoLuong > 0),
    DonGia    DECIMAL(18,2) NOT NULL,
    ThanhTien DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (MaHD, MaSP)
);
GO

/* ---------- Dữ liệu mẫu ---------- */
INSERT INTO NhanVien(TenNV, SoDienThoai, ChucVu) VALUES
(N'Nguyễn Văn An',  '0901000001', N'Thu ngân'),
(N'Trần Thị Bình',  '0901000002', N'Thu ngân'),
(N'Lê Quang Cường', '0901000003', N'Quản lý');

INSERT INTO KhachHang(TenKH, DienThoai, DiemTichLuy) VALUES
(N'Phạm Minh Dũng', '0912000001', 1),
(N'Hoàng Thị Em',   '0912000002', 3),
(N'Vũ Gia Phúc',    '0912000003', 0),
(N'Đỗ Thu Giang',   '0912000004', 0);

INSERT INTO SanPham(TenSP, DonGia, SoLuongTon) VALUES
(N'Mì gói Hảo Hảo',            5000,   200),   -- MaSP 1
(N'Nước ngọt Coca-Cola 330ml', 10000,  150),   -- 2
(N'Sữa tươi Vinamilk 1L',      32000,  80),    -- 3
(N'Bánh Oreo',                 28000,  60),    -- 4
(N'Dầu ăn Simply 1L',          55000,  3),     -- 5 (tồn thấp, để thử Rollback)
(N'Gạo ST25 túi 5kg',          120000, 40),    -- 6
(N'Bột giặt OMO 3kg',          150000, 25),    -- 7
(N'Nước mắm Nam Ngư 500ml',    35000,  50),    -- 8
(N'Trứng gà (vỉ 10 quả)',      38000,  30),    -- 9
(N'Cà phê G7 hộp 16 gói',      52000,  45);    -- 10

SET IDENTITY_INSERT HoaDon ON;
INSERT INTO HoaDon(MaHD, NgayLap, MaNV, MaKH, TongTien) VALUES
(1, GETDATE(), 1, 1, 100000),
(2, GETDATE(), 2, NULL, 84000),
(3, GETDATE(), 1, 2, 330000),
(4, GETDATE(), 2, 3, 84000);
SET IDENTITY_INSERT HoaDon OFF;

INSERT INTO ChiTietHoaDon(MaHD, MaSP, SoLuong, DonGia, ThanhTien) VALUES
(1,1,10,5000,50000),(1,2,5,10000,50000),
(2,3,2,32000,64000),(2,1,4,5000,20000),
(3,6,1,120000,120000),(3,7,1,150000,150000),(3,2,6,10000,60000),
(4,4,3,28000,84000);
GO
