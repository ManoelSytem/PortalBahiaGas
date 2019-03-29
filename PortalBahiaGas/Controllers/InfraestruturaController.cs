using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class InfraestruturaController : Controller
    {
        private Repositorio<Infraestrutura> InfraestruturaRepositorio = new Repositorio<Infraestrutura>();

        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Index", "Login");
            return View(InfraestruturaRepositorio.Listar());
        }

        public JsonResult Salvar(Infraestrutura pInfraestrutura)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                Validar(pInfraestrutura);
                pInfraestrutura = (pInfraestrutura.Id == 0) ? InfraestruturaRepositorio.Adicionar(pInfraestrutura) : InfraestruturaRepositorio.Editar(pInfraestrutura);
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

        public JsonResult Remover(Infraestrutura pInfraestrutura)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                pInfraestrutura = InfraestruturaRepositorio.ObterPorId(pInfraestrutura.Id);
                InfraestruturaRepositorio.Remover(pInfraestrutura);
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

        public JsonResult ObterPorIdProtheus(String Id)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                lRetorno.Objeto = InfraestruturaRepositorio.Listar().FirstOrDefault(x => x.IdProtheus == Id.ToString());
            }
            catch (Exception ex)
            {
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
                lRetorno.Erro = true;
            }
            return Json(lRetorno);
        }

        public JsonResult ObterPorNomeInfraestruturaDoProtheus(FormCollection pForm)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                lRetorno.Objeto = InfraestruturaRepositorio.ObterPorNomeInfraEstruturaDoProtheus(pForm["Nome"]);
            }
            catch (Exception ex)
            {
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
                lRetorno.Erro = true;
            }
            return Json(lRetorno);
        }

        private void Validar(Infraestrutura pInfraestrutura)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (pInfraestrutura.Nome == null) lMensagem.AppendLine("Informe o nome da infraestrutura");
            if (pInfraestrutura.IdProtheus == null) lMensagem.AppendLine("Informe o id do Protheus");
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());
        }
    }
}