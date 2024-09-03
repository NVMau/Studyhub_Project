using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class LoaiCauHoi
{
    public int IdLoaiCauHoi { get; set; }

    public string TenLoaiCauHoi { get; set; } = null!;

    public virtual ICollection<CauHoi> CauHois { get; set; } = new List<CauHoi>();
}
