using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyHub.BLL;
using StudyHub.DAL.Models;
//MVC
namespace AdminStudyHub.Controllers
{
    public class UserController : Controller
    {
        private readonly UserBLL _context;

        public UserController()
        {
            _context = new UserBLL();
        }

        // lấy danh sách user
        public IActionResult Index()
        {
            var users = _context.GetAllUsers();
            return View(users);
        }


        // GET: User/Details/5
        public IActionResult Details(int id)
        {
            var user = _context.GetUserById(id);
            if (user == null)
            {
                return NotFound(); // Trả về HTTP 404 Not Found nếu không tìm thấy user
            }
            return View(user);
        }

        //tạo user
        //GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create tạo user gọi hàm này sau khi chọn tạo

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(UserOu user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.AddUser(user);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(user);
        //}


        public IActionResult Edit(int id)
        {
            var user = _context.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("IdUser,Username,Password,FirstName,LastName,Email,Address,Avatar,Role")] UserOu user)
        {
            if (id != user.IdUser)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra xem người dùng có thay đổi mật khẩu không
                    var existingUser = _context.GetUserById(id);
                    if (existingUser.Password != user.Password)
                    {
                        // Người dùng đã thay đổi mật khẩu, cập nhật mật khẩu
                        existingUser.Password = user.Password;
                    }

                    _context.UpdateUser(existingUser);
                }
                catch (Exception)
                {
                    // Xử lý ngoại lệ (nếu có)
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                _context.DeleteUser(id);
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ (nếu có)
                throw;
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: User/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.GetUserById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        //private bool UserOuExists(int id)
        //{
        //    return _context.UserOus.Any(e => e.IdUser == id);
        //}
    }
}
