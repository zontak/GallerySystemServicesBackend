namespace GallerySysteServices.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        [Required]
        public string AuthCode { get; set; }

        public string SessionKey { get; set; }

        public DateTime CreatedAt { get; set; }
        public User()
        {
            this.Albums = new HashSet<Album>();
            this.Categories = new HashSet<Category>();
        }
    }
}
