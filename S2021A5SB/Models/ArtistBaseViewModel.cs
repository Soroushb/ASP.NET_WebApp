using S2021A5SB.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.Models
{
    public class ArtistBaseViewModel
    {
        [Key]
        public int Id { get; set; }


        [Display(Name = "Artist name or stage name")]
        [StringLength(40)]
        public string Name { get; set; }

        [Display(Name = "If applicable, artist's birth name")]
        [StringLength(40)]
        public string BirthName { get; set; }

        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Artist photo")]
        [StringLength(512)]
        public string UrlArtist { get; set; }

        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }


        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

       



    }
}