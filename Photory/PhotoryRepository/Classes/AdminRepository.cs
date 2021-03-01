﻿using PhotoryData;
using PhotoryModels;
using PhotoryRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoryRepository.Classes
{
    public class AdminRepository : IAdminRepository
    {
        private PhotoryDbContext context;

        public AdminRepository(string ConnectionPassword)
        {
            this.context = new PhotoryDbContext(ConnectionPassword);
        }

        public void Add(User entity)
        {
            this.context.Users.Add(entity);
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


        public void AddMember(string userID, string GroupID)//Groupid,userid //public void AcceptUser(User u)
        {
            var groupentity = (from x in context.Groups
                               where x.GroupName == GroupID
                               select x).FirstOrDefault();


            groupentity.UsersID.Add(userID);

            SaveDatabase();
        }

        public void CreateGroup(Group group)
        {
            this.context.Groups.Add(group);
            SaveDatabase();
        }
    }
}
