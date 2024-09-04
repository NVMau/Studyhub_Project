using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.DAL.Models;
using StudyHub.DAL;
using StudyHub.BLL;

namespace StudyHub.Controllers
{

    public class DapAnDto
    {
        public string NoiDung { get; set; } = "";
        public bool KetQua { get; set; }
        public int IdCauHoi { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class DapAnController : ControllerBase
    {
        private readonly DapAnBLL _dapAnBLL;

        public DapAnController()
        {
            _dapAnBLL = new DapAnBLL();
        }

        [HttpGet("{id}")]
        public IActionResult GetDapAnById(int id)
        {
            var dapAn = _dapAnBLL.GetDapAnById(id);
            if (dapAn == null)
            {
                return NotFound();
            }
            return Ok(dapAn);
        }

        //lấy danh sách đap án theo cau hoi
        [HttpGet("byCauHoi/{idCauHoi}")]
        public IActionResult GetDapAnsByCauHoiId(int idCauHoi)

        {
            var dapAns = _dapAnBLL.GetDapAnsByCauHoiId(idCauHoi);
            if (dapAns == null || !dapAns.Any())
            {
                return NotFound("Không tìm thấy đáp án nào cho câu hỏi này.");
            }
            return Ok(dapAns);
        }



        [HttpPost]
        public IActionResult AddDapAn([FromBody] DapAnDto dapAnDto)
        {
            var dapAn = new DapAn
            {
                NoiDung = dapAnDto.NoiDung,
                KetQua = dapAnDto.KetQua,
                IdCauHoi = dapAnDto.IdCauHoi
            };

            try
            {
                _dapAnBLL.AddDapAn(dapAn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetDapAnById), new { id = dapAn.IdDapAn }, dapAn);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDapAn(int id, [FromBody] DapAnDto dapAnDto)
        {
            var dapAn = new DapAn
            {
                IdDapAn = id,
                NoiDung = dapAnDto.NoiDung,
                KetQua = dapAnDto.KetQua,
                IdCauHoi = dapAnDto.IdCauHoi
            };

            try
            {
                _dapAnBLL.UpdateDapAn(dapAn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(dapAn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDapAn(int id)
        {
            try
            {
                _dapAnBLL.DeleteDapAn(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
