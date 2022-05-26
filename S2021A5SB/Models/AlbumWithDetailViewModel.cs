using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.Models
{
    public class AlbumWithDetailViewModel : AlbumBaseViewModel
    {
        [Display(Name = "Number of tracks on this album")]
        public int TracksCount { get; set; }

        [Display(Name = "Number of artists on this album")]
        public int ArtistsCount { get; set; }

        public ICollection<ArtistBaseViewModel> Artists { get; set; }

        public ICollection<TrackBaseViewModel> Tracks { get; set; }

    }
}