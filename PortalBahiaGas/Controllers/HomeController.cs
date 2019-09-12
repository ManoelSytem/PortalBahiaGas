using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Entidade.Enuns;
using PortalBahiaGas.Models.Persistencia;
using PortalBahiaGas.Models.Persitencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class HomeController : Controller
    {
        private Repositorio<RegistroTurno> RegistroTurno = new Repositorio<RegistroTurno>();

        public ActionResult Index()
        {
            Usuario lUsuario = ((Usuario)Session["UsuarioLogado"]);
            if (lUsuario == null)
            {
                Session["UsuarioLogado"] = lUsuario = new Usuario()
                {
                    Perfil = Models.Entidade.Enuns.EPerfil.Visitante,
                    Nome = "Visitante",
                    Login = "Visitante"
                };
            }

            ViewData.Add("perfil", lUsuario.Perfil);
            return View(RegistroTurno.ListarPorExpressao(x => x.Data >= DateTime.Today.AddDays(-7)).Where(x => x.Data >= DateTime.Today.AddDays(-7)));
        }

        public ActionResult Liberar(Int32 Id)
        {
            RegistroTurno lRegistroTurno = RegistroTurno.ObterPorId(Id);
            lRegistroTurno.Bloqueado = false;
            RegistroTurno.Editar(lRegistroTurno);

            return RedirectToAction("Index");
        }

        public ActionResult Cadastro()
        {
            return View("Cadastro");
        }

        public ActionResult Relatorio(Int32 Id)
        {
            RegistroTurno lRegistroTurno = RegistroTurno.ObterPorId(Id);
            Repositorio<Operador> OperadorRepositorio = new Repositorio<Operador>();
            Repositorio<Ocorrencia> OcorrenciaRepositorio = new Repositorio<Ocorrencia>();
            List<Operador> lOperadores = new List<Operador>();
            DateTime inicioTurno = GetInicioTurno(lRegistroTurno);

            foreach (OperadorRegistroTurno item in lRegistroTurno.OperadorRegistroTurno)
            {
                var operador = OperadorRepositorio.ObterOperadoresDoProtheus(item.Operador?.CodigoProtheus).FirstOrDefault();
                operador.Localidade = item.Local;
                lOperadores.Add(operador);
            }

            lRegistroTurno.Operadores.Clear();
            lOperadores.ForEach(x => lRegistroTurno.Operadores.Add(x));

            lRegistroTurno.OutrasOcorrencias.AddRange(OcorrenciaRepositorio.ObterOcorrenciasAnteriores(lRegistroTurno, DateTime.Now.AddDays(-60)));

            foreach (Ocorrencia item in lRegistroTurno.Ocorrencias)
            {
                if (!String.IsNullOrEmpty(item.Cliente))
                {
                    item.ClienteObj = OperadorRepositorio.ObterPorCodigoClienteDoProtheus(item.Cliente).FirstOrDefault();
                }
                if (!String.IsNullOrEmpty(item.Infraestrutura))
                {
                    item.InfraestruturaObj = OperadorRepositorio.ObterPorCodigoInfraestruturaDoProtheus(item.Infraestrutura).FirstOrDefault();
                }
            }

            foreach (Ocorrencia item in lRegistroTurno.OutrasOcorrencias)
            {
                if (!String.IsNullOrEmpty(item.Cliente))
                {
                    item.ClienteObj = OperadorRepositorio.ObterPorCodigoClienteDoProtheus(item.Cliente).FirstOrDefault();
                }
                if (!String.IsNullOrEmpty(item.Infraestrutura))
                {
                    item.InfraestruturaObj = OperadorRepositorio.ObterPorCodigoInfraestruturaDoProtheus(item.Infraestrutura).FirstOrDefault();
                }
            }

            foreach (Pendencia item in lRegistroTurno.Pendencias)
            {
                if (!String.IsNullOrEmpty(item.Cliente))
                {
                    item.ClienteObj = OperadorRepositorio.ObterPorCodigoClienteDoProtheus(item.Cliente).FirstOrDefault();
                }
                if (!String.IsNullOrEmpty(item.Infraestrutura))
                {
                    item.InfraestruturaObj = OperadorRepositorio.ObterPorCodigoInfraestruturaDoProtheus(item.Infraestrutura).FirstOrDefault();
                }
            }

            Repositorio<Gasoduto> GasodutoRepositorio = new Repositorio<Gasoduto>();

            if (lRegistroTurno.RegistrosGasoduto.Count() == 0)
                GasodutoRepositorio.Listar().ToList().ForEach(x => lRegistroTurno.RegistrosGasoduto.Add(new RegistroGasoduto()
                {
                    RegistroTurno = lRegistroTurno,
                    Gasoduto = x
                }));

            Repositorio<PontoEntrega> PontoEntregaRepositorio = new Repositorio<PontoEntrega>();
            RegistroTurno lRegistroTurnoAnterior = RegistroTurno.ListarTurnoAnterior(ETurno.De7as15, lRegistroTurno).FirstOrDefault();
            List<RegistroPontoEntrega> lRegistrosPontosEntrega = null;
            if (lRegistroTurnoAnterior != null) lRegistrosPontosEntrega = lRegistroTurnoAnterior.RegistrosPontoEntrega.ToList();

            if (lRegistroTurno.RegistrosPontoEntrega.Count() == 0)
                PontoEntregaRepositorio.Listar().ToList().ForEach(x => lRegistroTurno.RegistrosPontoEntrega.Add(new RegistroPontoEntrega()
                {
                    RegistroTurno = lRegistroTurno,
                    PontoEntrega = x,
                    VazaoEntrada = (lRegistrosPontosEntrega != null && lRegistrosPontosEntrega.Any(y => y.PontoEntrega.Id == x.Id)) ? lRegistrosPontosEntrega.FirstOrDefault(y => y.PontoEntrega.Id == x.Id).VazaoEntrada : null
                }));
            else
            {
                foreach (var item in lRegistroTurno.RegistrosPontoEntrega)
                {
                    if (lRegistroTurnoAnterior != null)
                    {
                        if (item.PontoEntrega != null && lRegistrosPontosEntrega.Any(y => y.PontoEntrega.Id == item.PontoEntrega.Id))
                        {
                            item.VazaoEntrada = lRegistrosPontosEntrega.FirstOrDefault(y => y.PontoEntrega.Id == item.PontoEntrega.Id).VazaoEntrada;
                        }

                    }
                }
            }

            Repositorio<Cliente> ClienteRepositorio = new Repositorio<Cliente>();
            if (lRegistroTurno.RegistrosCliente.Count() == 0)
                ClienteRepositorio.Listar().ToList().ForEach(x => lRegistroTurno.RegistrosCliente.Add(new RegistroCliente()
                {
                    RegistroTurno = lRegistroTurno,
                    Cliente = x
                }));

            Repositorio<Odorizador> OdorizadorRepositorio = new Repositorio<Odorizador>();
            if (lRegistroTurno.RegistrosOdorizador.Count() == 0)
                OdorizadorRepositorio.Listar().ToList().ForEach(x => lRegistroTurno.RegistrosOdorizador.Add(new RegistroOdorizador()
                {
                    RegistroTurno = lRegistroTurno,
                    Odorizador = x
                }));

            return View("Relatorio", lRegistroTurno);
        }

        public ActionResult Pesquisar(FormCollection pFormulario)
        {
            Usuario lUsuario = (Usuario)Session["UsuarioLogado"];
            if (lUsuario == null) return RedirectToAction("Index", "Login");
            else
            {
                DateTime? lDataInicial = null;
                DateTime? lDataFinal = null;

                if (!String.IsNullOrEmpty((pFormulario["DataInicial"]))) lDataInicial = Convert.ToDateTime(pFormulario["DataInicial"]);
                if (!String.IsNullOrEmpty((pFormulario["DataFinal"]))) lDataFinal = Convert.ToDateTime(pFormulario["DataFinal"]);

                var lParam = Parametro.True<RegistroTurno>();

                if (lDataInicial.HasValue)
                {
                    lParam = lParam.And(x => x.Data >= lDataInicial);
                    ViewData.Add("Inicio", lDataInicial);
                }

                if (lDataFinal.HasValue)
                {
                    lDataFinal = lDataFinal.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    lParam = lParam.And(x => x.Data <= lDataFinal);
                    ViewData.Add("Conclusao", lDataFinal);
                }
                ViewData.Add("perfil", lUsuario.Perfil);
                return View("Index", RegistroTurno.ListarPesquisa(lDataInicial, lDataFinal).ToList());
            }
        }

        private DateTime GetInicioTurno(RegistroTurno pRegistroTurno)
        {

            DateTime inicioTurno = pRegistroTurno.Data;

            if (pRegistroTurno.Turno.Equals(ETurno.De7as15))
            {
                inicioTurno = inicioTurno.AddHours(7);
            }
            else if (pRegistroTurno.Turno.Equals(ETurno.De15as23))
            {
                inicioTurno = inicioTurno.AddHours(15);
            }
            else
            {
                inicioTurno = inicioTurno.AddHours(23);
            }

            return inicioTurno;
        }
    }
}