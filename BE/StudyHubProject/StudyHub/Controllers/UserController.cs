using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHub.BLL;
using StudyHub.DAL.Models;
//API
namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserBLL _userBLL;

        public UserController()
        {
            _userBLL = new UserBLL();
        }

        // xac thuc

        // GET: api/User
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userBLL.GetAllUsers();
            return Ok(users);
        }

        //GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userBLL.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //// POST: api/User
        //[HttpPost]
        //public IActionResult AddUser([FromBody] UserOu user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    _userBLL.AddUser(user);
        //    return CreatedAtAction(nameof(GetUserById), new { id = user.IdUser }, user);
        //}

        // cập nhật user
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserOu user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }
            _userBLL.UpdateUser(user);
            return NoContent();
        }

        //DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userBLL.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            _userBLL.DeleteUser(id);
            return NoContent();
        }

        // cap nhat
        [HttpPut("changPassword")]
        public IActionResult UpdatePassUser(int idUser, [FromBody] string password)
        {
            if (idUser != null && password != null)
            {
                var user = _userBLL.ChangePass(idUser, password);
                return Ok(user);
            }
            return BadRequest();
        }

    }
}
