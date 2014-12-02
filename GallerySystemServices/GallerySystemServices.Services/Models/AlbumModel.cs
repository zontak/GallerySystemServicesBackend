using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Models
{
    public class AlbumModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }

        public CategoryModel Category { get; set; }

        public IEnumerable<CommentModel> Comments { get; set; }

        public DateTime CreatedAt { get; set; }

        public IEnumerable<PictureModel> Pictures { get; set; }

        public string MainImageUrl { get; set; }

        public int CategoryId { get; set; }

        public UserModel User { get; set; }
    }
}