using S2021A5SB.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.Models
{
    public class AlbumAddViewModel
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


        [Display(Name = "URL to album image (cover art)")]
        [StringLength(512)]
        public string UrlAlbum { get; set; }


        [Display(Name = "Album's Primary Genre")]
        public string Genre { get; set; }

        public IEnumerable<int> ArtistIds { get; set; }

        public IEnumerable<int> TrackIds { get; set; }

        public IEnumerable<Artist> Artists { get; set; }

        public IEnumerable<Track> Tracks { get; set; }

        public string Coordinator { get; set; }

    }
}