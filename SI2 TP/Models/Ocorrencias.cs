using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SI2_TP.Models
{
    public class Ocorrencias
    {
        public int id { get; set; }
        public DateTime dataHoraEnt { get; set; }
        public DateTime dataHoraAct { get; set; }
        public Estado estado { get; set; }
        public Tipo tipo { get; set; }
        public int secInst { get; set; }
        public int secPiso { get; set; }
        public int secZona { get; set; }

    }
}