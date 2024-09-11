using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;
using StudyHub.DAL;
using StudyHub.DAL.Models;
using System.Drawing;

namespace StudyHub.Controllers
{
    public class SinhVienLamBaiDto
    {
        public int IdSinhVien { get; set; }
        public int IdBaiTap { get; set; }
        public string? NoiDungBaiLam { get; set; }
        public double Diem { get; set; } // Thêm thuộc tính Diem
    }
    public class NhapDiemTuLuan
    {
        public int IdSinhVien { get; set; }
        public int IdBaiLam { get; set; }
        public double Diem { get; set; } // Thêm thuộc tính Diem
    }

    public class LamBaiTuLuan
    {
        public int IdSinhVien { get; set; }
        public int IdBaiTap { get; set; }
        public string? NoiDungBaiLam { get; set; }
    }
    public class TraloiSinhVienDTO
    {
        public int IdDapan { get; set; }
    }

    public class SinhVienKhoaHocforhgetCDsDto
    {
        public int IdSinhVien { get; set; }
        public int IdKhoaHoc { get; set; }
    }
    public class DiemofsinhvienDto
    {
        public string Tencotdiem { get; set; }
        public double Diem { get; set; }
    }
    public class KetQuaKhoaHoc
    {
        public string TenKhoaHoc { get; set; } = null!;
        public int IdKhoaHoc { get; set; }
        public List<DiemofsinhvienDto> Diems { get; set; }
    }

    public class BangDiemtheoKy
    {
        public int IdSinhVien { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NamHocKy { get; set; } = null!;
        public List<KetQuaKhoaHoc> KetQuaKhoaHocs { get; set; }
    }

    public class SinhVienDiemDto
    {
        public int IdSinhVien { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<DiemofsinhvienDto> Diems { get; set; }
    }

