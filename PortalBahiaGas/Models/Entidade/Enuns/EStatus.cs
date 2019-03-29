using System.ComponentModel;

namespace PortalBahiaGas.Models.Entidade.Enuns
{
    public enum EStatus
    {
        [Description("Pendente")]
        Pendente = 1,
        [Description("Iniciado")]
        Iniciado = 2,
        [Description("Concluído")]
        Concluído = 3
    }
}