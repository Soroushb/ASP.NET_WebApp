using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.Models
{
    public class ArtistWithDetailViewModel : ArtistBaseViewModel
    {


        [Display(Name = "Number of Albums")]
        public int AlbumsCount { get; set; }
        public ICollection<AlbumBaseViewModel> Albums { get; set; }
    }
}