    public class BangDiemtheoKH
    {
        public string TenKhoaHoc { get; set; } = null!;
        public string NamHocKy { get; set; } = null!;
        public List<SinhVienDiemDto> SinhViens { get; set; }
    }
    // tinh diem model
    public class TinhDiem
    {
        public int IDSinhVien { get; set; }
        public int IDBaiTap { get; set; }
        public List<int> DapAn { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class XyLyLamBaiController : ControllerBase
    {
        private readonly CotDiemBLL _cotDiemBLL;
        private readonly BaiTapBLL _baiTapBLL;
        private readonly SinhVienLamBaiBLL _sinhVienLamBaiBLL;
        private readonly CauHoiBLL _cauHoiBLL;
        private readonly KhoaHocBLL _khoaHocBLL;
        private readonly UserBLL _userBLL;
        private readonly SinhVienKhoaHocBLL _sinhVienKhoaHocBLL;
        public XyLyLamBaiController()
        {
            _cotDiemBLL = new CotDiemBLL();
            _baiTapBLL = new BaiTapBLL();
            _sinhVienLamBaiBLL = new SinhVienLamBaiBLL();
            _cauHoiBLL = new CauHoiBLL();
            _khoaHocBLL = new KhoaHocBLL();
            _userBLL = new UserBLL();
            _sinhVienKhoaHocBLL = new SinhVienKhoaHocBLL();
        }
        //API nhap diem cho sinh vien làm bài tự luận 
        [HttpPost("nhap-diem-tu-luan")]
        public IActionResult NhapDiemBaiTuLuan([FromBody] NhapDiemTuLuan nhapDiemTuLuan)
        {
            var sinhvienlambai = _sinhVienLamBaiBLL.GetSinhVienLamBaiById(nhapDiemTuLuan.IdBaiLam);
            if(sinhvienlambai == null)
                return NotFound();
            //Update điểm lại sau khi chấm 
            var cotDiem = _cotDiemBLL.GetCotDiemById(sinhvienlambai.IdCotDiem);
            if(cotDiem == null)
                return NotFound();
            cotDiem.Diem = nhapDiemTuLuan.Diem;

            _cotDiemBLL.UpdateCotDiem(cotDiem);

            return Ok("Đã lưu điểm thành công cho sinh vien");
        }

        //API làm bài tự luận

        [HttpPost("lam-bai-tu-luan")]
        public IActionResult LamBaiTuLuan([FromBody] LamBaiTuLuan lamBaiTuLuan)
        {
            var sinhvienlambai = new SinhVienLamBai();
            sinhvienlambai.IdSinhVien = lamBaiTuLuan.IdSinhVien;
            sinhvienlambai.IdBaiTap = lamBaiTuLuan.IdBaiTap;
            sinhvienlambai.NoiDungBaiLam = lamBaiTuLuan.NoiDungBaiLam;


            //Tạo đối tượng cột điểm trước do Cotdiem k đc phép null
            var cotDiem = new CotDiem
            {
                TenCotDiem = _baiTapBLL.GetBaiTapById(lamBaiTuLuan.IdBaiTap).TenBaiTap,
                Diem = 0
            };

            _cotDiemBLL.AddCotDiem(cotDiem);
            sinhvienlambai.IdCotDiem = cotDiem.IdCotDiem;
            _sinhVienLamBaiBLL.AddSinhVienLamBai(sinhvienlambai);
            return Ok(sinhvienlambai);
        }

        // API để lưu điểm sinh viên
        [HttpPost("GetCotDiems")]
        public IActionResult GetDSDiembySinhVienKhoaHoc([FromBody] SinhVienKhoaHocforhgetCDsDto sinhVienKhoaHocforhgetCDsDto)
        {
            var sinhVienLamBais = _cotDiemBLL.GetCotDiemsByKhoaHocAndUser(sinhVienKhoaHocforhgetCDsDto.IdKhoaHoc, sinhVienKhoaHocforhgetCDsDto.IdSinhVien);

            if (sinhVienLamBais == null || !sinhVienLamBais.Any())
            {
                return NotFound("Không tìm thấy cột điểm nào cho sinh viên trong khóa học này.");
            }

            var result = new List<object>();
            foreach (var sinhVienLamBai in sinhVienLamBais)
            {
                // Thu thập thông tin cần thiết từ sinhVienLamBai để gửi về client
                var cotDiemInfo = new
                {
                    IdBaiTap =_baiTapBLL.GetBaiTapById(sinhVienLamBai.IdBaiTap).IdBaiTap,

                    TenCotDiem = _baiTapBLL.GetBaiTapById(sinhVienLamBai.IdBaiTap).TenBaiTap,


                    Diem = _cotDiemBLL.GetCotDiemById(sinhVienLamBai.IdCotDiem).Diem
                    //Thêm các thông tin khác nếu cần
                };
                result.Add(cotDiemInfo);
            }
            return Ok(result);
        }

        [HttpGet("getsinhvienlambai/{id}")]
        public IActionResult GetSinhVienLamBaiById(int id)
        {
            var sinhVienLamBai = _sinhVienLamBaiBLL.GetSinhVienLamBaiById(id);
            if (sinhVienLamBai == null)
            {
                return NotFound();
            }
            return Ok(sinhVienLamBai);
        }

        // API để lưu điểm sinh viên
        [HttpPost("luuDiem")]
        public IActionResult AddCotDiem([FromBody] SinhVienLamBaiDto sinhVienLamBaiDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var baiTap = _baiTapBLL.GetBaiTapById(sinhVienLamBaiDto.IdBaiTap);
            if (baiTap == null)
            {
                return NotFound("Không tìm thấy bài tập với ID được cung cấp.");
            }

            var cotDiem = new CotDiem
            {
                TenCotDiem = baiTap.TenBaiTap, // Sử dụng tên bài tập làm tên cột điểm
                Diem = sinhVienLamBaiDto.Diem

            };

            _cotDiemBLL.AddCotDiem(cotDiem);
            var sinhVienLamBai = new SinhVienLamBai
            {
                IdSinhVien = sinhVienLamBaiDto.IdSinhVien,
                IdBaiTap = sinhVienLamBaiDto.IdBaiTap,
                IdCotDiem = cotDiem.IdCotDiem,
                NoiDungBaiLam = sinhVienLamBaiDto.NoiDungBaiLam
            };

            var addedSinhVienLamBai = _cotDiemBLL.AddSinhVienLamBai(sinhVienLamBai);
            return CreatedAtAction(nameof(GetSinhVienLamBaiById), new { id = sinhVienLamBai.IdBaiLam }, sinhVienLamBai);


        }
        // API để xem các cột điểm của khóa học
        [HttpGet("khoaHoc/{idKhoaHoc}/user/{idUser}")]
        public IActionResult GetCotDiemsByKhoaHoc(int idKhoaHoc, int idUser)
        {
            var cotDiems = _cotDiemBLL.GetCotDiemsByKhoaHocAndUser(idKhoaHoc, idUser);
            if (cotDiems == null || !cotDiems.Any())
            {
                return NotFound("Không tìm thấy cột điểm nào cho khóa học này.");
            }
            return Ok(cotDiems);
        }

        [HttpGet("xuat-diem-kh{idKhoaHoc}")]
        public IActionResult GetBangDiemByKhoaHoc(int idKhoaHoc)
        {
            var sinhVienDiemList = new List<SinhVienDiemDto>();
            // Lấy danh sách sinh viên trong khóa học và điểm của họ
            var sinhVienList = _cotDiemBLL.GetSinhVienByKhoaHoc(idKhoaHoc);

            foreach (var sinhVien in sinhVienList)
            {
                var danhsachlambai = _cotDiemBLL.GetCotDiemsByKhoaHocAndUser(idKhoaHoc, sinhVien.IdUser);
                var diemList = new List<CotDiem>();
                foreach (var bailam in danhsachlambai)
                {
                    var diem = _cotDiemBLL.GetCotDiemById(bailam.IdCotDiem);
                    diemList.Add(diem);

                }
                var sinhVienDiem = new SinhVienDiemDto
                {
                    IdSinhVien = sinhVien.IdUser,
                    FirstName = sinhVien.FirstName,
                    LastName = sinhVien.LastName,
                    Diems = diemList.Select(d => new DiemofsinhvienDto
                    {
                        Tencotdiem = d.TenCotDiem,
                        Diem = d.Diem
                    }).ToList()
                };
                sinhVienDiemList.Add(sinhVienDiem);
            }
            var bangDiemtheoKH = new BangDiemtheoKH()
            {
                NamHocKy = _khoaHocBLL.GetHocKyById(idKhoaHoc).NamHocKy,
                SinhViens = sinhVienDiemList,
                TenKhoaHoc = _khoaHocBLL.GetKhoaHocById(idKhoaHoc).TenKhoaHoc
            };
            return Ok(bangDiemtheoKH);
        }
        //
        [HttpGet("ketQuaHocTapTheoKy/{iDUser}/{iDKy}")]
        public IActionResult GetKetQuaHocTapbyKi(int iDUser, int iDKy)
        {
            var bangDiemtheoKy = new BangDiemtheoKy();
            var sinhvien = _userBLL.GetUserById(iDUser);
            if (sinhvien == null)
            {
                return NotFound("Không tìm thấy sinh viên.");
            }

            var khoahhocdangkis = _sinhVienKhoaHocBLL.GetSinhVienKhoaHocBySV(sinhvien.IdUser);
            bangDiemtheoKy.IdSinhVien = sinhvien.IdUser;
            bangDiemtheoKy.FirstName = sinhvien.FirstName;
            bangDiemtheoKy.LastName = sinhvien.LastName;
            bangDiemtheoKy.KetQuaKhoaHocs = new List<KetQuaKhoaHoc>();

            var hocKy = _khoaHocBLL.GetHocKyById(iDKy);
            if (hocKy == null)
            {
                return NotFound("Không tìm thấy học kỳ.");
            }
            bangDiemtheoKy.NamHocKy = hocKy.NamHocKy;

            foreach (var khoahoc in khoahhocdangkis)
            {
                var sinhvienlambais = _cotDiemBLL.GetCotDiemsByKhoaHocAndUser(khoahoc.IdKhoaHoc, sinhvien.IdUser);
                var diems = new List<DiemofsinhvienDto>();
                foreach (var sinhvienlambai in sinhvienlambais)
                {
                    var baiTap = _baiTapBLL.GetBaiTapById(sinhvienlambai.IdBaiTap);
                    var cotDiem = _cotDiemBLL.GetCotDiemById(sinhvienlambai.IdCotDiem);
                    if (baiTap != null && cotDiem != null)
                    {
                        diems.Add(new DiemofsinhvienDto
                        {
                            Tencotdiem = baiTap.TenBaiTap,
                            Diem = cotDiem.Diem
                        });
                    }
                }

                var ketquahocTap = new KetQuaKhoaHoc()
                {
                    IdKhoaHoc = khoahoc.IdKhoaHoc,
                    TenKhoaHoc = _khoaHocBLL.GetKhoaHocById(khoahoc.IdKhoaHoc)?.TenKhoaHoc ?? "Unknown",
                    Diems = diems
                };

                bangDiemtheoKy.KetQuaKhoaHocs.Add(ketquahocTap);
            }

            return Ok(bangDiemtheoKy);
        }

        // lấy bài làm
        [HttpGet("getBaiTapByUserandBaiTap/{idbaitap}/{userid}")]
        public IActionResult GetBaiLamByUserAndBaiTap(int idbaitap, int userid)
        {
            if (userid != null && idbaitap != null)
            {
                var baitap = _sinhVienLamBaiBLL.GetBaiLamByIduserAndBT(userid, idbaitap);
                return Ok(baitap);
            }
            else
                return NoContent();
        }
        //cập nhật bài làm tự luận
        [HttpPut("updateByIdBaiLam/{idbailam}")]
        public IActionResult UpdateBaiLam(int idbailam, [FromBody] string noidung)
        {
            var result = _sinhVienLamBaiBLL.CapNhatNoiDungBaiLam(idbailam, noidung);
            if (result)
                return NoContent();
            else
                return BadRequest();
        }

        // tính điểm trắc nghiệm
        [HttpPost("tinhDiemTracNghiem")]
        public IActionResult TinhDiemTracNghiem([FromBody] TinhDiem BaiLam)
        {
            if (BaiLam.DapAn.Count == 0)
                return BadRequest();
            else
            {
                var result = _sinhVienLamBaiBLL.TinhDiemTracNghiem(BaiLam.IDSinhVien, BaiLam.IDBaiTap, BaiLam.DapAn);
                return Ok(result);
            }
        }
        // lấy danh sách làm bài theo bài tập
        [HttpGet("layBaiLamByBaiTap/{idBaiTap}")]
        public IActionResult getBaiLamByBaiTap(int idBaiTap)
        {
            var result = _sinhVienLamBaiBLL.getBaiLamByBaiTap(idBaiTap);
            return Ok(result);
        }

    }
}


