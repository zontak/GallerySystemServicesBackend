using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GallerySysteServices.Models
{
    public class PictureVote
    {
        public int Id { get; set; }
        public bool isPositive { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual User User { get; set; }
    }
}
