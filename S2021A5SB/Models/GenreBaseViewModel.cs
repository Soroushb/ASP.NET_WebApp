using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A5SB.Models
{
    public class GenreBaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60)]
        [Required]
        public string Name { get; set; }

    }
}