using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class CauHoiDAL
    {
        HeThongQuanLyHocTapContext context = new HeThongQuanLyHocTapContext();

        public IEnumerable<CauHoi> GetCauhoisByIdMon(int IdMon)
        {
            // Sử dụng Include để tải trước danh sách các đáp án
            var cauHois = context.CauHois.Where(b => b.IdMonHoc == IdMon).ToList();
            return cauHois;
        }

        public List<CauHoi> LayDanhSachCauHoiTheoMonHocVaLoaiCauHoi(int idMonHoc, int idLoaiCauHoi)
        {
            var query = from cauHoi in context.CauHois
                        where cauHoi.IdMonHoc == idMonHoc && cauHoi.IdLoaiCauHoi == idLoaiCauHoi
                        select cauHoi;
            return query.ToList();
        }

        public CauHoi? GetCauHoiById(int cauHoiId)
        {
            // Sử dụng Include để tải trước danh sách các đáp án
            var cauHoi = context.CauHois
                .Include(ch => ch.DapAns)
                .FirstOrDefault(ch => ch.IdCauHoi == cauHoiId);
            return cauHoi;
        }

        public void AddCauHoi(CauHoi cauHoi)
        {
            // Kiểm tra và gán MonHoc
            var monHoc = context.MonHocs.FirstOrDefault(m => m.IdMonHoc == cauHoi.IdMonHoc);
            if (monHoc == null)
            {
                throw new Exception("Môn học với ID được cung cấp không tồn tại.");
            }
            cauHoi.IdMonHocNavigation = monHoc;

            // Kiểm tra và gán LoaiCauHoi
            var loaiCauHoi = context.LoaiCauHois.FirstOrDefault(l => l.IdLoaiCauHoi == cauHoi.IdLoaiCauHoi);
            if (loaiCauHoi == null)
            {
                throw new Exception("Loại câu hỏi với ID được cung cấp không tồn tại.");
            }
            cauHoi.IdLoaiCauHoiNavigation = loaiCauHoi;

            // Thêm câu hỏi đã cập nhật vào cơ sở dữ liệu
            context.CauHois.Add(cauHoi);
            context.SaveChanges();
        }

        public void UpdateCauHoi(CauHoi cauHoi)
        {
            // Kiểm tra MonHoc
            var monHoc = context.MonHocs.FirstOrDefault(m => m.IdMonHoc == cauHoi.IdMonHoc);
            if (monHoc == null)
            {
                throw new Exception("Môn học với ID được cung cấp không tồn tại.");
            }
            cauHoi.IdMonHocNavigation = monHoc;

            // Kiểm tra LoaiCauHoi
            var loaiCauHoi = context.LoaiCauHois.FirstOrDefault(l => l.IdLoaiCauHoi == cauHoi.IdLoaiCauHoi);
            if (loaiCauHoi == null)
            {
                throw new Exception("Loại câu hỏi với ID được cung cấp không tồn tại.");
            }
            cauHoi.IdLoaiCauHoiNavigation = loaiCauHoi;

            // Cập nhật thông tin câu hỏi
            context.CauHois.Update(cauHoi);
            context.SaveChanges();
        }
        public void DeleteCauHoi(int CauHoiId)
        {
            var cauHoi = context.CauHois.FirstOrDefault(c => c.IdCauHoi == CauHoiId);
            if (cauHoi == null)
            {
                throw new Exception("Câu hỏi với ID được cung cấp không tồn tại.");
            }

            context.CauHois.Remove(cauHoi);
            context.SaveChanges();
        }


        public CauHoi? GetCauHoiByIdDapAn(int IdDapAn)
        {
            var dapAn = context.DapAns.FirstOrDefault(da => da.IdDapAn == IdDapAn);

            return context.CauHois
                .Include(ch => ch.DapAns)
                .FirstOrDefault(ch => ch.IdCauHoi == dapAn.IdCauHoi);
        }




        public class cauHoiDapAn()
        {
            public int idCauHoi { get; set; }
            public String noiDungCauHoi { get; set; }
            public List<DapAn> listDapAn { get; set; }
        }

        //lấy list câu hỏi theo bài tập
        public List<cauHoiDapAn> GetListCauHoiDapAnByBaiTap(int idBaiTap)
        {
            var danhSachCauHoiDapAn = new List<cauHoiDapAn>();

            var danhSachCauHoi = context.ListTracNghiems
                .Include(ltn => ltn.IdCauHoiNavigation.DapAns) // Kết hợp danh sách đáp án từ bảng CauHoi
                .Where(ltn => ltn.IdBaiTapNavigation.IdBaiTap == idBaiTap) // Lọc ListTracNghiem theo IdBaiTap
                .Select(ltn => ltn.IdCauHoiNavigation) // Chọn các IdCauHoiNavigation tương ứng
                .ToList();

            foreach (var cauHoi in danhSachCauHoi)
            {
                var cauHoiDapAn = new cauHoiDapAn
                {
                    idCauHoi = cauHoi.IdCauHoi,
                    noiDungCauHoi = cauHoi.NoiDung,
                    listDapAn = cauHoi.DapAns.ToList()
                };
                danhSachCauHoiDapAn.Add(cauHoiDapAn);
            }

            return danhSachCauHoiDapAn;
        }

        public DapAn? GetDapAnDungById(int cauHoiId)
        {
            // Tìm câu hỏi bằng Id
            var dapAnDung = context.DapAns.FirstOrDefault(da => da.IdCauHoi == cauHoiId && da.KetQua == true);
            // Trả về đáp án đúng (nếu có)
            return dapAnDung;

        }

        public string? GetTenBaiTapByCauHoiId(int cauHoiId)
        {

            var liscauhoi = context.ListTracNghiems.FirstOrDefault(ls => ls.IdCauHoi == cauHoiId);
            var baitap = context.BaiTaps.FirstOrDefault(bt => bt.IdBaiTap == liscauhoi.IdBaiTap);
            if (baitap != null && liscauhoi != null)
            {
                return baitap.TenBaiTap;
            }

            return null; // Trả về null nếu không tìm thấy hoặc không có bài tập liên quan
        }

        public int GetIdBaiTapByCauHoiId(int cauHoiId)
        {

            var liscauhoi = context.ListTracNghiems.FirstOrDefault(ls => ls.IdCauHoi == cauHoiId);
            var baitap = context.BaiTaps.FirstOrDefault(bt => bt.IdBaiTap == liscauhoi.IdBaiTap);
            if (baitap != null && liscauhoi != null)
            {
                return baitap.IdBaiTap;
            }

            return -1; // Trả về null nếu không tìm thấy hoặc không có bài tập liên quan
        }

        // tạo câu hỏi và list đáp án 
        public void CreateQuestionAndAnswer(string noidungCauHoi, int iDmonHoc, int iDloaiCauHoi, List<String> noiDungDapAn, List<Boolean> kq)
        {
            // Tạo câu hỏi
            CauHoi cauHoi = new CauHoi
            {
                NoiDung = noidungCauHoi,
                IdMonHoc = iDmonHoc,
                IdLoaiCauHoi = iDloaiCauHoi
            };

            // Thêm câu hỏi vào database hoặc context của bạn
            context.CauHois.Add(cauHoi);
            context.SaveChanges();

            // Lấy id của câu hỏi vừa tạo
            int idCauHoiMoi = cauHoi.IdCauHoi;

            // Tạo đối tượng DapAn cho mỗi đáp án và liên kết với câu hỏi vừa tạo
            for (int i = 0; i < noiDungDapAn.Count; i++)
            {
                DapAn dapAn = new DapAn
                {
                    NoiDung = noiDungDapAn[i],
                    KetQua = kq[i],
                    IdCauHoi = idCauHoiMoi
                };
                context.DapAns.Add(dapAn);
            }
            context.SaveChanges();
        }
    }
}
