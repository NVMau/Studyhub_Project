using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyHub.DAL;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using Xunit;

public class KhoaHocDALTests
{
    private HeThongQuanLyHocTapContext _context;
    private KhoaHocDAL _khoaHocDal;

    public KhoaHocDALTests()
    {
        // Khởi tạo In-Memory Database cho test với ServiceProvider
        var options = new DbContextOptionsBuilder<HeThongQuanLyHocTapContext>()
                          .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                          .UseInternalServiceProvider(new ServiceCollection()
                              .AddEntityFrameworkInMemoryDatabase()
                              .BuildServiceProvider())
                          .Options;

        _context = new HeThongQuanLyHocTapContext(options);
        _khoaHocDal = new KhoaHocDAL(_context); // Sử dụng context này cho DAL
    }
    [Fact]
    public void GetAllKhoaHoc_ShouldReturnListOfKhoaHocDTO()
    {
        // Arrange: Thêm dữ liệu mẫu vào In-Memory Database
        _context.KhoaHocs.Add(new KhoaHoc { IdKhoaHoc = 1, TenKhoaHoc = "KhoaHoc1", IdGiangVien = 1, IdHocKy = 1, IdMonHoc = 1 });
        _context.KhoaHocs.Add(new KhoaHoc { IdKhoaHoc = 2, TenKhoaHoc = "KhoaHoc2", IdGiangVien = 2, IdHocKy = 2, IdMonHoc = 2 });

        // Gán giá trị cho các thuộc tính bắt buộc: Password, Username, và Role
        _context.UserOus.Add(new UserOu
        {
            IdUser = 1,
            FirstName = "John",
            LastName = "Doe",
            Password = "password123",
            Username = "johndoe",
            Role = "Lecturer"
        });
        _context.UserOus.Add(new UserOu
        {
            IdUser = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Password = "password123",
            Username = "janesmith",
            Role = "Lecturer"
        });

        _context.HocKies.Add(new HocKy { IdHocKy = 1, NamHocKy = "2021-2022" });
        _context.HocKies.Add(new HocKy { IdHocKy = 2, NamHocKy = "2022-2023" });

        _context.MonHocs.Add(new MonHoc { IdMonHoc = 1, TenMonHoc = "Math" });
        _context.MonHocs.Add(new MonHoc { IdMonHoc = 2, TenMonHoc = "Physics" });

        _context.SaveChanges(); // Lưu dữ liệu vào In-Memory Database

        // Act: Thực hiện phương thức trong KhoaHocDAL
        var result = _khoaHocDal.GetAllKhoaHoc();

        // Assert: Kiểm tra kết quả
        Assert.NotNull(result);
        Assert.Equal(2, result.Count); // Kiểm tra số lượng kết quả mong đợi là 2
    }

}
