using System;

namespace PortalBahiaGas.Models.Entidade
{
    public class RegistroPontoEntrega : AEntidade
    {
        public virtual RegistroTurno RegistroTurno { get; set; }
        public virtual PontoEntrega PontoEntrega { get; set; }
        public DateTime? Hora { get; set; }
        public Decimal? PressaoEntrada { get; set; }
        public Decimal? PressaoSaida { get; set; }
        public Decimal? VazaoEntrada { get; set; }
        public Decimal? VazaoSaida { get; set; }
        public Decimal? Desvio { get; set; }
        public Boolean? Penalidade { get; set; }
    }
}