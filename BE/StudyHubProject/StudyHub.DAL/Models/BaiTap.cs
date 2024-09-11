using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StudyHub.DAL.Models;

public partial class BaiTap
{
    public int IdBaiTap { get; set; }

    public int IdLoaiBaiTap { get; set; }

    public int IdKhoaHoc { get; set; }

    public string TenBaiTap { get; set; } = null!;

    public int ThoiGian { get; set; }
    [JsonIgnore]
    public virtual KhoaHoc IdKhoaHocNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual LoaiBaiTap IdLoaiBaiTapNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<ListTracNghiem> ListTracNghiems { get; set; } = new List<ListTracNghiem>();
    [JsonIgnore]
    public virtual ICollection<SinhVienLamBai> SinhVienLamBais { get; set; } = new List<SinhVienLamBai>();
}
