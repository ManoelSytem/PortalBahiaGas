using System.ComponentModel;

namespace PortalBahiaGas.Models.Entidade.Enuns
{
    public enum ETipoOcorrencia
    {
        [Description("Vazamento")]
        Vazamento = 1,
        [Description("Falta de gás")]
        FaltaDeGás = 2,
        [Description("Baixa pressão")]
        BaixaPressão = 3,
        [Description("Liberar GN")]
        LiberarGN = 4,
        [Description("Problema na ERPM")]
        ProblemaNaERPM = 5,
        [Description("Informação")]
        Informacao = 6,
        [Description("Solicitação de Cota")]
        SolicitacaoCota = 7,
        [Description("Escavação")]
        Escavação = 9,
        [Description("Incêndio na vegetação")]
        IncêndioNaVegetação = 10,
        [Description("Outros")]
        Outros = 8
    }
}