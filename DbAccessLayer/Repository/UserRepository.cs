using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbAccessLayer.Context;
using DbAccessLayer.Models;
using DbAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace DbAccessLayer.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<UserEntity>> GetActiveUsers()
        {
            return await _context.Users.Where(p => p.IPLocal != null).ToListAsync();
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersFromDb()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> RegisterUserAsync(UserEntity user)
        {
            if (await _context.Users.AnyAsync(p => p.Username == user.Username) ||  await _context.Users.AnyAsync(p => p.Email == user.Email))
            {
                return false;
            }
            else
            {
               await  _context.Users.AddAsync(user);
               await _context.SaveChangesAsync();
               return true;
            }
        }

        public Task<UserEntity> GetUserByID(int userId)
        {
            var task = new Task<UserEntity>(() =>
            {
                UserEntity userEntity = new UserEntity();
                var userObject = _context.Users.FirstOrDefault(user => user.Id == userId);
                //var userRoleObject = _context.User_Role.FirstOrDefault(userRole => userRole.UserID == userId);
                if (userObject != null)
                {
                //    var roleObject = _context.Roles.FirstOrDefault<Role>(role => role.Id == userRoleObject.RoleID);
                //    if (roleObject != null)
                //    {
                        userEntity = new UserEntity(userObject.Id, userObject.FullName, userObject.Email, userObject.Username, userObject.Password);
                 }
                //}
                 return userEntity;
            });

            task.Start();

            return task;
        }

        public Task<UserEntity> GetUserByUsername(string userName)
        {
            var task = new Task<UserEntity>(() =>
            {
                var userEntity = new UserEntity();
                var userObject = _context.Users.FirstOrDefault<UserEntity>(user => user.Username.Equals(userName));
               // var userRoleObject = _context.User_Role.FirstOrDefault<User_Role>(userRole => userRole.UserID == userObject.id);
                if (userObject != null)
                {
                //    var roleObject = _context.Roles.FirstOrDefault<Role>(role => role.Id == userRoleObject.RoleID);
                //    if (roleObject != null)
                //    {
                    userEntity = new UserEntity(userObject.Id, userObject.FullName, userObject.Email, userObject.Username, userObject.Password);
                }
                return userEntity;
                //}
            });

            task.Start();

            return task;
        }

        public bool CheckUser(UserLogin user)
        {
            var existingUser = _context.Users.Where(o => (o.Username == user.Username && o.Password == user.Password));
            if (existingUser.Count() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public string Encrypt(string plainText)
        //{
        //    if (plainText == null) throw new ArgumentNullException("plainText");

        //    //encrypt data
        //    var data = Encoding.Unicode.GetBytes(plainText);
        //    byte[] encrypted = ProtectedData.Protect(data, null, Scope);

        //    //return as base64 string
        //    return Convert.ToBase64String(encrypted);
        //}
    }
}
