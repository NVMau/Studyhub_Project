using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class KhoaHoc
{
    public int IdKhoaHoc { get; set; }

    public int IdMonHoc { get; set; }

    public int IdGiangVien { get; set; }

    public int IdHocKy { get; set; }

    public string TenKhoaHoc { get; set; } = null!;

    public virtual ICollection<BaiTap> BaiTaps { get; set; } = new List<BaiTap>();

    public virtual UserOu IdGiangVienNavigation { get; set; } = null!;

    public virtual HocKy IdHocKyNavigation { get; set; } = null!;

    public virtual MonHoc IdMonHocNavigation { get; set; } = null!;

    public virtual ICollection<SinhVienKhoaHoc> SinhVienKhoaHocs { get; set; } = new List<SinhVienKhoaHoc>();
}
