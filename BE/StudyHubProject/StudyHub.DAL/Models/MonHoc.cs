using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class MonHoc
{
    public int IdMonHoc { get; set; }

    public string TenMonHoc { get; set; } = null!;

    public int SoTinChi { get; set; }

    public virtual ICollection<CauHoi> CauHois { get; set; } = new List<CauHoi>();

    public virtual ICollection<KhoaHoc> KhoaHocs { get; set; } = new List<KhoaHoc>();
}
