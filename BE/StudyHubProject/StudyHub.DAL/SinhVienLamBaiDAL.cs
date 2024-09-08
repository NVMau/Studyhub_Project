using Microsoft.EntityFrameworkCore;
using StudyHub.DAL.Models;
using System.Linq;

namespace StudyHub.DAL
{
    public class SinhVienLamBaiDAL
    {
        private readonly HeThongQuanLyHocTapContext _context;

        public SinhVienLamBaiDAL()
        {
            _context = new HeThongQuanLyHocTapContext();
        }

        // lấy làm bài theo id
        public SinhVienLamBai? GetSinhVienLamBaiById(int id)
        {
            return _context.SinhVienLamBais.FirstOrDefault(slb => slb.IdBaiLam == id);
        }
        // thêm bài làm vào dữ liệu
        public SinhVienLamBai? AddSinhVienLamBai(SinhVienLamBai sinhVienLamBai)
        {
            _context.SinhVienLamBais.Add(sinhVienLamBai);
            _context.SaveChanges();
            return sinhVienLamBai;
        }
        // lấy nội dung bài làm theo user và bai tập
        public SinhVienLamBai GetBaiLamByIduserAndBT(int idUser, int idBaiTap)
        {
            var sinhVienLamBai = _context.SinhVienLamBais
            .FirstOrDefault(s => s.IdSinhVienNavigation.IdUser == idUser && s.IdBaiTap == idBaiTap);

            if (sinhVienLamBai != null)
                return sinhVienLamBai;
            else
                return null;

        }
        // cập nhật bài làm
        public Boolean CapNhatNoiDungBaiLam(int idSinhVienLamBai, string noiDungMoi)
        {
            var sinhVienLamBai = _context.SinhVienLamBais
                    .FirstOrDefault(s => s.IdBaiLam == idSinhVienLamBai);

            if (sinhVienLamBai != null)
            {
                sinhVienLamBai.NoiDungBaiLam = noiDungMoi;
                _context.SaveChanges();
                return true;
            }
            else
                return false;

        }
        // tính điểm trắc nghiệm truyền idsinhvien, idBaiTap, list idDapAn
        // kết quả nhận về là cột điểm 
        // tạo bài làm trước xong tạo cột điểm
        // tạo đối tượng xử lý với DapAn
        private readonly DapAnDAL dapAnDAL = new DapAnDAL();
        private readonly BaiTapDAL baiTapDAL = new BaiTapDAL();
        public CotDiem TinhDiemTracNghiem(int IdSinhVien, int IdBaiTap, List<int> IdDapAn)
        {
            var diem = 0.0;
            int soluong = 0;
            foreach (var id in IdDapAn)
            {
                if (id == 0)
                    continue;
                var dapAn = dapAnDAL.GetDapAnById(id);// lấy đối tượng đáp án
                if (dapAn != null)
                {
                    if (dapAn.KetQua)// nếu là đáp án đúng thì cộng điểm
                        soluong++;
                }
            }
            double rawDiem = ((double)soluong / IdDapAn.Count) * 10.0;
            diem = Math.Round(rawDiem, 1); // Làm tròn kết quả với một số sau dấu chấm

            // tạo đối tượng cột điểm
            var cotDiem = new CotDiem
            {
                TenCotDiem = baiTapDAL.GetBaiTapById(IdBaiTap).TenBaiTap,
                Diem = diem
            };
            _context.CotDiems.Add(cotDiem);
            _context.SaveChanges();
            // tạo đối tượng bài làm
            var baiLam = new SinhVienLamBai
            {
                IdSinhVien = IdSinhVien,
                IdBaiTap = IdBaiTap,
                IdCotDiem = cotDiem.IdCotDiem
            };
            _context.SinhVienLamBais.Add(baiLam);
            _context.SaveChanges();
            return cotDiem;
        }
        // lấy danh sách bài làm của sinh viên theo bài tập
        public List<object> getBaiLamByBaiTap(int idBaiTap)
        {
            List<object> result = new List<object>();

            // Lấy danh sách SinhVienLamBai với thông tin CotDiem và thông tin sinh viên
            var danhSachSinhVienLamBai = _context.SinhVienLamBais
                .Where(sv => sv.IdBaiTap == idBaiTap) // Lọc theo IdBaiTap
                .Select(sv => new
                {
                    sv.IdBaiLam,
                    sv.IdSinhVien,
                    sv.IdBaiTap,
                    sv.IdCotDiem,
                    sv.NoiDungBaiLam,
                    SinhVien = _context.UserOus.FirstOrDefault(u => u.IdUser == sv.IdSinhVien), // Lấy thông tin sinh viên
                    CotDiem = _context.CotDiems.FirstOrDefault(cd => cd.IdCotDiem == sv.IdCotDiem) // Lấy thông tin từ bảng CotDiem
                })
                .ToList();

            // Tạo danh sách các object để trả về
            foreach (var sinhVienLamBai in danhSachSinhVienLamBai)
            {
                // Tạo một dictionary để chứa thông tin từ cả ba bảng SinhVienLamBai, CotDiem và UserOu
                var baiLamInfo = new Dictionary<string, object>
                {
                    { "IdBaiLam", sinhVienLamBai.IdBaiLam },
                    { "IdSinhVien", sinhVienLamBai.IdSinhVien },
                    { "HoTenSinhVien", $"{sinhVienLamBai.SinhVien.FirstName} {sinhVienLamBai.SinhVien.LastName}" }, // Họ và tên của sinh viên
                    { "IdBaiTap", sinhVienLamBai.IdBaiTap },
                    { "IdCotDiem", sinhVienLamBai.IdCotDiem },
                    { "NoiDungBaiLam", sinhVienLamBai.NoiDungBaiLam }
                };

                // Nếu có thông tin từ bảng CotDiem
                if (sinhVienLamBai.CotDiem != null)
                {
                    baiLamInfo.Add("CotDiem", new
                    {
                        IdCotDiem = sinhVienLamBai.CotDiem.IdCotDiem,
                        TenCotDiem = sinhVienLamBai.CotDiem.TenCotDiem,
                        Diem = sinhVienLamBai.CotDiem.Diem
                    });
                }
                else
                {
                    baiLamInfo.Add("CotDiem", null); // Nếu không có thông tin từ bảng CotDiem, thêm null vào
                }

                // Thêm dictionary này vào danh sách kết quả
                result.Add(baiLamInfo);
            }

            return result;
        }

    }
}
