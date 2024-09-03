using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudyHub.DAL.Models;

public partial class HeThongQuanLyHocTapContext : DbContext
{
    public HeThongQuanLyHocTapContext()
    {
    }

    public HeThongQuanLyHocTapContext(DbContextOptions<HeThongQuanLyHocTapContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaiTap> BaiTaps { get; set; }

    public virtual DbSet<CauHoi> CauHois { get; set; }

    public virtual DbSet<CotDiem> CotDiems { get; set; }

    public virtual DbSet<DapAn> DapAns { get; set; }

    public virtual DbSet<HocKy> HocKies { get; set; }

    public virtual DbSet<KhoaHoc> KhoaHocs { get; set; }

    public virtual DbSet<ListTracNghiem> ListTracNghiems { get; set; }

    public virtual DbSet<LoaiBaiTap> LoaiBaiTaps { get; set; }

    public virtual DbSet<LoaiCauHoi> LoaiCauHois { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    public virtual DbSet<SinhVienKhoaHoc> SinhVienKhoaHocs { get; set; }

    public virtual DbSet<SinhVienLamBai> SinhVienLamBais { get; set; }

    public virtual DbSet<UserOu> UserOus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=BEOSBUJ\\BEOSBUJ;Initial Catalog=HeThongQuanLyHocTap;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaiTap>(entity =>
        {
            entity.HasKey(e => e.IdBaiTap).HasName("PK__BaiTap__B168D425CBB39FD7");

            entity.ToTable("BaiTap");

            entity.Property(e => e.IdBaiTap).HasColumnName("idBaiTap");
            entity.Property(e => e.IdKhoaHoc).HasColumnName("idKhoaHoc");
            entity.Property(e => e.IdLoaiBaiTap).HasColumnName("idLoaiBaiTap");
            entity.Property(e => e.TenBaiTap)
                .HasMaxLength(255)
                .HasColumnName("tenBaiTap");
            entity.Property(e => e.ThoiGian).HasColumnName("thoiGian");

            entity.HasOne(d => d.IdKhoaHocNavigation).WithMany(p => p.BaiTaps)
                .HasForeignKey(d => d.IdKhoaHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaiTap__idKhoaHo__52593CB8");

            entity.HasOne(d => d.IdLoaiBaiTapNavigation).WithMany(p => p.BaiTaps)
                .HasForeignKey(d => d.IdLoaiBaiTap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaiTap__idLoaiBa__534D60F1");
        });

        modelBuilder.Entity<CauHoi>(entity =>
        {
            entity.HasKey(e => e.IdCauHoi).HasName("PK__CauHoi__205222720BD837B8");

            entity.ToTable("CauHoi");

            entity.Property(e => e.IdCauHoi).HasColumnName("idCauHoi");
            entity.Property(e => e.IdLoaiCauHoi).HasColumnName("idLoaiCauHoi");
            entity.Property(e => e.IdMonHoc).HasColumnName("idMonHoc");
            entity.Property(e => e.NoiDung).HasColumnName("noiDung");

            entity.HasOne(d => d.IdLoaiCauHoiNavigation).WithMany(p => p.CauHois)
                .HasForeignKey(d => d.IdLoaiCauHoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CauHoi__idLoaiCa__5441852A");

            entity.HasOne(d => d.IdMonHocNavigation).WithMany(p => p.CauHois)
                .HasForeignKey(d => d.IdMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CauHoi__idMonHoc__5535A963");
        });

        modelBuilder.Entity<CotDiem>(entity =>
        {
            entity.HasKey(e => e.IdCotDiem).HasName("PK__CotDiem__BD9EB946381DA477");

            entity.ToTable("CotDiem");

            entity.Property(e => e.IdCotDiem).HasColumnName("idCotDiem");
            entity.Property(e => e.TenCotDiem)
                .HasMaxLength(255)
                .HasColumnName("tenCotDiem");
        });

        modelBuilder.Entity<DapAn>(entity =>
        {
            entity.HasKey(e => e.IdDapAn).HasName("PK__DapAn__B1FBEE8A54C592BC");

            entity.ToTable("DapAn");

            entity.Property(e => e.IdDapAn).HasColumnName("idDapAn");
            entity.Property(e => e.IdCauHoi).HasColumnName("idCauHoi");
            entity.Property(e => e.KetQua).HasColumnName("ketQua");
            entity.Property(e => e.NoiDung).HasColumnName("noiDung");

            entity.HasOne(d => d.IdCauHoiNavigation).WithMany(p => p.DapAns)
                .HasForeignKey(d => d.IdCauHoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DapAn__idCauHoi__5629CD9C");
        });

        modelBuilder.Entity<HocKy>(entity =>
        {
            entity.HasKey(e => e.IdHocKy).HasName("PK__HocKy__4C048364D047CB2D");

            entity.ToTable("HocKy");

            entity.Property(e => e.IdHocKy).HasColumnName("idHocKy");
            entity.Property(e => e.NamHocKy)
                .HasMaxLength(100)
                .HasColumnName("namHocKy");
        });

        modelBuilder.Entity<KhoaHoc>(entity =>
        {
            entity.HasKey(e => e.IdKhoaHoc).HasName("PK__KhoaHoc__BD521AA8383CFCA9");

            entity.ToTable("KhoaHoc");

            entity.Property(e => e.IdKhoaHoc).HasColumnName("idKhoaHoc");
            entity.Property(e => e.IdGiangVien).HasColumnName("idGiangVien");
            entity.Property(e => e.IdHocKy).HasColumnName("idHocKy");
            entity.Property(e => e.IdMonHoc).HasColumnName("idMonHoc");
            entity.Property(e => e.TenKhoaHoc)
                .HasMaxLength(255)
                .HasColumnName("tenKhoaHoc");

            entity.HasOne(d => d.IdGiangVienNavigation).WithMany(p => p.KhoaHocs)
                .HasForeignKey(d => d.IdGiangVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhoaHoc__idGiang__571DF1D5");

            entity.HasOne(d => d.IdHocKyNavigation).WithMany(p => p.KhoaHocs)
                .HasForeignKey(d => d.IdHocKy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhoaHoc__idHocKy__5812160E");

            entity.HasOne(d => d.IdMonHocNavigation).WithMany(p => p.KhoaHocs)
                .HasForeignKey(d => d.IdMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__KhoaHoc__idMonHo__59063A47");
        });

        modelBuilder.Entity<ListTracNghiem>(entity =>
        {
            entity.HasKey(e => e.IdTracNghiem).HasName("PK__ListTrac__34BFD0746423ED1A");

            entity.ToTable("ListTracNghiem");

            entity.Property(e => e.IdTracNghiem).HasColumnName("idTracNghiem");
            entity.Property(e => e.IdBaiTap).HasColumnName("idBaiTap");
            entity.Property(e => e.IdCauHoi).HasColumnName("idCauHoi");

            entity.HasOne(d => d.IdBaiTapNavigation).WithMany(p => p.ListTracNghiems)
                .HasForeignKey(d => d.IdBaiTap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ListTracN__idBai__59FA5E80");

            entity.HasOne(d => d.IdCauHoiNavigation).WithMany(p => p.ListTracNghiems)
                .HasForeignKey(d => d.IdCauHoi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ListTracN__idCau__5AEE82B9");
        });

        modelBuilder.Entity<LoaiBaiTap>(entity =>
        {
            entity.HasKey(e => e.IdLoaiBaiTap).HasName("PK__LoaiBaiT__A41853A16C0BD484");

            entity.ToTable("LoaiBaiTap");

            entity.Property(e => e.IdLoaiBaiTap).HasColumnName("idLoaiBaiTap");
            entity.Property(e => e.TenBaiTap)
                .HasMaxLength(255)
                .HasColumnName("tenBaiTap");
        });

        modelBuilder.Entity<LoaiCauHoi>(entity =>
        {
            entity.HasKey(e => e.IdLoaiCauHoi).HasName("PK__LoaiCauH__EC77D0E61522B5B1");

            entity.ToTable("LoaiCauHoi");

            entity.Property(e => e.IdLoaiCauHoi).HasColumnName("idLoaiCauHoi");
            entity.Property(e => e.TenLoaiCauHoi)
                .HasMaxLength(255)
                .HasColumnName("tenLoaiCauHoi");
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.IdMonHoc).HasName("PK__MonHoc__EC81BF91F92AD147");

            entity.ToTable("MonHoc");

            entity.Property(e => e.IdMonHoc).HasColumnName("idMonHoc");
            entity.Property(e => e.SoTinChi).HasColumnName("soTinChi");
            entity.Property(e => e.TenMonHoc)
                .HasMaxLength(255)
                .HasColumnName("tenMonHoc");
        });

        modelBuilder.Entity<SinhVienKhoaHoc>(entity =>
        {
            entity.HasKey(e => e.IdSvkhoaHoc).HasName("PK__SinhVien__4FA396A088EFED69");

            entity.ToTable("SinhVienKhoaHoc");

            entity.Property(e => e.IdSvkhoaHoc).HasColumnName("idSVKhoaHoc");
            entity.Property(e => e.IdKhoaHoc).HasColumnName("idKhoaHoc");
            entity.Property(e => e.IdSinhVien).HasColumnName("idSinhVien");

            entity.HasOne(d => d.IdKhoaHocNavigation).WithMany(p => p.SinhVienKhoaHocs)
                .HasForeignKey(d => d.IdKhoaHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SinhVienK__idKho__5BE2A6F2");

            entity.HasOne(d => d.IdSinhVienNavigation).WithMany(p => p.SinhVienKhoaHocs)
                .HasForeignKey(d => d.IdSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SinhVienK__idSin__5CD6CB2B");
        });

        modelBuilder.Entity<SinhVienLamBai>(entity =>
        {
            entity.HasKey(e => e.IdBaiLam).HasName("PK__SinhVien__BF2971AF676BB323");

            entity.ToTable("SinhVienLamBai");

            entity.Property(e => e.IdBaiLam).HasColumnName("idBaiLam");
            entity.Property(e => e.IdBaiTap).HasColumnName("idBaiTap");
            entity.Property(e => e.IdCotDiem).HasColumnName("idCotDiem");
            entity.Property(e => e.IdSinhVien).HasColumnName("idSinhVien");
            entity.Property(e => e.NoiDungBaiLam).HasColumnName("noiDungBaiLam");

            entity.HasOne(d => d.IdBaiTapNavigation).WithMany(p => p.SinhVienLamBais)
                .HasForeignKey(d => d.IdBaiTap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SinhVienL__idBai__5DCAEF64");

            entity.HasOne(d => d.IdCotDiemNavigation).WithMany(p => p.SinhVienLamBais)
                .HasForeignKey(d => d.IdCotDiem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SinhVienL__idCot__5EBF139D");

            entity.HasOne(d => d.IdSinhVienNavigation).WithMany(p => p.SinhVienLamBais)
                .HasForeignKey(d => d.IdSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SinhVienL__idSin__5FB337D6");
        });

        modelBuilder.Entity<UserOu>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__UserOu__3717C9826E12DF75");

            entity.ToTable("UserOu");

            entity.HasIndex(e => e.Username, "UQ__UserOu__F3DBC572A6E5E228").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
