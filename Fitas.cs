using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitas_TandemIMA
{
    class Fitas
    {
        public string Descricao { get; set; }

        public Fitas() { }

        public Fitas(string descricao)
        {
            Descricao = descricao;
        }
    }
}
