using PortalBahiaGas.Models.Entidade.Enuns;
using System;

namespace PortalBahiaGas.Models.Entidade
{
    public class RegistroOdorizador : AEntidade
    {
        public virtual RegistroTurno RegistroTurno { get; set; }
        public virtual Odorizador Odorizador { get; set; }
        public EStatusComunicacao? StatusComunicacao { get; set; }
        public Int32? PressaoTqOdor { get; set; }
        public Decimal? PumpDisp { get; set; }
    }
}