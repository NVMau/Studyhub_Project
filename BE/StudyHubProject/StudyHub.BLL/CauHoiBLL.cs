using StudyHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using StudyHub.DAL.Models;

using System.Text;
using System.Threading.Tasks;
using static StudyHub.DAL.CauHoiDAL;

namespace StudyHub.BLL
{
    public class CauHoiBLL
    {
        public readonly CauHoiDAL _cauHoiDAL;
        public CauHoiBLL()
        {
            _cauHoiDAL = new CauHoiDAL();
        }

        public void AddCauHoi(CauHoi cauHoi)
        {
            _cauHoiDAL.AddCauHoi(cauHoi);

        }

        public CauHoi? GetCauHoiById(int IdCauHoi)
        {
            return _cauHoiDAL.GetCauHoiById(IdCauHoi);
        }

        public void UpdateCauHoi(CauHoi cauHoi)
        {
            _cauHoiDAL.UpdateCauHoi(cauHoi);
        }

        public void DeleteCauHoi(int idCauHoi)
        {
            _cauHoiDAL.DeleteCauHoi(idCauHoi);
        }

        public List<cauHoiDapAn> GetListCauHoiByBaiTap(int idBaiTap)
        {
            return _cauHoiDAL.GetListCauHoiDapAnByBaiTap(idBaiTap);
        }

        public IEnumerable<CauHoi> GetCauhoisByIdMon(int IdMon)
        {
            return _cauHoiDAL.GetCauhoisByIdMon(IdMon);
        }

        public DapAn? GetDapAnDungById(int IdCauHoi)
        {
            return _cauHoiDAL.GetDapAnDungById(IdCauHoi);

        }

        public string? GetTenBaiTapByCauHoiId(int cauHoiId)
        {
            return _cauHoiDAL.GetTenBaiTapByCauHoiId(cauHoiId);
        }

        public int GetIdBaiTapByCauHoiId(int cauHoiId)
        {
            return _cauHoiDAL.GetIdBaiTapByCauHoiId(cauHoiId);
        }

        public List<CauHoi> LayDanhSachCauHoiTheoMonHocVaLoaiCauHoi(int idMonHoc, int idLoaiCauHoi)
        {
            return _cauHoiDAL.LayDanhSachCauHoiTheoMonHocVaLoaiCauHoi(idMonHoc, idLoaiCauHoi);
        }

        //tạo cau hoi và dap an
        public void CreateQuestionAndAnswer(string noidungCauHoi, int iDmonHoc, int iDloaiCauHoi, List<String> noiDungDapAn, List<Boolean> kq)
        {
            _cauHoiDAL.CreateQuestionAndAnswer( noidungCauHoi,  iDmonHoc,  iDloaiCauHoi, noiDungDapAn, kq);
        }

        public CauHoi? GetCauHoiByIdDapAn(int IdDapAn)
        {
            return _cauHoiDAL.GetCauHoiByIdDapAn(IdDapAn);
        }






    }
}
