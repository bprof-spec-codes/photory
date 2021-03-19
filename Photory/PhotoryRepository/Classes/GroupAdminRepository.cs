﻿using PhotoryData;
using PhotoryModels;
using PhotoryRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoryRepository
{
    public class GroupAdminRepository : IGroupAdminRepository
    {
        private PhotoryDbContext context;

        public GroupAdminRepository(string ConnectionPassword)
        {
            this.context = new PhotoryDbContext(ConnectionPassword);
        }



        public void Add(User entity)
        {
            this.context.Users.Add(entity);
            SaveDatabase();

        }

        public void Delete(string id)
        {
            var entity = GetOne(id);

            this.context.Users.Remove(entity);

        }

  

        public IQueryable<User> GetAll()
        {
            return context.Users;
        }

        public User GetOne(string id)
        {
            var entity = (from x in context.Users
                         where x.UserName == id
                         select x).FirstOrDefault();

            return entity;
        }

        public void SaveDatabase()
        {
            this.context.SaveChanges();
        }



        public void Update(string oldid, User entity)
        {
            var olduser = GetOne(oldid);

            olduser.BirthDate = entity.BirthDate;
            olduser.Email = entity.Email;
            olduser.UserAccess = entity.UserAccess;
            olduser.Password = entity.Password;
            SaveDatabase();
        }

        public void AcceptUser(string userID, string GroupID)//Groupid,userid //public void AcceptUser(User u)
        {
            var groupentity = (from x in context.Groups
                              where x.GroupName == GroupID
                              select x).FirstOrDefault();

            groupentity.PendingUserIDList.Remove(userID);

            groupentity.UsersID.Add(userID);

            SaveDatabase();
        }

        public void DenyUser(string userID, string GroupID) //public void DenyUser(User u)
        {
            var groupentity = (from x in context.Groups
                               where x.GroupName == GroupID
                               select x).FirstOrDefault();

            groupentity.PendingUserIDList.Remove(userID);

            SaveDatabase();
        }
    }
}