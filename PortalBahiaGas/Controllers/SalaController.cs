﻿using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Entidade.Enuns;
using PortalBahiaGas.Models.Persistencia;
using PortalBahiaGas.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class SalaController : Controller
    {
        private Repositorio<RegistroTurno> TurnoRepositorio = new Repositorio<RegistroTurno>();
        private Repositorio<Operador> OperadorRepositorio;
        // GET: Sala
        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null)
                return RedirectToAction("Index", "Login");

            
            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);
            List<VmOperador> listaOperaSalaControle = new List<VmOperador>();
            VmOperador operadorVm;

            foreach (Operador operador in OperadorRepositorio.Listar())
            {
                foreach (Operador operadorProthues in OperadorRepositorio.ObterOperadoresDoProtheus(operador.CodigoProtheus))
                {
                    operadorVm = new VmOperador();
                    operadorVm.CodigoProtheus = operadorProthues.CodigoProtheus;
                    operadorVm.Nome = operadorProthues.Nome;
                    operadorVm.Alocacao = operador.Alocacao;
                    operadorVm.Localidade = operadorProthues.Localidade;
                    if ((int)ESala.Reg1 ==  operadorVm.Alocacao)
                    {
                        operadorVm.Camacari = true;
                    }
                    if ((int)ESala.Reg2 == operadorVm.Alocacao)
                    {
                        operadorVm.Feira = true;
                    }
                    if ((int)ESala.Reg3 == operadorVm.Alocacao)
                    {
                        operadorVm.Salvador = true;
                    }
                    if ((int) ESala.Reg4 == operadorVm.Alocacao)
                    {
                        operadorVm.SalaControle = true;
                    }
                   
                    listaOperaSalaControle.Add(operadorVm);
                }
            }
               
            ViewData.Add("OperadorSalaControle", listaOperaSalaControle.ToList());
            return View();
        }

        [HttpGet]
        public ActionResult AlocacaoOperador(string CodigoProthues, string Local, string NomeOperador, string CodigoProthuesF, string LocalF, string NomeOperadorF, string CodigoProthuesS, string LocalS, string NomeOperadorS, string CodigoProthuesSl, string LocalSl, string NomeOperadorSl)
        {
           

                List<VmOperador> listaOperaPreSelecionadoNew = new List<VmOperador>();

                VmOperador operadorVm = new VmOperador();
                operadorVm.CodigoProtheus = CodigoProthues;
                operadorVm.Localidade = "Camaçari";
                operadorVm.Nome = NomeOperador;
            if (operadorVm.CodigoProtheus != "")
                listaOperaPreSelecionadoNew.Add(operadorVm);

                VmOperador operadorVF = new VmOperador();
                        operadorVF.CodigoProtheus = CodigoProthuesF;
                        operadorVF.Localidade = "Feira de Santana";
                        operadorVF.Nome = NomeOperadorF;
            if(operadorVF.CodigoProtheus != "")
                listaOperaPreSelecionadoNew.Add(operadorVF);

                     VmOperador operadorVS = new VmOperador();
                        operadorVS.CodigoProtheus = CodigoProthuesS;
                        operadorVS.Localidade = "Salvador";
                        operadorVS.Nome = NomeOperadorS;
            if (operadorVS.CodigoProtheus != "")
                listaOperaPreSelecionadoNew.Add(operadorVS);

                     VmOperador operadorVSL = new VmOperador();
                        operadorVSL.CodigoProtheus = CodigoProthuesSl;
                        operadorVSL.Localidade = "Sala de Controle";
                        operadorVSL.Nome = NomeOperadorSl;
            if (operadorVSL.CodigoProtheus != "")
                listaOperaPreSelecionadoNew.Add(operadorVSL);

                ViewData.Add("OperadorPreSelecao", listaOperaPreSelecionadoNew.ToList());
            
            return View();
        }

        [HttpPost]
        public ActionResult SalvarAlocacaoOperadores(string[] valuesSelect)
        {

            Retorno lRetorno = new Retorno();
            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);

            try
            {

                foreach (var operadorBase in OperadorRepositorio.Listar())
                {
                    operadorBase.Alocacao = 0;
                    OperadorRepositorio.Editar(operadorBase);
                }

                foreach (var operadorBase in OperadorRepositorio.Listar())
                {
                    foreach (string value in valuesSelect)
                    {
                        int position = value.IndexOf("-");
                        string codigoProthuesAlocacao = value.Substring(0, position);
                        if (codigoProthuesAlocacao == operadorBase.CodigoProtheus)
                        {
                            string Alocacao = value.Substring(position+1);
                            if(ESala.Reg1.ObterDescricao() == Alocacao)
                            {
                                operadorBase.Alocacao = (int)ESala.Reg1;
                            }
                            if (ESala.Reg2.ObterDescricao() == Alocacao)
                            {
                                operadorBase.Alocacao = (int)ESala.Reg2;
                            }
                            if (ESala.Reg3.ObterDescricao() == Alocacao)
                            {
                                operadorBase.Alocacao = (int)ESala.Reg3;
                            }
                            if (ESala.Reg4.ObterDescricao() == Alocacao)
                            {
                                operadorBase.Alocacao = (int)ESala.Reg4;
                            }
                            OperadorRepositorio.Editar(operadorBase);
                        }
                    }
                }

                lRetorno.Mensagem = "Alocação realizada com sucesso!";

            }
            catch (Exception ex)
            {
                lRetorno.Erro = true;
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
            }
            return Json(lRetorno);
        }
    }
}