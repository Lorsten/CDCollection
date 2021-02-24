using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MyCDCollection.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCDCollection.Models
{
    public class CDLended
    {

        [Key]
        public int PersonID { get; set; }

        [Required]
        [StringLength(30,MinimumLength = 3)]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Efternamn")]
        public string SurName { get; set; }


        [Display(Name = "Datumlånad")]
        public DateTime Lended { get; set; } = DateTime.Now;
        [ForeignKey("CD")]
        public int CDSID { get; set; }
        public CD CDs { get; set; }
    }
}
