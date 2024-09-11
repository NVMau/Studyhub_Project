using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyHub.BLL;
using StudyHub.DAL.Models;

namespace AdminStudyHub.Controllers
{
    public class HocKysController : Controller
    {
        private readonly HocKyBLL hocKyBLL = new HocKyBLL();

        // GET: HocKys
        public async Task<IActionResult> Index()
        {
            var hockys = hocKyBLL.GetListHocKy();
            return View(hockys);
        }


        // GET: HocKys/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: HocKys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HocKy hocKy)
        {
            if (hocKy.NamHocKy != null)
            {
                hocKyBLL.AddHocKy(hocKy.NamHocKy);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HocKy hocKy = hocKyBLL.GetHocKybyid(id);

            if (hocKy == null)
            {
                return NotFound();
            }
            return View(hocKy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HocKy hocKy)
        {
            if (id != hocKy.IdHocKy)
            {
                return NotFound();
            }
            else
            {
                hocKyBLL.updateHocKy(hocKy);
                return RedirectToAction(nameof(Index));
            }
            //return View(hocKy);
        }

        //// GET: HocKys/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocKy = hocKyBLL.GetHocKybyid(id);
            if (hocKy == null)
            {
                return NotFound();
            }

            return View(hocKy);
        }

        //// POST: HocKys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            hocKyBLL.deletehocky(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
