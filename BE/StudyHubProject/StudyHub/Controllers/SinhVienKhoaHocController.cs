using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyHub.BLL;
using StudyHub.DAL;
using StudyHub.DAL.Models;

namespace StudyHub.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienKhoaHocController : ControllerBase
    {
        private readonly SinhVienKhoaHocBLL _sinhVienKhoaHocBLL =  new SinhVienKhoaHocBLL();
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();
        //đối tượng dùng để truyền qua form
        public class DangKyMonHocDTO
        {
            public int IdKhoaHoc { get; set; }
            public int IdSinhVien { get; set; }
        }

        // Đăng ký môn học
        [HttpPost("dangkykhoahoc")]
        public IActionResult SinhVienDangKyMonHoc([FromBody] DangKyMonHocDTO dangKyDTO)
        {
            try
            {
                if (dangKyDTO.IdKhoaHoc == null || dangKyDTO.IdSinhVien == null)
                {
                    return StatusCode(422, "Khóa học hoặc sinh viên không tồn tại");
                }
                else
                {
                    var daDangKy = _context.SinhVienKhoaHocs.Any(svkh => svkh.IdSinhVien == dangKyDTO.IdSinhVien && svkh.IdKhoaHoc == dangKyDTO.IdKhoaHoc);
                    if (daDangKy)
                        return StatusCode(422, "Bạn đã đăng ký khóa học này");
                    else
                    {
                        _sinhVienKhoaHocBLL.SinhVienDangKyKhoaHoc(dangKyDTO.IdKhoaHoc, dangKyDTO.IdSinhVien);
                        return Ok("Sinh viên đã đăng ký khóa học thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return StatusCode(500, $"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        // Hủy đăng ký môn học
        [HttpDelete("huydangky/{id}")]
        public IActionResult HuyDangKyMonHoc(int id)
        {
            if (!_sinhVienKhoaHocBLL.DeleteSinhVienKhoaHoc(id))
            {
                return NotFound("Không tìm thấy đăng ký môn học để hủy.");
            }else return NoContent();
        }

        // lấy list khoa học
        [HttpGet("{iduser}")]
        public IActionResult GetListKhoaHoc(int iduser)
        {
            var listKH = _sinhVienKhoaHocBLL.GetListKhoaHocBySinhVien(iduser);
            return Ok(listKH);
        }
        // lấy list sinh viên theo khóa

        [HttpGet("laydanhsachsinhvientheoKhoa/{idkhoa}")]
        public IActionResult GetSinhVienByKhoaHoc(int idkhoa)
        {
            var listSV = _sinhVienKhoaHocBLL.GetSinhVienByKhoaHoc(idkhoa);
            return Ok(listSV);
        }
    }
}
