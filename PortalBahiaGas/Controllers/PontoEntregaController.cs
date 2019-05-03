using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using PortalBahiaGas.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class PontoEntregaController : Controller
    {
        private Repositorio<PontoEntrega> PontoEntregaRepositorio = new Repositorio<PontoEntrega>();
        private Repositorio<Regiao> RegiaoRepositorio = new Repositorio<Regiao>();

        public ActionResult Index()
        {
            List<VmPontoEntrega> regiaolist = new List<VmPontoEntrega>();
            List<Regiao> regioes = new List<Regiao>();
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Index", "Login");


            foreach (PontoEntrega pontoEntrega in PontoEntregaRepositorio.Listar())
            {
                foreach (Regiao regiao in RegiaoRepositorio.Listar()) 
                {
                    if (regiao.Id == pontoEntrega.Regiao)
                    {
                        VmPontoEntrega vmPontoEntrega = new VmPontoEntrega();
                        vmPontoEntrega.Id = regiao.Id;
                        vmPontoEntrega.Regiao = regiao.Nome;
                        regiaolist.Add(vmPontoEntrega);
                    }
                }
            }

            Regiao selecione = new Regiao();
            selecione.Id = 0;
            selecione.Nome = "Selecione...";
            regioes.Add(selecione);
            foreach (Regiao regiao in RegiaoRepositorio.Listar())
            {
                     regioes.Add(regiao);
            }

            ViewData["RegiaoPorMunicipio"] = regiaolist;
            ViewData["Regiao"] = regioes;
            return View(PontoEntregaRepositorio.Listar());
        }

        public JsonResult Salvar(PontoEntrega pPontoEntrega)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                Validar(pPontoEntrega);
                pPontoEntrega = (pPontoEntrega.Id == 0) ? PontoEntregaRepositorio.Adicionar(pPontoEntrega) : PontoEntregaRepositorio.Editar(pPontoEntrega);
                lRetorno.Mensagem = "Registro inserido com sucesso.";
            }
            catch (Exception ex)
            {
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
                lRetorno.Erro = true;
            }
            return Json(lRetorno);
        }

        public JsonResult Remover(PontoEntrega pPontoEntrega)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                pPontoEntrega = PontoEntregaRepositorio.ObterPorId(pPontoEntrega.Id);
                PontoEntregaRepositorio.Remover(pPontoEntrega);
                lRetorno.Mensagem = "Registro removido com sucesso.";
            }
            catch (Exception ex)
            {
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
                lRetorno.Erro = true;
            }
            return Json(lRetorno);
        }

        private void Validar(PontoEntrega pPontoEntrega)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (pPontoEntrega.Nome == null) lMensagem.AppendLine("Informe o nome do ponto de entrega");
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());
        }
    }
}