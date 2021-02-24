using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCDCollection.Models
{
    public class CD
    {

        public int CDID { get; set; }


        [Required]
        public string Album { get; set; }

        [Required]
        public string Genre { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "UtgivningsDatum")]
        public DateTime ReleaseDate { get; set; }


        [ForeignKey("ArtistNameID")]
    
        public int ArtistNameID { get; set; }
        public Artist Artist { get; set; }

        public CDLended Name { get; set; }

        [Display(Name ="Utlånad")]
        public Boolean Lended { get; set; }
    }
}
