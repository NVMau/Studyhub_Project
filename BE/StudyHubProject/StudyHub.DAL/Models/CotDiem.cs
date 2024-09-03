using System;
using System.Collections.Generic;

namespace StudyHub.DAL.Models;

public partial class CotDiem
{
    public int IdCotDiem { get; set; }

    public string TenCotDiem { get; set; } = null!;

    public double Diem { get; set; }

    public virtual ICollection<SinhVienLamBai> SinhVienLamBais { get; set; } = new List<SinhVienLamBai>();
}
