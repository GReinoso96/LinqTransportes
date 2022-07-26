using LinqTransportes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinqTransportes.Statics
{
    public static class Datos
    {
        public static Dictionary<int,Trabajador> Trabajadores = new Dictionary<int,Trabajador>();
        public static Dictionary<int,Puesto> Puestos = new Dictionary<int, Puesto>();
    }
}