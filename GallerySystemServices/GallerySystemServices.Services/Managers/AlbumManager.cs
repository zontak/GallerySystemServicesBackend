using GallerySystemServices.Data;
using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GallerySystemServices.Services.Managers
{
    public class AlbumManager
    {
        public GallerySystemServicesContext dbContext;

        public AlbumManager()
        {
            this.dbContext = WebApiApplication.dbContext;
        }

        public Album CreateAlbum(Album album, User user, Category category)
        {
            dbContext.Albums.Add(album);

            album.User = user;

            album.Category = category;

            dbContext.SaveChanges();

            return album;

        }

        public Album GetAlbumById(int id)
        {
            return dbContext.Albums.Include(a => a.User).FirstOrDefault(a => a.Id == id);
        }

        public Album EditAlbum(Album album)
        {
            dbContext.SaveChanges();
            return album;
        }

        public void DeleteAlbum(Album album)
        {
            var albumPictures = album.Pictures.ToList();
            var albumComments = album.Comments.ToList();
            var albumVotes = album.Votes.ToList();

            foreach (var picture in albumPictures)
            {
                DeletePictureFromAlbum(picture);
            }
            foreach (var comment in albumComments)
            {
                dbContext.AlbumComments.Remove(comment);
            }
            foreach (var vote in albumVotes)
            {
                dbContext.AlbumVotes.Remove(vote);
            }
            dbContext.Albums.Remove(album);
            dbContext.SaveChanges();

        }

        public AlbumComment AddComment(AlbumComment comment, Album album, User user)
        {
            comment.User = user;

            album.Comments.Add(comment);

            dbContext.SaveChanges();
            return comment;

        }

        public AlbumVote AddVote(AlbumVote vote, Album album, User user)
        {
            vote.User = user;

            album.Votes.Add(vote);

            dbContext.SaveChanges();
            return vote;

        }

        public Picture AddPicture(Picture picture, Album album)
        {
            picture.Album = album;
            album.Pictures.Add(picture);

            dbContext.SaveChanges();
            return picture;
        }

        public void DeletePictureFromAlbum(Picture picture)
        {
            var pictureComments = picture.Comments.ToList();
            var pictureVotes = picture.Votes.ToList();

            foreach (var comment in pictureComments)
            {
                dbContext.PictureComments.Remove(comment);
            }
            foreach (var vote in pictureVotes)
            {
                dbContext.PictureVotes.Remove(vote);
            }
            picture.Album.Pictures.Remove(picture);
            dbContext.Pictures.Remove(picture);
            dbContext.SaveChanges();
        }
    }
}