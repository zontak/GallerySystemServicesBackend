using GallerySystemServices.Services.Managers;
using GallerySystemServices.Services.Models;
using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Services
{
    public class PictureService
    {
        private PictureManager pictureManager;

        public PictureService()
        {
            this.pictureManager = new PictureManager();
        }

        public PictureComment AddPictureComment(CommentModel comment, Picture picture, User user)
        {
            var pictureComment = new PictureComment()
            {
                Text = comment.Text,
                CreatedAt = DateTime.Now
            };

            return this.pictureManager.AddPictureComment(pictureComment, picture, user);
        }

        public PictureVote AddVoteToPicture(VoteModel pictureVote, Picture picture, User user)
        {
            var vote = new PictureVote()
            {
                isPositive = pictureVote.isPositive
            };

            return this.pictureManager.AddVoteToPicture(vote, picture, user);
        }

        public Picture GetPictureById(int Id)
        {
            var picture = this.pictureManager.GetPictureById(Id);
            if (picture == null)
            {
                throw new ArgumentException("Album not found");
            }

            return picture;
        }

        public IEnumerable<Picture> GetAllPictures(int pictureCount)
        {
            return this.pictureManager.GetAllPictures(pictureCount);
        }
    }
}