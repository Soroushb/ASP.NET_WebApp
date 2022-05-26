using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.EntityModels
{
    public class Artist
    {

        public Artist()
        {
            BirthOrStartDate = new DateTime();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(60)]
        public string BirthName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

        public string Executive { get; set; }

        public string Genre { get; set; }

        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(512)]
        public string UrlArtist { get; set; }

        public  ICollection<Album> Albums { get; set; }

    }
}