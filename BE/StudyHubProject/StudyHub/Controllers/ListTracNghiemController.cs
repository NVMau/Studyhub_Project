using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;
using StudyHub.DAL.Models;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListTracNghiemController : ControllerBase
    {
        private readonly ListTracNghiemBLL _listTracNghiemBLL;

        public ListTracNghiemController()
        {
            _listTracNghiemBLL = new ListTracNghiemBLL();
        }
        public class ListTracNghienDto
        {
            public int IdBaiTap { get; set; }
            public int IdCauHoi { get; set; }
        }


        [HttpGet("{id}")]
        public IActionResult GetListTracNghiemById(int id)
        {
            var listTracNghiem = _listTracNghiemBLL.GetListTracNghiemById(id);
            if (listTracNghiem == null)
            {
                return NotFound();
            }
            return Ok(listTracNghiem);
        }




        [HttpPost]
        public IActionResult AddListTracNghiem([FromBody] ListTracNghienDto listTracNghienDto)
        {
            var listracnghiem = new ListTracNghiem
            {
                IdBaiTap = listTracNghienDto.IdBaiTap,
                IdCauHoi = listTracNghienDto.IdCauHoi,
            };

            try
            {
                _listTracNghiemBLL.AddListTracNghiem(listracnghiem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetListTracNghiemById), new { id = listracnghiem.IdTracNghiem }, listracnghiem);
        }
    }
}
