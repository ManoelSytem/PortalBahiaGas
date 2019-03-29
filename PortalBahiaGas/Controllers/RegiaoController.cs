using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Text;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class RegiaoController : Controller
    {
        private Repositorio<Regiao> RegiaoRepositorio = new Repositorio<Regiao>();

        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Index", "Login");
            return View(RegiaoRepositorio.Listar());
        }

        public JsonResult Salvar(Regiao pRegiao)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                Validar(pRegiao);
                pRegiao = (pRegiao.Id == 0) ? RegiaoRepositorio.Adicionar(pRegiao) : RegiaoRepositorio.Editar(pRegiao);
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

        public JsonResult Remover(Regiao pRegiao)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                pRegiao = RegiaoRepositorio.ObterPorId(pRegiao.Id);
                RegiaoRepositorio.Remover(pRegiao);
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

        private void Validar(Regiao pRegiao)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (pRegiao.Nome == null) lMensagem.AppendLine("Informe o nome da região");
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());
        }
    }
}