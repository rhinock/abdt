using _01.Enums;
using _01.Filters;
using _01.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// Get users by filter
        /// </summary>
        /// <response code="200">Users retrieved</response>
        /// <response code="500">Oops! Can't lookup your users right now</response>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public ActionResult<IEnumerable<User>> Get([FromQuery] UserFilterRequest filter)
        {
            var query = Users.Values.AsEnumerable();

            if (filter.Ids.Any())
                query = query.Where(u => filter.Ids.Contains(u.Id));

            if (filter.Name?.Length > 0)
                query = query.Where(u => u.Name.StartsWith(filter.Name));

            if (filter.Roles.Any())
                query = query.Where(u => filter.Roles.Contains(u.Role));

            return Ok(query);
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
            return Ok(user);
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
