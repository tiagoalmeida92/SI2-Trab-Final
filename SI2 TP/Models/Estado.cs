using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SI2_TP.Models
{
    public enum Estado
    {
        Cancelado = 1,
        recusado,
        emresolução,
        emprocessamento,
        inicial,
        concluído
    }
}