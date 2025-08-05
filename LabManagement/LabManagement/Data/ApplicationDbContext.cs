using LabManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LabManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Các DbSet hiện tại
        public DbSet<ChucVu> ChucVu { get; set; }
        public DbSet<NhomQuyen> NhomQuyen { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<LoaiDungCu> LoaiDungCu { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<DungCu> DungCu { get; set; }
        public DbSet<LoaiThietBi> LoaiThietBi { get; set; }
        public DbSet<ThietBi> ThietBi { get; set; }
        public DbSet<DuyetPhieuDK> DuyetPhieuDK { get; set; }
        public DbSet<ChiTietNhapTB> ChiTietNhapTB { get; set; }
        public DbSet<ChiTietNhapDC> ChiTietNhapDC { get; set; }

        // Chức năng đề xuất thiết bị/ dụng cụ mới
        public DbSet<PhieuDeXuat> PhieuDeXuat { get; set; }
        public DbSet<DuyetPhieu> DuyetPhieu { get; set; }
        public DbSet<ChiTietDeXuatDungCu> ChiTietDeXuatDungCu { get; set; }
        public DbSet<ChiTietDeXuatThietBi> ChiTietDeXuatThietBi { get; set; }
        //Chức năng nhập thiết bị/ dụng cụ mới 
        public DbSet<PhieuNhap> PhieuNhap { get; set; }
        //public DbSet<ChiTietNhapDC> chiTietNhapDCs { get; set; }
        public DbSet<PhieuBaoDuong> PhieuBaoDuong { get; set; }
        public DbSet<ChiTietBaoDuongTB> ChiTietBaoDuongTB { get; set; }
        public DbSet<PhieuDangKi> PhieuDangKi { get; set; }

        public DbSet<DangKiDungCu> DangKiDungCu { get; set; }
        public DbSet<DangKiThietBi> DangKiThietBi { get; set; }
        public DbSet<CongTyThanhLy> CongTyThanhLy { get; set; }
        public DbSet<PhieuThanhLy> PhieuThanhLy { get; set; }
        public DbSet<ChiTietPhieuThanhLy> ChiTietPhieuThanhLy { get; set; }
        public DbSet<DuyetPhieuThanhLy> DuyetPhieuThanhLy{ get; set; }
        public DbSet<LichSuPhieuThanhLy> LichSuPhieuThanhLy { get; set; }

        public DbSet<ThoiGianSuDung> ThoiGianSuDung { get; set; }

        public DbSet<PhongThiNghiem> PhongThiNghiem { get; set; }
        public DbSet<PhieuPhanBoTB> PhieuPhanBoTB { get; set; }
        public DbSet<PhanBoDC> PhanBoDC { get; set; }
        // Luân chuyển thuyết bị và dụng cụ
        public DbSet<PhieuDeXuatLuanChuyen> PhieuDeXuatLuanChuyen { get; set; }
        public DbSet<ChiTietLuanChuyenDC> ChiTietLuanChuyenDC { get; set; }
        public DbSet<ChiTietLuanChuyenTB> ChiTietLuanChuyenTB { get; set; }
        public DbSet<LichSuPhieuLuanChuyen> LichSuPhieuLuanChuyen { get; set; }
        public DbSet<LichSuPhieuDeXuat> LichSuPhieuDeXuat { get; set; }
        public DbSet<DuyetPhieuLuanChuyen> DuyetPhieuLuanChuyen { get; set; }

        public DbSet<LichThietBi> LichThietBi { get; set; }
        public DbSet<LichDungCu> LichDungCu { get; set; }
        public DbSet<ViTriDungCu> ViTriDungCu { get; set; }

        public DbSet<QuanLyGioTB> QuanLyGioTB { get; set; }
        public DbSet<QuanLyGioDC> QuanLyGioDC { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình cho các bảng hiện tại
            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.HasKey(e => e.MaCV);
                entity.Property(e => e.TenCV).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<NhomQuyen>(entity =>
            {
                entity.HasKey(e => e.MaNhom);
                entity.Property(e => e.TenNhom).IsRequired().HasMaxLength(50);
            });
            modelBuilder.Entity<CongTyThanhLy>(entity =>
            {
                entity.HasKey(e => e.MaCty);
                entity.Property(e => e.MaCty).IsRequired().HasMaxLength(20);
                entity.Property(e => e.TenCty).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DiaChi).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNV);

                entity.HasOne(e => e.ChucVu)
                      .WithMany()
                      .HasForeignKey(e => e.MaCV)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.NhomQuyen)
                      .WithMany()
                      .HasForeignKey(e => e.MaNhom)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.isDeleted)
                     .HasDefaultValue(false)
                     .IsRequired();
                entity.Property(e => e.TenNV).IsRequired().HasMaxLength(100);
                entity.Property(e => e.GioiTinh).HasMaxLength(10);
                entity.Property(e => e.DiaChi).HasMaxLength(200);
                entity.Property(e => e.SoDT).HasMaxLength(15);
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.MatKhau).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<LoaiDungCu>(entity =>
            {
                entity.HasKey(e => e.MaLoaiDC);
                entity.Property(e => e.TenLoaiDC)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.MoTa)
                      .HasMaxLength(255);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNCC);
                entity.Property(e => e.TenNCC)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.DiaChi)
                      .HasMaxLength(255);
            });

            modelBuilder.Entity<DungCu>(entity =>
            {
                entity.HasKey(e => e.MaDungCu);

                entity.HasOne(e => e.LoaiDungCu)
                      .WithMany()
                      .HasForeignKey(e => e.MaLoaiDC)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.NhaCungCap)
                      .WithMany()
                      .HasForeignKey(e => e.MaNCC)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.TenDungCu)
                     .IsRequired()
                     .HasMaxLength(100);
                entity.Property(e => e.SoLuong)
                      .IsRequired();
                entity.Property(e => e.TinhTrang)
                      .HasMaxLength(50);
                entity.Property(e => e.NgayCapNhat);
                entity.Property(e => e.NgaySX);
                entity.Property(e => e.NhaSX)
                      .HasMaxLength(100);
                entity.Property(e => e.NgayBaoHanh);
                entity.Property(e => e.XuatXu)
                      .HasMaxLength(100);
                entity.Property(e => e.HinhAnh)
                      .HasMaxLength(255);
                entity.Property(e => e.IsDeleted)
                     .HasDefaultValue(false) 
                     .IsRequired();
            });

            modelBuilder.Entity<LoaiThietBi>(entity =>
            {
                entity.HasKey(e => e.MaLoaiThietBi);
                entity.Property(e => e.TenLoaiThietBi)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.MoTa)
                      .HasMaxLength(255);
            });

            modelBuilder.Entity<ThietBi>(entity =>
            {
                entity.HasKey(e => e.MaThietBi);
                entity.Property(e => e.TenThietBi)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.TinhTrang)
                      .HasMaxLength(50);
                entity.Property(e => e.NhaSX)
                      .HasMaxLength(100);
                entity.Property(e => e.XuatXu)
                      .HasMaxLength(100);
                entity.Property(e => e.HinhAnh)
                      .HasMaxLength(255);
                entity.Property(e => e.isDeleted)
                     .HasDefaultValue(false)
                     .IsRequired();
                entity.HasOne(e => e.LoaiThietBi)
                      .WithMany()
                      .HasForeignKey(e => e.MaLoaiThietBi)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.NhaCungCap)
                      .WithMany()
                      .HasForeignKey(e => e.MaNCC)
                      .OnDelete(DeleteBehavior.Restrict);
                
            });

            modelBuilder.Entity<PhieuDeXuat>(entity =>
            {
                entity.HasKey(e => e.MaPhieu);
                entity.Property(e => e.MaPhieu).HasMaxLength(20).IsRequired();
                entity.Property(e => e.LyDoDeXuat).HasMaxLength(100).IsRequired();
                entity.Property(e => e.MaNV).HasMaxLength(20).IsRequired();
                entity.Property(e => e.GhiChu).HasMaxLength(255);
                entity.Property(e => e.TrangThai).HasMaxLength(50);
                entity.Property(e => e.NgayTao).IsRequired(false);
                entity.Property(e => e.NgayHoanTat).IsRequired(false);

                entity.HasOne(e => e.NhanVien)
                      .WithMany() 
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ChiTietDeXuatDungCu>(entity =>
            {
                entity.HasKey(e => e.MaCTDeXuatDC);
                entity.Property(e => e.MaCTDeXuatDC).IsRequired();
                entity.Property(e => e.TenDungCu).HasMaxLength(100).IsRequired();
                entity.Property(e => e.MoTa).HasMaxLength(100).IsRequired();
                entity.Property(e => e.SoLuongDeXuat).IsRequired();
                entity.HasOne(e => e.PhieuDeXuat)
                      .WithMany() 
                      .HasForeignKey(e => e.MaPhieu)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.LoaiDungCu)
                      .WithMany()  
                      .HasForeignKey(e => e.MaLoaiDC)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<ChiTietDeXuatThietBi>(entity =>
            {
                entity.HasKey(e => e.MaCTDeXuatTB);
                entity.Property(e => e.MaCTDeXuatTB).IsRequired();
                entity.Property(e => e.TenThietBi).HasMaxLength(100).IsRequired();
                entity.Property(e => e.MoTa).HasMaxLength(100).IsRequired();
                entity.Property(e => e.SoLuongDeXuat).IsRequired();
                entity.HasOne(e => e.PhieuDeXuat)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieu)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.LoaiThietBi)
                      .WithMany()
                      .HasForeignKey(e => e.MaLoaiThietBi)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình cho DuyetPhieuDeXuat
            modelBuilder.Entity<DuyetPhieu>(entity =>
            {
                entity.HasKey(e => e.MaPhieu);
                entity.Property(e => e.MaPhieu).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaNV).HasMaxLength(20).IsRequired();
                entity.Property(e => e.NgayDuyet).IsRequired(false);
                entity.Property(e => e.TrangThai).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LyDoTuChoi).HasMaxLength(255).IsRequired();

                entity.HasOne(e => e.PhieuDeXuat)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieu)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            // Cấu hình cho bảng LichSuPhieuDeXuat
            modelBuilder.Entity<LichSuPhieuDeXuat>(entity =>
            {
                entity.HasKey(e => e.MaLichSu);

                entity.Property(e => e.MaPhieu).HasMaxLength(20).IsRequired();
                entity.Property(e => e.TrangThaiTruoc).HasMaxLength(50);
                entity.Property(e => e.TrangThaiSau).HasMaxLength(50).IsRequired();
                entity.Property(e => e.NgayThayDoi).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.MaNV).HasMaxLength(20);

                entity.HasOne(e => e.PhieuDeXuat)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieu)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<PhieuNhap>(entity => 
            {
            
                    entity.HasKey(e => e.MaPhieuNhap);
                    entity.Property(e => e.MaPhieuNhap).HasMaxLength(20).IsRequired();
                    entity.Property(e => e.MaNV).HasMaxLength(20).IsRequired();
                    entity.Property(e => e.NgayNhap).IsRequired(false);
                    entity.Property(e => e.MaPhieu).HasMaxLength(20).IsRequired(false);
                    entity.Property(e => e.TongTien).HasColumnType("decimal(18,2)").IsRequired();

                    entity.HasOne(e => e.NhanVien)
                          .WithMany() 
                          .HasForeignKey(e => e.MaNV)
                          .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.PhieuDeXuat)
                         .WithMany()
                         .HasForeignKey(e => e.MaPhieu)
                         .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ChiTietNhapTB>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuNhap, e.MaThietBi }); 

                entity.Property(e => e.MaPhieuNhap)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.MaThietBi)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.GiaNhap)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.HasOne(e => e.PhieuNhap) 
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuNhap)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ThietBi) 
                      .WithMany()
                      .HasForeignKey(e => e.MaThietBi)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ChiTietNhapDC>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuNhap, e.MaDungCu });

                entity.Property(e => e.MaPhieuNhap)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.MaDungCu)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.GiaNhap)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.SoLuongNhap)
                      .IsRequired();

                entity.HasOne(e => e.PhieuNhap) 
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuNhap)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.DungCu) 
                      .WithMany()
                      .HasForeignKey(e => e.MaDungCu)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PhieuBaoDuong>(entity =>
            {
                entity.HasKey(e => e.MaPhieuBD);

                entity.Property(e => e.MaPhieuBD)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.MaNV)
                      .HasMaxLength(20)
                      .IsRequired(false);
                entity.Property(e => e.MaNCC)
                     .HasMaxLength(20)
                     .IsRequired(false);
                entity.Property(e => e.NoiDung)
                      .HasMaxLength(100)
                      .IsRequired(false);

                entity.Property(e => e.NgayBaoDuong)
                      .IsRequired(false);

                entity.Property(e => e.TongTien)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.HasOne(e => e.NhanVien)
                      .WithMany() 
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.NhaCungCap)
                      .WithMany()
                      .HasForeignKey(e => e.MaNCC)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ChiTietBaoDuongTB>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuBD, e.MaThietBi }); 

                entity.Property(e => e.MaPhieuBD)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.MaThietBi)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.DonGia)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
                entity.HasOne(e => e.PhieuBaoDuong)
                      .WithMany()  
                      .HasForeignKey(e => e.MaPhieuBD)
                      .OnDelete(DeleteBehavior.Cascade);  

                entity.HasOne(e => e.ThietBi)
                      .WithMany() 
                      .HasForeignKey(e => e.MaThietBi)
                      .OnDelete(DeleteBehavior.Cascade); 
            });
                        modelBuilder.Entity<PhieuDangKi>(entity => { 
                entity.HasKey(e => e.MaPhieuDK);
                entity.Property(e => e.MaPhieuDK).HasMaxLength(20).IsRequired();
                entity.Property(e => e.LyDoDK).HasMaxLength(100).IsRequired();
                entity.Property(e => e.MaNV).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaPhong).HasMaxLength(20).IsRequired();
                entity.Property(e => e.GhiChu).HasMaxLength(255);
                entity.Property(e => e.TrangThai).HasMaxLength(50);
                entity.Property(e => e.NgayLap).IsRequired(false);
                entity.Property(e => e.NgayHoanTat).IsRequired(false);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.PhongThiNghiem)
                            .WithMany()
                            .HasForeignKey(e => e.MaPhong)
                            .OnDelete(DeleteBehavior.Restrict);

            });
            // Cấu hình cho DuyetPhieuDK
            modelBuilder.Entity<DuyetPhieuDK>(entity =>
            {
                entity.HasKey(e => e.MaPhieuDK);
                entity.Property(e => e.MaPhieuDK).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaNV).HasMaxLength(20).IsRequired();
                entity.Property(e => e.NgayDuyet).IsRequired(false);
                entity.Property(e => e.TrangThai).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LyDoTuChoi).HasMaxLength(255).IsRequired();

                entity.HasOne(e => e.PhieuDangKi)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuDK)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            // Cấu hình cho DKDungCu
            modelBuilder.Entity<DangKiDungCu>(entity =>
            {
                entity.HasKey(e => new{ e.MaPhieuDK, e.MaDungCu });
                entity.Property(e => e.SoLuong).IsRequired();
                entity.Property(e => e.NgayDangKi).IsRequired();
                entity.Property(e => e.NgaySuDung).IsRequired();
                entity.Property(e => e.NgayKetThuc).IsRequired();
                entity.Property(e => e.TrangThaiSuDung).HasMaxLength(50);
                entity.Property(e => e.TinhTrangSuDung).HasMaxLength(255).IsRequired();
                entity.Property(e => e.NgayBatDauThucTe).IsRequired(false);
                entity.Property(e => e.NgayKetThucThucTe).IsRequired(false);

                entity.HasOne(e => e.PhieuDangKi)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuDK)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.DungCu)
                      .WithMany()
                      .HasForeignKey(e => e.MaDungCu)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            // Cấu hình cho DKDungCu
            modelBuilder.Entity<DangKiThietBi>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuDK, e.MaThietBi });
                entity.Property(e => e.NgayDangKi).IsRequired();
                entity.Property(e => e.NgaySuDung).IsRequired();
                entity.Property(e => e.NgayKetThuc).IsRequired();
                entity.Property(e => e.TrangThaiSuDung).HasMaxLength(50);
                entity.Property(e => e.TinhTrangSuDung).HasMaxLength(255).IsRequired();
                entity.Property(e => e.NgayBatDauThucTe).IsRequired(false);
                entity.Property(e => e.NgayKetThucThucTe).IsRequired(false);

                entity.HasOne(e => e.PhieuDangKi)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuDK)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ThietBi)
                      .WithMany()
                      .HasForeignKey(e => e.MaThietBi)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<ThoiGianSuDung>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuDK, e.MaThietBi });
                entity.Property(e => e.MaNV).HasMaxLength(20).IsRequired();
                entity.Property(e => e.NgayBatDau).IsRequired();
                entity.Property(e => e.NgayKetThuc).IsRequired();
                entity.Property(e => e.SoGio).IsRequired();

                entity.HasOne(e => e.PhieuDangKi)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuDK)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ThietBi)
                      .WithMany()
                      .HasForeignKey(e => e.MaThietBi)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PhongThiNghiem>(entity =>
            {
                entity.HasKey(e => e.MaPhong);
                entity.Property(e => e.MaPhong).HasMaxLength(20).IsRequired();
                entity.Property(e => e.LoaiPhong).HasMaxLength(50);
                entity.Property(e => e.ChucNang).HasMaxLength(50);
            });

            modelBuilder.Entity<PhieuPhanBoTB>(entity =>
            {
                entity.HasKey(e => e.MaPhieu);
                entity.Property(e => e.MaPhieu).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaThietBi).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaNV).HasMaxLength(20).IsRequired();
                entity.Property(e => e.NoiDung).HasMaxLength(255);
                entity.Property(e => e.NgayLap);
                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ThietBi)
                      .WithMany()
                      .HasForeignKey(e => e.MaThietBi)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PhanBoDC>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieu, e.MaDungCu });
                entity.Property(e => e.SoLuong).IsRequired();
                entity.Property(e => e.NgayLap);
                entity.Property(e => e.TrangThai).HasMaxLength(50);

                entity.HasOne(e => e.PhieuPhanBoTB)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieu)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.DungCu)
                      .WithMany()
                      .HasForeignKey(e => e.MaDungCu)
                      .OnDelete(DeleteBehavior.Restrict);
            });

           
            // Cấu hình PhieuThanhLy
            modelBuilder.Entity<PhieuThanhLy>(entity =>
            {
                entity.HasKey(e => e.MaPhieuTL);

                entity.Property(e => e.MaPhieuTL)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.MaCty)
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.Property(e => e.MaNV)
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.Property(e => e.TrangThai)
                      .HasMaxLength(50)
                      .HasDefaultValue("Chờ duyệt");

                entity.Property(e => e.LyDoChung)
                      .HasMaxLength(255)
                      .IsRequired(false);

                entity.Property(e => e.TongTien)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired(false);

                entity.Property(e => e.NgayLapPhieu)
                      .IsRequired(false);

                entity.Property(e => e.NgayHoanTat)
                      .IsRequired(false);

                entity.Property(e => e.TrangThaiThanhLy)
                      .HasMaxLength(50)
                      .HasDefaultValue("Chưa hoàn thành");

                entity.HasOne(e => e.CongTyThanhLy)
                      .WithMany()
                      .HasForeignKey(e => e.MaCty)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình ChiTietPhieuThanhLy
            modelBuilder.Entity<ChiTietPhieuThanhLy>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuTL, e.MaThietBi });

                entity.Property(e => e.MaPhieuTL)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.MaThietBi)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.GiaTL)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(e => e.LyDo)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.HasOne(e => e.PhieuThanhLy)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuTL)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ThietBi)
                      .WithMany()
                      .HasForeignKey(e => e.MaThietBi)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình DuyetPhieuThanhLy
            modelBuilder.Entity<DuyetPhieuThanhLy>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuTL, e.MaNV });

                entity.Property(e => e.MaPhieuTL)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.MaNV)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.TrangThai)
                      .HasMaxLength(50)
                      .IsRequired(false);

                entity.Property(e => e.LyDoTuChoi)
                      .IsRequired(false);

                entity.Property(e => e.GhiChu)
                      .HasMaxLength(255)
                      .IsRequired(false);

                entity.Property(e => e.NgayDuyet)
                      .IsRequired(false);

                entity.HasOne(e => e.PhieuThanhLy)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuTL)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<LichSuPhieuThanhLy>(entity =>
            {
                entity.HasKey(e => e.MaLichSu); 
                entity.Property(e => e.MaLichSu).IsRequired();

                entity.Property(e => e.MaPhieuTL)
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.Property(e => e.TrangThaiTruoc)
                      .HasMaxLength(50)
                      .IsRequired(false);

                entity.Property(e => e.TrangThaiSau)
                      .HasMaxLength(50)
                      .IsRequired(false);

                entity.Property(e => e.NgayThayDoi)
                      .IsRequired();

                entity.Property(e => e.MaNV)
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.HasOne(e => e.PhieuThanhLy)
                      .WithMany() 
                      .HasForeignKey(e => e.MaPhieuTL)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.NhanVien)
                      .WithMany() 
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            //Luân chuyển TB, DC
            modelBuilder.Entity<PhieuDeXuatLuanChuyen>(entity =>
            {
                entity.HasKey(e => e.MaPhieuLC); 
                entity.Property(e => e.MaPhieuLC).HasMaxLength(20).IsRequired();
                entity.Property(e => e.TrangThai).HasMaxLength(50);
                entity.Property(e => e.NgayTao).IsRequired();

                entity.Property(e => e.MaNV)
                      .HasMaxLength(20)
                      .IsRequired(false);
                entity.Property(e => e.GhiChu).HasMaxLength(255);
                entity.Property(e => e.NgayLuanChuyen).IsRequired(false);
                entity.Property(e => e.NgayHoanTat).IsRequired(false);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình cho bảng ChiTietLuanChuyenDC
            modelBuilder.Entity<ChiTietLuanChuyenDC>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuLC, e.MaDungCu }); // Đặt khóa chính cho ChiTietLuanChuyenDC

                entity.Property(e => e.MaPhieuLC).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaDungCu).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaPhongTu).HasMaxLength(20);
                entity.Property(e => e.MaPhongDen).HasMaxLength(20);
                entity.Property(e => e.SoLuong).IsRequired();

                // Thiết lập các quan hệ ForeignKey
                entity.HasOne(e => e.PhieuDeXuatLuanChuyen)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuLC)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.DungCu)
                      .WithMany()
                      .HasForeignKey(e => e.MaDungCu)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PhongTu)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhongTu)
                      .OnDelete(DeleteBehavior.SetNull); // Xóa cascade nếu cần thiết

                entity.HasOne(e => e.PhongDen)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhongDen)
                      .OnDelete(DeleteBehavior.SetNull); // Xóa cascade nếu cần thiết
            });

            // Cấu hình cho bảng ChiTietLuanChuyenTB
            modelBuilder.Entity<ChiTietLuanChuyenTB>(entity =>
            {
                entity.HasKey(e => new { e.MaPhieuLC, e.MaThietBi }); // Đặt khóa chính cho ChiTietLuanChuyenTB

                entity.Property(e => e.MaPhieuLC).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaThietBi).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaPhongTu).HasMaxLength(20);
                entity.Property(e => e.MaPhongDen).HasMaxLength(20);

                // Thiết lập các quan hệ ForeignKey
                entity.HasOne(e => e.PhieuDeXuatLuanChuyen)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuLC)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ThietBi)
                      .WithMany()
                      .HasForeignKey(e => e.MaThietBi)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PhongTu)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhongTu)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(e => e.PhongDen)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhongDen)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Cấu hình cho bảng LichSuPhieuLuanChuyen
            modelBuilder.Entity<LichSuPhieuLuanChuyen>(entity =>
            {
                entity.HasKey(e => e.MaLichSu); 

                entity.Property(e => e.MaPhieuLC).HasMaxLength(20).IsRequired();
                entity.Property(e => e.TrangThaiTruoc).HasMaxLength(50);
                entity.Property(e => e.TrangThaiSau).HasMaxLength(50).IsRequired();
                entity.Property(e => e.NgayThayDoi).HasDefaultValueSql("GETDATE()"); 
                entity.Property(e => e.MaNV).HasMaxLength(20);

                entity.HasOne(e => e.PhieuDeXuatLuanChuyen)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuLC)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<DuyetPhieuLuanChuyen>(entity =>
            {
                entity.HasKey(e => e.MaPhieuLC); 

                entity.Property(e => e.MaPhieuLC).HasMaxLength(20).IsRequired();
                entity.Property(e => e.MaNV).HasMaxLength(20);
                entity.Property(e => e.NgayDuyet).HasColumnType("DATETIME");
                entity.Property(e => e.TrangThai).HasMaxLength(50);
                entity.Property(e => e.LyDoTuChoi).HasMaxLength(255);

                entity.HasOne(e => e.PhieuDeXuatLuanChuyen)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuLC)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.NhanVien)
                      .WithMany()
                      .HasForeignKey(e => e.MaNV)
                      .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<ViTriDungCu>(entity =>
            {
                entity.HasKey(e => new { e.MaDungCu, e.MaPhong });
                entity.Property(e => e.MaDungCu).HasMaxLength(20).IsRequired();
                entity.Property(e => e.SoLuong).IsRequired();
                entity.Property(e => e.NgayCapNhat).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.DungCu)
                     .WithMany()
                     .HasForeignKey(e => e.MaDungCu)
                     .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.PhongThiNghiem)
                     .WithMany()
                     .HasForeignKey(e => e.MaPhong)
                     .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<QuanLyGioTB>(entity =>
            {
                entity.HasKey(e => e.MaQuanLyTB);
                entity.Property(e => e.MaQuanLyTB).IsRequired();

                entity.Property(e => e.MaPhieuDK)
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.Property(e => e.soGioSuDungThucTe)
                      .IsRequired(false);

                entity.Property(e => e.MaThietBi)
                      .HasMaxLength(20)
                      .IsRequired(false);
                entity.Property(e => e.MaPhong)
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.HasOne(e => e.PhieuDangKi)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuDK)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.PhongThiNghiem)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhong)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.ThietBi)
                     .WithMany()
                     .HasForeignKey(e => e.MaThietBi)
                     .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<QuanLyGioDC>(entity =>
            {
                entity.HasKey(e => e.MaQuanLyDC);
                entity.Property(e => e.MaQuanLyDC).IsRequired();

                entity.Property(e => e.MaPhieuDK)
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.Property(e => e.soGioSuDungThucTe)
                      .IsRequired(false);

                entity.Property(e => e.MaDungCu)
                      .HasMaxLength(20)
                      .IsRequired(false);
                entity.Property(e => e.MaPhong)
                      .HasMaxLength(20)
                      .IsRequired(false);

                entity.HasOne(e => e.PhieuDangKi)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhieuDK)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.PhongThiNghiem)
                      .WithMany()
                      .HasForeignKey(e => e.MaPhong)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.DungCu)
                     .WithMany()
                     .HasForeignKey(e => e.MaDungCu)
                     .OnDelete(DeleteBehavior.Restrict);
            });
    }
    }
}
