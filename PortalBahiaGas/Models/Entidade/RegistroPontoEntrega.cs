﻿using PortalBahiaGas.Models.Entidade.Enuns;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PortalBahiaGas.Models.Entidade
{
    public class RegistroPontoEntrega : AEntidade
    {
        public static Repositorio<Regiao> RegiaoRepositorio = new Repositorio<Regiao>();
        public virtual RegistroTurno RegistroTurno { get; set; }
        public virtual PontoEntrega PontoEntrega { get; set; }
        public DateTime? Hora { get; set; }
        public Decimal? PressaoEntrada { get; set; }
      
        public Decimal? PressaoSaida { get; set; }
        public Decimal? VazaoEntrada { get; set; }
        public Decimal? VazaoSaida { get; set; }
        public Decimal? Desvio { get; set; }
        public Boolean? Penalidade { get; set; }


        public static List<RegistroPontoEntrega> CalcularDesvioEPenalidadePorRegiao(IOrderedEnumerable<RegistroPontoEntrega> registroPontoEntrega)
        {
            int regiao = 0;
            decimal? lDesvio = 0;
            decimal? totalVazaoSaida = 0;
            decimal? totalVazaoEntrada = 0;

            foreach (RegistroPontoEntrega regPontoEntrega in registroPontoEntrega)
            {
                if (regiao == regPontoEntrega.PontoEntrega.Regiao || regiao == 0)
                {
                    regiao = regPontoEntrega.PontoEntrega.Regiao;
                    if (regPontoEntrega.VazaoSaida != null && regPontoEntrega.VazaoEntrada != null)
                    {
                        totalVazaoSaida += regPontoEntrega.VazaoSaida;
                        totalVazaoEntrada += regPontoEntrega.VazaoEntrada;
                    }
                }
                else
                {
                    lDesvio = (Convert.ToInt32(Convert.ToDecimal(totalVazaoSaida / totalVazaoEntrada) * 100)) - 100;
                    if (CalcularPenalidade(lDesvio))
                    {
                        var regPtEntrega = registroPontoEntrega.ToList().FindAll(x => x.PontoEntrega.Regiao == regiao);
                        foreach(RegistroPontoEntrega item in regPtEntrega)
                        {
                              item.Penalidade = true;
                        }
                    }
                    regiao = 0;
                    totalVazaoSaida = 0;
                    totalVazaoEntrada = 0;
                    if (regPontoEntrega.VazaoSaida != null && regPontoEntrega.VazaoEntrada != null)
                    {
                        totalVazaoSaida += regPontoEntrega.VazaoSaida;
                        totalVazaoEntrada += regPontoEntrega.VazaoEntrada;
                    }

                }

            }

            lDesvio = (Convert.ToInt32(Convert.ToDecimal(totalVazaoSaida / totalVazaoEntrada) * 100)) - 100;
            if (CalcularPenalidade(lDesvio))
            {
                var regPtEntrega = registroPontoEntrega.ToList().FindAll(x => x.PontoEntrega.Regiao == regiao);
                foreach (RegistroPontoEntrega item in regPtEntrega)
                {
                    item.Penalidade = true;
                }
            }

            return registroPontoEntrega.ToList();
        }

        public static decimal CalcularDesvioPorRegiao(decimal pVazaoProgramada, decimal pVazaoRetirada)
        {
            decimal lDesvio = 0;
            if(pVazaoProgramada !=0 || pVazaoRetirada != 0) {
            lDesvio = (Convert.ToInt32(Convert.ToDecimal(pVazaoRetirada / pVazaoProgramada) * 100)) - 100;
            }
            return lDesvio;
        }
        public static bool CalcularPenalidade(decimal? pDesvio)
        {
            bool lPenalidade = false;
            if (pDesvio < -10 || pDesvio > 5) lPenalidade = true;
            else lPenalidade = false;
            return lPenalidade;
        }

        public static decimal CalcularAjusteDesvioPorRegiao(decimal porcentagemRegiao)
        {
            decimal porcentagemRegiaoAjuste = 0;
            if (porcentagemRegiao > 5)
            {
                return porcentagemRegiaoAjuste =  porcentagemRegiao - 5;
            }
            else if(porcentagemRegiao < -5) {
                return porcentagemRegiaoAjuste = porcentagemRegiao + 5;
            }

            return porcentagemRegiaoAjuste;
        }


        public static decimal CalcularRestoMetroCubicoRegiao(decimal pVazaoProgramada, decimal pVazaoRetirada)
        {
            decimal total = 0;
            if (CalcularAjusteDesvioPorRegiao(CalcularDesvioPorRegiao(pVazaoProgramada, pVazaoRetirada)) < 0)
            {
              total = pVazaoProgramada - pVazaoRetirada;
            }
            return total;
        }

        public static string ObterRegiao(int codigo)
        {
            string regiaodesc = "";
            Regiao regiao = new Regiao();
            regiao = RegiaoRepositorio.ObterPorId(codigo);
            return regiao.Nome;
        }

    }
}