using StudyHub.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.DAL
{
    public class UserDAL
    {
        HeThongQuanLyHocTapContext context = new HeThongQuanLyHocTapContext();

        // lấy list user
        public List<UserOu> GetAllUsers()
        {
            var users = context.UserOus
                        .Select(i => new UserOu
                        {
                            IdUser = i.IdUser,
                            Username = i.Username,
                            FirstName = i.FirstName,
                            LastName = i.LastName,
                            Email = i.Email,
                            Address = i.Address,
                            Avatar = i.Avatar,
                            Role = i.Role
                        })
                        .ToList<UserOu>();
            return users;
        }
        //lấy user theo id
        public UserOu GetUserById(int userId)
        {
            var user = context.UserOus
                .FirstOrDefault(u => u.IdUser == userId);
            return user;
        }

        // tạo user
        public UserOu AddUser(UserOu user)
        {
            context.UserOus.Add(user);
            context.SaveChanges();
            var addedUser = context.UserOus.FirstOrDefault(u => u.Username == user.Username);
            return addedUser;
        }

        //cập nhật usr
        public void UpdateUser(UserOu user)
        {
            var existingUser = context.UserOus.Find(user.IdUser);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.Address = user.Address;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                context.SaveChanges();
            }
        }
        // xóa user
        public void DeleteUser(int userId)
        {
            var userToDelete = context.UserOus.Find(userId);
            if (userToDelete != null)
            {
                context.UserOus.Remove(userToDelete);
                context.SaveChanges();
            }
        }

        // lấy user theo username và passwork
        public UserOu GetUserByUsernameAndPassword(string username, string password)
        {
            var user = context.UserOus
                .FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }

        // lấy user theo username
        public UserOu GetUserByUsername(string username)
        {
            var user = context.UserOus
                .FirstOrDefault(u => u.Username == username);
            return user;
        }

        // đôi pass
        public UserOu ChangePass(int userId, string password)
        {
            var user = context.UserOus
                .FirstOrDefault(u => u.IdUser == userId);
            if (user != null)
            {
                user.Password = password;
                context.SaveChanges();
                return user;
            }
            return null;
        }

    }
}
