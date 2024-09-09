<<<<<<< HEAD
﻿using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class LoaiCauHoiDAL
    {
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();

        //lấy danh sách các loại câu hỏi
        public List<LoaiCauHoi> getAll()
        {
            return _context.LoaiCauHois.ToList();
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
    public class LoaiCauHoiDAL
    {
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();

        //lấy danh sách các loại câu hỏi
        public List<LoaiCauHoi> getAll()
        {
            return _context.LoaiCauHois.ToList();
        }
    }
}
>>>>>>> 13ab140b0e0a0a80fb317b1ba04f4949f0da3a1f
