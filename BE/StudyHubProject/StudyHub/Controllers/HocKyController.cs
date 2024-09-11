using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;

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
