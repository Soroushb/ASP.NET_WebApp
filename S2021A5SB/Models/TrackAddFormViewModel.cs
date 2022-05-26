using S2021A5SB.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A5SB.Models
{
    public class TrackAddFormViewModel
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Track name")]
        [StringLength(40)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Composer names (comma-separated)")]
        public string Composers { get; set; }


        [Display(Name = "Track genre")]
        public SelectList GenreList { get; set; }

        public int AlbumId { get; set; }

        [Display(Name="Album Name")]
        public string AlbumName { get; set; }


        public virtual ICollection<Album> Albums { get; set; }

    }
}