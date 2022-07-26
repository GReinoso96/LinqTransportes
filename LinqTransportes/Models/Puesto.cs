using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LinqTransportes.Models
{
    public class Puesto
    {
        public int Id { get; set; }
        [Display(Name = "Puesto")]
        public string Nombre { get; set; }
    }
}