using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.EntityModels
{
    public class Album
    {

        public Album()
        {
            ReleaseDate = new DateTime();
        }

        [Key]
        public int Id { get; set; }

        public string Coordinator { get; set; }

        public string Genre { get; set; }

        [StringLength(60)]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [StringLength(512)]
        public string UrlAlbum { get; set; }

        public  ICollection<Artist> Artists { get; set; }

        public  ICollection<Track> Tracks { get; set; }



    }
}