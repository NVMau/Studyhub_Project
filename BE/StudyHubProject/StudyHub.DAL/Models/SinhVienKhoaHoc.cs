using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class SinhVienKhoaHoc
{
    public int IdSvkhoaHoc { get; set; }

    public int IdSinhVien { get; set; }

    public int IdKhoaHoc { get; set; }

    public virtual KhoaHoc IdKhoaHocNavigation { get; set; } = null!;

    public virtual UserOu IdSinhVienNavigation { get; set; } = null!;
}
