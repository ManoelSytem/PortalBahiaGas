using System.ComponentModel;

namespace PortalBahiaGas.Models.Entidade.Enuns
{
    public enum ETipoMedicao
    {
        [Description("Manutenção")]
        SaintGlobal = 1,
        [Description("Calibração")]
        Calibração = 2,
        [Description("Validação")]
        Validação = 3
    }
}