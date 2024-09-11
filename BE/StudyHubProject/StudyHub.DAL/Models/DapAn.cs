using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyHub.DAL.Models;

public partial class DapAn
{
    public int IdDapAn { get; set; }

    public string NoiDung { get; set; } = null!;

    public bool KetQua { get; set; }
    [JsonIgnore]
    public int IdCauHoi { get; set; }
    [JsonIgnore]
    public virtual CauHoi IdCauHoiNavigation { get; set; } = null!;
}
