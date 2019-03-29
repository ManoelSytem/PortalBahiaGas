using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalBahiaGas.Models
{
    public class Retorno
    {
        public Retorno()
        {
            Erro = false;
        }

        public Object Objeto { get; set; }
        public bool Erro { get; set; }
        public String Mensagem { get; set; }
        public String PilhaErro { get; set; }
    }
}