using GallerySystemServices.Services.Managers;
using GallerySystemServices.Services.Models;
using GallerySystemServices.Services.Utils;
using GallerySysteServices.Models;
using System;
using System.Text;

namespace GallerySystemServices.Services.Services
{
    public class UserService
    {
        private const string SESSION_KEY_CHARS = "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890";
        private const int SESSION_KEY_LENGTH = 50;
        private const string USER_EXIST = "There is user with the same username!";
        private const string WRONG_USERNAME = "There is no user with such username!";
        private const string WRONG_PASSWORD = "Wrong password";

        private UserManager userManager;

        public UserService() 
        {
            this.userManager = new UserManager();
        }

        public User RegisterUser(UserModel userModel)
        {
            var user = this.userManager.GetUserByUserName(userModel.UserName);

            if (user != null)
            {
                throw new InvalidOperationException(USER_EXIST);
            }

            //Validator.ValidateEmail(userModel.Email);
            //Validator.ValidateName(userModel.UserName);
            //Validator.ValidatePassword(userModel.AuthCode);

            user = new User()
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                AuthCode = PasswordHasher.ConvertStringToSHA1(userModel.AuthCode),
                CreatedAt = DateTime.Now
            };

            var newUser = this.userManager.RegisterUser(user);

            string sesionKey = this.GenerateSessionKey(newUser.Id);

            newUser = this.userManager.SetUserSessionKey(newUser, sesionKey);

            return newUser;
        }

        public User AuthenticateUser(string userName, string password)
        {
            var user = this.userManager.GetUserByUserName(userName);

            if (user == null)
            {
                throw new InvalidOperationException(WRONG_USERNAME);
            }

            string passwordHash = PasswordHasher.ConvertStringToSHA1(password);
            if (user.AuthCode != passwordHash)
            {
                throw new ArgumentException(WRONG_PASSWORD);
            }

            string sessionKey = this.GenerateSessionKey(user.Id);

            user = this.userManager.SetUserSessionKey(user, sessionKey);

            return user;
        }

        public void LogoutUser(string sessionKey)
        {
            var user = GetUserBySessionKey(sessionKey);

            if (user != null) {
                this.userManager.SetUserSessionKeyByUserId(user.Id, "");
            }
        }

        public User GetUserBySessionKey(string sessionKey)
        {
            return this.userManager.GetUserBySessionKey(sessionKey);
        }

        private string GenerateSessionKey(int userId)
        {
            Random rand = new Random();
            StringBuilder skeyBuilder = new StringBuilder(SESSION_KEY_LENGTH);
            skeyBuilder.Append(userId);
            while (skeyBuilder.Length < SESSION_KEY_LENGTH)
            {
                var index = rand.Next(SESSION_KEY_CHARS.Length);
                skeyBuilder.Append(SESSION_KEY_CHARS[index]);
            }
            return skeyBuilder.ToString();
        }

        public Category AddCategoryToUser(CategoryModel category, User user)
        {
            var newCategory = new Category()
            {
                Name = category.Name
            };

            return this.userManager.AddCategoryToUser(newCategory, user);
        }
    }
}