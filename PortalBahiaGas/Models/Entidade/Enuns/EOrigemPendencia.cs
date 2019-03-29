using System.ComponentModel;

namespace PortalBahiaGas.Models.Entidade.Enuns
{
    public enum EOrigemPendencia
    {
        [Description("Medição")]
        Medição = 2,
        [Description("Programação")]
        Programação = 3,
        [Description("Automação")]
        Automação = 5,
        [Description("Odoração")]
        Odoração = 6,
        [Description("Outros")]
        Outros = 7
    }
}