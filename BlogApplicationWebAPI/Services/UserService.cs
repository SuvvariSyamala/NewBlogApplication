using BlogApplicationWebAPI.Database;
using BlogApplicationWebAPI.DTO;
using BlogApplicationWebAPI.Entitys;
using BlogApplicationWebAPI.model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlogApplicationWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly BlogContext Context = null;
       // private readonly EmailService _emailService;
        public UserService (BlogContext context)
        {
            Context = context;  
        //    _emailService = emailService;
        }
        public void  AddUser(User user)
        {
            try
            {
                if (user != null)
                {
                Context.Users.Add(user);
                Context.SaveChanges();
           //     _emailService.SendRegistrationEmail(user.Email, user.UserName);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void  DeleteUser(int userId)
        {
            try
            {
                User user = Context.Users.SingleOrDefault(u=>u.UserId==userId);
                if (user != null)
                {
                    Context.Users.Remove(user);
                    Context.SaveChanges();
                    
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        

        public User GetUserById(int userId)
        {
            try
            {
                var res = Context.Users.SingleOrDefault(u=>u.UserId==userId);

                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<User> GetUserByRoleName(string Role)
        {
           var res=Context.Users.Where(u=>u.Role==Role).ToList();
            return res; 
        }

        public List<UsersData> GetUsers()
        {
            try
            {
                var Result = Context.Users.Select(r => new UsersData { UserId = r.UserId, UserName = r.UserName, Email = r.Email,Role=r.Role, PhoneNumber = r.PhoneNumber, UserStatus = r.UserStatus }).ToList();
                return Result;
              // return Context.Users.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void  UpdateUser(User user)
        {

            try
            {
                    Context.Users.Update(user);
                    Context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public User ValidteUser(string email, string password)
        {
            return Context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }
        public void BlockUser(int userId)
        {
            var user = Context.Users.SingleOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.UserStatus = "Blocked";
                Context.SaveChanges();
            }
        }
        public void UnBlockUser(int userId) {
            var user = Context.Users.Find(userId);
            if (user != null)
            {
                user.UserStatus = "Active";
                Context.SaveChanges();
            }
        }
        public User GetUserByName(string userName)
        {
            return Context.Users.SingleOrDefault(u => u.UserName == userName);
        }
    }
}
