using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Text;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class PontoEntregaController : Controller
    {
        private Repositorio<PontoEntrega> PontoEntregaRepositorio = new Repositorio<PontoEntrega>();

        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Index", "Login");
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