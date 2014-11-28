﻿using GallerySystemServices.Services.Models;
using GallerySystemServices.Services.Services;
using GallerySystemServices.Services.Utils;
using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GallerySystemServices.Services.Controllers
{
    public class AlbumController : ApiController
    {
        private const string USER_ACCESS_DENIED = "Access Denied Ebalnik!";

        [HttpPost]
        [ActionName("createAlbum")]

        public HttpResponseMessage PostCreateAlbum(AlbumModel albumModel, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if (user == null)
                {
                    throw new Exception(USER_ACCESS_DENIED);
                }

                var albumService = new AlbumService();
                var newAlbum = albumService.CreateAlbum(albumModel, user);

                var userModel = ModelCreator.CreateUserModel(user);

                albumModel = new AlbumModel();
                albumModel.Title = newAlbum.Title;
                albumModel.Id = newAlbum.Id;
                albumModel.User = userModel;
                albumModel.CreatedAt = newAlbum.CreatedAt;

                return this.Request.CreateResponse(HttpStatusCode.Created , albumModel);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest , ex.Message);
            }
                            
        }

        [HttpDelete]
        [ActionName("DeleteAlbum")]

        public HttpResponseMessage DeleteAlbum (int albumId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if(user == null)
                {
                    throw new Exception("Cannot delete album");
                }

                var albumService = new AlbumService();
                var album = albumService.GetAlbumById(albumId);

                if (album == null)
                {
                    throw new Exception("Album not found");
                }

                if(album.User.Id != user.Id)
                {
                    throw new Exception("Access denied");
                }

                albumService.DeleteAlbum(album);

                return this.Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest , ex.Message);
            }

        }

        [HttpPost]
        [ActionName("addComment")]
        public HttpResponseMessage PostAddComment (CommentModel comment, int albumId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if(user == null) 
                {
                    throw new Exception("Access denied!");
                }

                var albumService = new AlbumService();
                var album = albumService.GetAlbumById(albumId);

                if(album == null) 
                {
                    throw new Exception("Album not found!");
                }

                var newComment = albumService.AddComment(comment, album, user);

                return this.Request.CreateResponse(HttpStatusCode.Created);
            }
            catch(Exception ex)
            {
                 return this.Request.CreateResponse(HttpStatusCode.BadRequest , ex.Message);
            }
        }

        [HttpPut]
        [ActionName("editAlbum")]

        public HttpResponseMessage PutEditAlbum (int albumId, AlbumModel albumModel, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if (user == null)
                {
                    throw new Exception("Cannot edit album");
                }

                var albumService = new AlbumService();
                var album = albumService.GetAlbumById(albumId);

                if (album == null)
                {
                    throw new Exception("Album not found");
                }

                if (album.User.Id != user.Id)
                {
                    throw new Exception("Access Denied");
                }

                var userModel = ModelCreator.CreateUserModel(user);

                var newAlbum = albumService.EditAlbum(album, albumModel);

                var albumToReturn = new AlbumModel();
                albumToReturn.Title = newAlbum.Title;
                albumToReturn.User = userModel;
                albumToReturn.Id = newAlbum.Id;
                albumToReturn.CreatedAt = newAlbum.CreatedAt;


                return this.Request.CreateResponse(HttpStatusCode.OK, albumToReturn);
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [ActionName("vote")]
        public HttpResponseMessage PostVoteForAlbum(VoteModel vote, int albumId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if (user == null)
                {
                    throw new Exception("Cannot edit album");
                }

                var albumService = new AlbumService();
                var album = albumService.GetAlbumById(albumId);

                if (album == null)
                {
                    throw new Exception("Album not found");
                }

                if (album.User.Id != user.Id)
                {
                    throw new Exception("Access Denied");
                }

                var isVoted = album.Votes.Count(v => v.User.Id == user.Id) > 0;
                if(isVoted)
                {
                    throw new Exception("Glasuval si ebalnik..!");
                }

                var newVote = albumService.AddVoteToAlbum(vote, album, user);

                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [ActionName("addImage")]
        public HttpResponseMessage PostAddImageToAlbum(PictureModel picture, int albumId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if (user == null)
                {
                    throw new Exception("Cannot edit album");
                }

                var albumService = new AlbumService();
                var album = albumService.GetAlbumById(albumId);

                if (album == null)
                {
                    throw new Exception("Album not found");
                }

                if (album.User.Id != user.Id)
                {
                    throw new Exception("Access Denied");
                }

                var newPicture = albumService.AddPictureToAlbum(picture, album);

                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [ActionName("DeletePicture")]

        public HttpResponseMessage DeletePicture(int pictureId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if (user == null)
                {
                    throw new Exception("Cannot delete album");
                }

                var pictureService = new PictureService();

                var albumService = new AlbumService();
                var picture = pictureService.GetPictureById(pictureId);

                if (picture.Album.User.Id != user.Id)
                {
                    throw new Exception("Access denied");
                }

                albumService.DeletePictureFromAlbum(picture);

                return this.Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpGet]
        [ActionName("GetAlbums")]
        public HttpResponseMessage GetAlbums (string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if (user == null)
                {
                    throw new Exception("Cannot delete album");
                }

                var albums = from album in user.Albums
                             select new AlbumModel()
                             {
                                 Title = album.Title,
                                 Id = album.Id,
                                 CreatedAt = album.CreatedAt,
                                 CategoryId = album.Category.Id,
                                 MainImageUrl = album.Pictures.Count > 0 ? album.Pictures.First().Url : ""
                             };

                return this.Request.CreateResponse(HttpStatusCode.OK, albums);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetAlbumsByCategory")]
        public HttpResponseMessage GetAlbumsByCategory(int categoryId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if (user == null)
                {
                    throw new Exception("Cannot delete album!");
                }

                var categoryService = new CategoriesService();
                var category = categoryService.GetCategoryById(categoryId);

                if(category == null)
                {
                    throw new Exception("Cannot found this category!");
                }

                if(category.User.Id != user.Id)
                {
                    throw new Exception("This user has no such this category!");
                }

                var albums = from album in user.Albums.Where(a => a.Id == categoryId)
                             select new AlbumModel()
                             {
                                 Title = album.Title,
                                 Id = album.Id,
                                 CreatedAt = album.CreatedAt,
                                 CategoryId = album.Category.Id,
                                 MainImageUrl = album.Pictures.Count > 0 ? album.Pictures.First().Url : ""
                             };

                return this.Request.CreateResponse(HttpStatusCode.OK, albums);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetPicturesFromAlbum")]
        public HttpResponseMessage GetPicturesFromAlbum(int albumId,string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if (user == null)
                {
                    throw new Exception("Cannot delete album");
                }

                var albumService = new AlbumService();
                var album = albumService.GetAlbumById(albumId);

                if (album == null)
                {
                    throw new Exception("Album not found");
                }

                if (album.User.Id != user.Id)
                {
                    throw new Exception("Access Denied");
                }

                var pictures = from picture in album.Pictures
                             select new PictureModel()
                             {
                                 Id = picture.Id,
                                 Title = picture.Title,
                                 CreateDate = picture.CreateDate,
                                 Url = picture.Url
                             };

                return this.Request.CreateResponse(HttpStatusCode.OK, pictures);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetAlbumById")]

        public HttpResponseMessage GetAlbumById(int albumId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                if (user == null)
                {
                    throw new Exception("Cannot delete album");
                }

                var albumService = new AlbumService();
                var album = albumService.GetAlbumById(albumId);

                if (album == null)
                {
                    throw new Exception("Album not found");
                }

                if (album.User.Id != user.Id)
                {
                    throw new Exception("Access Denied");
                }


                var albumToReturn = new AlbumModel()
                {
                    Category = new CategoryModel()
                    {
                        Id = album.Category.Id,
                        Name = album.Category.Name
                    },
                    Comments = from comment in album.Comments
                               select new CommentModel()
                                {
                                    Text = comment.Text,
                                    UserName = comment.User.UserName,
                                    CreatedAt = comment.CreatedAt
                                },
                    CreatedAt = album.CreatedAt,
                    Id = album.Id,
                    MainImageUrl = album.Pictures.Count > 0 ? album.Pictures.First().Url : "",
                    Pictures = from picture in album.Pictures
                               select new PictureModel()
                               {
                                   CreateDate = picture.CreateDate,
                                   Description = picture.Description,
                                   Id = picture.Id,
                                   Title = picture.Title,
                                   Url = picture.Url
                               },
                    Title = album.Title,
                    User = new UserModel()
                    {
                        CreatedAt = album.User.CreatedAt,
                        Email = album.User.Email,
                        Id = album.User.Id,
                        UserName = album.User.UserName
                    },
                    PositiveVotes = album.Votes.Count(v => v.isPositive == true),
                    NegativeVotes = album.Votes.Count(v => v.isPositive == false)
                };

                return this.Request.CreateResponse(HttpStatusCode.OK, albumToReturn);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}