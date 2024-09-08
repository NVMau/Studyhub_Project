using StudyHub.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

namespace StudyHub.DAL
{
    public class CotDiemDAL
    {
        private readonly HeThongQuanLyHocTapContext _context;

        public CotDiemDAL(HeThongQuanLyHocTapContext context)
        {
            _context = context;
        }

        public SinhVienLamBai AddSinhVienLamBai(SinhVienLamBai sinhVienLamBai)
        {
            _context.SinhVienLamBais.Add(sinhVienLamBai);
            _context.SaveChanges();
            return sinhVienLamBai;
        }
        // lấy cột điểm theo khóa và ng dùng
        public IEnumerable<SinhVienLamBai> GetCotDiemsByKhoaHocAndUser(int idKhoaHoc, int idUser)
        {
            return _context.SinhVienLamBais
                .Where(slb => slb.IdSinhVien == idUser && slb.IdBaiTapNavigation.IdKhoaHoc == idKhoaHoc)
                .ToList();
        }
        //lấy cột điểm theo id
        public CotDiem? GetCotDiemById(int idCotDiem)
        {
            return _context.CotDiems.FirstOrDefault(cd => cd.IdCotDiem == idCotDiem);
        }
        // thêm cột điểm
        public void AddCotDiem(CotDiem cotDiem)
        {
            _context.CotDiems.Add(cotDiem);
            _context.SaveChanges();
        }
        // cập nhật cột điểm
        public void UpdateCotDiem(CotDiem cotDiem)
        {
            var existingCotDiem = _context.CotDiems.FirstOrDefault(cd => cd.IdCotDiem == cotDiem.IdCotDiem);
            if (existingCotDiem != null)
            {
                existingCotDiem.TenCotDiem = cotDiem.TenCotDiem;
                existingCotDiem.Diem = cotDiem.Diem;
                _context.SaveChanges();
            }
        }
        // lấy list danh sách sinh viên trong khóa học đó
        public IEnumerable<UserOu> GetSinhVienByKhoaHoc(int idKhoaHoc)
        {
            var sinhVienList = _context.SinhVienKhoaHocs
                .Where(svkh => svkh.IdKhoaHoc == idKhoaHoc)
                .Select(svkh => svkh.IdSinhVienNavigation)
                .ToList();

            return sinhVienList;
        }
        // lấy bài làm theo id
        public SinhVienLamBai? GetSinhVienLamBaibyId(int IdBailam)
        {
            return _context.SinhVienLamBais.FirstOrDefault(bl => bl.IdBaiLam == IdBailam);
        }
    }
}
