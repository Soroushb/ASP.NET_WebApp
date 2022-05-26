using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        [Display(Name = "Number of albums with this track")]
        public int AlbumsCount { get; set; }

       // [Display(Name = "Albums with this track")]
       // public IEnumerable<AlbumBaseViewModel> AlbumsName { get; set; }

        [Display(Name = "Albums with this track")]
        public IEnumerable<string> AlbumNames { get; set; }

    }
}