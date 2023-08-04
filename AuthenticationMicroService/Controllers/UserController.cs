
using AuthenticationMicroService.Entities;
using AuthenticationMicroService.Models;
using AuthenticationMicroService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using MassTransit;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationMicroService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private ITokenService _tokenService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<UserController> _logger;

        public UserController (IUserService userService, ITokenService tokenService, IPublishEndpoint publishEndpoint, ILogger<UserController> logger)
        {
            _userService = userService;
            _tokenService = tokenService;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        // POST api/<UserController>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public ActionResult Register([FromBody] Register model)
        {
            try
            {
                var isUserExist = _userService.IsUerExist(model.UserName);

                if (isUserExist)
                {
                    _logger.LogError("User already exist", model.UserName);
                    return StatusCode(StatusCodes.Status409Conflict, "User already exist");
                }

                _userService.Add(model);

                _logger.LogInformation("User Registered Successfully", model.UserName);

                return Ok(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex?.Message, model.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }

        // POST api/<UserController>
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] Login model)
        {
            try
            {
                var isValidUser = _userService.IsValidUser(model);

                if (isValidUser)
                {
                    _publishEndpoint.Publish(new UserMessageQueue() { UserId = model.UserName });

                    var tokenString =_tokenService.GenerateToken(model.UserName);

                    return Ok(new AuthenticatedResponse { Token = tokenString, StatusCode = StatusCodes.Status200OK });
                }

                _logger.LogError("Invalid login credentials", model.UserName);

                return Ok(new AuthenticatedResponse { StatusCode = StatusCodes.Status401Unauthorized });
                
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex?.Message, model.UserName);
                return StatusCode(StatusCodes.Status500InternalServerError, ex?.Message);
            }
        }
    }
}
