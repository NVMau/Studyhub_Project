using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyHub.DAL.Models;

public partial class CauHoi
{
    public int IdCauHoi { get; set; }

    public string NoiDung { get; set; } = null!;

    public int IdMonHoc { get; set; }

    public int IdLoaiCauHoi { get; set; }
    [JsonIgnore]
    public virtual ICollection<DapAn> DapAns { get; set; } = new List<DapAn>();
    [JsonIgnore]

    public virtual LoaiCauHoi IdLoaiCauHoiNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual MonHoc IdMonHocNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<ListTracNghiem> ListTracNghiems { get; set; } = new List<ListTracNghiem>();
}
