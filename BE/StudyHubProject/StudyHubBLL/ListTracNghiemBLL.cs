using StudyHub.DAL.Models;
using StudyHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.BLL
{
    public class ListTracNghiemBLL
    {
        private readonly ListTracNghiemDAL _listTracNghiemDAL;

        public ListTracNghiemBLL()
        {
            _listTracNghiemDAL = new ListTracNghiemDAL();
        }

        public void AddListTracNghiem(ListTracNghiem listTracNghiem)
        {
            _listTracNghiemDAL.AddListTracNghiem(listTracNghiem);
        }

        public ListTracNghiem? GetListTracNghiemById(int id)
        {
            return _listTracNghiemDAL.GetListTracNghiemById(id);
        }
    }
}
