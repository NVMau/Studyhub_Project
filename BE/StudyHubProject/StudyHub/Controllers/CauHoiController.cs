using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;
using StudyHub.DAL;
using StudyHub.DAL.Models;
using System.Drawing;

namespace StudyHub.Controllers
{
    public class CauHoiDto
    {
        public string NoiDung { get; set; } = "";
        public int IdMonHoc { get; set; }
        public int IdLoaiCauHoi { get; set; }
    }

    public class QuestionRequest
    {
        public string NoiDungCauHoi { get; set; }
        public int IdMonHoc { get; set; }
        public int IdLoaiCauHoi { get; set; }
        public List<string> NoiDungDapAn { get; set; }
        public List<bool> KetQua { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CauHoiController : ControllerBase
    {
        private readonly CauHoiBLL _cauHoiBLL;

        public CauHoiController()
        {
            _cauHoiBLL = new CauHoiBLL();
        }

        [HttpGet("{id}")]
        public IActionResult GetCauHoiById(int id)
        {
            var cauHoi = _cauHoiBLL.GetCauHoiById(id);
            if (cauHoi == null)
            {
                return NotFound();
            }
            return Ok(cauHoi);
        }
        // lấy danh sách theo bài tập
        [HttpGet("byBaiTap/{idBaiTap}")]
        public IActionResult GetListCauHoiByBaiTap(int idBaiTap)
        {
            var listCauHoi = _cauHoiBLL.GetListCauHoiByBaiTap(idBaiTap);
            if (listCauHoi == null)
                return NotFound();
            else
                return Ok(listCauHoi);
        }

        [HttpPost]
        public IActionResult AddCauHoi([FromBody] CauHoiDto cauHoiDto)
        {

            var cauHoi = new CauHoi
            {
                NoiDung = cauHoiDto.NoiDung,
                IdMonHoc = cauHoiDto.IdMonHoc,
                IdLoaiCauHoi = cauHoiDto.IdLoaiCauHoi
            };

            try
            {
                _cauHoiBLL.AddCauHoi(cauHoi);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetCauHoiById), new { id = cauHoi.IdCauHoi }, cauHoi);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCauHoi(int id, [FromBody] CauHoiDto cauHoiDto)
        {
            var cauHoi = _cauHoiBLL.GetCauHoiById(id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            cauHoi.NoiDung = cauHoiDto.NoiDung;
            cauHoi.IdMonHoc = cauHoiDto.IdMonHoc;
            cauHoi.IdLoaiCauHoi = cauHoiDto.IdLoaiCauHoi;

            try
            {
                _cauHoiBLL.UpdateCauHoi(cauHoi);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCauHoi(int id)
        {
            var cauHoi = _cauHoiBLL.GetCauHoiById(id);
            if (cauHoi == null)
            {
                return NotFound();
            }

            try
            {
                _cauHoiBLL.DeleteCauHoi(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpGet("laydapandung/{id}")]
        public IActionResult GetDapAnDungById(int id)
        {
            var dapandung = _cauHoiBLL.GetDapAnDungById(id);
            if (dapandung == null)
            {
                return NotFound();
            }
            return Ok(dapandung);
        }

        [HttpGet("GetListCauHoiByMonAndByType/{idMon}/{idType}")]
        public IActionResult GetCauhoisByIdMon(int idMon, int idType)
        {
            var cauhois = _cauHoiBLL.LayDanhSachCauHoiTheoMonHocVaLoaiCauHoi(idMon, idType);
            return Ok(cauhois);
        }

        //tạo cau hoi và đáp án
        [HttpPost("createCauHoiAndDapAn")]
        public IActionResult CreateQuestionAndAnswer([FromBody] QuestionRequest request)
        {
            _cauHoiBLL.CreateQuestionAndAnswer(request.NoiDungCauHoi, request.IdMonHoc, request.IdLoaiCauHoi, request.NoiDungDapAn, request.KetQua);
            return Ok("Question and answers created successfully");
        }

    }
}
