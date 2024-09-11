using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyHub.DAL.Models;

public partial class HocKy
{
    public int IdHocKy { get; set; }

    public string NamHocKy { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<KhoaHoc> KhoaHocs { get; set; } = new List<KhoaHoc>();
}
