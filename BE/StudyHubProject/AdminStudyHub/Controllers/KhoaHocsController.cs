using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyHub.BLL;
using StudyHub.BLL.StudyHub.BLL;
using StudyHub.DAL.Models;

namespace AdminStudyHub.Controllers
{
    public class KhoaHocsController : Controller
    {
        private readonly KhoaHocBLL khoaHocBLL = new KhoaHocBLL();
        private readonly HeThongQuanLyHocTapContext _context = new HeThongQuanLyHocTapContext();

        // GET: KhoaHocs
        public async Task<IActionResult> Index()
        {
            var kh = khoaHocBLL.GetAllKhoaHoc();
            return View(kh);
        }


        // GET: KhoaHocs/Create
        public IActionResult Create()
        {
            var filteredUsers = _context.UserOus.Where(u => u.Role == "Teacher").ToList();
            ViewData["IdGiangVien"] = new SelectList(filteredUsers, "IdUser", "FirstName");
            ViewData["IdHocKy"] = new SelectList(_context.HocKies, "IdHocKy", "NamHocKy");
            ViewData["IdMonHoc"] = new SelectList(_context.MonHocs, "IdMonHoc", "TenMonHoc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KhoaHoc khoaHoc)
        {
            if (khoaHoc != null)
            {
                khoaHocBLL.AddKhoaHoc(khoaHoc);
                return RedirectToAction(nameof(Index));
            }
            return View(khoaHoc);
        }

        // GET: KhoaHocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null)
            {
                return NotFound();
            }
            var filteredUsers = _context.UserOus.Where(u => u.Role == "Teacher").ToList();

            ViewData["IdGiangVien"] = new SelectList(filteredUsers, "IdUser", "FirstName", khoaHoc.IdGiangVien);
            ViewData["IdHocKy"] = new SelectList(_context.HocKies, "IdHocKy", "NamHocKy", khoaHoc.IdHocKy);
            ViewData["IdMonHoc"] = new SelectList(_context.MonHocs, "IdMonHoc", "TenMonHoc", khoaHoc.IdMonHoc);
            return View(khoaHoc);
        }

        //// POST: KhoaHocs/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KhoaHoc khoaHoc)
        {
            if (id != khoaHoc.IdKhoaHoc)
            {
                return NotFound();
            }
            else
            {
                khoaHocBLL.UpdateKhoaHoc(khoaHoc);
                return RedirectToAction(nameof(Index));
            }
            return View(khoaHoc);
        }

        //// GET: KhoaHocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khoaHoc = await _context.KhoaHocs
                .Include(k => k.IdGiangVienNavigation)
                .Include(k => k.IdHocKyNavigation)
                .Include(k => k.IdMonHocNavigation)
                .FirstOrDefaultAsync(m => m.IdKhoaHoc == id);
            if (khoaHoc == null)
            {
                return NotFound();
            }

            return View(khoaHoc);
        }

        //// POST: KhoaHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            khoaHocBLL.DeleteKhoaHoc(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
