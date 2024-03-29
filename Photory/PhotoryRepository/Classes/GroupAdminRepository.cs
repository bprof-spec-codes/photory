﻿using PhotoryData;
using PhotoryModels;
using PhotoryRepository.Interfaces;
using System;
using System.Linq;

namespace PhotoryRepository
{
    public class GroupAdminRepository : IGroupAdminRepository
    {
        private PhotoryDbContext context = new PhotoryDbContext();

        public GroupAdminRepository(PhotoryDbContext context)
        {
            this.context = context;
        }

        public void Add(User entity)
        {
            entity.UserId = Guid.NewGuid().ToString();
            entity.UserAccess = UserAccess.GroupAdmin;
            this.context.MyUsers.Add(entity);
            SaveDatabase();
        }

        public void Delete(string id)
        {
            var entity = GetOne(id);

            this.context.MyUsers.Remove(entity);
            SaveDatabase();
        }

        public IQueryable<User> GetAll()
        {
            var groupadmins = from x in context.MyUsers
                              where x.UserAccess == UserAccess.GroupAdmin
                              select x;

            return groupadmins;
        }

        public User GetOne(string id)
        {
            var entity = (from x in context.MyUsers
                          where x.UserId == id                 //&& x.UserAccess == UserAccess.GroupAdmin
                          select x).FirstOrDefault();

            if (entity.UserAccess == UserAccess.GroupAdmin)
            {
                return entity;
            }
            return null;
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
            SaveDatabase();
        }

        public void AcceptUser(string userID, string GroupID)//Groupid,userid //public void AcceptUser(User u)
        {
            //var groupentity = (from x in context.Groups
            //                  where x.GroupName == GroupID
            //                  select x).FirstOrDefault();

            var entity = (from x in context.UserOfGroup
                          where x.GroupName == GroupID && x.ID == userID
                          select x).FirstOrDefault();

            entity.IsPending = false;

            //groupentity.PendingUserIDList.Remove(userID);
            //groupentity.UsersID.Add(userID);
            SaveDatabase();
        }

        public void DenyUser(string userID, string GroupID) //public void DenyUser(User u)
        {
            //var groupentity = (from x in context.Groups
            //                   where x.GroupName == GroupID
            //                   select x).FirstOrDefault();

            //groupentity.PendingUserIDList.Remove(userID);

            var entity = (from x in context.UserOfGroup
                          where x.GroupName == userID
                          select x).FirstOrDefault();

            context.UserOfGroup.Remove(entity);

            SaveDatabase();
        }
    }
}