using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyHub.DAL.Models;

public partial class LoaiBaiTap
{
    public int IdLoaiBaiTap { get; set; }

    public string TenBaiTap { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<BaiTap> BaiTaps { get; set; } = new List<BaiTap>();
}
