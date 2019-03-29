using System;

namespace PortalBahiaGas.Attribute
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Filtro : System.Attribute
    {
        private TipoFiltro _tipo;

        public TipoFiltro Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        public Filtro(TipoFiltro tipo)
        {
            _tipo = tipo;
        }
    }

    public enum TipoFiltro
    {
        Ocorrencia,
        Pendencia
    }
}