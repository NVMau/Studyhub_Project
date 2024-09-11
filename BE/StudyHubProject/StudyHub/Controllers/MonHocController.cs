using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;
using StudyHub.BLL.StudyHub.BLL;
using StudyHub.DAL.Models;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        private readonly MonHocBLL _monHocBLL;

        public MonHocController()
        {
            _monHocBLL = new MonHocBLL();
        }

        //lấy danh sách đap án theo cau hoi
        [HttpGet("byMonHoc/{id}")]
        public IActionResult GetCauHoiByMonHoc(int id)

        {
            var Mon = _monHocBLL.GetCauHoiByMonHoc(id);
            if (Mon == null || !Mon.Any())
            {
                return NotFound("Không tìm thấy đáp án nào cho câu hỏi này.");
            }
            return Ok(Mon);
        }


        // Lấy môn học theo ID (để sử dụng cho CreatedAtAction)
        [HttpGet("{id}")]
        public IActionResult GetMonHocById(int id)
        {
            var monHoc = _monHocBLL.GetMonHocById(id);
            if (monHoc == null)
            {
                return NotFound("Không tìm thấy môn học với ID được cung cấp.");
            }
            return Ok(monHoc);
        }
    }
}
