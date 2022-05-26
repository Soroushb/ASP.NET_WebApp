using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.Models
{
    public class TrackBaseViewModel
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Track Name")]
        [StringLength(40)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Composer name(s)")]
        public string Composers { get; set; }

        [Display(Name = "Track genre")]
        public string Genre { get; set; }

        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }



    }
}