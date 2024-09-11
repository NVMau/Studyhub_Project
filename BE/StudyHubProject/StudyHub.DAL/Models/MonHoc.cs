using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyHub.DAL.Models;

public partial class MonHoc
{
    public int IdMonHoc { get; set; }

    public string TenMonHoc { get; set; } = null!;

    public int SoTinChi { get; set; }
    [JsonIgnore]
    public virtual ICollection<CauHoi> CauHois { get; set; } = new List<CauHoi>();
    [JsonIgnore]
    public virtual ICollection<KhoaHoc> KhoaHocs { get; set; } = new List<KhoaHoc>();
}
