using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Utils
{
    public static class Validator
    {
        private const int MIN_NAME_LENGTH = 2;
        private const int MAX_NAME_LENGTH = 30;
        private const int MIN_PASSWORD_LENGTH = 4;

        private const string VALID_NAME_CHARS =
            "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";

        public static void ValidatePicture(Picture picture, string error)
        {
            if(picture == null)
            {
                throw new Exception(error);
            }
        }

        public static void ValidateAlbum(Album album, string error)
        {
            if(album == null)
            {
                throw new Exception(error);
            }
        }

        public static void ValidateUser(User user, string error)
        {
            if(user == null)
            {
                throw new Exception(error);
            }
        }

        public static void ValidateEmail(string email)
        {
            new System.Net.Mail.MailAddress(email);
        }

        public static void ValidateName(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("Names cannot be null");
            }
            else if (name.Length < MIN_NAME_LENGTH)
            {
                throw new ArgumentException(
                    string.Format("Names must be at least {0} characters long.",
                    MIN_NAME_LENGTH));
            }
            else if (name.Length > MAX_NAME_LENGTH)
            {
                throw new ArgumentException(
                    string.Format("Names must be less than {0} characters long.",
                    MAX_NAME_LENGTH));
            }
            else if (name.Any(ch => !VALID_NAME_CHARS.Contains(ch)))
            {
                throw new ArgumentException(
                    "Names must contain only Latin letters.");
            }
        }

        public static void ValidatePassword(string password)
        {
            if (String.IsNullOrEmpty(password) || password.Length < MIN_PASSWORD_LENGTH)
            {
                throw new ArgumentException(
                    string.Format("Password must be at least {0} characters long.",
                    MIN_PASSWORD_LENGTH));
            }
        }
    }
}