using GallerySystemServices.Services.Models;
using GallerySystemServices.Services.Services;
using GallerySystemServices.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GallerySystemServices.Services.Controllers
{
    public class AuthenticationController : ApiController
    {

        [HttpPost]
        [ActionName("login")]
        public HttpResponseMessage PostLoginUser(UserModel userModel)
        {
            try
            {
                var userService = new UserService();
                var user = userService.AuthenticateUser(userModel.UserName, userModel.AuthCode);

                var userToReturn = ModelCreator.CreateUserModel(user);
                var response = this.Request.CreateResponse(HttpStatusCode.OK, userToReturn);
                return response;
            }
            catch (Exception ex)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.BadRequest,
                                             ex.Message);
                return response;
            }
        }

        [HttpPut]
        [ActionName("logout")]
        public HttpResponseMessage PutLogoutUser(string sessionKey)
        {
            try
            {
                var userService = new UserService();
                userService.LogoutUser(sessionKey);

                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return response;
            }
        }
    }
}