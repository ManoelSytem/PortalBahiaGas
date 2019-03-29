using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Text;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class UsuarioController : Controller
    {
        private Repositorio<Usuario> UsuarioRepositorio = new Repositorio<Usuario>();

        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Index", "Login");
            else if (((Usuario)Session["UsuarioLogado"]).Perfil != Models.Entidade.Enuns.EPerfil.Administrador) return RedirectToAction("Index", "Home");

            return View(UsuarioRepositorio.Listar());
        }

        public JsonResult Salvar(Usuario pUsuario)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                Validar(pUsuario);
                pUsuario.Senha = pUsuario.Senha.ToStringMD5();
                pUsuario = (pUsuario.Id == 0) ? UsuarioRepositorio.Adicionar(pUsuario) : UsuarioRepositorio.Editar(pUsuario);
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

        public JsonResult Remover(Usuario pUsuario)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                pUsuario = UsuarioRepositorio.ObterPorId(pUsuario.Id);
                UsuarioRepositorio.Remover(pUsuario);
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

        private void Validar(Usuario pUsuario)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (pUsuario.Nome == null) lMensagem.AppendLine("Informe o nome do usuário");
            if (pUsuario.Login == null) lMensagem.AppendLine("Informe o login do usuário");
            if (pUsuario.Senha == null) lMensagem.AppendLine("Informe a senha do usuário");
            if (pUsuario.Perfil == null) lMensagem.AppendLine("Informe o perfil do usuário");
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());
        }
    }
}