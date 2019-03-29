using System.ComponentModel;

namespace PortalBahiaGas.Models.Entidade.Enuns
{
    public enum EModalidade
    {
        [Description("Flexível")]
        Flexível = 1,
        [Description("Firme infléxivel")]
        FirmeInfléxivel = 2,
        [Description("Interruptível")]
        Interruptível = 3
    }
}