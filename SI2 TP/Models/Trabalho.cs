using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SI2_TP.Models
{
    public class Trabalho
    {
        public int Coordenador { get; set; }
        public int AreaIntervencao { get; set; }
        public string Desc { get; set; }
        public bool Concluido { get; set; }
    }
}