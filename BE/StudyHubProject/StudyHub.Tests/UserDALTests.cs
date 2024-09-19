using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyHub.DAL;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using Xunit;

public class UserDALTests
{
    private HeThongQuanLyHocTapContext _context;
    private UserDAL _userDAL;

    public UserDALTests()
    {
        // Khởi tạo In-Memory Database cho test với ServiceProvider
        var options = new DbContextOptionsBuilder<HeThongQuanLyHocTapContext>()
                          .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                          .UseInternalServiceProvider(new ServiceCollection()
                              .AddEntityFrameworkInMemoryDatabase()
                              .BuildServiceProvider())
                          .Options;

        _context = new HeThongQuanLyHocTapContext(options);
        _userDAL = new UserDAL();
    }

    [Fact]
    public void AddUser_ShouldAddUserToDatabase()
    {
        // Arrange: Tạo một user mới
        var user = new UserOu
        {
            Username = "testuser",
            Password = "testpassword",
            FirstName = "Test",
            LastName = "User",
            Email = "testuser@example.com",
            Role = "Student"
        };

        // Act: Thực hiện thêm user vào cơ sở dữ liệu
        var addedUser = _userDAL.AddUser(user);

        // Assert: Kiểm tra kết quả
        Assert.NotNull(addedUser);
        Assert.Equal("testuser", addedUser.Username);
    }

    [Fact]
    public void GetUserById_ShouldReturnCorrectUser()
    {
        // Arrange: Thêm user mẫu vào cơ sở dữ liệu
        var user = new UserOu
        {
            Username = "testuser2",
            Password = "testpassword",
            FirstName = "Test",
            LastName = "User2",
            Email = "testuser2@example.com",
            Role = "Student"
        };
        _context.UserOus.Add(user);
        _context.SaveChanges();

        // Act: Lấy user theo ID
        var fetchedUser = _userDAL.GetUserById(user.IdUser);

        // Assert: Kiểm tra kết quả
        Assert.NotNull(fetchedUser);
        Assert.Equal("testuser2", fetchedUser.Username);
    }

    [Fact]
    public void UpdateUser_ShouldUpdateUserDetails()
    {
        // Arrange: Thêm user mẫu vào cơ sở dữ liệu
        var user = new UserOu
        {
            Username = "testuser3",
            Password = "testpassword",
            FirstName = "Test",
            LastName = "User3",
            Email = "testuser3@example.com",
            Role = "Student"
        };
        _context.UserOus.Add(user);
        _context.SaveChanges();

        // Thay đổi chi tiết user
        user.Email = "updateduser3@example.com";
        user.FirstName = "Updated";

        // Act: Thực hiện cập nhật user
        _userDAL.UpdateUser(user);

        // Assert: Kiểm tra kết quả cập nhật
        var updatedUser = _userDAL.GetUserById(user.IdUser);
        Assert.Equal("updateduser3@example.com", updatedUser.Email);
        Assert.Equal("Updated", updatedUser.FirstName);
    }

    [Fact]
    public void DeleteUser_ShouldRemoveUserFromDatabase()
    {
        // Arrange: Thêm user mẫu vào cơ sở dữ liệu
        var user = new UserOu
        {
            Username = "testuser4",
            Password = "testpassword",
            FirstName = "Test",
            LastName = "User4",
            Email = "testuser4@example.com",
            Role = "Student"
        };
        _context.UserOus.Add(user);
        _context.SaveChanges();

        // Act: Thực hiện xóa user
        _userDAL.DeleteUser(user.IdUser);

        // Assert: Kiểm tra user đã bị xóa
        var deletedUser = _userDAL.GetUserById(user.IdUser);
        Assert.Null(deletedUser);
    }

    [Fact]
    public void GetUserByUsernameAndPassword_ShouldReturnUserIfCredentialsAreCorrect()
    {
        // Arrange: Thêm user mẫu vào cơ sở dữ liệu
        var user = new UserOu
        {
            Username = "testuser5",
            Password = "testpassword",
            FirstName = "Test",
            LastName = "User5",
            Email = "testuser5@example.com",
            Role = "Student"
        };
        _context.UserOus.Add(user);
        _context.SaveChanges();

        // Act: Lấy user theo username và password
        var fetchedUser = _userDAL.GetUserByUsernameAndPassword("testuser5", "testpassword");

        // Assert: Kiểm tra kết quả
        Assert.NotNull(fetchedUser);
        Assert.Equal("testuser5", fetchedUser.Username);
    }

    [Fact]
    public void ChangePass_ShouldUpdatePassword()
    {
        // Arrange: Thêm user mẫu vào cơ sở dữ liệu
        var user = new UserOu
        {
            Username = "testuser6",
            Password = "oldpassword",
            FirstName = "Test",
            LastName = "User6",
            Email = "testuser6@example.com",
            Role = "Student"
        };
        _context.UserOus.Add(user);
        _context.SaveChanges();

        // Act: Thay đổi mật khẩu của user
        var updatedUser = _userDAL.ChangePass(user.IdUser, "newpassword");

        // Assert: Kiểm tra mật khẩu đã được thay đổi
        Assert.NotNull(updatedUser);
        Assert.Equal("newpassword", updatedUser.Password);
    }
}
