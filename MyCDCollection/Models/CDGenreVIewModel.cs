using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MyCDCollection.Models
{
    public class CDGenreVIewModel
    {
        public List<CD> CDS { get; set; }
        public SelectList Genre { get; set; }

        [Display(Name ="Genre")]
        public string CDGenre { get; set; }

        [Display(Name = "Album")]
        public string SearchName { get;set;}
    }
}
