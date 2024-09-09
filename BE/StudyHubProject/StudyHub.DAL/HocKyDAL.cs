<<<<<<< HEAD
﻿using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class HocKyDAL
    {
        HeThongQuanLyHocTapContext context = new HeThongQuanLyHocTapContext();

        //lấy list HocKy
        public List<HocKy> GetListHocKy()
        {
            return context.HocKies.ToList();
        }

        //lấy HocKybyid
        public HocKy GetHocKybyid(int id)
        {
            return context.HocKies.Find(id);
        }

        // thêm
        public void AddHocKy(string namHocKy)
        {
            var hocky = new HocKy();
            hocky.NamHocKy = namHocKy;
            context.HocKies.Add(hocky);
            context.SaveChanges();
        }
        // sửa
        public void updateHocKy(HocKy hocky)
        {
            var hk = context.HocKies.FirstOrDefault(m => m.IdHocKy == hocky.IdHocKy);
            hk.NamHocKy = hocky.NamHocKy;
            context.HocKies.Update(hk);
            context.SaveChanges();
        }
        // xóa
        public void DeleteHocKy(int idHocKy)
        {
            var hocky = context.HocKies.FirstOrDefault(c => c.IdHocKy == idHocKy);
            if (hocky == null)
            {
                throw new Exception("Câu hỏi với ID được cung cấp không tồn tại.");
            }

            context.HocKies.Remove(hocky);
            context.SaveChanges();
        }


    }
}
=======
﻿using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class HocKyDAL
    {
        HeThongQuanLyHocTapContext context = new HeThongQuanLyHocTapContext();

        //lấy list HocKy
        public List<HocKy> GetListHocKy()
        {
            return context.HocKies.ToList();
        }

        //lấy HocKybyid
        public HocKy GetHocKybyid(int id)
        {
            return context.HocKies.Find(id);
        }

        // thêm
        public void AddHocKy(string namHocKy)
        {
            var hocky = new HocKy();
            hocky.NamHocKy = namHocKy;
            context.HocKies.Add(hocky);
            context.SaveChanges();
        }
        // sửa
        public void updateHocKy(HocKy hocky)
        {
            var hk = context.HocKies.FirstOrDefault(m => m.IdHocKy == hocky.IdHocKy);
            hk.NamHocKy = hocky.NamHocKy;
            context.HocKies.Update(hk);
            context.SaveChanges();
        }
        // xóa
        public void DeleteHocKy(int idHocKy)
        {
            var hocky = context.HocKies.FirstOrDefault(c => c.IdHocKy == idHocKy);
            if (hocky == null)
            {
                throw new Exception("Câu hỏi với ID được cung cấp không tồn tại.");
            }

            context.HocKies.Remove(hocky);
            context.SaveChanges();
        }


    }
}
>>>>>>> 13ab140b0e0a0a80fb317b1ba04f4949f0da3a1f
