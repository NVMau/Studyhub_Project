using StudyHub.DAL;
using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
namespace StudyHub.BLL
{
    public class CreateUser
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; }
    }
    public class UserBLL
    {
        private readonly UserDAL _userDAL;
        public UserBLL()
        {
            _userDAL = new UserDAL();
        }
        // lấy list user
        public List<UserOu> GetAllUsers()
        {
            return _userDAL.GetAllUsers();
        }

        public UserOu GetUserById(int userId)
        {
            return _userDAL.GetUserById(userId);
        }

        public UserOu AddUser(CreateUser user)
        {
            UserOu newUser = new UserOu();
            newUser.Username = user.Username;
            newUser.Password = user.Password;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Email = user.Email;
            newUser.Avatar = "https://i.pinimg.com/564x/bd/1c/c7/bd1cc751865c67de695216da045579d5.jpg";
            newUser.Role = "Student";
            return _userDAL.AddUser(newUser);
        }

        public void UpdateUser(UserOu user)
        {
            _userDAL.UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            _userDAL.DeleteUser(userId);
        }

        public UserOu GetUserByUsernameAndPassword(string username, string password)
        {
            return _userDAL.GetUserByUsernameAndPassword(username, password);
        }

        public UserOu GetUserByusername(string username)
        { 
            return _userDAL.GetUserByUsername(username);
        }
        // cap nhat pass
        public UserOu ChangePass(int userId, string password)
        {
            return _userDAL.ChangePass(userId, password);
        }
        
    }
}
