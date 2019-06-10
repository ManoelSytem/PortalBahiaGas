using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalBahiaGas.ViewModel
{
    public class VmOperador
    {
        public String Nome { get; set; }
        public String CodigoProtheus { get; set; }
        public virtual String Localidade { get; set; }
        public int Alocacao { get; set; }
        public bool Camacari { get; set; }
        public bool Feira { get; set; }
        public bool Salvador { get; set; }
        public bool SalaControle { get; set; }
    }
}