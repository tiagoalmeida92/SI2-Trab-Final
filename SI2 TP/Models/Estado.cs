using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI2_TP.Models
{
    public enum Estado
    {
        Inicial = 1,
        EmProcessamento,
        EmResolução,
        Recusado,
        Cancelado,
        Concluído
    }
}
