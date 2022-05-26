using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S2021A5SB.Models
{
    public class ArtistAddFormViewModel
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

        [Display(Name="Artist's primary genre")]
        public SelectList GenreList { get; set; }

        [Display(Name = "URL to artist photo")]
        [StringLength(512)]
        public string UrlArtist { get; set; }

    }
}