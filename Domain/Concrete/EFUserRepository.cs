using System;
using System.Linq;
using TicketManager.Domain.Abstract;
using TicketManager.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace TicketManager.Domain.Concrete
{
    public class EFUserRepository : IUsersRepository
    {
        private EntityContext context = new EntityContext();

        public IQueryable<User> Users
        {
            get
            {
                return context.Users;
            }
        }

        public bool AddUser(User tempUser, LoginInformation loginInformation)
        {

            var temp = context.LoginInformations.Where(x => x.Username == loginInformation.Username).ToList();

            if(tempUser.UserID == 0 && loginInformation.LoginID == 0 && temp.Count == 0)
            {
                loginInformation.Password = GetMD5(loginInformation.Password);
                tempUser.LoginInformation = context.LoginInformations.Add(loginInformation);
                tempUser.Role = "Normal";
                context.Users.Add(tempUser);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool IsValidUser(string Username, string Password)
        {
            string temp = GetMD5(Password);
            bool isValid = context.LoginInformations.Any(p => p.Username == Username &&
                p.Password == temp);

            return isValid;
        }

        public void ModifyUser(User tempUser)
        {
            throw new NotImplementedException();
        }

        public User DeleteUser(int UserId)
        {
            throw new NotImplementedException();
            //return new User();
        } 

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}
