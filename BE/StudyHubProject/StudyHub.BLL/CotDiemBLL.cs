using StudyHub.DAL;
using StudyHub.DAL.Models;
using System.Collections.Generic;

namespace StudyHub.BLL
{
    public class CotDiemBLL
    {
        private readonly CotDiemDAL _cotDiemDAL;

        public CotDiemBLL()
        {
            _cotDiemDAL = new CotDiemDAL(new HeThongQuanLyHocTapContext());
        }

        public SinhVienLamBai AddSinhVienLamBai(SinhVienLamBai sinhVienLamBai)
        {
            return _cotDiemDAL.AddSinhVienLamBai(sinhVienLamBai);
        }

        public IEnumerable<SinhVienLamBai> GetCotDiemsByKhoaHocAndUser(int idKhoaHoc, int idUser)
        {
            return _cotDiemDAL.GetCotDiemsByKhoaHocAndUser(idKhoaHoc, idUser);
        }

        public CotDiem? GetCotDiemById(int idCotDiem)
        {
            return _cotDiemDAL.GetCotDiemById(idCotDiem);
        }

        public void AddCotDiem(CotDiem cotDiem)
        {
            _cotDiemDAL.AddCotDiem(cotDiem);
        }

        public void UpdateCotDiem(CotDiem cotDiem)
        {
            _cotDiemDAL.UpdateCotDiem(cotDiem);
        }

        public IEnumerable<UserOu> GetSinhVienByKhoaHoc(int idKhoaHoc)
        {
            return _cotDiemDAL.GetSinhVienByKhoaHoc(idKhoaHoc);
        }
    }
}
