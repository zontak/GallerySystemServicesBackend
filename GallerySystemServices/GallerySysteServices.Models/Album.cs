namespace GallerySysteServices.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<AlbumComment> Comments { get; set; }
        public virtual ICollection<AlbumVote> Votes { get; set; }

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }

        public Album ()
        {
            this.Pictures = new HashSet<Picture>();
            this.Votes = new HashSet<AlbumVote>();
            this.Comments = new HashSet<AlbumComment>();
        }
    }
}
