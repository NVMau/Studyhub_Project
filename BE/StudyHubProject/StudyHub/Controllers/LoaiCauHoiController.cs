using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiCauHoiController : ControllerBase
    {
        private readonly LoaiCauHoiBLL loaiCauHoiBLL = new LoaiCauHoiBLL();

        [HttpGet]
        public IActionResult Get() {
            return Ok(loaiCauHoiBLL.GetALL());
        }
    }
}
