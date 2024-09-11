using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
namespace StudyHub.DAL.Models;

public partial class UserOu
{
    public int IdUser { get; set; }

    public string Username { get; set; } = null!;
    [JsonIgnore]
    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Avatar { get; set; }

    public string? Email { get; set; }
    public string? Address { get; set; } // Thêm trường Address

    public string Role { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<KhoaHoc> KhoaHocs { get; set; } = new List<KhoaHoc>();
    [JsonIgnore]
    public virtual ICollection<SinhVienKhoaHoc> SinhVienKhoaHocs { get; set; } = new List<SinhVienKhoaHoc>();
    [JsonIgnore]
    public virtual ICollection<SinhVienLamBai> SinhVienLamBais { get; set; } = new List<SinhVienLamBai>();

}

public class Jwt
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Subject { get; set; }
}
