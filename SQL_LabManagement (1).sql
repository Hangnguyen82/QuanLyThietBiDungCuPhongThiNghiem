
CREATE DATABASE QuanLyTThietBiDungCuPhongLab;
go
USE QuanLyTThietBiDungCuPhongLab;
go
CREATE TABLE ChucVu (
    MaCV VARCHAR(20) PRIMARY KEY,
    TenCV NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE LoaiDungCu (
    MaLoaiDC VARCHAR(20) PRIMARY KEY,
    TenLoaiDC NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255) NULL
);
GO

CREATE TABLE LoaiThietBi (
    MaLoaiThietBi VARCHAR(20) PRIMARY KEY,
    TenLoaiThietBi NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255) NULL
);
GO

CREATE TABLE NhomQuyen (
    MaNhom VARCHAR(20) PRIMARY KEY,
    TenNhom NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE NhanVien (
    MaNV VARCHAR(20) PRIMARY KEY,
    TenNV NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10) NULL,
    NgaySinh DATE NULL,
    DiaChi NVARCHAR(255) NULL,
    MaCV VARCHAR(20),
    SoDT NVARCHAR(15) NULL,
	Email VARCHAR(100),
	MatKhau VARCHAR(100),
    MaNhom VARCHAR(20),
    FOREIGN KEY (MaCV) REFERENCES ChucVu(MaCV),
    FOREIGN KEY (MaNhom) REFERENCES NhomQuyen(MaNhom)
);
GO

