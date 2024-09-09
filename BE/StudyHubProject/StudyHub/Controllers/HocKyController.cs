<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;
using StudyHub.DAL;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocKyController : ControllerBase
    {
        private readonly HocKyBLL hocKyBLL;

        public HocKyController()
        {
            hocKyBLL = new HocKyBLL();
        }


        [HttpGet]
        public IActionResult GetListHocKy()
        {
            return Ok(hocKyBLL.GetListHocKy());
        }
    }
}
=======
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;
using StudyHub.DAL;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocKyController : ControllerBase
    {
        private readonly HocKyBLL hocKyBLL;

        public HocKyController()
        {
            hocKyBLL = new HocKyBLL();
        }


        [HttpGet]
        public IActionResult GetListHocKy()
        {
            return Ok(hocKyBLL.GetListHocKy());
        }
    }
}
>>>>>>> 13ab140b0e0a0a80fb317b1ba04f4949f0da3a1f
