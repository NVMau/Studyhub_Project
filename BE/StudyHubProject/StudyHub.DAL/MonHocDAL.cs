using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class MonHocDAL
    {
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();

        // lấy môn 
        public List<MonHoc> getMonhocs()
        {
            return _context.MonHocs.ToList();
        }

        public IEnumerable<CauHoi> GetCauHoiByMonHoc(int idMonHoc)
        {
            // Lấy danh sách câu hỏi theo môn học bằng `IdMonHoc`
            return _context.MonHocs
                .Where(m => m.IdMonHoc == idMonHoc)
                .SelectMany(m => m.CauHois) // Giả định 'm.CauHois' là danh sách các 'CauHoi'
                .ToList();
        }
        // thêm môn
        public void AddMonHoc(MonHoc monHoc)
        {
            _context.MonHocs.Add(monHoc);
            _context.SaveChanges();
        }

        public bool UpdateMonHoc(MonHoc monHoc)
        {
            var mh = _context.MonHocs.FirstOrDefault(m => m.IdMonHoc == monHoc.IdMonHoc);
            if (mh == null)
                return false;
            else
            {
                mh.TenMonHoc = monHoc.TenMonHoc;
                mh.SoTinChi = monHoc.SoTinChi;
                _context.MonHocs.Update(mh);
                _context.SaveChanges();
                return true;
            }
            
        }

        public void DeleteMonHoc(int id)
        {
            var mh = _context.MonHocs.FirstOrDefault(m => m.IdMonHoc == id);
            _context.MonHocs.Remove(mh);
            _context.SaveChanges();
        }

        public MonHoc? GetMonHocById(int id)
        {
            return _context.MonHocs.FirstOrDefault(m => m.IdMonHoc == id);
        }
    }
}