using PortalBahiaGas.Models.Entidade.Enuns;
using System;
using System.Collections.Generic;
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
        public Decimal? PorRegiao { get; set; }
        public Boolean? Penalidade { get; set; }
        public Boolean? PenalidadePorRegiao { get; set; }
     
        public static RegistroTurno CalcularDesvioEPenalidadePorRegiao(RegistroTurno registroTurno)
        {
            int regiao = 0;
            decimal? lDesvio = 0;
            decimal? totalVazaoSaida = 0;
            decimal? totalVazaoEntrada = 0;
            foreach (RegistroPontoEntrega item in registroTurno.RegistrosPontoEntrega)
            {
                if (regiao == item.PontoEntrega.Id || regiao == 0)
                {
                    regiao = item.PontoEntrega.Id;
                    totalVazaoSaida += item.VazaoSaida;
                    totalVazaoEntrada += item.VazaoEntrada;
                }
                else
                {
                    lDesvio = (Convert.ToInt32(Convert.ToDecimal(totalVazaoSaida / totalVazaoEntrada) * 100)) - 100;
                    if (CalcularPenalidade(item.RegistroTurno.Turno, lDesvio))
                    {
                        foreach (RegistroPontoEntrega itemEntrega in registroTurno.RegistrosPontoEntrega)
                        {
                        }
                    }
                    break;
                    
                }
            }
            return registroTurno;
        }

        public static bool CalcularPenalidade(ETurno pTurno, decimal? pDesvio)
        {
            bool lPenalidade = false;
            if (pTurno == ETurno.De23as7)
                if (pDesvio < -10 || pDesvio > 5) lPenalidade = true;
                else lPenalidade = false;
            return lPenalidade;
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