/* ===== Bài 6.2 - HRMDB: Quản lý nhân sự & tính lương ===== */
USE master;
GO
IF DB_ID(N'HRMDB') IS NOT NULL
BEGIN
    ALTER DATABASE HRMDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HRMDB;
END
GO
CREATE DATABASE HRMDB;
GO
USE HRMDB;
GO

CREATE TABLE PhongBan (
    MaPB  INT IDENTITY(1,1) PRIMARY KEY,
    TenPB NVARCHAR(100) NOT NULL,
    Mota  NVARCHAR(255) NULL
);

CREATE TABLE NhanVien (
    MaNV        INT IDENTITY(1,1) PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    NgaySinh    DATETIME NULL,
    CCCD        VARCHAR(20) NULL,
    MaPB        INT NOT NULL REFERENCES PhongBan(MaPB),
    LuongCoBan  DECIMAL(18,2) NOT NULL CHECK (LuongCoBan > 0),
    HeSoLuong   FLOAT NOT NULL DEFAULT 1.0
);
CREATE UNIQUE INDEX UQ_NhanVien_CCCD ON NhanVien(CCCD) WHERE CCCD IS NOT NULL;

CREATE TABLE BangChamCong (
    MaCong     INT IDENTITY(1,1) PRIMARY KEY,
    MaNV       INT NOT NULL REFERENCES NhanVien(MaNV),
    ThangNam   VARCHAR(7) NOT NULL,                 -- ví dụ '11/2026'
    SoNgayCong FLOAT NOT NULL DEFAULT 0 CHECK (SoNgayCong >= 0 AND SoNgayCong <= 31),
    SoGioOT    FLOAT NOT NULL DEFAULT 0 CHECK (SoGioOT >= 0),
    DaChot     BIT   NOT NULL DEFAULT 0,            -- 1 = đã chốt lương, khóa chấm công
    CONSTRAINT UQ_ChamCong UNIQUE (MaNV, ThangNam)
);

CREATE TABLE BangLuong (
    MaLuong  INT IDENTITY(1,1) NOT NULL,
    MaNV     INT NOT NULL REFERENCES NhanVien(MaNV),
    ThangNam VARCHAR(7) NOT NULL,
    ThucLinh DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (MaNV, ThangNam)
);
GO

/* ---------- Dữ liệu mẫu (5 phòng ban khớp danh sách trong giao diện) ---------- */
INSERT INTO PhongBan(TenPB, Mota) VALUES
(N'Nhân sự',    N'Quản lý hồ sơ, tuyển dụng'),
(N'Kế toán',    N'Tài chính, lương, thuế'),
(N'Kỹ thuật',   N'Phát triển và vận hành phần mềm'),
(N'Kinh doanh', N'Bán hàng, chăm sóc khách hàng'),
(N'Hành chính', N'Văn phòng, hậu cần');

INSERT INTO NhanVien(HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) VALUES
(N'Nguyễn Văn An',   '1990-03-12', '001090000001', 1, 12000000, 1.2),
(N'Trần Thị Bích',   '1992-07-25', '001092000002', 2, 13000000, 1.3),
(N'Lê Quốc Cường',   '1988-11-02', '001088000003', 3, 20000000, 1.5),
(N'Phạm Thu Dung',   '1995-01-18', '001095000004', 3, 18000000, 1.4),
(N'Hoàng Minh Đức',  '1993-09-09', '001093000005', 4, 10000000, 1.0),
(N'Vũ Thị Hà',       '1996-05-30', '001096000006', 4, 10000000, 1.1),
(N'Đặng Gia Huy',    '1991-12-15', '001091000007', 3, 22000000, 1.6),
(N'Bùi Ngọc Lan',    '1994-04-04', '001094000008', 5, 14000000, 1.2);
GO
