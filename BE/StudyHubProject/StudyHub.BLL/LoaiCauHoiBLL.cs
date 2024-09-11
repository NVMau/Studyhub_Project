using StudyHub.DAL;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.BLL
{
    public class LoaiCauHoiBLL
    {
        private readonly LoaiCauHoiDAL loaiCauHoiDAL = new LoaiCauHoiDAL();

        public List<LoaiCauHoi> GetALL() { 
            return loaiCauHoiDAL.getAll();
        }
    }
}
