using GallerySystemServices.Services.Managers;
using GallerySystemServices.Services.Models;
using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Services
{
    public class AlbumService
    {
        private AlbumManager albumManager;

        public AlbumService()
        {
            this.albumManager = new AlbumManager();
        }

        public Album CreateAlbum(AlbumModel albumModel, User user)
        {
            var album = new Album();
            album.Title = albumModel.Title;
            album.CreatedAt = DateTime.Now;

            var categoryService = new CategoriesService();
            var category = categoryService.GetCategoryById(albumModel.CategoryId);

            var newAlbum = albumManager.CreateAlbum(album, user, category);

            return newAlbum;
        }

        public Album GetAlbumById(int Id)
        {
            var album = this.albumManager.GetAlbumById(Id);
            if (album == null)
            {
                throw new ArgumentException("Album not found");
            }

            return album;
        }

        public Album EditAlbum(Album album, AlbumModel newAlbumData)
        {
            album.Title = newAlbumData.Title;
            return this.albumManager.EditAlbum(album);
        }

        public void DeleteAlbum(Album album)
        {
            this.albumManager.DeleteAlbum(album);
        }

        public AlbumComment AddComment(CommentModel comment, Album album, User user)
        {
            var albumComment = new AlbumComment()
            {
                Text = comment.Text,
                CreatedAt = DateTime.Now
            };

            return this.albumManager.AddComment(albumComment, album, user);
        }

        public AlbumVote AddVoteToAlbum(VoteModel vote, Album album, User user)
        {
            var albumVote = new AlbumVote()
            {
                isPositive = vote.isPositive
            };

            return this.albumManager.AddVote(albumVote, album, user);
        }
        public Picture AddPictureToAlbum(PictureModel picture, Album album)
        {
            var albumPicture = new Picture()
            {
                Title = picture.Title,
                Url = picture.Url,
                Description = picture.Description,
                CreateDate = DateTime.Now
            };

            return this.albumManager.AddPicture(albumPicture, album);
        }

        public void DeletePictureFromAlbum(Picture picture)
        {
            this.albumManager.DeletePictureFromAlbum(picture);
        }

        

    }
}