﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Movies.Application.Auth.Commands.LogIn;
using Movies.Application.Auth.Commands.SignUp;
using Movies.Application.Common.Models;
using NSwag.Annotations;

namespace Movies.WebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        #region Documentation
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Result))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
        #endregion
        [HttpPost("signup")]
        public async Task<ActionResult<Result>> SignUp([FromBody] SignUpCommand command)
        {
            return await Mediator.Send(command);
        }
        
        
        #region Documentation
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Result))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(ProblemDetails))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
        #endregion
        [HttpPost("login")]
        public async Task<ActionResult<Result>> LogIn([FromBody] LogInCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
