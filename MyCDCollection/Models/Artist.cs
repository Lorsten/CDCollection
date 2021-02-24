using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyCDCollection.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }

        [Display(Name="Artist")]
        public string ArtistName { get; set; }
        public ICollection<CD> CDS { get; set; }
    }
}
