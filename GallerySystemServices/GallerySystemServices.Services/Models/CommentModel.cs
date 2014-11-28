using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Models
{
    public class CommentModel
    {
        public string Text { get; set; }

        public int Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}