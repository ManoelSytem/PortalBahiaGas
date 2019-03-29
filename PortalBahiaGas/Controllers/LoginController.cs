using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace PortalBahiaGas.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            using (Repositorio<Usuario> lUsuarioRepositorio = new Repositorio<Usuario>())
            {
                if (!lUsuarioRepositorio.VerificarExistencia(x => x.Login == "evertonalmeida"))
                {
                    lUsuarioRepositorio.Adicionar(new Usuario()
                    {
                        Nome = "Everton",
                        Login = "evertonalmeida",
                        Senha = "totvs@2015".ToStringMD5(),
                        Perfil = Models.Entidade.Enuns.EPerfil.Administrador
                    });
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {

            StringBuilder lMensagem = new StringBuilder();

            if (!this.ModelState.IsValid)
            {
                Danger("Usuário não válido. Verifique se digitou os campos corretamente.");
                return this.View(usuario);
            }

            if (Membership.ValidateUser(usuario.Login, usuario.Senha))
            {
                using (Repositorio<Usuario> lRepositorio = new Repositorio<Usuario>())
                {
                    Usuario lUsuario = lRepositorio.Listar(x => x.Login.Equals(usuario.Login)).FirstOrDefault();
                    if (lUsuario == null)
                    {
                        Danger("Usuário não autorizado.");
                        return this.RedirectToAction("Index", "Login");
                    }

                    Session["UsuarioLogado"] = lUsuario;
                    Session.Timeout = 960;
                    return this.RedirectToAction("Index", "Home");
                }
            }

            Danger("Usuário não encontrado. Verifique se digitou login e senha corretamente.");
            return this.RedirectToAction("Index", "Login");
        }

        public ActionResult Sair()
        {
            Session["UsuarioLogado"] = null;
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Login");
        }
    }
}