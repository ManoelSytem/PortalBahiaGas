using System.ComponentModel;

namespace PortalBahiaGas.Models.Entidade.Enuns
{
    public enum EStatusComunicacao
    {
        [Description("Conectado")]
        Conectado = 1,
        [Description("Desconectado")]
        Desconectado = 2,
    }
}