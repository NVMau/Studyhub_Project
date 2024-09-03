using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class SinhVienLamBai
{
    public int IdBaiLam { get; set; }

    public int IdSinhVien { get; set; }

    public int IdBaiTap { get; set; }

    public int IdCotDiem { get; set; }

    public string? NoiDungBaiLam { get; set; }

    public virtual BaiTap IdBaiTapNavigation { get; set; } = null!;

    public virtual CotDiem IdCotDiemNavigation { get; set; } = null!;

    public virtual UserOu IdSinhVienNavigation { get; set; } = null!;
}
