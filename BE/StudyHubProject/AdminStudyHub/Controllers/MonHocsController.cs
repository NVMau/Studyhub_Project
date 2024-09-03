using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyHub.BLL;
using StudyHub.BLL.StudyHub.BLL;
using StudyHub.DAL;
using StudyHub.DAL.Models;

namespace AdminStudyHub.Controllers
{
    public class MonHocsController : Controller
    {
        private readonly MonHocBLL monHocBLL = new MonHocBLL();

        public async Task<IActionResult> Index()
        {
            var monhocs = monHocBLL.getMonhocs();
            return View(monhocs);
        }


        public IActionResult Create()
        {
            return View();
        }
            // thêm môn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                monHocBLL.AddMonHoc(monHoc);
                return RedirectToAction(nameof(Index));
            }
            return View(monHoc);
        }

        //// GET: MonHocs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monHoc = monHocBLL.GetMonHocById(id);
            if (monHoc == null)
            {
                return NotFound();
            }
            return View(monHoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MonHoc monHoc)
        {
            if (id != monHoc.IdMonHoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (id != monHoc.IdMonHoc)
                {
                    return NotFound();
                }
                else
                {
                    monHocBLL.UpdateMonHoc(monHoc);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(monHoc);
        }

        //// GET: MonHocs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var monHoc = monHocBLL.GetMonHocById(id);
            if (monHoc == null)
            {
                return NotFound();
            }

            return View(monHoc);
        }

        //// POST: MonHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            monHocBLL.DeleteMonHoc(id);   
            return RedirectToAction(nameof(Index));
        }
        //private bool MonHocExists(int id)
        //{
        //    return _context.MonHocs.Any(e => e.IdMonHoc == id);
        //}
    }
}
