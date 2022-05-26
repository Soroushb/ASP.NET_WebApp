using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.EntityModels
{
    public class Track
    {
        [Key]
        public int id { get; set; }

        public string Clerk { get; set; }

        public string Composers { get; set; }

        public string Genre { get; set; }

        [StringLength(60)]
        [Required]
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }

    }
}