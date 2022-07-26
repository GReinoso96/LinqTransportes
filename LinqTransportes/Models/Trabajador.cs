using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqTransportes.Models
{
    public class Trabajador
    {
        public int ID { get; set; }
        public string Nombres { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Rut { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha { get; set; }
        public virtual Puesto Puesto { get; set; }
    }
}