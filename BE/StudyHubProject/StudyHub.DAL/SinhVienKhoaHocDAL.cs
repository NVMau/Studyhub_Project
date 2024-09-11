using Microsoft.EntityFrameworkCore;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class SinhVienKhoaHocDAL
    {
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();


        // sinh viên đăng ký khóa học
        public void SinhVienDangKyKhoaHoc(int idKhoaHoc, int idSinhVien)
        {
            // Kiểm tra xem sinh viên và khóa học có tồn tại trong cơ sở dữ liệu không
            var sinhVien = _context.UserOus.FirstOrDefault(sv => sv.IdUser == idSinhVien);
            var khoaHoc = _context.KhoaHocs.FirstOrDefault(kh => kh.IdKhoaHoc == idKhoaHoc);

            if (sinhVien != null && khoaHoc != null)
            {
                // Tạo một đối tượng SinhVienKhoaHoc mới
                var sinhvienKhoaHoc = new SinhVienKhoaHoc
                {
                    IdSinhVien = idSinhVien,
                    IdKhoaHoc = idKhoaHoc,
                    IdKhoaHocNavigation = khoaHoc,
                    IdSinhVienNavigation = sinhVien
                };

                // Thêm đối tượng SinhVienKhoaHoc mới vào DbContext
                _context.SinhVienKhoaHocs.Add(sinhvienKhoaHoc);

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }
        }
        // sinh viên hủy khóa học
        public Boolean DeleteSinhVienKhoaHoc(int idSinhVienKhoaHoc)
        {
            var svkhToRemove = _context.SinhVienKhoaHocs.FirstOrDefault(svkh => svkh.IdSvkhoaHoc == idSinhVienKhoaHoc);
            if (svkhToRemove != null)
            {
                _context.SinhVienKhoaHocs.Remove(svkhToRemove);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        // lấy khóa học theo user
        public List<object> GetListKhoaHocBySinhVien(int idSinhVien)
        {
            var khoaHocs = _context.SinhVienKhoaHocs
                .Where(svk => svk.IdSinhVien == idSinhVien)
                .Select(svk => new
                {
                    IdSvKhoaHoc = svk.IdSvkhoaHoc,
                    TenKhoaHoc = svk.IdKhoaHocNavigation.TenKhoaHoc
                })
                .ToList<object>();

            return khoaHocs;
        }


        public IEnumerable<SinhVienKhoaHoc> GetSinhVienKhoaHocBySV(int idSinhhVien)
        {
            return _context.SinhVienKhoaHocs.Where(svkh => svkh.IdSinhVien == idSinhhVien).ToList();
        }

        // lấy danh ssách theo khóa học

        public List<UserOu> GetSinhVienByKhoaHoc(int idKhoaHoc)
        {
            var query = from svkh in _context.SinhVienKhoaHocs
                        join sv in _context.UserOus on svkh.IdSinhVien equals sv.IdUser
                        where svkh.IdKhoaHoc == idKhoaHoc
                        select sv;

            return query.ToList();
        }

    }
} 
