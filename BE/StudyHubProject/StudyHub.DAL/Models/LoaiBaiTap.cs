using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class LoaiBaiTap
{
    public int IdLoaiBaiTap { get; set; }

    public string TenBaiTap { get; set; } = null!;

    public virtual ICollection<BaiTap> BaiTaps { get; set; } = new List<BaiTap>();
}
