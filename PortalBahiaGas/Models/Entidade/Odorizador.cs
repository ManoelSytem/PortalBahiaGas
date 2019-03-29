using System;

namespace PortalBahiaGas.Models.Entidade
{
    public class Odorizador : AEntidade
    {
        public String Nome { get; set; }
        public Decimal SetPointCcStk { get; set; }
        public String SetPointPsi { get; set; }
    }
}