using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A5SB.Models
{
    public class AlbumAddFormViewModel
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Album Name")]
        [StringLength(40)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string Coordinator { get; set; }


        [Display(Name = "URL to album image (cover art)")]
        [StringLength(512)]
        public string UrlAlbum { get; set; }

        public string ArtistName { get; set; }

        [Display(Name = "Album's Primary Genre")]
        public SelectList GenreList { get; set; }

        public MultiSelectList ArtistList { get; set; }

        public MultiSelectList TrackList { get; set; }

    }
}