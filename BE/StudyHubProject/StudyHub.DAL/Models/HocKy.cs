using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class HocKy
{
    public int IdHocKy { get; set; }

    public string NamHocKy { get; set; } = null!;

    public virtual ICollection<KhoaHoc> KhoaHocs { get; set; } = new List<KhoaHoc>();
}
