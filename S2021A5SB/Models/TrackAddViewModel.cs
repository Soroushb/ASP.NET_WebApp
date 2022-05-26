using S2021A5SB.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.Models
{
    public class TrackAddViewModel
    {
         [Key]
        public int id { get; set; }

        public string Clerk { get; set; }

        public string Composers { get; set; }

   
        public string Genre { get; set; }

        [StringLength(40)]
        [Required]
        public string Name { get; set; }

        public int AlbumId { get; set; }


        public virtual ICollection<Album> Albums { get; set; }
    }
}