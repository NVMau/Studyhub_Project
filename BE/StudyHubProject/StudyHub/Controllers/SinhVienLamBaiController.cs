//using Microsoft.AspNetCore.Mvc;
//using StudyHub.BLL;
//using StudyHub.DAL.Models;

//namespace StudyHub.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SinhVienLamBaiController : ControllerBase
//    {
//        private readonly SinhVienLamBaiBLL _sinhVienLamBaiBLL;

//        public SinhVienLamBaiController()
//        {
//            _sinhVienLamBaiBLL = new SinhVienLamBaiBLL();
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetSinhVienLamBaiById(int id)
//        {
//            var sinhVienLamBai = _sinhVienLamBaiBLL.GetSinhVienLamBaiById(id);
//            if (sinhVienLamBai == null)
//            {
//                return NotFound();
//            }
//            return Ok(sinhVienLamBai);
//        }

//        // Các phương thức khác như Add, Update, Delete có thể được thêm vào đây nếu cần
//    }
//}
