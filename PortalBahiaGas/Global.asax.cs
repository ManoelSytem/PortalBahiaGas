using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PortalBahiaGas
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Repositorio<Usuario> UsuarioRepositorio = new Repositorio<Usuario>();
            if (!UsuarioRepositorio.VerificarExistencia(x => x.Login == "everton")) UsuarioRepositorio.Adicionar(new Usuario() { Login = "everton", Senha = "everton".ToStringMD5(), Nome="Everton", Perfil = Models.Entidade.Enuns.EPerfil.Administrador });
        }
    }
}
