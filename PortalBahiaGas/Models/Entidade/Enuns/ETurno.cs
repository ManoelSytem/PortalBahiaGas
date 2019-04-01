using System.ComponentModel;

namespace PortalBahiaGas.Models.Entidade.Enuns
{
    public enum ETurno
    {
        [Description("7:00 às 15:00")]
        De7as15 = 1,
        [Description("15:00 às 23:00")]
        De15as23 = 2,
        [Description("23:00 às 7:00")]
        De23as7 = 3
    }
}