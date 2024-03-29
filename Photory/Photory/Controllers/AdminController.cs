﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoryLogic.Classes;
using PhotoryModels;
using System;
using System.Collections.Generic;

namespace Photory.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("Admin")]
    public class AdminController : ControllerBase
    {
        private AdminLogic adminLogic;

        public AdminController(AdminLogic adminLogic)
        {
            this.adminLogic = adminLogic;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGroupAdmin(string id)
        {
            try
            {
                adminLogic.DeleteAdmin(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllAdmin()
        {
            try
            {
                var admins = adminLogic.GetAllUser();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetGroupAdmin(string id)
        {
            try
            {
                var admin = adminLogic.GetAdmin(id);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpPut("{oldid}")]
        public IActionResult UpdateUser(string oldid, [FromBody] User user)
        {
            try
            {
                adminLogic.UpdateAdmin(oldid, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpPost("AddMember/{userID}&{GroupID}")]
        public IActionResult AddMembers(string userID, string GroupID)
        {
            try
            {
                adminLogic.AddMember(userID, GroupID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpPost("/CreateGroup")]
        public IActionResult CreateGroup([FromBody] Group group)
        {
            try
            {
                adminLogic.CreateGroup(group);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpPost("DeleteGroup/{groupid}")]
        public IActionResult DeleteGroup(string groupid)
        {
            try
            {
                adminLogic.DeleteGroup(groupid);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }
    }
}