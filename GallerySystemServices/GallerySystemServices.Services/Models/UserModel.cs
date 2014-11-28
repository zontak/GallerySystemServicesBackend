using System;
using System.Collections.Generic;
namespace GallerySystemServices.Services.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string AuthCode { get; set; }

        public string UserName { get; set; }

        public string SessionKey { get; set; }

        public IEnumerable<CategoryModel> Categories { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}