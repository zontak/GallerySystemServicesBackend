using GallerySystemServices.Data;
using GallerySystemServices.Services.Models;
using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Managers
{
    public class UserManager
    {

        public GallerySystemServicesContext dbContext;

        public UserManager()
        {
            this.dbContext = WebApiApplication.dbContext;
        }

        public User RegisterUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return user;
        }

        public User GetUserByUserName(string userName)
        {
            return dbContext.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public User GetUserBySessionKey(string sessionKey)
        {
            return dbContext.Users.FirstOrDefault(u => u.SessionKey == sessionKey);
        }

        public User SetUserSessionKey(User user, string sessionKey)
        {
            user.SessionKey = sessionKey;
            dbContext.SaveChanges();

            return user;
        }

        public User SetUserSessionKeyByUserId(int userId, string sessionKey)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.SessionKey = sessionKey;
                dbContext.SaveChanges();
            }

            return user;
        }

        public Category AddCategoryToUser(Category category, User user)
        {
            category.User = user;

            user.Categories.Add(category);

            dbContext.SaveChanges();
            return category;
        }
    }
}