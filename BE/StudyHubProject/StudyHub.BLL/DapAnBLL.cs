using StudyHub.DAL.Models;
using StudyHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.BLL
{
    public class DapAnBLL
    {
        private readonly DapAnDAL _dapAnDAL;

        public DapAnBLL()
        {
            _dapAnDAL = new DapAnDAL();
        }

        public void AddDapAn(DapAn dapAn)
        {
            _dapAnDAL.AddDapAn(dapAn);
        }

        public IEnumerable<DapAn> GetDapAnsByCauHoiId(int idCauHoi)
        {
            return _dapAnDAL.GetDapAnsByCauHoiId(idCauHoi);
        }




        public void UpdateDapAn(DapAn dapAn)
        {
            _dapAnDAL.UpdateDapAn(dapAn);
        }

        public void DeleteDapAn(int id)
        {
            _dapAnDAL.DeleteDapAn(id);
        }

        public DapAn? GetDapAnById(int id)
        {
            return _dapAnDAL.GetDapAnById(id);
        }
    }
}
