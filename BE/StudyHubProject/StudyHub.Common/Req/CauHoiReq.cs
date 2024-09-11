using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Common.Req
{
    public class CauHoiReq
    {

        //public int IdCauHoi { get; set; }

        public string NoiDung { get; set; } = null!;

        public int IdMonHoc { get; set; } = -1;

        public int IdLoaiCauHoi { get; set; } = -1;


    }
}
