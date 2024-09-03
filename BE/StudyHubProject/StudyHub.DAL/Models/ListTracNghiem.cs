using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class ListTracNghiem
{
    public int IdTracNghiem { get; set; }

    public int IdBaiTap { get; set; }

    public int IdCauHoi { get; set; }

    public virtual BaiTap IdBaiTapNavigation { get; set; } = null!;

    public virtual CauHoi IdCauHoiNavigation { get; set; } = null!;
}
