using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Text;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class OdorizadorController : Controller
    {
        private Repositorio<Odorizador> OdorizadorRepositorio = new Repositorio<Odorizador>();

        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Index", "Login");
            return View(OdorizadorRepositorio.Listar());
        }

        public JsonResult Salvar(Odorizador pOdorizador)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                Validar(pOdorizador);
                pOdorizador = (pOdorizador.Id == 0) ? OdorizadorRepositorio.Adicionar(pOdorizador) : OdorizadorRepositorio.Editar(pOdorizador);
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

        public JsonResult Remover(Odorizador pOdorizador)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                pOdorizador = OdorizadorRepositorio.ObterPorId(pOdorizador.Id);
                OdorizadorRepositorio.Remover(pOdorizador);
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

        private void Validar(Odorizador pOdorizador)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (pOdorizador.Nome == null) lMensagem.AppendLine("Informe o nome do Odorizador");
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());
        }
    }
}