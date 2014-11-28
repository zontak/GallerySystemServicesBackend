using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GallerySysteServices.Models
{
    public class AlbumComment
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual Album Album { get; set; }
        public virtual User User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
