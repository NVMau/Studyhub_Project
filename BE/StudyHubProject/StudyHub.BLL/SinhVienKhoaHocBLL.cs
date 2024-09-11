using Microsoft.EntityFrameworkCore;
using StudyHub.DAL;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.BLL
{
    public class SinhVienKhoaHocBLL
    {
        private readonly SinhVienKhoaHocDAL _sinhVienKhoaHocDAL;

        public SinhVienKhoaHocBLL()
        {
            _sinhVienKhoaHocDAL = new SinhVienKhoaHocDAL();
        }

        // sinh viên đăng ký môn
        public void SinhVienDangKyKhoaHoc(int idKhoaHoc, int idSinhVien)
        {
            _sinhVienKhoaHocDAL.SinhVienDangKyKhoaHoc(idKhoaHoc, idSinhVien);
        }
        //xóa 
        public Boolean DeleteSinhVienKhoaHoc(int idSinhVienKhoaHoc)
        {
            return _sinhVienKhoaHocDAL.DeleteSinhVienKhoaHoc(idSinhVienKhoaHoc);
        }
        // lấy list
        public List<object> GetListKhoaHocBySinhVien(int idSinhVien)
        {
            return _sinhVienKhoaHocDAL.GetListKhoaHocBySinhVien(idSinhVien);
        }
        public IEnumerable<SinhVienKhoaHoc> GetSinhVienKhoaHocBySV(int idSinhhVien)
        {
            return _sinhVienKhoaHocDAL.GetSinhVienKhoaHocBySV(idSinhhVien);
        }
        // lấy sinhviêns teho khóa
        public List<UserOu> GetSinhVienByKhoaHoc(int idKhoaHoc)
        {
            return _sinhVienKhoaHocDAL.GetSinhVienByKhoaHoc(idKhoaHoc);
        }

    }
}