CREATE TABLE QLTaiKhoan (
    MaNV VARCHAR(20) PRIMARY KEY,
    TenNguoiDung NVARCHAR(100) NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    MaNhom VARCHAR(20),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

CREATE TABLE CongTyThanhLy (
    MaCty VARCHAR(20) PRIMARY KEY,
    TenCty NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NULL
);
GO

CREATE TABLE NhaCungCap (
    MaNCC VARCHAR(20) PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NULL
);
GO

CREATE TABLE PhongThiNghiem (
    MaPhong VARCHAR(20) PRIMARY KEY,
    LoaiPhong NVARCHAR(50) NULL,
    ChucNang NVARCHAR(50) NULL
);
GO

CREATE TABLE DungCu (
    MaDungCu VARCHAR(20) PRIMARY KEY,
    TenDungCu NVARCHAR(100) NOT NULL,
    MaLoaiDC VARCHAR(20),
    SoLuong INT NOT NULL,
    TinhTrang NVARCHAR(50) NULL,
    NgayCapNhat DATE NULL,
    NgaySX DATE NULL,
    NhaSX NVARCHAR(100) NULL,
    NgayBaoHanh DATE NULL,
    XuatXu NVARCHAR(100) NULL,
	HinhAnh NVARCHAR(255),
	MaNCC VARCHAR(20),
	isDeleted BIT DEFAULT 0,
    FOREIGN KEY (MaLoaiDC) REFERENCES LoaiDungCu(MaLoaiDC),
	FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);
GO

CREATE TABLE ViTriDungCu (
    MaDungCu VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL,
    NgayCapNhat DATE DEFAULT GETDATE(),
    PRIMARY KEY (MaDungCu, MaPhong),
    FOREIGN KEY (MaDungCu) REFERENCES DungCu(MaDungCu),
    FOREIGN KEY (MaPhong) REFERENCES PhongThiNghiem(MaPhong)
);



CREATE TABLE ThietBi (
    MaThietBi VARCHAR(20) PRIMARY KEY,
    TenThietBi NVARCHAR(100) NOT NULL,
    MaLoaiThietBi VARCHAR(20),
    TinhTrang NVARCHAR(50) NULL,
    NgayCapNhat DATE NULL,
    NgaySX DATE NULL,
    NhaSX NVARCHAR(100) NULL,
    NgayBaoHanh DATE NULL,
    XuatXu NVARCHAR(100) NULL,
	HinhAnh NVARCHAR(255),
	MaNCC VARCHAR(20),
	MaPhong VARCHAR(20),
	isDeleted BIT DEFAULT 0
    FOREIGN KEY (MaLoaiThietBi) REFERENCES LoaiThietBi(MaLoaiThietBi),
	FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC),
	FOREIGN KEY (MaPhong) REFERENCES PhongThiNghiem(MaPhong)
);
GO
--CHỨC NĂNG THANH LÝ-----
---- Phiếu thanh lý
CREATE TABLE PhieuThanhLy (
    MaPhieuTL VARCHAR(20) PRIMARY KEY,--
    MaCty VARCHAR(20),--
    MaNV VARCHAR(20),--
    NgayLapPhieu DATE NULL,--
    TrangThai NVARCHAR(50) DEFAULT 'Chờ duyệt',--
	LyDoChung NVARCHAR(255) NULL,
    TongTien DECIMAL(18, 2) NULL, --sinh ra sau khi hoàn thành hoàn tất thủ 
	NgayHoanTat DATE NULL,                    -- Ngày hoàn thành thủ tục
    TrangThaiThanhLy NVARCHAR(50) DEFAULT 'Chưa hoàn thành', -- Trạng thái thủ tục sinh ra khi Trạng Thái được chuyển sang Đã duyệt
    FOREIGN KEY (MaCty) REFERENCES CongTyThanhLy(MaCty),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO
CREATE TABLE ChiTietPhieuThanhLy (
    MaPhieuTL VARCHAR(20),                     -- Mã phiếu thanh lý
    MaThietBi VARCHAR(20),                     -- Mã thiết bị  
	GiaTL DECIMAL(18, 2) NOT NULL,-- giá thanh lý
    LyDo NVARCHAR(255) NOT NULL,              -- Lý do thanh lý chi tiết (nếu khác với lý do chung)
    PRIMARY KEY (MaPhieuTL, MaThietBi), -- Khóa chính kết hợp
    FOREIGN KEY (MaPhieuTL) REFERENCES PhieuThanhLy(MaPhieuTL), -- Liên kết phiếu thanh lý
    FOREIGN KEY (MaThietBi) REFERENCES ThietBi(MaThietBi)     -- Liên kết thiết bị
);
GO


CREATE TABLE DuyetPhieuThanhLy (
    MaPhieuTL VARCHAR(20),
    MaNV VARCHAR(20),
    NgayDuyet DATE NULL,
    TrangThai NVARCHAR(50) NULL,--Từ chối/ Phê duyệt
	LyDoTuChoi TEXT NULL, 
	GhiChu NVARCHAR(255) NULL,   
    PRIMARY KEY (MaPhieuTL, MaNV),
    FOREIGN KEY (MaPhieuTL) REFERENCES PhieuThanhLy(MaPhieuTL),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

CREATE TABLE LichSuPhieuThanhLy (
    MaLichSu INT IDENTITY(1,1) PRIMARY KEY, -- Mã lịch sử (tự tăng)
    MaPhieuTL VARCHAR(20) NOT NULL,        -- Mã phiếu thanh lý
    TrangThaiTruoc NVARCHAR(50) NULL,      -- Trạng thái trước khi thay đổi
    TrangThaiSau NVARCHAR(50) NOT NULL,    -- Trạng thái sau khi thay đổi
    NgayThayDoi DATETIME DEFAULT GETDATE(),-- Thời gian thay đổi
    MaNV VARCHAR(20),                      -- Mã nhân viên thực hiện thay đổi
    FOREIGN KEY (MaPhieuTL) REFERENCES PhieuThanhLy(MaPhieuTL),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO
----CHỨC NĂNG ĐỀ XUẤT DỤNG CỤ THIẾT BỊ MỚI

--Đề xuất thiết bị / dụng cụ mới
CREATE TABLE PhieuDeXuat (
    MaPhieu VARCHAR(20) PRIMARY KEY,
    NgayTao DATE NOT NULL,
    LyDoDeXuat NVARCHAR(100) NULL,
    MaNV VARCHAR(20), -- Mã nhân viên tạo phiếu
    GhiChu NVARCHAR(255) NULL, -- Thông tin bổ sung
    NgayHoanTat DATE NULL, -- Ngày hoàn thành/phê duyệt phiếu
    TrangThai NVARCHAR(50) DEFAULT N'Chưa phê duyệt', -- Trạng thái của phiếu
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

CREATE TABLE ChiTietDeXuatThietBi (
	MaCTDeXuatTB INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieu VARCHAR(20),
    MaLoaiThietBi VARCHAR(20),
    TenThietBi NVARCHAR(100) NOT NULL, -- Tên loại thiết bị được đề xuất
    SoLuongDeXuat INT NOT NULL, -- Số lượng thiết bị đề xuất
    MoTa NVARCHAR(255) NULL, -- Mô tả loại thiết bị
    FOREIGN KEY (MaPhieu) REFERENCES PhieuDeXuat(MaPhieu),
    FOREIGN KEY (MaLoaiThietBi) REFERENCES LoaiThietBi(MaLoaiThietBi)
);
GO

CREATE TABLE ChiTietDeXuatDungCu (
	MaCTDeXuatDC INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieu VARCHAR(20),
    MaLoaiDC VARCHAR(20),
	TenDungCu NVARCHAR(100) NOT NULL,
    SoLuongDeXuat INT NOT NULL,
	MoTa NVARCHAR(255) NULL, 
    FOREIGN KEY (MaPhieu) REFERENCES PhieuDeXuat(MaPhieu),
    FOREIGN KEY (MaLoaiDC) REFERENCES LoaiDungCu(MaLoaiDC)
);
GO
CREATE TABLE DuyetPhieu (
    MaPhieu VARCHAR(20) PRIMARY KEY,
    MaNV VARCHAR(20),
    NgayDuyet DATE NULL,
    TrangThai NVARCHAR(100) NULL,
    LyDoTuChoi NVARCHAR(255) NULL, -- Lý do từ chối phê duyệt
    FOREIGN KEY (MaPhieu) REFERENCES PhieuDeXuat(MaPhieu),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO
CREATE TABLE LichSuPhieuDeXuat (
    MaLichSu INT IDENTITY(1,1) PRIMARY KEY, -- Mã lịch sử (tự tăng)
    MaPhieu VARCHAR(20) NOT NULL,        -- Mã phiếu 
    TrangThaiTruoc NVARCHAR(50) NULL,      -- Trạng thái trước khi thay đổi
    TrangThaiSau NVARCHAR(50) NOT NULL,    -- Trạng thái sau khi thay đổi
    NgayThayDoi DATETIME DEFAULT GETDATE(),-- Thời gian thay đổi
    MaNV VARCHAR(20),                      -- Mã nhân viên thực hiện thay đổi
    FOREIGN KEY (MaPhieu) REFERENCES PhieuDeXuat(MaPhieu),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO
--Phiếu nhập thiết bị/ dụng cụ mới
-- Bảng Phiếu Nhập
CREATE TABLE PhieuNhap (
    MaPhieuNhap VARCHAR(20) PRIMARY KEY, -- Mã phiếu nhập
    MaNV VARCHAR(20), -- Nhân viên nhập phiếu
    MaPhieu VARCHAR(20) NULL, -- Mã phiếu đề xuất 
    NgayNhap DATE NULL, -- Ngày nhập
    TongTien DECIMAL(18, 2) NOT NULL, -- Tổng tiền của phiếu nhập
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaPhieu) REFERENCES PhieuDeXuat(MaPhieu) -- Thêm liên kết đến Phiếu Đề Xuất
);

-- Bảng Chi Tiết Nhập Thiết Bị
CREATE TABLE ChiTietNhapTB (
    MaPhieuNhap VARCHAR(20), -- Mã phiếu nhập
    MaThietBi VARCHAR(20), -- Mã thiết bị
    GiaNhap DECIMAL(18, 2) NOT NULL, -- Giá nhập của thiết bị
    PRIMARY KEY (MaPhieuNhap, MaThietBi), -- Khóa chính ghép
    FOREIGN KEY (MaPhieuNhap) REFERENCES PhieuNhap(MaPhieuNhap),
    FOREIGN KEY (MaThietBi) REFERENCES ThietBi(MaThietBi)
);
GO

-- Bảng Chi Tiết Nhập Dụng Cụ
CREATE TABLE ChiTietNhapDC (
    MaPhieuNhap VARCHAR(20), -- Mã phiếu nhập
    MaDungCu VARCHAR(20), -- Mã dụng cụ
    GiaNhap DECIMAL(18, 2) NOT NULL, -- Giá nhập của dụng cụ
    SoLuongNhap INT NOT NULL, -- Số lượng nhập
    PRIMARY KEY (MaPhieuNhap, MaDungCu), -- Khóa chính ghép
    FOREIGN KEY (MaPhieuNhap) REFERENCES PhieuNhap(MaPhieuNhap),
    FOREIGN KEY (MaDungCu) REFERENCES DungCu(MaDungCu)
);
GO
-----PHIẾU ĐĂNG KÝ SỬ DỤNG
-- Bảng lịch sử dụng dụng cụ


CREATE TABLE PhieuDangKi (
    MaPhieuDK VARCHAR(20) PRIMARY KEY,
    MaNV VARCHAR(20),
	MaPhong VARCHAR(20) NOT NULL,
    NgayLap DATETIME NULL,
    GhiChu NVARCHAR(255), -- Thông tin bổ sung
    NgayHoanTat DATETIME NULL, -- Ngày hoàn thành/phê duyệt phiếu
    TrangThai NVARCHAR(50) DEFAULT N'Chưa phê duyệt', -- Trạng thái phiếu
    LyDoDK NVARCHAR(100) NULL, -- Lý do từ chối (nếu có)
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
	FOREIGN KEY (MaPhong) REFERENCES PhongThiNghiem(MaPhong)
);
GO

CREATE TABLE DuyetPhieuDK (
    MaPhieuDK VARCHAR(20) PRIMARY KEY,
    MaNV VARCHAR(20),
    NgayDuyet DATETIME NULL,
    TrangThai NVARCHAR(50) NULL, -- Trạng thái phê duyệt
    LyDoTuChoi NVARCHAR(255), -- Lý do từ chối hoặc lý do phê duyệt
    FOREIGN KEY (MaPhieuDK) REFERENCES PhieuDangKi(MaPhieuDK),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

CREATE TABLE DangKiThietBi (
    MaPhieuDK VARCHAR(20),
    MaThietBi VARCHAR(20),
    NgayDangKi DATETIME NULL,
	NgaySuDung DATETIME NULL,
	NgayKetThuc DATETIME NULL,
	TrangThaiSuDung NVARCHAR(50) DEFAULT N'Chưa sử dụng',--trạng thái phiếu đã duyệt thì sẽ có trạng thái này, sẽ có 4 trạng thái đang sử dụng, quá hạn sử dụng, hoàn thành sử dụng
	TinhTrangSuDung NVARCHAR(255),--khi bấm nút kết thúc sử dụng sẽ hiển thị modal điền tình trạng khi trạng thái sử dụng là hoàn thành sử dụng và lưu vào database
    NgayBatDauThucTe DATETIME NULL,
    NgayKetThucThucTe DATETIME NULL,
	PRIMARY KEY (MaPhieuDK, MaThietBi),
    FOREIGN KEY (MaPhieuDK) REFERENCES PhieuDangKi(MaPhieuDK),
    FOREIGN KEY (MaThietBi) REFERENCES ThietBi(MaThietBi),
);
GO

CREATE TABLE DangKiDungCu (
    MaPhieuDK VARCHAR(20),
    MaDungCu VARCHAR(20),
    SoLuong INT NOT NULL,
	NgayDangKi DATETIME NULL,
	NgaySuDung DATETIME NULL,
	NgayKetThuc DATETIME NULL,
	TrangThaiSuDung NVARCHAR(50) DEFAULT N'Chưa sử dụng',--trạng thái phiếu đã duyệt thì sẽ có trạng thái này, sẽ có 4 trạng thái đang sử dụng, quá hạn sử dụng, hoàn thành sử dụng
	TinhTrangSuDung NVARCHAR(255),--khi bấm nút kết thúc sử dụng sẽ hiển thị modal điền tình trạng khi trạng thái sử dụng là hoàn thành sử dụng và lưu vào database
    NgayBatDauThucTe DATETIME NULL,
    NgayKetThucThucTe DATETIME NULL,
	PRIMARY KEY (MaPhieuDK, MaDungCu),
    FOREIGN KEY (MaPhieuDK) REFERENCES PhieuDangKi(MaPhieuDK),
    FOREIGN KEY (MaDungCu) REFERENCES DungCu(MaDungCu)
);
GO
CREATE TABLE LichDungCu (
    MaLichDC INT IDENTITY(1,1) PRIMARY KEY,
	MaPhieuDK VARCHAR(20) NOT NULL,
    MaDungCu VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    NgaySuDung DATETIME NOT NULL,
    NgayKetThuc DATETIME NOT NULL,
    SoLuong INT NOT NULL,
	FOREIGN KEY (MaPhieuDK) REFERENCES PhieuDangKi(MaPhieuDK),
    FOREIGN KEY (MaDungCu) REFERENCES DungCu(MaDungCu),
    FOREIGN KEY (MaPhong) REFERENCES PhongThiNghiem(MaPhong)
);
GO

-- Bảng lịch sử dụng thiết bị
CREATE TABLE LichThietBi (
    MaLichTB INT IDENTITY(1,1) PRIMARY KEY,
	MaPhieuDK VARCHAR(20) NOT NULL,
    MaThietBi VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    NgaySuDung DATETIME NOT NULL,
    NgayKetThuc DATETIME NOT NULL,
	FOREIGN KEY (MaPhieuDK) REFERENCES PhieuDangKi(MaPhieuDK),
    FOREIGN KEY (MaThietBi) REFERENCES ThietBi(MaThietBi),
    FOREIGN KEY (MaPhong) REFERENCES PhongThiNghiem(MaPhong)
);
GO
CREATE TABLE QuanLyGioTB (
	MaQuanLyTB INT IDENTITY(1,1) PRIMARY KEY,
	MaPhieuDK VARCHAR(20) NOT NULL,
	MaThietBi VARCHAR(20) NOT NULL,
	MaPhong VARCHAR(20) NOT NULL,
	soGioSuDungThucTe DECIMAL(5, 2) NULL,
	FOREIGN KEY (MaPhieuDK) REFERENCES PhieuDangKi(MaPhieuDK),
    FOREIGN KEY (MaThietBi) REFERENCES ThietBi(MaThietBi),
	FOREIGN KEY (MaPhong) REFERENCES PhongThiNghiem(MaPhong)
)
CREATE TABLE QuanLyGioDC (
	MaQuanLyDC INT IDENTITY(1,1) PRIMARY KEY,
	MaPhieuDK VARCHAR(20) NOT NULL,
	MaDungCu VARCHAR(20) NOT NULL,
	MaPhong VARCHAR(20) NOT NULL,
	soGioSuDungThucTe DECIMAL(5, 2) NULL,
	FOREIGN KEY (MaPhieuDK) REFERENCES PhieuDangKi(MaPhieuDK),
    FOREIGN KEY (MaDungCu) REFERENCES DungCu(MaDungCu),
	FOREIGN KEY (MaPhong) REFERENCES PhongThiNghiem(MaPhong)
)

CREATE TABLE PhieuPhanBoTB (
    MaPhieu VARCHAR(20) PRIMARY KEY,
    MaThietBi VARCHAR(20),
	MaPhongTruoc VARCHAR(20),
	MaPhongSau VARCHAR(20),
    MaNV VARCHAR(20),
    NoiDung NVARCHAR(255) NULL,
    NgayLap DATE NULL,
	TrangThai NVARCHAR(50) DEFAULT N'Đã phân bổ',
	FOREIGN KEY (MaPhongTruoc) REFERENCES PhongThiNghiem(MaPhong),
	FOREIGN KEY (MaPhongSau) REFERENCES PhongThiNghiem(MaPhong),
    FOREIGN KEY (MaThietBi) REFERENCES ThietBi(MaThietBi),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

CREATE TABLE PhanBoDC (
    MaPhieu VARCHAR(20),
    MaDungCu VARCHAR(20),
	SoLuong INT NOT NULL,
	MaPhongTruoc VARCHAR(20),
	MaPhongSau VARCHAR(20),
	NgayLap DATE NULL,
	MaNV VARCHAR(20),
	NoiDung NVARCHAR(255) NULL,
	TrangThai NVARCHAR(50) DEFAULT N'Đã phân bổ', -- Trạng thái sẽ có Đã phân bổ/ Chưa phân bổ 
    PRIMARY KEY (MaPhieu),
    FOREIGN KEY (MaPhieu) REFERENCES PhieuPhanBoTB(MaPhieu),
    FOREIGN KEY (MaDungCu) REFERENCES DungCu(MaDungCu),
	FOREIGN KEY (MaPhongTruoc) REFERENCES PhongThiNghiem(MaPhong),
	FOREIGN KEY (MaPhongSau) REFERENCES PhongThiNghiem(MaPhong),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO
---------------- chức năng luân chuyển dụng cụ, thiết bị------------
CREATE TABLE PhieuDeXuatLuanChuyen (
    MaPhieuLC VARCHAR(20) PRIMARY KEY, -- Mã phiếu đề xuất (Unique)
    NgayTao DATE NOT NULL, -- Ngày tạo phiếu
    TrangThai NVARCHAR(50) DEFAULT N'Đang xử lý', -- Trạng thái của phiếu (ví dụ: "Đã duyệt", "Đang xử lý", "Từ chối")
    MaNV VARCHAR(20) NOT NULL, -- Người đề xuất luân chuyển
    GhiChu NVARCHAR(255) NULL, -- Ghi chú thêm
	NgayLuanChuyen DATE NULL, -- Ngày dự kiến luân chuyển
    NgayHoanTat DATE NULL, -- Ngày hoàn tất luân chuyển
	FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

CREATE TABLE ChiTietLuanChuyenDC (
    MaPhieuLC VARCHAR(20), -- Mã phiếu đề xuất (FK từ PhieuDeXuatLuanChuyen)
    MaDungCu VARCHAR(20), -- Mã dụng cụ (FK từ bảng DungCu)
    MaPhongTu VARCHAR(20), -- Phòng hiện tại của dụng cụ (FK từ PhongThiNghiem)
    MaPhongDen VARCHAR(20), -- Phòng cần luân chuyển tới (FK từ PhongThiNghiem)
    SoLuong INT NOT NULL, -- Số lượng dụng cụ luân chuyển
    PRIMARY KEY (MaPhieuLC, MaDungCu),
    FOREIGN KEY (MaPhieuLC) REFERENCES PhieuDeXuatLuanChuyen(MaPhieuLC),
    FOREIGN KEY (MaDungCu) REFERENCES DungCu(MaDungCu),
    FOREIGN KEY (MaPhongTu) REFERENCES PhongThiNghiem(MaPhong),
    FOREIGN KEY (MaPhongDen) REFERENCES PhongThiNghiem(MaPhong)
);

CREATE TABLE ChiTietLuanChuyenTB (
    MaPhieuLC VARCHAR(20), -- Mã phiếu đề xuất (FK từ PhieuDeXuatLuanChuyen)
    MaThietBi VARCHAR(20), -- Mã thiết bị (FK từ bảng ThietBi)
    MaPhongTu VARCHAR(20), -- Phòng hiện tại của thiết bị (FK từ PhongThiNghiem)
    MaPhongDen VARCHAR(20), -- Phòng cần luân chuyển tới (FK từ PhongThiNghiem)
    PRIMARY KEY (MaPhieuLC, MaThietBi),
    FOREIGN KEY (MaPhieuLC) REFERENCES PhieuDeXuatLuanChuyen(MaPhieuLC),
    FOREIGN KEY (MaThietBi) REFERENCES ThietBi(MaThietBi),
    FOREIGN KEY (MaPhongTu) REFERENCES PhongThiNghiem(MaPhong),
    FOREIGN KEY (MaPhongDen) REFERENCES PhongThiNghiem(MaPhong)
);

CREATE TABLE LichSuPhieuLuanChuyen (
    MaLichSu INT IDENTITY(1,1) PRIMARY KEY, -- Mã lịch sử (tự tăng)
    MaPhieuLC VARCHAR(20) NOT NULL,        -- Mã phiếu thanh lý
    TrangThaiTruoc NVARCHAR(50) NULL,      -- Trạng thái trước khi thay đổi
    TrangThaiSau NVARCHAR(50) NOT NULL,    -- Trạng thái sau khi thay đổi
    NgayThayDoi DATETIME DEFAULT GETDATE(),-- Thời gian thay đổi
    MaNV VARCHAR(20),                      -- Mã nhân viên thực hiện thay đổi
    FOREIGN KEY (MaPhieuLC) REFERENCES PhieuDeXuatLuanChuyen(MaPhieuLC),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO
CREATE TABLE DuyetPhieuLuanChuyen (
    MaPhieuLC VARCHAR(20) PRIMARY KEY,
    MaNV VARCHAR(20),
    NgayDuyet DATETIME NULL,
    TrangThai NVARCHAR(50) NULL, -- Trạng thái phê duyệt
    LyDoTuChoi NVARCHAR(255), -- Lý do từ chối hoặc lý do phê duyệt
    FOREIGN KEY (MaPhieuLC) REFERENCES PhieuDeXuatLuanChuyen(MaPhieuLC),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO
--Phiếu bão dưỡng 
CREATE TABLE PhieuBaoDuong (
    MaPhieuBD VARCHAR(20) PRIMARY KEY,
    MaNV VARCHAR(20),
	MaNCC VARCHAR(20),
    NoiDung NVARCHAR(100) NULL,
    NgayBaoDuong DATE NULL,
    TongTien DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
	FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC),
);
GO

CREATE TABLE ChiTietBaoDuongTB (
    MaPhieuBD VARCHAR(20),
    MaThietBi VARCHAR(20),
    DonGia DECIMAL(18, 2) NOT NULL,
    PRIMARY KEY (MaPhieuBD, MaThietBi),
    FOREIGN KEY (MaPhieuBD) REFERENCES PhieuBaoDuong(MaPhieuBD),
    FOREIGN KEY (MaThietBi) REFERENCES ThietBi(MaThietBi)
);
GO

------------------NHẬP LIỆU--------------------------------
INSERT INTO ChucVu (MaCV, TenCV) VALUES
('CV001', N'Chuyên viên'),
('CV002', N'Giáo viên'),
('CV003', N'Sinh viên'),
('CV004', N'Trưởng khoa'),
('CV005', N'Giám đốc trung tâm'),
('CV006', N'Phó trưởng khoa'),
('CV007', N'Hiệu trưởng'),
('CV008', N'Phó hiệu trưởng'),
('CV009', N'Cán bộ quản lý học sinh'),
('CV010', N'Cán bộ thư viện'),
('CV011', N'Nhân viên văn phòng'),
('CV012', N'Nhân viên IT');
GO
-- Insert dữ liệu vào bảng LoaiDungCu
INSERT INTO LoaiDungCu (MaLoaiDC, TenLoaiDC, MoTa) VALUES
('LDC001', N'Micropipet', N'Micropipet phòng thí nghiệm'),
('LDC002', N'Ống Nghiệm', N'Ống nghiệm đa dạng dùng trong thí nghiệm'),
('LDC003', N'Chai Thí Nghiệm', N'Chai thí nghiệm để lưu trữ hoá chất'),
('LDC004', N'Phễu Thí Nghiệm', N'Phễu lọc dùng trong các thí nghiệm'),
('LDC005', N'Dụng Cụ Y Tế', N'Dụng cụ y tế cho phòng thí nghiệm'),
('LDC006', N'Giấy Lọc Phòng Thí Nghiệm', N'Giấy lọc dùng trong các phân tích thí nghiệm'),
('LDC007', N'Đầu Lọc', N'Đầu lọc chuyên dụng cho thí nghiệm'),
('LDC008', N'Đầu Típ', N'Đầu típ cho các loại micropipet'),
('LDC009', N'Kính Bảo Hộ', N'Kính bảo hộ an toàn cho phòng thí nghiệm'),
('LDC010', N'Găng Tay Bảo Hộ', N'Găng tay bảo vệ dùng cho phòng thí nghiệm'),
('LDC011', N'Bảo Vệ Đầu', N'Thiết bị bảo vệ đầu cho nhân viên'),
('LDC012', N'Máy Đo Đa Chỉ Tiêu', N'Máy đo pH, EC, TDS, Nhiệt độ và nhiều chỉ tiêu khác'),
('LDC013', N'Máy Đo pH', N'Thiết bị đo pH cho các mẫu'),
('LDC014', N'Máy Đo Độ Dẫn Điện', N'Máy đo độ dẫn điện cho dung dịch'),
('LDC015', N'Máy Đo Nhiệt Độ', N'Thiết bị đo nhiệt độ chuyên dụng'),
('LDC016', N'Máy Đo Clo', N'Máy đo nồng độ Clo trong nước'),
('LDC017', N'Hóa Chất Tinh Khiết', N'Hóa chất tinh khiết dùng trong các phân tích'),
('LDC018', N'Môi Trường Vi Sinh', N'Môi trường nuôi cấy vi sinh'),
('LDC019', N'Hóa Chất Vi Sinh', N'Hóa chất hỗ trợ phân tích vi sinh');
GO
INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi) VALUES
('NCC001', N'Nhà Cung Cấp Hoàng Minh', N'123 Đường Hoàng Minh, Phường 1, Quận 1, TP. Hồ Chí Minh'),
('NCC002', N'Nhà Cung Cấp An Phú', N'456 Đường An Phú, Phường 2, Quận 2, TP. Hồ Chí Minh'),
('NCC003', N'Nhà Cung Cấp Vạn Lộc', N'789 Đường Vạn Lộc, Phường 3, Quận 3, TP. Hồ Chí Minh'),
('NCC004', N'Nhà Cung Cấp Ngọc Bích', N'321 Đường Ngọc Bích, Phường 4, Quận 4, TP. Hồ Chí Minh'),
('NCC005', N'Nhà Cung Cấp Phúc Thành', N'654 Đường Phúc Thành, Phường 5, Quận 5, TP. Hồ Chí Minh');
GO
INSERT INTO PhongThiNghiem (MaPhong, LoaiPhong, ChucNang) VALUES
('PTN001', N'Phong Hóa', N'Thí nghiệm hóa học cơ bản'),
('PTN002', N'Phong Sinh', N'Thí nghiệm sinh học tế bào'),
('PTN003', N'Phong Vật Lý', N'Thí nghiệm vật lý cơ bản'),
('PTN004', N'Phong Điện', N'Thí nghiệm điện và điện tử'),
('PTN005', N'Phong Cơ Khí', N'Thí nghiệm cơ khí và chế tạo'),
('PTN006', N'Phong Mô Phỏng', N'Thí nghiệm mô phỏng và thiết kế'),
('PTN007', N'Phong Kỹ Thuật', N'Thí nghiệm kỹ thuật tự động hóa'),
('PTN008', N'Phong Máy Tính', N'Thí nghiệm lập trình và mạng máy tính'),
('PTN009', N'Phong Địa Chất', N'Thí nghiệm khảo sát địa chất'),
('PTN010', N'Phong Nguyên Liệu', N'Thí nghiệm phân tích nguyên liệu thí nghiệm');
GO
INSERT INTO PhongThiNghiem (MaPhong, LoaiPhong, ChucNang) VALUES
('PTN011', N'Phong Khoa Học Môi Trường', N'Thí nghiệm phân tích môi trường và chất thải'),
('PTN012', N'Phong Hóa Sinh', N'Thí nghiệm hóa học và sinh học kết hợp'),
('PTN013', N'Phong Vi Sinh', N'Thí nghiệm nghiên cứu vi sinh vật'),
('PTN014', N'Phong Tự Động Hóa', N'Thí nghiệm thiết kế hệ thống tự động hóa'),
('PTN015', N'Phong Quang Học', N'Thí nghiệm nghiên cứu về ánh sáng và quang học'),
('PTN016', N'Phong Vật Liệu', N'Thí nghiệm vật liệu và chế tạo vật liệu mới'),
('PTN017', N'Phong Hệ Thống Thông Tin', N'Thí nghiệm nghiên cứu và phát triển phần mềm'),
('PTN018', N'Phong Công Nghệ Thực Phẩm', N'Thí nghiệm sản xuất và kiểm tra thực phẩm'),
('PTN019', N'Phong Xử Lý Dữ Liệu', N'Thí nghiệm xử lý và phân tích dữ liệu lớn');
GO

-- Bảng DungCu
INSERT INTO DungCu (MaDungCu, TenDungCu, MaLoaiDC, SoLuong, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC) VALUES 
('DC001', N'Micropipet - Nichipet F & V-Nichiryo', 'LDC001', 10, N'Mới', '2024-10-30', '2024-01-01', 'Nichiryo', '2025-01-01', N'Việt Nam', 'A001.jpg','NCC001'),
('DC002', N'Micropipet cơ đơn kênh TopPette, thể tích cố định - Dlab - 7010102018', 'LDC001', 10, N'Mới', '2024-10-30', '2024-01-01', 'Dlab', '2025-01-01', N'Việt Nam', 'A002.jpg','NCC001'),
('DC003', N'Micropipet cơ đơn kênh MicroPette Plus, thể tích cố định - Dlab - 7030302018', 'LDC001', 10, N'Mới', '2024-10-30', '2024-01-01', 'Dlab', '2025-01-01', N'Việt Nam', 'A003.jpg','NCC001'),
('DC004', N'Micropipet điện tử 8 kênh 30-399ul - DLAB - 70362022003', 'LDC001', 10, N'Mới', '2024-10-30', '2024-01-01', 'DLAB', '2025-01-01', N'Việt Nam', 'A004.jpg','NCC001'),
('DC005', N'Micropipet cơ đơn kênh TopPette, thể tích điều chỉnh - Dlab - 7010101001', 'LDC001', 10, N'Mới', '2024-10-30', '2024-01-01', 'Dlab', '2025-01-01', N'Việt Nam', 'A005.jpg','NCC001'),
('DC006', N'Micropipet cơ 8 kênh TopPette, thể tích điều chỉnh - Dlab - 7010103004', 'LDC001', 10, N'Mới', '2024-10-30', '2024-01-01', 'Dlab', '2025-01-01', N'Việt Nam', 'A006.jpg','NCC001');
GO

INSERT INTO DungCu (MaDungCu, TenDungCu, MaLoaiDC, SoLuong, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC) VALUES
('DC007', N'Ống nghiệm Polystyrene - Biologix', 'LDC002', 20, N'Mới', '2024-10-30', '2024-01-01', N'Biologix', '2025-01-01', N'Việt Nam', 'B001.jpg','NCC002'),
('DC008', N'Ống nghiệm nhựa 16x100mm, có nắp 12ml Kartell', 'LDC002', 20, N'Mới', '2024-10-30', '2024-01-01', N'Kartell', '2025-01-01', N'Việt Nam', 'B002.jpg','NCC002'),
('DC009', N'Ống mao quản minixapsR - 990001 - Hirschmann', 'LDC002', 20, N'Mới', '2024-10-30', '2024-01-01', N'Hirschmann', '2025-01-01', N'Việt Nam', 'B003.jpg','NCC002'),

('DC010', N'Chai nuôi cấy tế bào - Biologix', 'LDC003', 30, N'Mới', '2024-10-30', '2024-01-01', N'Biologix', '2025-01-01', N'Việt Nam', 'C001.jpg','NCC003'),
('DC011', N'Chai nhựa PP miệng rộng - Biologix - 04 - 008U', 'LDC003', 30, N'Mới', '2024-10-30', '2024-01-01', N'Biologix', '2025-01-01', N'Việt Nam', 'C002.jpg','NCC003'),
('DC012', N'Chai trung tính trắng nắp vặn, có vạch chia - Omark - OM.1105.01', 'LDC003', 30, N'Mới', '2024-10-30', '2024-01-01', N'Omark', '2025-01-01', N'Việt Nam', 'C003.jpg','NCC003'),
('DC013', N'Chai trung tính nâu nắp vặn, có vạch chia - Omark - OM.1106.01', 'LDC003', 30, N'Mới', '2024-10-30', '2024-01-01', N'Omark', '2025-01-01', N'Việt Nam', 'C004.jpg','NCC003');
GO

INSERT INTO DungCu (MaDungCu, TenDungCu, MaLoaiDC, SoLuong, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC) VALUES
('DC014', N'Phễu thủy tinh - Omark - OM.1500.01', 'LDC004', 40, N'Mới', '2024-10-30', '2024-10-01', N'Omark', '2025-10-01', N'Việt Nam', 'D001.jpg', 'NCC004'),
('DC015', N'Phễu thủy tinh - 21351 - Duran', 'LDC004', 40, N'Mới', '2024-10-30', '2024-10-01', N'Duran', '2025-10-01', N'Việt Nam', 'D002.jpg', 'NCC004'),
('DC016', N'Phễu vi sinh bộ lọc có đệm Ptfe và kẹp có thể sử dụng đĩa lọc - 25710 - Duran', 'LDC004', 40, N'Mới', '2024-10-30', '2024-10-01', N'Duran', '2025-10-01', N'Việt Nam', 'D003.jpg', 'NCC004'),
('DC017', N'Phễu thủy tinh - 041.01 - Isolab', 'LDC004', 40, N'Mới', '2024-10-30', '2024-10-01', N'Isolab', '2025-10-01', N'Việt Nam', 'D004.jpg', 'NCC004'),

('DC018', N'Hộp đựng bông cồn inox 7,5x6 nhỏ đúc - Hàng Việt Nam', 'LDC005', 5, N'Mới', '2024-10-30', '2024-10-01', N'Việt Nam', '2025-10-01', N'Việt Nam', 'E001.jpg', 'NCC005'),
('DC019', N'Lưỡi dao mổ 15 - Ribbel', 'LDC005', 5, N'Mới', '2024-10-30', '2024-10-01', N'Ribbel', '2025-10-01', N'Việt Nam', 'E002.jpg', 'NCC005'),
('DC020', N'Bơm tiêm dung 1 lần - Vinahankook', 'LDC005', 5, N'Mới', '2024-10-30', '2024-10-01', N'Vinahankook', '2025-10-01', N'Việt Nam', 'E003.jpg', 'NCC005'),
('DC021', N'Kéo 16cm, thẳng, 1 tù, 1 nhọn - 3-184-Pakistan', 'LDC005', 5, N'Mới', '2024-10-30', '2024-10-01', N'Pakistan', '2025-10-01', N'Việt Nam', 'E004.jpg', 'NCC005'),
('DC022', N'Pen - Pakistan', 'LDC005', 5, N'Mới', '2024-10-30', '2024-10-01', N'Pakistan', '2025-10-01', N'Việt Nam', 'E005.jpg', 'NCC005'),
('DC023', N'Bông y tế 1kg - Bảo Thạch', 'LDC005', 5, N'Mới', '2024-10-30', '2024-10-01', N'Bảo Thạch', '2025-10-01', N'Việt Nam', 'E006.jpg', 'NCC005'),
('DC024', N'Bông y tế không thấm nước - Bảo Thạch', 'LDC005', 5, N'Mới', '2024-10-30', '2024-10-01', N'Bảo Thạch', '2025-10-01', N'Việt Nam', 'E007.jpg', 'NCC005'),
('DC025', N'Nhíp y tế - Pakistan', 'LDC005', 5, N'Mới', '2024-10-30', '2024-10-01', N'Pakistan', '2025-10-01', N'Việt Nam', 'E008.jpg', 'NCC005');
GO
INSERT INTO DungCu (MaDungCu, TenDungCu, MaLoaiDC, SoLuong, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC) VALUES
('DC026', N'Giấy lọc định lượng 5B, 110mm - Advantec - 5B.110', 'LDC006', 6, N'Mới', '2024-10-30', '2024-10-01', N'Advantec', '2025-10-01', N'Việt Nam',  'F001.jpg', 'NCC001'),
('DC027', N'Giấy lọc tách chiết - 2S - Advantec', 'LDC006', 6, N'Mới', '2024-10-30', '2024-10-01', N'Advantec', '2025-10-01', N'Việt Nam',  'F002.jpg', 'NCC001'),
('DC028', N'Giấy lọc định tính - No1 - Advantec', 'LDC006', 6, N'Mới', '2024-10-30', '2024-10-01', N'Advantec', '2025-10-01', N'Việt Nam',  'F003.jpg', 'NCC001'),
('DC029', N'Bộ giữ lọc - Finetech', 'LDC007', 7, N'Mới', '2024-10-30', '2024-10-01', N'Finetech', '2025-10-01', N'Việt Nam',  'G001.jpg', 'NCC001'),
('DC030', N'Đầu lọc khí Midisart - Sartorius', 'LDC007', 7, N'Mới', '2024-10-30', '2024-10-01', N'Sartorius', '2025-10-01', N'Việt Nam',  'G002.jpg', 'NCC001'),
('DC031', N'Đầu lọc nylon - SFNY - Membrane Solutions', 'LDC007', 7, N'Mới', '2024-10-30', '2024-10-01', N'Membrane Solutions', '2025-10-01', N'Việt Nam',  'G003.jpg', 'NCC001'),
('DC032', N'Đầu típ có lọc, tiệt trùng, không Rnasse & Dnase, endotoxin', 'LDC008', 8, N'Mới', '2024-10-30', '2024-10-01', N'Biologix', '2025-10-01', N'Việt Nam',  'H001.jpg', 'NCC002'),
('DC033', N'Đầu típ có lọc, dòng S, tiệt trùng chống bám dính - 23-0011S-G - Biologix', 'LDC008', 8, N'Mới', '2024-10-30', '2024-10-01', N'Biologix', '2025-10-01', N'Việt Nam',  'H002.jpg', 'NCC002'),
('DC034', N'Hộp đựng tuýp âm sâu - Biologix', 'LDC008', 8, N'Mới', '2024-10-30', '2024-10-01', N'Biologix', '2025-10-01', N'Việt Nam',  'H003.jpg', 'NCC002');
GO

INSERT INTO DungCu (MaDungCu, TenDungCu, MaLoaiDC, SoLuong, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC) VALUES
('DC035', N'Mắt kính chống hóa chất 1623Af - 70071560992 - 3M', 'LDC009', 9, N'Mới', '2024-10-30', '2024-10-30', N'3M', '2025-10-30', N'Việt Nam',  'I001.jpg','NCC005'),
('DC036', N'Kính Bảo Hộ 3M 10434 - 00000-20 Asian Virtua Tròng chống Đọng Sương 20 cái/Thùng - Xi003855214 - 3M', 'LDC009', 9, N'Mới', '2024-10-30', '2024-10-30', N'3M', '2025-10-30', N'Việt Nam',  'I002.jpg','NCC005'),
('DC037', N'Kính chống đọng sương SF401AF', 'LDC009', 9, N'Mới', '2024-10-30', '2024-10-30', N'3M', '2025-10-30', N'Việt Nam', 'I003.jpg','NCC005'),
('DC038', N'Kính bảo hộ S', 'LDC009', 9, N'Mới', '2024-10-30', '2024-10-30', N'3M', '2025-10-30', N'Việt Nam',  'I004.jpg','NCC005'),
('DC039', N'Kính che mặt chống va đập 3M 82700-00000 Wp96, 10 cái/hộp - 70071522182 - 3M', 'LDC009', 9, N'Mới', '2024-10-30', '2024-10-30', N'3M', '2025-10-30', N'Việt Nam',  'I005.jpg','NCC005'),
('DC040', N'Kính bảo hộ 3M', 'LDC009', 9, N'Mới', '2024-10-30', '2024-10-30', N'3M', '2025-10-30', N'Việt Nam',  'I006.jpg','NCC005');
GO

INSERT INTO DungCu (MaDungCu, TenDungCu, MaLoaiDC, SoLuong, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC) VALUES
('DC041', N'Găng tay không bột nitril trắng - DC-55 - Vglove', 'LDC010', 10, N'Mới', '2024-10-30', '2024-10-30', N'Vglove', '2025-10-30', N'Việt Nam',  'J001.jpg','NCC001'),
('DC042', N'Găng tay cao su không bột phủ Polymer (Size M) - 220203 - Vglove', 'LDC010', 10, N'Mới', '2024-10-30', '2024-10-30', N'Vglove', '2025-10-30', N'Việt Nam',  'J002.jpg','NCC001'),
('DC043', N'Găng tay chống cắt', 'LDC010', 10, N'Mới', '2024-10-30', '2024-10-30', N'Thông số nhà cung cấp', '2025-10-30', N'Việt Nam',  'J003.jpg','NCC001'),
('DC044', N'Găng tay cao su thiên nhiên có bột - Top Glove', 'LDC010', 10, N'Mới', '2024-10-30', '2024-10-30', N'Top Glove', '2025-10-30', N'Việt Nam',  'J004.jpg','NCC001'),

('DC045', N'Mũ bảo hộ M-407 Dùng Với Bộ Cấp Khí Versaflo, 1 Cái/Thùng - 70071562063-3M', 'LDC011', 11, N'Mới', '2024-10-30', '2024-10-30', N'3M', '2025-10-30', N'Việt Nam',  'K001.jpg','NCC003'),
('DC046', N'Chụp tai chống ồn X3P3E - Xa007706923 - 3M', 'LDC011', 11, N'Mới', '2024-10-30', '2024-10-30', N'3M', '2025-10-30', N'Việt Nam',  'K002.jpg','NCC003'),
('DC047', N'Mũ trùm kiểm tra độ kín khít 10 cái/thùng - 70070121481-3M', 'LDC011', 11, N'Mới', '2024-10-30', '2024-10-30', N'3M', '2025-10-30', N'Việt Nam',  'K003.jpg','NCC003'),

('DC048', N'Máy đo pH / EC/TDS/Nhiệt độ (Thang cao) CAL check - HI9813-6-Hanna', 'LDC012', 12, N'Mới', '2024-10-30', '2024-10-30', N'Hanna', '2025-10-30', N'Việt Nam',  'M001.jpg','NCC004'),
('DC049', N'Máy đo pH/Ec/TDS/Nhiệt độ - chống thấm nuosc - HI9814-Hanna', 'LDC012', 12, N'Mới', '2024-10-30', '2024-10-30', N'Hanna', '2025-10-30', N'Việt Nam',  'M002.jpg','NCC004');
GO

INSERT INTO DungCu (MaDungCu, TenDungCu, MaLoaiDC, SoLuong, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC) VALUES
('DC050', N'Bút đo pH trong sữa - HI981034 - Hanna', 'LDC013', 13, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'M001.jpg', 'NCC003'),
('DC051', N'Bút đo pH/Nhiệt độ độ phân giải 0.1 pHepR4 HI98127 - Hanna', 'LDC013', 13, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'M002.jpg', 'NCC003'),
('DC052', N'Máy đo pH/ORP/Nhiệt độ để bàn PH2000-S(Trọn bộ) - Horiba', 'LDC013', 13, N'Mới', '2024-10-30', '2024-10-01', N'Horiba', '2025-10-01', N'Việt Nam', 'M004.jpg', 'NCC003'),

('DC053', N'Bút Đo Độ Dẫn/Nhiệt Độ – DiST4 – HI98304 – Hanna', 'LDC014', 14, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'N001.jpg', 'NCC002'),
('DC054', N'Bút Đo Độ Dẫn Trực Tiếp Trong Đất – HI98331 – Hanna', 'LDC014', 14, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'N002.jpg', 'NCC002'),
('DC055', N'Máy đo pH / ORP / EC / TDS / Độ Mặn / DO / Áp Suất / Nhiệt Độ – Chống Thấm Nước – HI98194 – Hanna', 'LDC014', 14, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'N003.jpg', 'NCC002'),
('DC056', N'Máy đo pH / EC / TDS / Nhiệt độ (Thang cao) CAL Check – HI9813-6 – Hanna', 'LDC014', 14, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'N004.jpg', 'NCC002'),

('DC057', N'Bút Đo Nhiệt Độ – Checktemp – HI98501 – Hanna', 'LDC015', 15, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'O001.jpg', 'NCC002'),
('DC058', N'Bút Đo Nhiệt Độ – Checktemp – HI98509 – Hanna', 'LDC015', 15, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'O002.jpg', 'NCC002'),
('DC059', N'Máy Đo Độ Ẩm, Nhiệt Độ – 445703 – Extech', 'LDC015', 15, N'Mới', '2024-10-30', '2024-10-01', N'Extech', '2025-10-01', N'Việt Nam', 'O003.jpg', 'NCC002'),
('DC060', N'Bút Đo Nhiệt Độ / Độ Ẩm – 44550 – Extech', 'LDC015', 15, N'Mới', '2024-10-30', '2024-10-01', N'Extech', '2025-10-01', N'Việt Nam', 'O004.jpg', 'NCC002'),

('DC061', N'Máy đo Checker Đo Clo Dư – HI701 – Hanna', 'LDC016', 16, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'P001.jpg', 'NCC001'),
('DC062', N'Máy quang đo Clo Dư Và Clo Tổng Loại Mới – HI97711 – Hanna', 'LDC016', 16, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'P002.jpg', 'NCC001'),
('DC063', N'Máy quang đo Clo Dư Loại Mới – HI97701 – Hanna', 'LDC016', 16, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'P003.jpg', 'NCC001'),
('DC064', N'Máy quang đo Clo Dư và Clo Tổng trong nước sạch – Hanna – HI97734', 'LDC016', 16, N'Mới', '2024-10-30', '2024-10-01', N'Hanna', '2025-10-01', N'Việt Nam', 'P004.jpg', 'NCC001'),

('DC065', N'Hóa chất Sulfuric acid 98% – 112080 – Merck', 'LDC017', 17, N'Mới', '2024-10-30', '2024-10-01', N'Merck', '2025-10-01', N'Việt Nam', 'Q001.jpg', 'NCC001'),
('DC066', N'Hóa chất Nitric acid 65% – Merck', 'LDC017', 17, N'Mới', '2024-10-30', '2024-10-01', N'Merck', '2025-10-01', N'Việt Nam', 'Q002.jpg', 'NCC001'),
('DC067', N'Hóa chất Natri chloride – 106404 – Merck', 'LDC017', 17, N'Mới', '2024-10-30', '2024-10-01', N'Merck', '2025-10-01', N'Việt Nam', 'Q003.jpg', 'NCC001'),
('DC068', N'Hóa chất Hydrochloric Acid Fuming 37% – 1L – 100317 – Merck', 'LDC017', 17, N'Mới', '2024-10-30', '2024-10-01', N'Merck', '2025-10-01', N'Việt Nam', 'Q004.jpg', 'NCC001'),
('DC069', N'Hóa chất Sodium Hydroxide – NaOH – 106498 – Merck', 'LDC017', 17, N'Mới', '2024-10-30', '2024-10-01', N'Merck', '2025-10-01', N'Việt Nam', 'Q005.jpg', 'NCC001'),

('DC070', N'Hóa chất Sabouraud Dextrose Agar – Himedia', 'LDC018', 18, N'Mới', '2024-10-30', '2024-10-01', N'Kobold', '2025-10-01', N'Việt Nam', 'R001.jpg', 'NCC001'),
('DC071', N'Môi trường vi sinh Plate count agar – M091 – Himedia', 'LDC018', 18, N'Mới', '2024-10-30', '2024-10-01', N'EHEIM', '2025-10-01', N'Việt Nam', 'R002.jpg', 'NCC001'),
('DC072', N'Môi Trường Soybean Casein Digest Medium (Tryptone Soya Broth) – M011 – 500G – Himedia', 'LDC018', 18, N'Mới', '2024-10-30', '2024-10-01', N'Seiwa', '2025-10-01', N'Việt Nam', 'R003.jpg', 'NCC001'),
('DC073', N'Hóa chất Tryptone Soya Agar – HIMEDIA', 'LDC018', 18, N'Mới', '2024-10-30', '2024-10-01', N'Bio-Merieux', '2025-10-01', N'Việt Nam', 'R004.jpg', 'NCC001'),
('DC074', N'Hóa chất Kovacs Indole reagent – 109293 – Merck', 'LDC019', 19, N'Mới', '2024-10-30', '2024-10-01', 'Merck', '2025-10-01', N'Việt Nam', 'S001.jpg','NCC005'),
('DC075', N'Hóa chất Egg Yolk tellurite 20% – 103785 – Merck', 'LDC019', 19, N'Mới', '2024-10-30', '2024-10-01', N'Merck', '2025-10-01', N'Việt Nam', 'S002.jpg',N'NCC005'),
('DC076', N'Hóa chất Egg Yolk Tellurite 50% – 103784 – Merck', 'LDC019', 19, N'Mới', '2024-10-30', '2024-10-01', N'Merck', '2025-10-01', N'Việt Nam', 'S003.jpg',N'NCC005');
GO
INSERT INTO LoaiThietBi (MaLoaiThietBi, TenLoaiThietBi, MoTa) VALUES
('LTB001', N'Cân Thí Nghiệm', N'Cân dùng để đo khối lượng với độ chính xác cao.'),
('LTB002', N'Nồi Hấp Tiệt Trùng', N'Thiết bị dùng để tiệt trùng các dụng cụ.'),
('LTB003', N'Kính Hiển Vi', N'Thiết bị dùng để quan sát các mẫu nhỏ.'),
('LTB004', N'Máy Khuấy Từ', N'Máy dùng để khuấy trộn chất lỏng.'),
('LTB005', N'Máy Ly Tâm', N'Thiết bị dùng để ly tâm các mẫu.'),
('LTB006', N'Máy Đồng Hóa', N'Máy dùng để đồng hóa các mẫu chất lỏng.'),
('LTB007', N'Máy Phá Mẫu', N'Máy dùng để phá mẫu hóa học.'),
('LTB008', N'Bơm Chân Không', N'Bơm dùng để tạo chân không trong các quá trình thí nghiệm.'),
('LTB009', N'Máy Cất Nước', N'Máy dùng để cất nước tinh khiết.'),
('LTB010', N'Tủ An Toàn Sinh Học', N'Tủ dùng để làm việc với các mẫu sinh học nguy hiểm.'),
('LTB011', N'Tủ Đựng Hóa Chất', N'Tủ dùng để lưu trữ hóa chất an toàn.'),
('LTB012', N'Tủ Lạnh Âm Sâu', N'Tủ lạnh dùng để lưu trữ mẫu trong điều kiện âm sâu.'),
('LTB013', N'Tủ Sấy', N'Thiết bị dùng để sấy khô mẫu.'),
('LTB014', N'Bếp Gia Nhiệt', N'Bếp dùng để gia nhiệt mẫu trong các thí nghiệm.');
GO
INSERT INTO ThietBi (MaThietBi, TenThietBi, MaLoaiThietBi, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC, MaPhong) VALUES
('TB001', N'Cân phân tích (220g/0.0001g) – PR224/E – Ohaus', 'LTB001', N'Mới', '2024-01-01', '2023-12-01', 'Ohaus', '2025-01-01', 'USA', 'A001.jpg', 'NCC001', 'PTN001'),
('TB002', N'Cân kỹ thuật điện tử (2200g x0.01g) – PR2202/E – Ohaus', 'LTB001', N'Mới', '2024-01-01', '2023-12-01', 'Ohaus', '2025-01-01', 'USA', 'A002.jpg', 'NCC001', 'PTN002'),
('TB003', N'Cân phân tích PX – Ohaus', 'LTB001', N'Mới', '2024-01-01', '2023-12-01', 'Ohaus', '2025-01-01', 'USA', 'A003.jpg', 'NCC001', 'PTN003'),
('TB004', N'Cân Kỹ Thuật SPX – Ohaus', 'LTB001', N'Mới', '2024-01-01', '2023-12-01', 'Ohaus', '2025-01-01', 'USA', 'A004.jpg', 'NCC001', 'PTN004'),
('TB005', N'Cân sấy ẩm – MB90 – Ohaus', 'LTB001', N'Mới', '2024-01-01', '2023-12-01', 'Ohaus', '2025-01-01', 'USA', 'A005.jpg', 'NCC001', 'PTN005'),
('TB006', N'Nồi hấp tiệt trùng 16 lít đứng SA-232V – Sturdy', 'LTB002', N'Mới', '2024-01-01', '2023-12-01', 'Sturdy', '2025-01-01', 'USA', 'B001.jpg', 'NCC002', 'PTN006'),
('TB007', N'Nồi hấp tiệt trùng 40 lít (Ngang) – SA-300H – Sturdy', 'LTB002', N'Mới', '2024-01-01', '2023-12-01', 'Sturdy', '2025-01-01', 'USA', 'B002.jpg', 'NCC002', 'PTN007'),
('TB008', N'Nồi hấp tiệt trùng 100 lít – SA-400 – Sturdy', 'LTB002', N'Mới', '2024-01-01', '2023-12-01', 'Sturdy', '2025-01-01', 'USA', 'B003.jpg', 'NCC002', 'PTN008'),
('TB009', N'Kính hiển vi soi nổi 20x STX – Optika', 'LTB003', N'Mới', '2024-01-01', '2023-12-01', 'Optika', '2025-01-01', 'USA', 'C001.jpg', 'NCC003', 'PTN009'),
('TB010', N'Kính hiển vi sinh học 2 mắt Motic – Swift – SW380B', 'LTB003', N'Mới', '2024-01-01', '2023-12-01', 'Motic', '2025-01-01', 'USA', 'C002.jpg', 'NCC003', 'PTN010'),
('TB011', N'Kính hiển vi sinh học 2 mắt – Optika – B-69', 'LTB003', N'Mới', '2024-01-01', '2023-12-01', 'Optika', '2025-01-01', 'USA', 'C003.jpg', 'NCC003', 'PTN011'),
('TB012', N'Kính hiển vi sinh học 3 mắt Motic – Swift – SW380T', 'LTB003', N'Mới', '2024-01-01', '2023-12-01', 'Motic', '2025-01-01', 'USA', 'C004.jpg', 'NCC003', 'PTN012');
GO
INSERT INTO ThietBi (MaThietBi, TenThietBi, MaLoaiThietBi, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC, MaPhong) VALUES
('TB013', N'Máy khuấy từ gia nhiệt MS7-H550-Pro – DLAB', 'LTB004', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'D001.jpg', 'NCC004', 'PTN001'),
('TB014', N'Máy khuấy từ gia nhiệt 1500rpm, 20L MS-H-ProT – Dlab – 8030201110', 'LTB004', N'Mới', '2024-01-01', '2023-12-01', 'Dlab', '2025-01-01', 'USA', 'D002.jpg', 'NCC004', 'PTN001'),
('TB015', N'Máy khuấy từ gia nhiệt MS7-H550-S – DLAB', 'LTB004', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'D003.jpg', 'NCC004', 'PTN001'),
('TB016', N'Máy khuấy từ gia nhiệt điện tử MS-H-ProA – 8160221110 – DLAB', 'LTB004', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'D004.jpg', 'NCC004', 'PTN001'),
('TB017', N'Máy khuấy từ bề mặt phẳng – 8030184000 – Dlab', 'LTB004', N'Mới', '2024-01-01', '2023-12-01', 'Dlab', '2025-01-01', 'USA', 'D005.jpg', 'NCC004', 'PTN001');
GO

INSERT INTO ThietBi (MaThietBi, TenThietBi, MaLoaiThietBi, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC, MaPhong) VALUES
('TB018', N'Máy ly tâm lọc vi mô D1012UA', 'LTB005', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'E001.jpg', 'NCC005', 'PTN002'),
('TB019', N'Máy ly tâm vi lượng tốc độ cao D3024 – 9033001121 – DLAB', 'LTB005', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'E002.jpg', 'NCC005', 'PTN002'),
('TB020', N'Máy ly tâm DM0408, rotor A12-10P – 9149002228 – DLAB', 'LTB005', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'E003.jpg', 'NCC005', 'PTN002'),
('TB021', N'Máy Ly Tâm 6000rpm DM0636 – DLAB – 9015001123', 'LTB005', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'E004.jpg', 'NCC005', 'PTN002'),
('TB022', N'Máy ly tâm máu DM1424 – DLAB – 9033001124', 'LTB005', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'E005.jpg', 'NCC005', 'PTN002');
GO

INSERT INTO ThietBi (MaThietBi, TenThietBi, MaLoaiThietBi, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC, MaPhong) VALUES
('TB023', N'Máy đồng hóa mẫu – D160', 'LTB006', N'Mới', '2024-01-01', '2023-12-01', 'Dlab', '2025-01-01', 'USA', 'F001.jpg', 'NCC005', 'PTN003'),
('TB024', N'Máy đồng hóa T25 digital ULTRA-TURRAX® – IKA', 'LTB006', N'Mới', '2024-01-01', '2023-12-01', 'IKA', '2025-01-01', 'Germany', 'F002.jpg', 'NCC005', 'PTN003'),
('TB025', N'Máy đồng hóa – T65 basic ULTRA-TURRAX® – 4023500', 'LTB006', N'Mới', '2024-01-01', '2023-12-01', 'IKA', '2025-01-01', 'Germany', 'F003.jpg', 'NCC005', 'PTN003'),
('TB026', N'Máy đồng hóa SHG-15A 27000rpm, 2.5L', 'LTB006', N'Mới', '2024-01-01', '2023-12-01', 'Scilab', '2025-01-01', 'China', 'F004.jpg', 'NCC005', 'PTN003');
GO

INSERT INTO ThietBi (MaThietBi, TenThietBi, MaLoaiThietBi, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC, MaPhong) VALUES
('TB027', N'Máy phá mẫu DRB200 dùng cho ống TNTplus', 'LTB007', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'G001.jpg', 'NCC006', 'PTN004'),
('TB028', N'Máy phá mẫu – DRB200', 'LTB007', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'G002.jpg', 'NCC006', 'PTN004'),
('TB029', N'Máy phá mẫu TE 62/2 B', 'LTB007', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'G003.jpg', 'NCC006', 'PTN004'),
('TB030', N'Máy phá mẫu TE 62/2A', 'LTB007', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'G004.jpg', 'NCC006', 'PTN004');
GO
INSERT INTO ThietBi (MaThietBi, TenThietBi, MaLoaiThietBi, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC, MaPhong) VALUES
('TB031', N'Máy bơm chân không C410', 'LTB008', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'H001.jpg', 'NCC003', 'PTN001'),
('TB032', N'Bộ điều khiển chân không 1000mbar VC100', 'LTB008', N'Mới', '2024-01-01', '2023-12-01', 'DLAB', '2025-01-01', 'USA', 'H002.jpg', 'NCC003', 'PTN001'),
('TB033', N'Bơm chân không, tuần hoàn nước 15L PuSHB-3S', 'LTB008', N'Mới', '2024-01-01', '2023-12-01', 'Scilab', '2025-01-01', 'China', 'H003.jpg', 'NCC003', 'PTN002'),
('TB034', N'Bơm hút không kháng hóa chất Chemker 410 – Rocker', 'LTB008', N'Mới', '2024-01-01', '2023-12-01', 'Scilab', '2025-01-01', 'China', 'H004.jpg', 'NCC003', 'PTN002'),

('TB035', N'Máy cất nước 2 lần 4 lít/h – Bhanu – DISTIL-ON4D', 'LTB009', N'Mới', '2024-01-01', '2023-12-01', 'Bhanu', '2025-01-01', 'India', 'I001.jpg', 'NCC002', 'PTN003'),
('TB036', N'Máy cất nước 1 lần 4 lít/h – Bhanu – BASIC/PH4', 'LTB009', N'Mới', '2024-01-01', '2023-12-01', 'Bhanu', '2025-01-01', 'India', 'I002.jpg', 'NCC002', 'PTN003'),
('TB037', N'Máy cất nước một lần Hàng có sẵn – W400 – STUART (BIBBY)', 'LTB009', N'Mới', '2024-01-01', '2023-12-01', 'STUART', '2025-01-01', 'UK', 'I003.jpg', 'NCC002', 'PTN004'),
('TB038', N'Máy cất nước 1 lần 4L/h WS-100-4 – Cole Parmer – 99293-00', 'LTB009', N'Mới', '2024-01-01', '2023-12-01', 'Cole Parmer', '2025-01-01', 'USA', 'I004.jpg', 'NCC002', 'PTN004'),

('TB039', N'Tủ an toàn sinh học cấp 3 Airstream® – ESCO', 'LTB010', N'Mới', '2024-01-01', '2023-12-01', 'ESCO', '2025-01-01', 'USA', 'J001.jpg', 'NCC001', 'PTN005'),
('TB040', N'Tủ an toàn sinh học cấp 2 loại A2 Airstream®, Thế hệ thứ 3 (Dòng sản phẩm D-Series) – ESCO', 'LTB010', N'Mới', '2024-01-01', '2023-12-01', 'ESCO', '2025-01-01', 'USA', 'J002.jpg', 'NCC001', 'PTN005'),
('TB041', N'Tủ an toàn sinh học cấp II Streamline® – ESCO', 'LTB010', N'Mới', '2024-01-01', '2023-12-01', 'ESCO', '2025-01-01', 'USA', 'J003.jpg', 'NCC001', 'PTN006'),
('TB042', N'Tủ An Tòan Sinh Học Class II – Hàng Việt Nam', 'LTB010', N'Mới', '2024-01-01', '2023-12-01', N'Việt Nam', '2025-01-01', 'Vietnam', 'J004.jpg', 'NCC001', 'PTN006'),

('TB043', N'Tủ mát chuyên dụng 14°C, 340L – PHC – MPR-S313-PK', 'LTB011', N'Mới', '2024-01-01', '2023-12-01', 'PHC', '2025-01-01', 'Japan', 'K001.jpg', 'NCC002', 'PTN007'),
('TB044', N'Tủ mát chuyên dụng 14°C, 158L – PHC – MPR-S163-PE', 'LTB011', N'Mới', '2024-01-01', '2023-12-01', 'PHC', '2025-01-01', 'Japan', 'K002.jpg', 'NCC002', 'PTN007'),
('TB045', N'Tủ Thao Tác PCR – Hàng Việt Nam', 'LTB011', N'Mới', '2024-01-01', '2023-12-01', N'Việt Nam', '2025-01-01', 'Vietnam', 'K003.jpg', 'NCC002', 'PTN008'),
('TB046', N'Tủ hóa chất an toàn chống cháy nổ (Loại 1 cửa) – Yakos65', 'LTB011', N'Mới', '2024-01-01', '2023-12-01', 'Yakos', '2025-01-01', 'Vietnam', 'K004.jpg', 'NCC002', 'PTN008'),
('TB047', N'Tủ kiểm tra môi trường 256L – CTC256 – Memmert', 'LTB011', N'Mới', '2024-01-01', '2023-12-01', N'Memmert', '2025-01-01', 'Germany', 'K005.jpg', 'NCC002', 'PTN009'),
('TB048', N'Tủ đựng hóa chất có lọc hấp thu – Việt Nam', 'LTB011', N'Mới', '2024-01-01', '2023-12-01', N'Việt Nam', '2025-01-01', 'Vietnam', 'K006.jpg', 'NCC002', 'PTN009'),

('TB049', N'Tủ lạnh âm sâu -80°C, 84L – PHC – MDF-C8V1-PE', 'LTB012', N'Mới', '2024-01-01', '2023-12-01', 'PHC', '2025-01-01', 'Japan', 'L001.jpg', 'NCC001', 'PTN010'),
('TB050', N'Tủ lạnh âm sâu -86°C, 845L – PHC – MDF-DU900V-PB', 'LTB012', N'Mới', '2024-01-01', '2023-12-01', 'PHC', '2025-01-01', 'Japan', 'L002.jpg', 'NCC001', 'PTN010');
GO
INSERT INTO ThietBi (MaThietBi, TenThietBi, MaLoaiThietBi, TinhTrang, NgayCapNhat, NgaySX, NhaSX, NgayBaoHanh, XuatXu, HinhAnh, MaNCC, MaPhong) VALUES
('TB051', N'Tủ sấy đối lưu cưỡng bức 250°C WiseVen SOF – Scilab – SL.SOF05050', 'LTB013', N'Mới', '2024-01-01', '2023-12-01', 'Scilab', '2025-01-01', 'China', 'M001.jpg', 'NCC003', 'PTN001'),
('TB052', N'Tủ sấy đối lưu tự nhiên 230°C WiseVen SON – Scilab – SL.SON05050', 'LTB013', N'Mới', '2024-01-01', '2023-12-01', 'Scilab', '2025-01-01', 'China', 'M002.jpg', 'NCC003', 'PTN001'),
('TB053', N'Tủ sấy tiệt trùng đối lưu cưỡng bức 32L – SF30 – Memmert', 'LTB013', N'Mới', '2024-01-01', '2023-12-01', 'Memmert', '2025-01-01', 'Germany', 'M003.jpg', 'NCC003', 'PTN002'),
('TB054', N'Tủ sấy 32l, 300°C, đối lưu tự nhiên, 1 màn hình – UN30 – Memmert', 'LTB013', N'Mới', '2024-01-01', '2023-12-01', 'Memmert', '2025-01-01', 'Germany', 'M004.jpg', 'NCC003', 'PTN003'),
('TB055', N'Tủ sấy tiệt trùng 74L – SN75 – Memmert', 'LTB013', N'Mới', '2024-01-01', '2023-12-01', 'Memmert', '2025-01-01', 'Germany', 'M005.jpg', 'NCC003', 'PTN003'),
('TB056', N'Bếp gia nhiệt bề mặt – CB300 – STUART (BIBBY)', 'LTB014', N'Mới', '2024-01-01', '2023-12-01', 'STUART', '2025-01-01', 'UK', 'N001.jpg', 'NCC004', 'PTN002'),
('TB057', N'Bếp gia nhiệt, bề mặt gia nhiệt bằng nhôm – BGN-NHOM – Joanlab', 'LTB014', N'Mới', '2024-01-01', '2023-12-01', 'Joanlab', '2025-01-01', 'China', 'N002.jpg', 'NCC004', 'PTN002'),
('TB058', N'Bếp Gia Nhiệt Hiện Số – HP180D – Misung', 'LTB014', N'Mới', '2024-01-01', '2023-12-01', 'Misung', '2025-01-01', 'Korea', 'N003.jpg', 'NCC004', 'PTN004'),
('TB059', N'Bếp gia nhiệt, mặt ceramic, 1000W – C-MAG HP 7 – 35818A0 – IKA', 'LTB014', N'Mới', '2024-01-01', '2023-12-01', 'IKA', '2025-01-01', 'Germany', 'N004.jpg', 'NCC004', 'PTN004');
GO
INSERT INTO NhomQuyen (MaNhom, TenNhom) VALUES
('NQ001', N'Quản lý dụng cụ'),
('NQ002', N'Nhân viên phòng thí nghiệm'),
('NQ003', N'Người dùng');
GO

INSERT INTO NhanVien (MaNV, TenNV, GioiTinh, NgaySinh, DiaChi, MaCV, SoDT, Email, MatKhau, MaNhom) VALUES
('NV001', N'Phan Thị Thanh Nga', N'Nữ', '1992-05-10', N'101 Đường Lý Thường Kiệt, Phường 1, Quận 1, TP. Hồ Chí Minh', 'CV001', '0123456780', 'ngaphan@gmail.com', '123456', 'NQ001'),
('NV002', N'Nguyễn Thị Hằng', N'Nữ', '1995-07-15', N'202 Đường Nguyễn Văn Bảo, Phường 2, Quận 2, TP. Hồ Chí Minh', 'CV002', '0123456781', 'hangnguyen@gmail.com', '123456', 'NQ002'),
('NV003', N'Trần Thị Ngọc Nhi', N'Nữ', '1994-08-20', N'303 Đường Lê Gia Hân, Phường 3, Quận 3, TP. Hồ Chí Minh', 'CV003', '0123456782', 'ngocnhi@gmail.com', '123456', 'NQ003'),
('NV004', N'Nguyễn Văn Danh', N'Nam', '1990-02-22', N'404 Đường Lý Tự Trọng, Phường 4, Quận 4, TP. Hồ Chí Minh', 'CV004', '0123456783', 'vandanh@gmail.com', '123456', 'NQ001'),
('NV005', N'Lê Thị Định', N'Nữ', '1988-12-12', N'505 Đường số 7, Phường 5, Quận 5, TP. Hồ Chí Minh', 'CV001', '0123456784', 'thidinh@gmail.com', '123456', 'NQ002'),
('NV006', N'Phạm Văn Hậu', N'Nam', '1993-11-11', N'606 Đường số 20, Phường 6, Quận 6, TP. Hồ Chí Minh', 'CV002', '0123456785', 'vanhau@gmail.com', '123456', 'NQ001'),
('NV007', N'Đặng Thị Giang', N'Nữ', '1991-09-09', N'707 Đường Lê Lợi, Phường 7, Quận 7, TP. Hồ Chí Minh', 'CV003', '0123456786', 'thigiang@gmail.com', '123456', 'NQ003'),
('NV008', N'Hoàng Văn Thụ', N'Nam', '1987-04-04', N'808 Đường Nguyễn Văn Nghi, Phường 8, Quận 8, TP. Hồ Chí Minh', 'CV004', '0123456787', 'vanthu@gmail.com', '123456', 'NQ002'),
('NV009', N'Trịnh Thị Nghi', N'Nữ', '1996-06-06', N'909 Đường Nguyễn Du, Phường 9, Quận 9, TP. Hồ Chí Minh', 'CV001', '0123456788', 'thinghi@gmail.com', '123456', 'NQ001'),
('NV010', N'Vũ Văn Bảo', N'Nam', '1985-01-01', N'1001 Đường 3 Tháng 4, Phường 10, Quận 10, TP. Hồ Chí Minh', 'CV002', '0123456789', 'vanbao@gmail.com', '123456', 'NQ002');
GO

INSERT INTO QLTaiKhoan (MaNV, TenNguoiDung, MatKhau, MaNhom) VALUES
('NV001', 'thanhnga', '123456', 'NQ001'),
('NV002', 'hangnt', '123456', 'NQ002'),
('NV003', 'ngocnhi', '123456', 'NQ003'),
('NV004', 'vandanh', '123456', 'NQ001'),
('NV005', 'thidinh', '123456', 'NQ002'),
('NV006', 'vanhau', '123456', 'NQ001'),
('NV007', 'thigiang', '123456', 'NQ003'),
('NV008', 'vanthu', '123456', 'NQ002'),
('NV009', 'thinghi', '123456', 'NQ001'),
('NV010', 'vanbao', '123456', 'NQ002');
GO
INSERT INTO CongTyThanhLy (MaCty, TenCty, DiaChi) VALUES
('CTY001', N'Công ty Thanh Lý Hòa Bình', N'303 Đường Hòa Bình, Phường 6, Quận 6, TP. Hồ Chí Minh'),
('CTY002', N'Công ty Thanh Lý Kim Cương', N'404 Đường Kim Cương, Phường 7, Quận 7, TP. Hồ Chí Minh'),
('CTY003', N'Công ty Thanh Lý Bảo Ngọc', N'505 Đường Bảo Ngọc, Phường 8, Quận 8, TP. Hồ Chí Minh'),
('CTY004', N'Công ty Thanh Lý Phú Quốc', N'606 Đường Phú Quốc, Phường 9, Quận 9, TP. Hồ Chí Minh'),
('CTY005', N'Công ty Thanh Lý Đại Dương', N'707 Đường Đại Dương, Phường 10, Quận 10, TP. Hồ Chí Minh'),
('CTY006', N'Công ty Thanh Lý Ngọc Minh', N'808 Đường Ngọc Minh, Phường 11, Quận 11, TP. Hồ Chí Minh'),
('CTY007', N'Công ty Thanh Lý Vạn Phúc', N'909 Đường Vạn Phúc, Phường 12, Quận 12, TP. Hồ Chí Minh'),
('CTY008', N'Công ty Thanh Lý Bình An', N'1010 Đường Bình An, Phường 13, Quận 13, TP. Hồ Chí Minh'),
('CTY009', N'Công ty Thanh Lý Sáng Tạo', N'1111 Đường Sáng Tạo, Phường 14, Quận 14, TP. Hồ Chí Minh'),
('CTY010', N'Công ty Thanh Lý Thịnh Vượng', N'1212 Đường Thịnh Vượng, Phường 15, Quận 15, TP. Hồ Chí Minh');
GO

INSERT INTO ViTriDungCu(MaDungCu, MaPhong, SoLuong, NgayCapNhat) Values('DC001', 'PTN001', 1, '2024-12-13');
-------------------------



------------------------TRIGGER
--phiếu đề xuất
CREATE TRIGGER trg_UpdatePhieuDeXuat
ON DuyetPhieu
AFTER INSERT
AS
BEGIN
    DECLARE @MaPhieu VARCHAR(20);
    DECLARE @TrangThai NVARCHAR(100);
    DECLARE @NgayDuyet DATE;

    -- Get values from the inserted row
    SELECT @MaPhieu = MaPhieu, @TrangThai = TrangThai, @NgayDuyet = NgayDuyet
    FROM inserted;

    -- Check the status of the approval (TrangThai) and update the PhieuDeXuat table
    IF @TrangThai = N'Phê Duyệt'
    BEGIN
        UPDATE PhieuDeXuat
        SET TrangThai = N'Đã phê duyệt', NgayHoanTat = @NgayDuyet
        WHERE MaPhieu = @MaPhieu;
    END
    ELSE IF @TrangThai = N'Từ Chối'
    BEGIN
        UPDATE PhieuDeXuat
        SET TrangThai = N'Không được phê duyệt', NgayHoanTat = @NgayDuyet
        WHERE MaPhieu = @MaPhieu;
    END
END;
GO


-------------
CREATE TRIGGER trg_UpdatePhieuThanhLyOnDuyet
ON DuyetPhieuThanhLy
AFTER INSERT
AS
BEGIN
    -- Declare variables to store the values from the inserted record
    DECLARE @MaPhieuTL VARCHAR(20);
    DECLARE @TrangThai NVARCHAR(50);

    -- Get the values from the inserted record
    SELECT @MaPhieuTL = MaPhieuTL, @TrangThai = TrangThai
    FROM inserted;

    -- Check if TrangThai is 'Phê Duyệt' or 'Từ Chối' and update PhieuThanhLy table
    IF @TrangThai = N'Phê Duyệt'
    BEGIN
        UPDATE PhieuThanhLy
        SET TrangThai = N'Đã duyệt'
        WHERE MaPhieuTL = @MaPhieuTL;
    END
    ELSE IF @TrangThai = N'Từ chối'
    BEGIN
        UPDATE PhieuThanhLy
        SET TrangThai = N'Không được phê duyệt'
        WHERE MaPhieuTL = @MaPhieuTL;
    END
END;
GO
--------------------- trigger update tổng tiền nhập ----------------
CREATE TRIGGER trg_UpdateTongTien_PhieuNhap_TB
ON ChiTietNhapTB
AFTER INSERT
AS
BEGIN
    DECLARE @MaPhieuNhap VARCHAR(20), @TongTien DECIMAL(18, 2);

    -- Lấy mã phiếu nhập từ bản ghi vừa thêm
    SELECT @MaPhieuNhap = MaPhieuNhap
    FROM INSERTED;

    -- Tính tổng tiền cho phiếu nhập (tổng tiền nhập của tất cả thiết bị)
    SELECT @TongTien = SUM(GiaNhap)
    FROM ChiTietNhapTB
    WHERE MaPhieuNhap = @MaPhieuNhap;

    -- Cập nhật tổng tiền vào bảng Phiếu nhập
    UPDATE PhieuNhap
    SET TongTien = @TongTien
    WHERE MaPhieuNhap = @MaPhieuNhap;
END;
GO


CREATE TRIGGER trg_UpdateTongTien_PhieuNhap_DC
ON ChiTietNhapDC
AFTER INSERT
AS
BEGIN
    DECLARE @MaPhieuNhap VARCHAR(20), @TongTien DECIMAL(18, 2);

    -- Lấy mã phiếu nhập từ bản ghi vừa thêm
    SELECT @MaPhieuNhap = MaPhieuNhap
    FROM INSERTED;

    -- Tính tổng tiền cho phiếu nhập (tổng tiền nhập của tất cả dụng cụ)
    SELECT @TongTien = SUM(GiaNhap * SoLuongNhap)
    FROM ChiTietNhapDC
    WHERE MaPhieuNhap = @MaPhieuNhap;

    -- Cập nhật tổng tiền vào bảng Phiếu nhập
    UPDATE PhieuNhap
    SET TongTien = @TongTien
    WHERE MaPhieuNhap = @MaPhieuNhap;
END;
GO

CREATE TRIGGER trg_UpdateDangKySuDung
ON DuyetPhieuDK
AFTER INSERT
AS
BEGIN
    -- Khai báo biến để lấy giá trị từ bản ghi vừa thêm
    DECLARE @MaDangKy VARCHAR(20);
    DECLARE @TrangThai NVARCHAR(100);
    DECLARE @NgayDuyet DATE;

    -- Lấy dữ liệu từ bản ghi được thêm vào bảng DuyetDangKy
    SELECT @MaDangKy = MaPhieuDK, @TrangThai = TrangThai, @NgayDuyet = NgayDuyet
    FROM inserted;

    -- Kiểm tra trạng thái duyệt và cập nhật bảng DangKySuDung tương ứng
    IF @TrangThai = N'Phê Duyệt'
    BEGIN
        UPDATE PhieuDangKi
        SET TrangThai = N'Đã phê duyệt', NgayHoanTat = @NgayDuyet
        WHERE MaPhieuDK = @MaDangKy;
    END
    ELSE IF @TrangThai = N'Từ Chối'
    BEGIN
        UPDATE PhieuDangKi
        SET TrangThai = N'Không được phê duyệt', NgayHoanTat = @NgayDuyet
        WHERE MaPhieuDK = @MaDangKy;
    END
END;
GO
CREATE TRIGGER trg_UpdateLichDungCu
ON DuyetPhieuDK
AFTER UPDATE
AS
BEGIN
    -- Chỉ xử lý khi trạng thái được cập nhật thành "Đã phê duyệt"
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        WHERE i.TrangThai = N'Đã phê duyệt'
    )
    BEGIN
        INSERT INTO LichDungCu (MaDungCu, MaPhong, NgaySuDung, NgayKetThuc, SoLuong)
        SELECT 
            ddc.MaDungCu,
            pd.MaPhong,
            ddc.NgaySuDung,
            ddc.NgayKetThuc,
            ddc.SoLuong
        FROM DangKiDungCu ddc
        INNER JOIN inserted i ON ddc.MaPhieuDK = i.MaPhieuDK
        INNER JOIN PhieuDangKi pd ON pd.MaPhieuDK = ddc.MaPhieuDK
        WHERE i.TrangThai = N'Đã phê duyệt';
    END
END;
GO
CREATE TRIGGER trg_UpdateLichThietBi
ON DuyetPhieuDK
AFTER UPDATE
AS
BEGIN
    -- Chỉ xử lý khi trạng thái được cập nhật thành "Đã phê duyệt"
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        WHERE i.TrangThai = N'Đã phê duyệt'
    )
    BEGIN
        INSERT INTO LichThietBi (MaThietBi, MaPhong, NgaySuDung, NgayKetThuc)
        SELECT 
            dtb.MaThietBi,
            pd.MaPhong,
            dtb.NgaySuDung,
            dtb.NgayKetThuc
        FROM DangKiThietBi dtb
        INNER JOIN inserted i ON dtb.MaPhieuDK = i.MaPhieuDK
        INNER JOIN PhieuDangKi pd ON pd.MaPhieuDK = dtb.MaPhieuDK
        WHERE i.TrangThai = N'Đã phê duyệt';
    END
END;
GO

CREATE PROCEDURE UpdateLichWhenRegisterSuccess
    @MaPhong VARCHAR(20),
    @MaThietBi VARCHAR(20) = NULL, -- Có thể NULL nếu không đăng ký thiết bị
    @MaDungCu VARCHAR(20) = NULL, -- Có thể NULL nếu không đăng ký dụng cụ
    @NgaySuDung DATETIME,
    @NgayKetThuc DATETIME,
    @SoLuong INT = NULL -- Số lượng dụng cụ, NULL nếu không đăng ký dụng cụ
AS
BEGIN
    BEGIN TRY
        -- Bắt đầu giao dịch
        BEGIN TRANSACTION;

        -- Nếu có thiết bị, thêm vào LichThietBi
        IF @MaThietBi IS NOT NULL
        BEGIN
            INSERT INTO LichThietBi (MaThietBi, MaPhong, NgaySuDung, NgayKetThuc)
            VALUES (@MaThietBi, @MaPhong, @NgaySuDung, @NgayKetThuc);
        END

        -- Nếu có dụng cụ, thêm vào LichDungCu
        IF @MaDungCu IS NOT NULL
        BEGIN
            INSERT INTO LichDungCu (MaDungCu, MaPhong, NgaySuDung, NgayKetThuc, SoLuong)
            VALUES (@MaDungCu, @MaPhong, @NgaySuDung, @NgayKetThuc, @SoLuong);
        END

        -- Commit giao dịch nếu thành công
        COMMIT TRANSACTION;

        PRINT 'Cập nhật lịch thành công!';
    END TRY
    BEGIN CATCH
        -- Rollback giao dịch nếu xảy ra lỗi
        ROLLBACK TRANSACTION;

        PRINT 'Có lỗi xảy ra khi cập nhật lịch!';
        THROW; -- Ném lại lỗi để hệ thống xử lý
    END CATCH
END
GO

CREATE TRIGGER trg_UpdateAfterApproval
ON DuyetPhieuLuanChuyen
AFTER UPDATE
AS
BEGIN
    -- Check if the status has been updated to "Đã duyệt"
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON i.MaPhieuLC = d.MaPhieuLC
        WHERE i.TrangThai = N'Đã phê duyệt' AND d.TrangThai <> N'Đã phê duyệt'
    )
    BEGIN
        -- Update DungCu (dụng cụ) location in ViTriDungCu table
        UPDATE vdc
        SET vdc.MaPhong = ctdc.MaPhongDen, 
            vdc.SoLuong = vdc.SoLuong + ctdc.SoLuong,
            vdc.NgayCapNhat = GETDATE()
        FROM ChiTietLuanChuyenDC ctdc
        INNER JOIN ViTriDungCu vdc ON ctdc.MaDungCu = vdc.MaDungCu
        INNER JOIN inserted i ON ctdc.MaPhieuLC = i.MaPhieuLC
        WHERE i.TrangThai = N'Đã phê duyệt';

        -- Update ThietBi (thiết bị) location in ThietBi table
        UPDATE tb
        SET tb.MaPhong = cttb.MaPhongDen,
            tb.NgayCapNhat = GETDATE()
        FROM ChiTietLuanChuyenTB cttb
        INNER JOIN ThietBi tb ON cttb.MaThietBi = tb.MaThietBi
        INNER JOIN inserted i ON cttb.MaPhieuLC = i.MaPhieuLC
        WHERE i.TrangThai = N'Đã phê duyệt';
    END
END;
GO

