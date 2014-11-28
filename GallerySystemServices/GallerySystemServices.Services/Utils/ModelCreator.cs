using GallerySystemServices.Services.Models;
using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Utils
{
    public class ModelCreator
    {
        public static UserModel CreateUserModel(User user)
        {
            var userModel = new UserModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                SessionKey = user.SessionKey,
                CreatedAt = user.CreatedAt,
                Email = user.Email
            };

            return userModel;

        }
    }
}