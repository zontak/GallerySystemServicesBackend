using GallerySystemServices.Data;
using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Managers
{
    public class PictureManager
    {
        public GallerySystemServicesContext dbContext;

        public PictureManager()
        {
            this.dbContext = WebApiApplication.dbContext;
        }

        public PictureComment AddPictureComment(PictureComment comment, Picture picture, User user)
        {
            comment.User = user;

            picture.Comments.Add(comment);

            dbContext.SaveChanges();
            return comment;
        }

        public PictureVote AddVoteToPicture(PictureVote vote, Picture picture, User user)
        {
            vote.User = user;

            picture.Votes.Add(vote);

            dbContext.SaveChanges();
            return vote;

        }

        public Picture GetPictureById(int id)
        {
            return dbContext.Pictures.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Picture> GetAllPictures(int pictureCount)
        {
            return dbContext.Pictures.OrderByDescending(picture => picture.Votes.Count(v => v.isPositive == true) - picture.Votes.Count(v => v.isPositive == false)).Take(pictureCount);
        }
    }
}