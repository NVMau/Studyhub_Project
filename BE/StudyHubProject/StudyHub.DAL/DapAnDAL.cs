using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class DapAnDAL
    {
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();

        public void AddDapAn(DapAn dapAn)
        {
            _context.DapAns.Add(dapAn);
            _context.SaveChanges();
        }


        public IEnumerable<DapAn> GetDapAnsByCauHoiId(int idCauHoi)
        {
            return _context.DapAns.Where(d => d.IdCauHoi == idCauHoi).ToList();
        }


        public void UpdateDapAn(DapAn dapAn)
        {
            var existing = _context.DapAns.Find(dapAn.IdDapAn);
            if (existing != null)
            {
                existing.NoiDung = dapAn.NoiDung;
                existing.IdCauHoi = dapAn.IdCauHoi;
                existing.KetQua = dapAn.KetQua;
                _context.SaveChanges();
            }
        }

        public void DeleteDapAn(int id)
        {
            var dapAn = _context.DapAns.Find(id);
            if (dapAn != null)
            {
                _context.DapAns.Remove(dapAn);
                _context.SaveChanges();
            }
        }

        // lây đáp án theo id
        public DapAn? GetDapAnById(int id)
        {
            return _context.DapAns.FirstOrDefault(d => d.IdDapAn == id);
        }
    }
}
