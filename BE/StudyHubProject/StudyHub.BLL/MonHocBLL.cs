using StudyHub.DAL;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.BLL
{
    namespace StudyHub.BLL
    {
        public class MonHocBLL
        {
            private readonly MonHocDAL _monHocDAL;

            public MonHocBLL()
            {
                _monHocDAL = new MonHocDAL();
            }

            // lấy môn học
            public List<MonHoc> getMonhocs()
            {
                return _monHocDAL.getMonhocs();
            }


            public IEnumerable<CauHoi> GetCauHoiByMonHoc(int idKhoaHoc)
            {
                return _monHocDAL.GetCauHoiByMonHoc(idKhoaHoc);
            }
            // thêm môn
            public void AddMonHoc(MonHoc monHoc)
            {
                _monHocDAL.AddMonHoc(monHoc);
            }
            // cập nhật 
            public bool UpdateMonHoc(MonHoc monHoc)
            {
                return _monHocDAL.UpdateMonHoc(monHoc);
            }
            // xóa
            public void DeleteMonHoc(int id)
            {
                _monHocDAL.DeleteMonHoc(id);
            }
            // lấy thep id
            public MonHoc? GetMonHocById(int id)
            {
                return _monHocDAL.GetMonHocById(id);
            }
        }
    }
}