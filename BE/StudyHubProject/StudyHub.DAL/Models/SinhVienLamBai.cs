using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyHub.DAL.Models;

public partial class SinhVienLamBai
{
    public int IdBaiLam { get; set; }

    public int IdSinhVien { get; set; }

    public int IdBaiTap { get; set; }

    public int IdCotDiem { get; set; }

    public string? NoiDungBaiLam { get; set; }
    [JsonIgnore]
    public virtual BaiTap IdBaiTapNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual CotDiem IdCotDiemNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual UserOu IdSinhVienNavigation { get; set; } = null!;
}
