namespace GallerySysteServices.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual ICollection<PictureComment> Comments { get; set; }
        public virtual ICollection<PictureVote> Votes { get; set; }

        [Required]
        public virtual Album Album { get; set; }

        public Picture ()
        {
            this.Comments = new HashSet<PictureComment>();
            this.Votes = new HashSet<PictureVote>();

        }
    }
}
