﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoryLogic.Classes;
using PhotoryModels;
using PhotoryRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Photory.Controllers
{
    [Authorize(Roles = "Customer")]
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private UserLogic userlogic;
        private IPhotoRepository photo;

        public UserController(UserLogic userlogic, IPhotoRepository photo)
        {
            this.userlogic = userlogic;
            this.photo = photo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUser()
        {
            try
            {
                var users = userlogic.GetAllUser();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetOneUser(string id)
        {
            try
            {
                var user = userlogic.GetUser(id);
                return Ok(user);
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
                userlogic.UpdateUser(oldid, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                userlogic.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpPost("{userID}&{GroupID}")]
        public IActionResult RequestJoin(string userID, string GroupID)
        {
            try
            {
                userlogic.RequestJoin(userID, GroupID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpPost]
        [Route("LeaveGroup")]
        public IActionResult LeaveGroup([FromBody] UserOfGroup uog)
        {
            try
            {
                userlogic.LeaveGroup(uog.ID, uog.GroupName);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpPost]
        [Route("AddPhoto")]
        public IActionResult AddPhoto([FromBody] Photo p)
        {
            try
            {
                userlogic.AddPhoto(p);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpDelete("DeletePhoto/{id}")]
        public IActionResult DeletePhoto(string id)
        {
            try
            {
                //userlogic.DeletePhoto(id);
                photo.DeletePhoto(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpPost]
        [Route("AddComment")]
        public IActionResult AddComment([FromBody] Comment m)
        {
            try
            {
                userlogic.AddComment(m);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpDelete("DeleteComment/{id}")]
        public IActionResult DeleteComment(string id)
        {
            try
            {
                userlogic.DeleteComment(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpPost("PhotoUpload/{groupID}&{userid}"), DisableRequestSizeLimit]
        public IActionResult PhotoUpload(IFormFile FileToUpload, string groupID, string userid)
        {
            try
            {
                var folderName = "Photos";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (FileToUpload != null || FileToUpload.Length > 0)
                {
                    var fullpath = Path.Combine(pathToSave, FileToUpload.FileName);

                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        FileToUpload.CopyTo(stream);
                    }
                    userlogic.UploadtoData(FileToUpload.FileName, groupID, userid);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error : {ex}");
            }
        }

        [HttpGet("PhotoDownload/{photoID}")]
        public FileResult Download(string photoID)
        {
            var p = photo.GetOnePhoto(photoID);
            byte[] allbytes = p.PhotoData;
            return File(allbytes, "application/octet-stream", "teszt.jpg");
        }

        [HttpGet("GetOnePhoto/{photoID}")]
        public Photo GetOnePhoto(string photoID)
        {
            var p = photo.GetOnePhoto(photoID);
            //byte[] allbytes = p.PhotoData;

            return p;
        }

        [HttpGet("GetOneRescaledPhoto/{photoID}")]
        public Photo GetOneRescaledPhoto(string photoID)
        {
            var p = photo.GetOneRescaledPhoto(photoID);
            //byte[] allbytes = p.PhotoData;

            return p;
        }
    }
}