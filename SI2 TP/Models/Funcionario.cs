using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SI2_TP.Models
{
    public class Funcionario
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public string Estado { get; set; }
        public bool Coordenador { get; set; }
        public bool Admin { get; set; }
    }
}