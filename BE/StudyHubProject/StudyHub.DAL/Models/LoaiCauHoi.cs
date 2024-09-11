using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyHub.DAL.Models;

public partial class LoaiCauHoi
{
    public int IdLoaiCauHoi { get; set; }

    public string TenLoaiCauHoi { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<CauHoi> CauHois { get; set; } = new List<CauHoi>();
}
