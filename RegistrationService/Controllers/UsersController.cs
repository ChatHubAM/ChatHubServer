using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AccountControlService.Models;
using AutoMapper;
using DbAccessLayer.Entities;
using DbAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace RegistrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper; 
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            this.mapper = mapper; //DI
            this.userRepository = userRepository; //DI
        }

        /// <summary>
        /// Method for user registration in database 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>user info</returns>
        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = mapper.Map<UserDTO, UserEntity>(user); //Maps DTO to Entity
                
            var isRegistered =  await userRepository.RegisterUserAsync(newUser); //Registers new User in Db

            if (!isRegistered)
            {
                return BadRequest("Please enter a different email or username");
            }
            else
            {
                return Ok();
            }
        }


        //[HttpPost]
        //[Route("login")]
        //public bool CheckUser([FromBody] UserLogin user)
        //{
        //    var isRegistered = userRepository.CheckUser(user);
        //    return isRegistered;
        //}
        //public async Task<IEnumerable<UserDTO>> GetAllUsers()
        //{
        //    return userRepository.GetAllUsersFromDb();
        //}
    }
}