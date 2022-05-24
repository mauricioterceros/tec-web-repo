using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthLayer;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoPracticeTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserManger _userManager;
        public UsersController(IUserManger userManger)
        {
            _userManager = userManger;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userManager.GetUsers());
        }

        [HttpGet]
        [Route("{userId}/accounts/{accountId}")]
        /*  /api/users/6400000/accounts/1232-asdfasdf-335-as */ // :) // :(
        public IActionResult GetUsersAccountsby(string userId, string accountId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PostUsers([FromBody] Logic.Models.User user)
        {
            return Ok(_userManager.PostUser(user));
        }

        [HttpPut]
        public IActionResult PutUsers([FromBody] Logic.Models.User user)
        {
            return Ok(_userManager.PutUser(user));
        }

        [HttpDelete]
        [Route("{userId}")]
        public IActionResult DeleteUsers(Guid userId)
        {
            return Ok(_userManager.DeleteUser(userId));
        }
    }
}