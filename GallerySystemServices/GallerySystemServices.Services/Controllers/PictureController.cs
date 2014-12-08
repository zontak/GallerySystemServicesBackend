using GallerySystemServices.Services.Models;
using GallerySystemServices.Services.Services;
using GallerySystemServices.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GallerySystemServices.Services.Controllers
{
    public class PictureController : ApiController
    {

        private const string USER_ACCESS_DENIED = "Access Denied!";
        private const string PICTURE_NOT_FOUND = "Picture not found";

        [HttpPost]
        [ActionName("addCommentToPicture")]
        public HttpResponseMessage PostAddCommentToPicture(CommentModel comment, int pictureId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                Validator.ValidateUser(user, USER_ACCESS_DENIED);

                var pictureService = new PictureService();
                var picture = pictureService.GetPictureById(pictureId);

                Validator.ValidatePicture(picture, PICTURE_NOT_FOUND);

                var newComment = pictureService.AddPictureComment(comment, picture, user);

                var commentToReturn = new CommentModel()
                {
                    CreatedAt = newComment.CreatedAt,
                    Id = newComment.Id,
                    Text = newComment.Text,
                    UserName = user.UserName
                };

                return this.Request.CreateResponse(HttpStatusCode.Created, commentToReturn);
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [ActionName("vote")]
        public HttpResponseMessage PostVoteForPicture(VoteModel vote, int pictureId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                Validator.ValidateUser(user, "Cannot vote for picture");

                var pictureService = new PictureService();
                var picture = pictureService.GetPictureById(pictureId);

                Validator.ValidatePicture(picture, PICTURE_NOT_FOUND);

                if (picture.Album.User.Id != user.Id)
                {
                    throw new Exception(USER_ACCESS_DENIED);
                }

                var isVoted = picture.Votes.Count(v => v.User.Id == user.Id) > 0;
                if (isVoted)
                {
                    throw new Exception("Already voted..");
                }

                var newVote = pictureService.AddVoteToPicture(vote, picture, user);

                var pictureToReturn = new PictureModel()
                {
                    Comments = from comment in picture.Comments
                               select new CommentModel()
                               {
                                   Text = comment.Text,
                                   UserName = comment.User.UserName,
                                   CreatedAt = comment.CreatedAt
                               },
                    CreateDate = picture.CreateDate,
                    Id = picture.Id,
                    Url = picture.Url,
                    Title = picture.Title,
                    PositiveVotes = picture.Votes.Count(v => v.isPositive == true),
                    NegativeVotes = picture.Votes.Count(v => v.isPositive == false)
                };

                return this.Request.CreateResponse(HttpStatusCode.OK, pictureToReturn);
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetPictureById")]

        public HttpResponseMessage GetPictureById(int pictureId, string sessionKey)
        {
            try
            {
                var userService = new UserService();
                var user = userService.GetUserBySessionKey(sessionKey);

                Validator.ValidateUser(user, "Cannot get picture by id");

                var pictureService = new PictureService();
                var picture = pictureService.GetPictureById(pictureId);

                Validator.ValidatePicture(picture, PICTURE_NOT_FOUND);

                if (picture.Album.User.Id != user.Id)
                {
                    throw new Exception(USER_ACCESS_DENIED);
                }


                var pictureToReturn = new PictureModel()
                {
                    Comments = from comment in picture.Comments
                               select new CommentModel()
                                {
                                    Text = comment.Text,
                                    UserName = comment.User.UserName,
                                    CreatedAt = comment.CreatedAt
                                },
                    CreateDate = picture.CreateDate,
                    Id = picture.Id,
                    Url = picture.Url,
                    Title = picture.Title,
                    PositiveVotes = picture.Votes.Count(v => v.isPositive == true),
                    NegativeVotes = picture.Votes.Count(v => v.isPositive == false)
                };

                return this.Request.CreateResponse(HttpStatusCode.OK, pictureToReturn);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [ActionName("GetAllPictures")]
        public HttpResponseMessage GetAllPictures(int pictureCount = 20)
        {
            try
            {
                var pictureService = new PictureService();
                var allPictures = pictureService.GetAllPictures(pictureCount).ToList();
                var pictures = from picture in allPictures
                             select new PictureModel()
                {
                    Comments = from comment in picture.Comments
                               select new CommentModel()
                               {
                                   Text = comment.Text,
                                   UserName = comment.User.UserName,
                                   CreatedAt = comment.CreatedAt
                               },
                    CreateDate = picture.CreateDate,
                    Id = picture.Id,
                    Url = picture.Url,
                    Title = picture.Title,
                    PositiveVotes = picture.Votes.Count(v => v.isPositive == true),
                    NegativeVotes = picture.Votes.Count(v => v.isPositive == false)
                };

                return this.Request.CreateResponse(HttpStatusCode.OK, pictures);

            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
