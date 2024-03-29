﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoryLogic.Classes;
using PhotoryModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Photory.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : Controller
    {
        private AuthLogic _authLogic;

        public AuthController(AuthLogic authLogic)
        {
            _authLogic = authLogic;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> InsertUser([FromBody] RegisterViewModel model)
        {
            string[] result = await _authLogic.RegisterUser(model);
            return Ok(new { User = result });
        }

        [HttpGet]
        public IEnumerable<IdentityUser> GetUsers()
        {
            return _authLogic.GetAllUser();
        }

        [HttpPut("Login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                return Ok(await _authLogic.LoginUser(model));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}