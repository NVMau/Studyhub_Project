using Microsoft.EntityFrameworkCore;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class KhoaHocDAL
    {
        HeThongQuanLyHocTapContext context = new HeThongQuanLyHocTapContext();
        // model đối tượng trả về 
        public class KhoaHocDTO
        {
            public int IdKhoaHoc { get; set; }
            public string TenGiangVien { get; set; }
            public string TenKhoaHoc { get; set; }
            public string NamHocKy { get; set; }
            public string MonHoc { get; set; }
        }
        //lấy list KhocHoc
        public List<KhoaHocDTO> GetAllKhoaHoc()
        {
            var result = from kh in context.KhoaHocs
                         join gv in context.UserOus on kh.IdGiangVien equals gv.IdUser
                         join hk in context.HocKies on kh.IdHocKy equals hk.IdHocKy
                         join mh in context.MonHocs on kh.IdMonHoc equals mh.IdMonHoc
                         select new KhoaHocDTO
                         {
                             IdKhoaHoc = kh.IdKhoaHoc,
                             TenKhoaHoc = kh.TenKhoaHoc,
                             TenGiangVien = gv.FirstName + " " + gv.LastName,
                             NamHocKy = hk.NamHocKy,
                             MonHoc = mh.TenMonHoc
                         };

            return result.ToList();
        }
        public List<object> GetListKhoaHoc()
        {
            var khoaHocs = context.KhoaHocs
                .Select(kh => new
                {
                    IdKhoaHoc = kh.IdKhoaHoc,
                    TenKhoaHoc = kh.TenKhoaHoc,
                    TenGiangVien = kh.IdGiangVienNavigation.FirstName + " " + kh.IdGiangVienNavigation.LastName,
                    SoTinChi = kh.IdMonHocNavigation.SoTinChi
                })
                .ToList<object>();

            return khoaHocs;
        }


        public List<object> GetListKhoaHocByUserAndByHocKy(int userId, int hocKyId)
        {
            var khoaHocs = context.KhoaHocs
                .Where(kh => kh.IdHocKy == hocKyId && kh.SinhVienKhoaHocs.Any(svk => svk.IdSinhVien == userId))
                .Select(kh => new
                {
                    kh.IdKhoaHoc,
                    kh.TenKhoaHoc,
                    TenGiangVien = kh.IdGiangVienNavigation.FirstName + " " + kh.IdGiangVienNavigation.LastName,
                    SoLuongSinhVien = kh.SinhVienKhoaHocs.Count,
                    TenMonHoc = kh.IdMonHocNavigation.TenMonHoc,
                    SoTinChi = kh.IdMonHocNavigation.SoTinChi
                })
                .ToList<object>();
            return khoaHocs;
        }

        // Hàm để lấy danh sách khóa học theo ID user là giảng viên và ID học kỳ
        public List<object> LayDanhSachKhoaHocTheoGiangVienVaHocKy(int idGiangVien, int idHocKy)
        {
            var query = from khoaHoc in context.KhoaHocs
                        where khoaHoc.IdGiangVien == idGiangVien && khoaHoc.IdHocKy == idHocKy
                        select new
                        {
                            IdKhoaHoc = khoaHoc.IdKhoaHoc,
                            TenKhoaHoc = khoaHoc.TenKhoaHoc,
                            IdMonHoc = khoaHoc.IdMonHoc, // Thêm trường IdMonHoc vào biểu thức projection
                            TenMonHoc = khoaHoc.IdMonHocNavigation.TenMonHoc,
                            TenGiangVien = khoaHoc.IdGiangVienNavigation.FirstName + " " + khoaHoc.IdGiangVienNavigation.LastName,
                            SoLuongSinhVien = khoaHoc.SinhVienKhoaHocs.Count
                        };

            return query.ToList<object>();
        }

        public IEnumerable<KhoaHoc> GetKhoaHocByTen(string tenKhoaHoc)
        {
            return context.KhoaHocs
                .Where(kh => kh.TenKhoaHoc.Contains(tenKhoaHoc))
                .ToList();
        }

        // lấy khóa học theo user
        public List<object> GetListKhoaHocByUser(int userId)
        {
            var khoaHocs = context.KhoaHocs
                .Where(kh => kh.SinhVienKhoaHocs.Any(svk => svk.IdSinhVien == userId))
                .Select(kh => new
                {
                    kh.IdKhoaHoc,
                    kh.TenKhoaHoc,
                    TenGiangVien = kh.IdGiangVienNavigation.FirstName + " " + kh.IdGiangVienNavigation.LastName,
                    SoLuongSinhVien = kh.SinhVienKhoaHocs.Count,
                    TenMonHoc = kh.IdMonHocNavigation.TenMonHoc,
                    SoTinChi = kh.IdMonHocNavigation.SoTinChi // Lấy số tín chỉ từ MonHoc
                })
                .ToList<object>();
            return khoaHocs;
        }

        public KhoaHoc? GetKhoaHocById(int idKhoaHoc)
        {
            return context.KhoaHocs.FirstOrDefault(kh => kh.IdKhoaHoc == idKhoaHoc);
        }

        public HocKy? GetHocKyById(int idKhoaHoc)
        {
            var kh = context.KhoaHocs.FirstOrDefault(kh => kh.IdKhoaHoc == idKhoaHoc);
            return context.HocKies.FirstOrDefault(ky => ky.IdHocKy == kh.IdHocKy);
        }

        //thêm
        public void AddKhoaHoc(KhoaHoc kh)
        {
            var khoaHoc = new KhoaHoc
            {
                IdMonHoc = kh.IdMonHoc,
                IdGiangVien = kh.IdGiangVien,
                IdHocKy = kh.IdHocKy,
                TenKhoaHoc = kh.TenKhoaHoc
            };

            context.KhoaHocs.Add(khoaHoc);
            context.SaveChanges();
        }
        // xóa
        public void DeleteKhoaHoc(int id)
        {
            var kh = context.KhoaHocs.FirstOrDefault(m => m.IdKhoaHoc == id);
            context.KhoaHocs.Remove(kh);
            context.SaveChanges();
        }
        // cập nhật
        public bool UpdateKhoaHoc(KhoaHoc khoahoc)
        {
            var kh = context.KhoaHocs.FirstOrDefault(m => m.IdKhoaHoc == khoahoc.IdKhoaHoc);
            if (kh == null)
                return false;
            else
            {
                kh.IdMonHoc = khoahoc.IdMonHoc;
                kh.IdGiangVien = khoahoc.IdGiangVien;
                kh.IdHocKy = khoahoc.IdHocKy;
                kh.TenKhoaHoc = khoahoc.TenKhoaHoc;
                context.KhoaHocs.Update(kh);
                context.SaveChanges();
                return true;
            }

        }

    }
}