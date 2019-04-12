using PortalBahiaGas.Models.Entidade.Enuns;
using System;
using System.ComponentModel;
using System.Reflection;

namespace PortalBahiaGas.Models.Entidade
{
    public class RegistroPontoEntrega : AEntidade
    {
        public virtual RegistroTurno RegistroTurno { get; set; }
        public virtual PontoEntrega PontoEntrega { get; set; }
        public DateTime? Hora { get; set; }
        public Decimal? PressaoEntrada { get; set; }
        public Decimal? PressaoSaida { get; set; }
        public Decimal? VazaoEntrada { get; set; }
        public Decimal? VazaoSaida { get; set; }
        public Decimal? Desvio { get; set; }
        public Boolean? Penalidade { get; set; }

        public static Decimal? CalcularDesvioPorRegisao(Decimal? pVazaoProgramada, Decimal? pVazaoRetirada)
        {
            Decimal? lDesvio = 0;
            lDesvio = (Convert.ToInt32(Convert.ToDecimal(pVazaoRetirada / pVazaoProgramada) * 100)) - 100;
            return lDesvio;
        }

        public static string ObterRegiao(int codigo)
        {
            string regiaodesc ="";
            switch (codigo)
            {
                case 1:
                    regiaodesc = ERegiao.Reg1.ObterDescricao();
                    break;
                case 2:
                    regiaodesc = ERegiao.Reg2.ObterDescricao();
                    break;
                case 3:
                    regiaodesc = ERegiao.Reg3.ObterDescricao();
                    break;
            }

            return regiaodesc;
        }

    }
}