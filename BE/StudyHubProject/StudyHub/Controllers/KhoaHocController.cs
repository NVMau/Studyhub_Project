using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;
using StudyHub.DAL.Models;
using StudyHub.DAL;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocController : ControllerBase
    {

        private readonly KhoaHocBLL khoaHocBLL;

        public KhoaHocController()
        {
            khoaHocBLL = new KhoaHocBLL();
        }


        [HttpGet]
        public IActionResult GetListKhoaHoc()
        {
            return Ok(khoaHocBLL.GetListKhoaHoc());
        }

        // API tìm kiếm khóa học theo tên
        [HttpGet("search")]
        public IActionResult GetKhoaHocByTen([FromQuery] string tenKhoaHoc)
        {
            var khoaHocs = khoaHocBLL.GetKhoaHocByTen(tenKhoaHoc);
            if (khoaHocs == null || !khoaHocs.Any())
            {
                return NotFound("Không tìm thấy khóa học nào với tên được cung cấp.");
            }
            return Ok(khoaHocs);
        }


        [HttpGet("{userId}/{hocKyId}")]
        public IActionResult GetListKhoaHocByUserAndByHocKy(int userId, int hocKyId)
        {
            var listKH = khoaHocBLL.GetListKhoaHocByUserAndByHocKy(userId, hocKyId);
            return Ok(listKH);
        }

        [HttpGet("khoaHocByGVHK/{idGV}/{hocKyId}")]
        public IActionResult LayDanhSachKhoaHocTheoGiangVienVaHocKy(int idGV, int hocKyId)
        {
            var listKH = khoaHocBLL.LayDanhSachKhoaHocTheoGiangVienVaHocKy(idGV, hocKyId);
            return Ok(listKH);
        }
        // lấy theo user
        [HttpGet("getbyuser/{userId}")]
        public IActionResult GetListKhoaHocByUser(int userId)
        {
            var listKH = khoaHocBLL.GetListKhoaHocByUser(userId);
            return Ok(listKH);
        }
    }
}
