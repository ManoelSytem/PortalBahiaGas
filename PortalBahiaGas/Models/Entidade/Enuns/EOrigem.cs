using PortalBahiaGas.Attribute;
using System.ComponentModel;

namespace PortalBahiaGas.Models.Entidade.Enuns
{
    public enum EOrigem
    {
        [Filtro(TipoFiltro.Ocorrencia)]
        [Description("Operação")]
        Operação = 1,
        [Filtro(TipoFiltro.Pendencia)]
        [Description("Medição")]
        Medição = 2,
        [Filtro(TipoFiltro.Pendencia)]
        [Description("Programação")]
        Programação = 3,
        [Filtro(TipoFiltro.Ocorrencia)]
        [Description("SAC")]
        SAC = 4,
        [Filtro(TipoFiltro.Pendencia)]
        [Description("Automação")]
        Automação = 5,
        [Filtro(TipoFiltro.Pendencia)]
        [Description("Odoração")]
        Odoração = 6,
        [Filtro(TipoFiltro.Pendencia)]
        [Description("Outros")]
        Outros = 7
    }
}