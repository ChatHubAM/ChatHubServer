using DbAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbAccessLayer.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Register user 
        /// </summary>
        /// <param name="user">New User Data</param>
        /// <returns></returns>
        Task<bool> RegisterUserAsync(UserEntity user);

        /// <summary>
        /// Gets all users info from Db
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetAllUsersFromDb();

        /// <summary>
        /// Gets the online users 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetActiveUsers();
        
        /// <summary>
        /// Gets user by user Id
        /// </summary>
        /// <returns></returns>
        Task<UserEntity> GetUserByID(int userId);
        
        /// <summary>
        /// Gets user by specified username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<UserEntity> GetUserByUsername(string userName);

        bool CheckUser(UserLogin user);
    }
}
