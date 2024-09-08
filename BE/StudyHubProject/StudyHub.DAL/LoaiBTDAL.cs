using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class LoaiBTDAL
    {
        private HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();

        public List<LoaiBaiTap> GetAll()
        {
            return _context.LoaiBaiTaps.ToList();
        }
    }
}
