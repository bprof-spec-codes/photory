﻿using PhotoryData;
using PhotoryModels;
using PhotoryRepository.Interfaces;
using System.Linq;

namespace PhotoryRepository.Classes
{
    public class PhotoRepository : IPhotoRepository
    {
        private PhotoryDbContext context = new PhotoryDbContext();

        public PhotoRepository(PhotoryDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Comment> GetAllCommentsFromPhoto(string PhotoID)
        {
            var entities = from x in context.Comments
                           where x.PhotoID == PhotoID
                           select x;

            return entities;
        }

        public Photo GetOnePhoto(string PhotoID)//Rendes méret
        {
            var entities = (from x in context.Photos
                            where x.PhotoID == PhotoID
                            select x).FirstOrDefault();

            return entities;
        }

        public Photo GetOneRescaledPhoto(string PhotoID)//Kicsi Fotó
        {
            var entities = (from x in context.Photos
                            where x.PhotoID == PhotoID && x.IsRescaled == true
                            select x).FirstOrDefault();

            return entities;
        }

        public void DeletePhoto(string PhotoID)
        {
            var entity = (from x in context.Photos
                          where x.PhotoID == PhotoID
                          select x).FirstOrDefault(); // nagy fotó

            var miniphoto = (from x in context.Photos
                             where x.ConnectionId == entity.ConnectionId && x.IsRescaled == true
                             select x).FirstOrDefault();



            this.context.Photos.Remove(miniphoto);
            this.context.Photos.Remove(entity);
            this.context.SaveChanges();
        }


    }
}