using StudyHub.DAL;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.BLL
{
    public class HocKyBLL
    {
        private readonly HocKyDAL hocKyDAL = new HocKyDAL();

        // lấy danh sách học kỳ
        public List<HocKy> GetListHocKy()
        {
            return hocKyDAL.GetListHocKy();
        }

        // thêm
        public void AddHocKy(string namHocKy)
        {
            hocKyDAL.AddHocKy(namHocKy);
        }

        // lấy id
        public HocKy GetHocKybyid(int id)
        {
            return hocKyDAL.GetHocKybyid(id);
        }
        // update
        public void updateHocKy(HocKy hocKy)
        {
           hocKyDAL.updateHocKy(hocKy);
        }
        // xóa
        public void deletehocky(int id)
        {
            hocKyDAL.DeleteHocKy(id);
        }
    }
}
