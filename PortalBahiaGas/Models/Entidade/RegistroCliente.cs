using System;

namespace PortalBahiaGas.Models.Entidade
{
    public class RegistroCliente : AEntidade
    {
        public virtual RegistroTurno RegistroTurno { get; set; }
        public virtual Cliente Cliente { get; set; }
        public DateTime? Hora { get; set; }
        public Decimal? PressaoEntrada { get; set; }
        public Decimal? PressaoSaida { get; set; }
        public Decimal? VazaoInstantanea { get; set; }
        public Decimal? VazaoAcumulada { get; set; }
    }
}