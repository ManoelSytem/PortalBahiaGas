using System.ComponentModel;

namespace PortalBahiaGas.Models.Entidade.Enuns
{
    public enum ELocal
    {
        [Description("Grandes clientes")]
        Cliente = 1,
        [Description("Infra-estrutura")]
        Infraestrutura = 2,
        [Description("Outro")]
        Outro = 3,
    }
}