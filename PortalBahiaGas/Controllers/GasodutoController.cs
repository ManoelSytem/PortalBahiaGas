using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Text;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class GasodutoController : Controller
    {
        private Repositorio<Gasoduto> GasodutoRepositorio = new Repositorio<Gasoduto>();

        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Index", "Login");
            return View(GasodutoRepositorio.Listar());
        }

        public JsonResult Salvar(Gasoduto pGasoduto)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                Validar(pGasoduto);
                pGasoduto = (pGasoduto.Id == 0) ? GasodutoRepositorio.Adicionar(pGasoduto) : GasodutoRepositorio.Editar(pGasoduto);
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

        public JsonResult Remover(Gasoduto pGasoduto)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                pGasoduto = GasodutoRepositorio.ObterPorId(pGasoduto.Id);
                GasodutoRepositorio.Remover(pGasoduto);
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

        private void Validar(Gasoduto pGasoduto)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (pGasoduto.Nome == null) lMensagem.AppendLine("Informe o nome do gasoduto");
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());
        }
    }
}