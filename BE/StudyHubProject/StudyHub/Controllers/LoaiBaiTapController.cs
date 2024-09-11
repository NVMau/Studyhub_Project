using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiBaiTapController : ControllerBase
    {
        private readonly LoaiBTBLL loaiBTBLL = new LoaiBTBLL();

        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok(loaiBTBLL.GetAll());
        }
    }
}
