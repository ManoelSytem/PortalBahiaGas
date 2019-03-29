using PortalBahiaGas.Models.Entidade;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class BemVindoController : Controller
    {
        // GET: BemVindo
        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null)
                Session["UsuarioLogado"] = new Usuario() { Perfil = Models.Entidade.Enuns.EPerfil.Visitante, Nome = "Visitante" };
            return View();
        }
    }
}