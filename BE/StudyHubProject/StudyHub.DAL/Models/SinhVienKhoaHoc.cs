using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyHub.DAL.Models;

public partial class SinhVienKhoaHoc
{
    public int IdSvkhoaHoc { get; set; }

    public int IdSinhVien { get; set; }

    public int IdKhoaHoc { get; set; }
    [JsonIgnore]
    public virtual KhoaHoc IdKhoaHocNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual UserOu IdSinhVienNavigation { get; set; } = null!;
}
