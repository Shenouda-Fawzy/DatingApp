using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
        /// <summary>
        /// Create User with the specified password and store it in the data store (DB).
        /// </summary>
        /// <param name="user">the predefined user, usually it provied with only the Username value</param>
        /// <param name="password">password will be hashed</param>
        /// <returns>The new created user with the hashed password stored in the data store</returns>
        Task<User> Register(User user, string password);

        Task<User> Login(string userName, string password);

        Task<bool> UserExsits(string userName);
    }
}
