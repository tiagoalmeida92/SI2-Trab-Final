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
        public string secZona { get; set; }

        public Ocorrencias(int id,DateTime ent,DateTime act,Estado estado,Tipo tipo,int secInst,int secPiso,string secZona)
        {
            this.id = id;
            dataHoraAct = act;
            dataHoraEnt = ent;
            this.estado = estado;
            this.tipo = tipo;
            this.secInst = secInst;
            this.secPiso = secPiso;
            this.secZona = secZona;
        }

    }
}