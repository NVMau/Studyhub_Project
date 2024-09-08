using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyHub.BLL;
using StudyHub.DAL;
using StudyHub.DAL.Models;

namespace StudyHub.Controllers
{
    public class BaiTapDto
    {
        public int IdLoaiBaiTap { get; set; }

        public int IdKhoaHoc { get; set; }

        public string TenBaiTap { get; set; } = null!;

        public int ThoiGian { get; set; }
    }

    public class BaiTapWithCauHoiDto
    {
        public BaiTapDto BaiTapDto { get; set; }
        public List<int> IdsCauHoi { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BaiTapController : ControllerBase
    {
        private readonly BaiTapBLL _baiTapBLL;
        private readonly ListTracNghiemBLL _tracNghiemBLL;

        public BaiTapController()
        {
            _baiTapBLL = new BaiTapBLL();
            _tracNghiemBLL = new ListTracNghiemBLL();
        }

        // Lấy danh sách bài tập theo khóa học
        [HttpGet("byKhoaHoc/{idKhoaHoc}")]
        public IActionResult GetBaiTapByKhoaHoc(int idKhoaHoc)
        {
            var baiTaps = _baiTapBLL.GetBaiTapByKhoaHoc(idKhoaHoc);
            if (baiTaps == null || !baiTaps.Any())
            {
                return NotFound("Không có bài tập nào cho khóa học này.");
            }
            return Ok(baiTaps);
        }

        // Thêm bài tập mới
        //[HttpPost]
        //public IActionResult AddBaiTap([FromBody] BaiTapDto baiTapDto)
        //{

        //    var baiTap = new BaiTap
        //    {
        //        IdLoaiBaiTap = baiTapDto.IdLoaiBaiTap,
        //        IdKhoaHoc = baiTapDto.IdKhoaHoc,
        //        TenBaiTap = baiTapDto.TenBaiTap,
        //        ThoiGian = baiTapDto.ThoiGian
        //    };
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var addedBaiTap = _baiTapBLL.AddBaiTap(baiTap);
        //    return CreatedAtAction(nameof(GetBaiTapById), new { id = addedBaiTap.IdBaiTap }, addedBaiTap);
        //}

        // Sửa bài tập
        [HttpPut("{id}")]
        public IActionResult UpdateBaiTap(int id, [FromBody] BaiTap baiTap)
        {
            if (id != baiTap.IdBaiTap || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var updated = _baiTapBLL.UpdateBaiTap(id, baiTap);
            if (!updated)
            {
                return NotFound("Không tìm thấy bài tập với ID được cung cấp.");
            }
            return NoContent();
        }

        // Lấy bài tập theo ID (để sử dụng cho CreatedAtAction)
        [HttpGet("{id}")]
        public IActionResult GetBaiTapById(int id)
        {
            var baiTap = _baiTapBLL.GetBaiTapById(id);
            if (baiTap == null)
            {
                return NotFound("Không tìm thấy bài tập với ID được cung cấp.");
            }
            return Ok(baiTap);
        }

        [HttpGet("{idBaiTap}/cauhoi")]
        public IActionResult GetCauHoiByBaiTap(int idBaiTap)
        {
            var cauHois = _baiTapBLL.GetCauHoiByBaiTap(idBaiTap);
            if (cauHois == null)
            {
                return NotFound($"Không có câu hỏi nào cho bài tập với ID {idBaiTap}.");
            }
            return Ok(cauHois);
        }


        [HttpPost("TaoListTracNghiem")]
        public IActionResult TaoListTracNghiem([FromBody] TaoListTracNghiemRequest request)
        {
            try
            {
                // Gọi phương thức TaoListTracNghiem với các tham số từ request body
                _baiTapBLL.createBaiTapAndListTracNghiem(request.IdKhoaHoc, request.TenBaiTap, request.IdLoaiBaiTap, request.DanhSachCauHoi, request.ThoiGian);
                return Ok("Tạo danh sách trắc nghiệm thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        //xoa bai tap
        // DELETE: api/BaiTap/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBaiTap(int id)
        {
            _baiTapBLL.XoaBaiTap(id);
            return NoContent(); // Trả về mã lỗi 204 khi xóa thành công
        }
    }

    // Class request để nhận dữ liệu từ body của HTTP POST request
    public class TaoListTracNghiemRequest
    {
        public int IdKhoaHoc { get; set; }
        public string TenBaiTap { get; set; }
        public int IdLoaiBaiTap { get; set; }
        public List<int> DanhSachCauHoi { get; set; }
        public int ThoiGian { get; set; }
    }
}