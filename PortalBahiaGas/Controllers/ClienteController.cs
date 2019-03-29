using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class ClienteController : Controller
    {
        private Repositorio<Cliente> ClienteRepositorio = new Repositorio<Cliente>();

        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Index", "Login");
            return View(ClienteRepositorio.Listar());
        }

        public JsonResult Salvar(Cliente pCliente)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                Validar(pCliente);
                pCliente = (pCliente.Id == 0) ? ClienteRepositorio.Adicionar(pCliente) : ClienteRepositorio.Editar(pCliente);
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

        public JsonResult Remover(Cliente pCliente)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                pCliente = ClienteRepositorio.ObterPorId(pCliente.Id);
                ClienteRepositorio.Remover(pCliente);
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
                lRetorno.Objeto = ClienteRepositorio.Listar().FirstOrDefault(x => x.IdProtheus == Id.ToString());
            }
            catch (Exception ex)
            {
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
                lRetorno.Erro = true;
            }
            return Json(lRetorno);
        }

        public JsonResult ObterPorNomeClienteDoProtheus(FormCollection pForm)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                lRetorno.Objeto = ClienteRepositorio.ObterPorNomeClienteDoProtheus(pForm["Nome"]);
            }
            catch (Exception ex)
            {
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
                lRetorno.Erro = true;
            }
            return Json(lRetorno);
        }
        
        private void Validar(Cliente pCliente)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (pCliente.Nome == null) lMensagem.AppendLine("Informe o nome do cliente");
            if (pCliente.IdProtheus == null) lMensagem.AppendLine("Informe o id do Protheus");
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());
        }
    }
}