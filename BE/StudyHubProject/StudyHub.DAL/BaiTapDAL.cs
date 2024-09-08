using Microsoft.EntityFrameworkCore;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class BaiTapDAL
    {
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();

        public List<BaiTap> GetBaiTapByKhoaHoc(int idKhoaHoc)
        {
            return _context.BaiTaps.Where(b => b.IdKhoaHoc == idKhoaHoc).ToList();
        }

        public BaiTap AddBaiTap(BaiTap baiTap)
        {
            _context.BaiTaps.Add(baiTap);
            _context.SaveChanges();
            return baiTap;
        }

        public bool UpdateBaiTap(int id, BaiTap baiTap)
        {
            var existingBaiTap = _context.BaiTaps.FirstOrDefault(b => b.IdBaiTap == id);
            if (existingBaiTap == null)
            {
                return false;
            }

            existingBaiTap.TenBaiTap = baiTap.TenBaiTap;
            existingBaiTap.ThoiGian = baiTap.ThoiGian;
            existingBaiTap.IdLoaiBaiTap = baiTap.IdLoaiBaiTap;
            existingBaiTap.IdKhoaHoc = baiTap.IdKhoaHoc;

            _context.SaveChanges();
            return true;
        }

        public BaiTap? GetBaiTapById(int id)
        {
            return _context.BaiTaps.FirstOrDefault(b => b.IdBaiTap == id);
        }

        public IEnumerable<CauHoi> GetCauHoiByBaiTap(int idBaiTap)
        {
            return _context.ListTracNghiems
            .Where(ln => ln.IdBaiTap == idBaiTap)
            .Select(ln => ln.IdCauHoiNavigation)
            .ToList();
        }

        public void createBaiTapAndListTracNghiem(int idKhoaHoc, string tenBaiTap, int idLoaiBaiTap, List<int> danhSachIdCauHoi, int thoiGian)
        {
            // Tạo một đối tượng BaiTap mới
            BaiTap baiTap = new BaiTap
            {
                IdKhoaHoc = idKhoaHoc,
                TenBaiTap = tenBaiTap,
                IdLoaiBaiTap = idLoaiBaiTap,
                ThoiGian = thoiGian
            };

            // Thêm bài tập mới vào Context
            _context.BaiTaps.Add(baiTap);
            _context.SaveChanges();

            // Lấy IdBaiTap của bài tập mới tạo
            int idBaiTap = baiTap.IdBaiTap;

            // Tạo danh sách ListTracNghiem từ danhSachIdCauHoi
            List<ListTracNghiem> listTracNghiem = danhSachIdCauHoi.Select(idCauHoi => new ListTracNghiem
            {
                IdBaiTap = idBaiTap,
                IdCauHoi = idCauHoi
            }).ToList();

            // Thêm danh sách ListTracNghiem vào Context
            _context.ListTracNghiems.AddRange(listTracNghiem);
            _context.SaveChanges();
        }

        //xóa bài
        public void XoaBaiTap(int idBaiTap)
        {
            // Lấy bài tập cần xóa từ cơ sở dữ liệu
            var baiTap = _context.BaiTaps.Include(b => b.ListTracNghiems).FirstOrDefault(b => b.IdBaiTap == idBaiTap);

            // Nếu bài tập không tồn tại
            if (baiTap == null)
            {
                throw new Exception("Bài tập không tồn tại.");
            }

            try
            {
                // Xóa các bản ghi trong bảng ListTracNghiem có tham chiếu đến bài tập
                _context.ListTracNghiems.RemoveRange(baiTap.ListTracNghiems);

                // Xóa bài tập khỏi cơ sở dữ liệu
                _context.BaiTaps.Remove(baiTap);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và ghi log nếu cần
                Console.WriteLine($"Lỗi khi xóa bài tập: {ex.Message}");
                // Hoặc có thể throw lại ngoại lệ để xử lý ở lớp gọi
                throw;
            }
        }
    }
}
