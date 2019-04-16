namespace PortalBahiaGas.Models.Entidade
{
    public class OperadorRegistroTurno 
    {
        public string Local { get; set; }
        public bool SalaControle { get; set; }

        public int IdRegistroTurno { get; set; }
        public int IdOperador { get; set; }

        public virtual RegistroTurno RegistroTurno { get; set; }
        public virtual Operador Operador { get; set; }
    }
}