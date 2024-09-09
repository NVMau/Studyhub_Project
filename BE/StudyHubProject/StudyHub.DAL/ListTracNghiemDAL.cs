<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class ListTracNghiemDAL
    {
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();


        public ListTracNghiemDAL()
        {
            _context = new HeThongQuanLyHocTapContext();
        }


        public void AddListTracNghiem(ListTracNghiem listTracNghiem)
        {
            _context.ListTracNghiems.Add(listTracNghiem);
            try
            {
                _context.SaveChanges();

            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                Console.WriteLine($"Lỗi: {innerException}");
            }
        }


        public ListTracNghiem? GetListTracNghiemById(int id)
        {
            return _context.ListTracNghiems.FirstOrDefault(d => d.IdTracNghiem == id);
        }


    }
}
=======
﻿using Microsoft.EntityFrameworkCore;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class ListTracNghiemDAL
    {
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();


        public ListTracNghiemDAL()
        {
            _context = new HeThongQuanLyHocTapContext();
        }


        public void AddListTracNghiem(ListTracNghiem listTracNghiem)
        {
            _context.ListTracNghiems.Add(listTracNghiem);
            try
            {
                _context.SaveChanges();

            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                Console.WriteLine($"Lỗi: {innerException}");
            }
        }


        public ListTracNghiem? GetListTracNghiemById(int id)
        {
            return _context.ListTracNghiems.FirstOrDefault(d => d.IdTracNghiem == id);
        }


    }
}
>>>>>>> 13ab140b0e0a0a80fb317b1ba04f4949f0da3a1f
