using System;
using System.Collections.Generic;

namespace PortalBahiaGas.Models.Entidade
{
    public class Operador : AEntidade
    {
        public String Nome { get; set; }
        public virtual ICollection<RegistroTurno> RegistrosTurno { get; protected set; }
        public String CodigoProtheus { get; set; }
        public virtual String Localidade { get; set; }
    }
}