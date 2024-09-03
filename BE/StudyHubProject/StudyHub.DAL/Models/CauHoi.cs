using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class CauHoi
{
    public int IdCauHoi { get; set; }

    public string NoiDung { get; set; } = null!;

    public int IdMonHoc { get; set; }

    public int IdLoaiCauHoi { get; set; }

    public virtual ICollection<DapAn> DapAns { get; set; } = new List<DapAn>();

    public virtual LoaiCauHoi IdLoaiCauHoiNavigation { get; set; } = null!;

    public virtual MonHoc IdMonHocNavigation { get; set; } = null!;

    public virtual ICollection<ListTracNghiem> ListTracNghiems { get; set; } = new List<ListTracNghiem>();
}
