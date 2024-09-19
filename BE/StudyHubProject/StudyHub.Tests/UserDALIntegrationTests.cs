using StudyHub.DAL;
using StudyHub.DAL.Models;
using Xunit;

public class UserDALIntegrationTest
{
    private readonly UserDAL _userDAL;

    public UserDALIntegrationTest()
    {
        // Khởi tạo DAL với cơ sở dữ liệu thật
        _userDAL = new UserDAL();
    }

    [Fact]
    public void AddUser_ShouldAddUserToDatabase()
    {
        // Arrange: Tạo một user mới
        var user = new UserOu
        {
            Username = "simpletestuser",
            Password = "password",
            FirstName = "Test",
            LastName = "User",
            Email = "simpletestuser@example.com",
            Role = "Student"
        };

        // Act: Thêm user vào cơ sở dữ liệu
        var addedUser = _userDAL.AddUser(user);

        // Assert: Kiểm tra kết quả trả về
        Assert.NotNull(addedUser);
        Assert.Equal("simpletestuser", addedUser.Username);

        // Cleanup: Xóa dữ liệu sau khi test
        _userDAL.DeleteUser(addedUser.IdUser);
    }
}
