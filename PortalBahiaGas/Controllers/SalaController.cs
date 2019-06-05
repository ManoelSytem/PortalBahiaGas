using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class SalaController : Controller
    {
        private Repositorio<RegistroTurno> TurnoRepositorio = new Repositorio<RegistroTurno>();
        private Repositorio<Operador> OperadorRepositorio;
        // GET: Sala
        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null)
                return RedirectToAction("Index", "Login");

            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);
            ViewData.Add("Operadores", OperadorRepositorio.ObterOperadoresDoProtheus());
            return View();
        }

        [HttpPost]
        public ActionResult SalvarOperadorSalaControle(string[] codigoOperadorSalaControle, string[] operadorSalaControleFalse)
        {

            Retorno lRetorno = new Retorno();
            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);

            try
            {

                string codigos = "";
                foreach (string value in codigoOperadorSalaControle)
                {
                    string ultimoElemento = codigoOperadorSalaControle.Last();
                    if (ultimoElemento == value)
                    {
                        codigos = codigos + value;
                    }
                    else
                    {
                        codigos = codigos + value + ",";
                    }
                }

                foreach (var item in OperadorRepositorio.ObterOperadoresDoProtheus(codigos))
                {
                    var OperadorSelect = OperadorRepositorio.Listar(x => x.CodigoProtheus == item.CodigoProtheus).FirstOrDefault();
                    if (OperadorSelect == null)
                    {
                        item.SalaControle = true;
                        OperadorRepositorio.Adicionar(item);

                    }
                    else
                    {
                        OperadorSelect.SalaControle = true;
                        OperadorRepositorio.Editar(OperadorSelect);
                    }

                    foreach (var operador in OperadorRepositorio.Listar(x => x.CodigoProtheus != item.CodigoProtheus))
                    {
                        operador.SalaControle = false;
                        OperadorRepositorio.Editar(OperadorSelect);
                    }
                }

            }
            catch (Exception ex)
            {
                lRetorno.Erro = true;
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
            }
            return Json(lRetorno);
        }
    }
}