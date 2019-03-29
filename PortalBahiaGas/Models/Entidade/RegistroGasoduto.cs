using System;

namespace PortalBahiaGas.Models.Entidade
{
    public class RegistroGasoduto : AEntidade
    {
        public virtual RegistroTurno RegistroTurno { get; set; }
        public virtual Gasoduto Gasoduto { get; set; }
        public Decimal? Pressao { get; set; }
        public Decimal? Vazao { get; set; }
        //public virtual Operador Operador { get; set; }
        //public DateTime Hora { get; set; }
    }
}