using Microsoft.EntityFrameworkCore;
using StudyHub.DAL;
using StudyHub.DAL.Models;

namespace StudyHub.BLL
{
    public class SinhVienLamBaiBLL
    {
        private readonly SinhVienLamBaiDAL _sinhVienLamBaiDAL;

        public SinhVienLamBaiBLL()
        {
            _sinhVienLamBaiDAL = new SinhVienLamBaiDAL();
        }

        public SinhVienLamBai? GetSinhVienLamBaiById(int id)
        {
            return _sinhVienLamBaiDAL.GetSinhVienLamBaiById(id);
        }
        public SinhVienLamBai? AddSinhVienLamBai(SinhVienLamBai sinhVienLamBai)
        {
            return _sinhVienLamBaiDAL.AddSinhVienLamBai(sinhVienLamBai);
        }
        // lấy bài
        public SinhVienLamBai GetBaiLamByIduserAndBT(int idUser, int idBaiTap)
        {
            return _sinhVienLamBaiDAL.GetBaiLamByIduserAndBT(idUser, idBaiTap);
        }
        
        // cập nhật bài làm
        public Boolean CapNhatNoiDungBaiLam(int idSinhVienLamBai, string noiDungMoi)
        {
            return _sinhVienLamBaiDAL.CapNhatNoiDungBaiLam(idSinhVienLamBai, noiDungMoi);
        }
        // tính điểm và lưu vào hệ thống
        public CotDiem TinhDiemTracNghiem(int IdSinhVien, int IdBaiTap, List<int> IdDapAn)
        {
            return _sinhVienLamBaiDAL.TinhDiemTracNghiem(IdSinhVien, IdBaiTap,IdDapAn);
        }

        // lấy danh sách bài làm theo id bài tập
        public List<object> getBaiLamByBaiTap(int idBaiTap)
        {
            return _sinhVienLamBaiDAL.getBaiLamByBaiTap(idBaiTap);
        }

        // Các phương thức khác như Add, Update, Delete có thể được thêm vào đây nếu cần
    }
}
