using StudyHub.DAL;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.BLL
{
    public class LoaiBTBLL
    {
        private readonly LoaiBTDAL loaiBTDAL = new LoaiBTDAL();

        public List<LoaiBaiTap> GetAll()
        { 
            return loaiBTDAL.GetAll();
        }

    }
}
