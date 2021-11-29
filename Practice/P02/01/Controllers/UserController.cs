using _01.Enums;
using _01.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace _01.Controllers
{

    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private static Dictionary<long, User> Users = new()
        {
            [1] = new User { Id = 1, Name = "Owner", Role = Role.Admin }
        };

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="404">User not found</response>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        public ActionResult<User> Get(long id)
        {
            if (!Users.ContainsKey(id))
                return NotFound(new ApiError() { Code = 1, Message = "User not found" });

            return Ok(Users[id]);
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <response code="400">Invalid role</response>
        /// <response code="409">User already exists</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(User user)
        {
            if (!Enum.IsDefined(user.Role))
                return BadRequest(new ApiError() { Code = 2, Message = "Invalid role" });

            if (Users.ContainsKey(user.Id))
                return Conflict(new ApiError { Code = 3, Message = "User already exist" });

            Users[user.Id] = user;
            return Ok();
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <response code="400">Invalid role</response>
        /// <response code="409">User already exists</response>
        /// <returns></returns>
        [Obsolete]
        [HttpPost("legacy")]
        public ActionResult PostLegacy(User user)
        {
            Users[user.Id] = user;
            return Ok();
        }
    }
}
