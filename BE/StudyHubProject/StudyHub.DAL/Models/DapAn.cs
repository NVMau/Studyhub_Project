using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class DapAn
{
    public int IdDapAn { get; set; }

    public string NoiDung { get; set; } = null!;

    public bool KetQua { get; set; }

    public int IdCauHoi { get; set; }

    public virtual CauHoi IdCauHoiNavigation { get; set; } = null!;
}
