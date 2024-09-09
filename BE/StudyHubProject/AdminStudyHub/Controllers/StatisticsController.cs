using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;
using StudyHub.BLL.StudyHub.BLL;
using AdminStudyHub.Models;

namespace AdminStudyHub.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly UserBLL _userBLL;
        private readonly MonHocBLL _monHocBLL;
        private readonly KhoaHocBLL _khoaHocBLL;

        public StatisticsController()
        {
            _userBLL = new UserBLL();
            _monHocBLL = new MonHocBLL();
            _khoaHocBLL = new KhoaHocBLL();
        }

        public IActionResult Index()
        {
            var totalUsers = _userBLL.GetAllUsers().Count;
            var totalMonHocs = _monHocBLL.getMonhocs().Count;
            var totalKhoaHocs = _khoaHocBLL.GetAllKhoaHoc().Count;

            var model = new StatisticsViewModel
            {
                TotalUsers = totalUsers,
                TotalMonHocs = totalMonHocs,
                TotalKhoaHocs = totalKhoaHocs
            };

            return View(model);
        }
    }
}
