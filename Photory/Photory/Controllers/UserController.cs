﻿using Microsoft.AspNetCore.Mvc;
using PhotoryLogic.Classes;
using PhotoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photory.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class UserController : ControllerBase
    {
        UserLogic userlogic;

        public UserController(UserLogic userlogic)
        {
            this.userlogic = userlogic;
        }


        [HttpPost]
        public void CreateUser([FromBody] User user)
        {
            userlogic.CreateUser(user);
        }


        [HttpGet]
        public IEnumerable<User> GetAllUser()
        {
            return userlogic.GetAllUser();
        }

        [HttpGet("{id}")]
        public User GetOneUser(string id)
        {
            return userlogic.GetUser(id);
        }

        [HttpPut("{oldid}")]

        public void UpdateUser(string oldid, [FromBody] User user)
        {
            userlogic.UpdateUser(oldid, user);
        }


        [HttpDelete("{id}")]
        public void DeleteUser(string id)
        {
            userlogic.DeleteUser(id);
        }


        [HttpPost("{userID}&{GroupID}")]
        //[Route("RequestJoin")]
        public void RequestJoin(string userID, string GroupID)
        {
            userlogic.RequestJoin(userID, GroupID);

        }






        //[HttpPost]
        //[Route("AddComment")]
        //public void AddComment([FromBody] Comment m)
        //{
        //    userlogic.AddComment(m);

        //}



        //[HttpDelete("{id}")]
        //[Route("DeleteComment")]
        //public void DeleteComment(string id)
        //{
        //    userlogic.DeleteComment(id);
        //}





    }
}

