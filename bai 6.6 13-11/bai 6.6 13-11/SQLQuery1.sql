USE LibraryDB;
GO

-- 1. Bảng Độc giả
CREATE TABLE DocGia (
    MaDocGia INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE,
    NgayHetHanThe DATETIME NOT NULL
);

-- 2. Bảng Đầu sách
CREATE TABLE DauSach (
    MaSach INT PRIMARY KEY IDENTITY(1,1),
    TenSach NVARCHAR(200) NOT NULL,
    TheLoai NVARCHAR(50),
    SoLuongCon INT CHECK (SoLuongCon >= 0)
);

-- 3. Bảng Phiếu mượn
CREATE TABLE PhieuMuon (
    MaPhieu INT PRIMARY KEY IDENTITY(1,1),
    MaDocGia INT FOREIGN KEY REFERENCES DocGia(MaDocGia),
    NgayMuon DATETIME DEFAULT GETDATE(),
    HanTra DATETIME NOT NULL,
    TrangThai NVARCHAR(50) -- 'Đang mượn', 'Đã trả', 'Quá hạn'
);

-- 4. Bảng Chi tiết mượn
CREATE TABLE ChiTietMuon (
    MaPhieu INT FOREIGN KEY REFERENCES PhieuMuon(MaPhieu),
    MaSach INT FOREIGN KEY REFERENCES DauSach(MaSach),
    SoLuong INT CHECK (SoLuong > 0),
    PRIMARY KEY (MaPhieu, MaSach)
);
GO

-- Thêm dữ liệu mẫu để test ngay
INSERT INTO DocGia (HoTen, Email, NgayHetHanThe) VALUES
(N'Nguyễn Văn An', 'an.nv@gmail.com', DATEADD(MONTH, 6, GETDATE())),      -- Thẻ còn hạn
(N'Trần Thị Bình', 'binh.tt@gmail.com', DATEADD(DAY, -5, GETDATE())),     -- Thẻ ĐÃ HẾT HẠN
(N'Lê Hoàng Cường', 'cuong.lh@gmail.com', DATEADD(YEAR, 1, GETDATE()));   -- Thẻ còn hạn

INSERT INTO DauSach (TenSach, TheLoai, SoLuongCon) VALUES
(N'Lập trình C# căn bản', N'Công nghệ thông tin', 10),
(N'Cấu trúc dữ liệu & Giải thuật', N'Công nghệ thông tin', 5),
(N'Kinh tế vĩ mô', N'Kinh tế', 8),
(N'Dế mèn phiêu lưu ký', N'Văn học', 12);

-- Phiếu mượn mẫu (1 phiếu đang mượn quá hạn, 1 phiếu đang mượn bình thường, 1 phiếu đã trả)
INSERT INTO PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) VALUES
(1, DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -3, GETDATE()), N'Quá hạn'),
(3, DATEADD(DAY, -2, GETDATE()), DATEADD(DAY, 7, GETDATE()), N'Đang mượn'),
(1, DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, -10, GETDATE()), N'Đã trả');

INSERT INTO ChiTietMuon (MaPhieu, MaSach, SoLuong) VALUES
(1, 1, 2),
(2, 3, 1),
(3, 4, 1);
GO