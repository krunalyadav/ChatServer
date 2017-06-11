using ChatServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatServer.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private ChatDbContext _dbContext;

        /// <summary>
        /// user constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public UserController(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// API to use for Login and Registration
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns></returns>
        [HttpGet("login")]
        public IActionResult Login(User user)
        {
            var existingUser = _dbContext.User.FirstOrDefault(x => x.Name.ToLower().Equals(user.Name.ToLower()));
            if (existingUser != null)
            {
                return Ok(existingUser);
            }
            else
            {
                user.Key = new Guid();
                _dbContext.User.Add(user);
                return Ok(user);
            }
        }

        /// <summary>
        /// API used to get all Registered user
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult GetAllUsers()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            if (authorizationHeader == null) return BadRequest();
            return Ok(_dbContext.User.Where(x => x.Key.ToString() != authorizationHeader).ToList());
        }

        /// <summary>
        /// API to get the chat with all registered user
        /// </summary>
        /// <returns></returns>
        [HttpGet("chat")]
        public IActionResult GetChatWithAllUsers()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            if (authorizationHeader == null) return BadRequest();
            return Ok(_dbContext.ChatLog.Where(x => x.ToUser.Key.ToString() == authorizationHeader).ToList());
        }

        /// <summary>
        /// API to get the chat with one particular user
        /// </summary>
        /// <param name="fromUserId">from user id</param>
        /// <returns></returns>
        [HttpGet("chat/{userId}")]
        public IActionResult GetChatWithOneUser(int fromUserId)
        {
            string authorizationHeader = Request.Headers["Authorization"];
            if (authorizationHeader == null) return BadRequest();
            return Ok(_dbContext.ChatLog.Where(x => x.FromUserId == fromUserId && x.ToUser.Key.ToString() == authorizationHeader).ToList());
        }

        /// <summary>
        /// API to post chat with the one user
        /// </summary>
        /// <param name="chatLog">chat object</param>
        /// <returns></returns>
        [HttpPost("chat")]
        public IActionResult PostChatWithUser([FromBody]ChatLog chatLog)
        {
            string authorizationHeader = Request.Headers["Authorization"];
            if (authorizationHeader == null) return BadRequest();
            chatLog.ToUser = _dbContext.User.SingleOrDefault(x => x.Key.ToString() == authorizationHeader);
            _dbContext.ChatLog.Add(chatLog);
            _dbContext.SaveChanges();
            return Ok(chatLog);
        }
    }
}