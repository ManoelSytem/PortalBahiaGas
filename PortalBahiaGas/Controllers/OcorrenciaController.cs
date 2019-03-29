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
    public class OcorrenciaController : Controller
    {
        private Repositorio<Ocorrencia> Repositorio = new Repositorio<Ocorrencia>();

        public ActionResult Index()
        {
            if (Session["UsuarioLogado"] == null) return RedirectToAction("Index", "Login");
            PopularCampos();
            return View(new List<Ocorrencia>());
        }

        public ActionResult Pesquisar(FormCollection pFormulario)
        {
            DateTime? lDataInicial = null;
            DateTime? lDataFinal = null;
            Int32? lRegiao = null;
            String lOperador = null;
            String lCliente = null;
            String lNomeCliente = null;
            String lInfraestrutura = null;
            String lNomeInfraestrutura = null;
            String lDescricao = null;

            if (!String.IsNullOrEmpty((pFormulario["Regiao"]))) lRegiao = Convert.ToInt32(pFormulario["Regiao"]);
            if (!String.IsNullOrEmpty((pFormulario["DataInicial"]))) lDataInicial = Convert.ToDateTime(pFormulario["DataInicial"]);
            if (!String.IsNullOrEmpty((pFormulario["DataFinal"]))) lDataFinal = Convert.ToDateTime(pFormulario["DataFinal"]);
            if (!String.IsNullOrEmpty((pFormulario["Atendente"]))) lOperador = pFormulario["Atendente"];
            if (!String.IsNullOrEmpty((pFormulario["SelectIdCliente"]))) lCliente = pFormulario["SelectIdCliente"];
            if (!String.IsNullOrEmpty((pFormulario["SelectCliente"]))) lNomeCliente = pFormulario["SelectCliente"];
            if (!String.IsNullOrEmpty((pFormulario["SelectIdInfraestrutura"]))) lInfraestrutura = pFormulario["SelectIdInfraestrutura"];
            if (!String.IsNullOrEmpty((pFormulario["SelectInfraestrutura"]))) lNomeInfraestrutura = pFormulario["SelectInfraestrutura"];
            if (!String.IsNullOrEmpty((pFormulario["Descricao"]))) lDescricao = pFormulario["Descricao"];

            var lParam = Parametro.True<Ocorrencia>();

            if (lDataInicial.HasValue)
            {
                lParam = lParam.And(x => x.Inicio >= lDataInicial);
                ViewData.Add("DataInicio", lDataInicial);
            }

            if (lDataFinal.HasValue)
            {
                lDataFinal = lDataFinal.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                lParam = lParam.And(x => x.Inicio <= lDataFinal);
                ViewData.Add("DataFim", lDataFinal);
            }

            if (!String.IsNullOrEmpty(lOperador))
            {
                lParam = lParam.And(x => x.Atendente != null && x.Atendente.ToUpper().Contains(lOperador.ToUpper()));
                ViewData.Add("Atendente", lOperador.ToUpper());
            }

            if (lRegiao != null)
            {
                lParam = lParam.And(x => x.Regiao != null && x.Regiao.Id == lRegiao);
                ViewData.Add("Regiao", lRegiao);
            }

            if (!String.IsNullOrEmpty(lCliente))
            {
                lParam = lParam.And(x => x.Cliente == lCliente);
                ViewData.Add("Cliente", lNomeCliente);
            }
            if (!String.IsNullOrEmpty(lInfraestrutura))
            {
                lParam = lParam.And(x => x.Infraestrutura == lInfraestrutura);
                ViewData.Add("Infraestrutura", lNomeInfraestrutura);
            }
            if (!String.IsNullOrEmpty(pFormulario["ViewData[Turno]"]))
            {
                ETurno lTurno = (ETurno)Enum.Parse(typeof(ETurno), pFormulario["ViewData[Turno]"]);
                lParam = lParam.And(x => x.RegistroTurno.Turno == lTurno);
                ViewData.Add("Turno", Convert.ToInt32(lTurno));
            }
            if (!String.IsNullOrEmpty(pFormulario["ViewData[Tipo]"]))
            {
                ETipoOcorrencia lTipoOcorrencia = (ETipoOcorrencia)Enum.Parse(typeof(ETipoOcorrencia), pFormulario["ViewData[Tipo]"]);
                lParam = lParam.And(x => x.Tipo == lTipoOcorrencia);
                ViewData.Add("Tipo", Convert.ToInt32(lTipoOcorrencia));
            }

            if (!String.IsNullOrEmpty(pFormulario["ViewData[Origem]"]))
            {
                EOrigem lOrigem = (EOrigem)Enum.Parse(typeof(EOrigem), pFormulario["ViewData[Origem]"]);
                lParam = lParam.And(x => x.Origem == lOrigem);
                ViewData.Add("Origem", Convert.ToInt32(lOrigem));
            }

            if (!String.IsNullOrEmpty(lDescricao))
            {
                lParam = lParam.And(x => x.Descricao != null && x.Descricao.Contains(lDescricao));
                ViewData.Add("Descricao", lDescricao);
            }

            if (!String.IsNullOrEmpty(pFormulario["ViewData[Status]"]))
            {
                EStatus lStatus = (EStatus)Enum.Parse(typeof(EStatus), pFormulario["ViewData[Status]"]);
                lParam = lParam.And(x => x.Status == lStatus);
                ViewData.Add("Status", Convert.ToInt32(lStatus));
            }

            List<Ocorrencia> Ocorrencias = Repositorio.Listar(lParam).ToList();

            PopularCampos();
            return View("Index", Ocorrencias);
        }

        public ActionResult PopUpOcorrencia(FormCollection pFormulario)
        {
            Ocorrencia lOcorrencia = null;
            Repositorio<Ocorrencia> OcorrenciaRepositorio = new Repositorio<Ocorrencia>(Repositorio.Contexto);
            Repositorio<RegistroTurno> TurnoRepositorio = new Repositorio<RegistroTurno>(Repositorio.Contexto);
            OcorrenciaRepositorio = new Repositorio<Ocorrencia>(TurnoRepositorio.Contexto);
            lOcorrencia = OcorrenciaRepositorio.ObterPorId(Convert.ToInt32(pFormulario["Id"])) ?? new Ocorrencia()
            {
                RegistroTurno = TurnoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["idRegistroTurno"]))
            };
            if (!String.IsNullOrEmpty(lOcorrencia.Cliente)) lOcorrencia.ClienteObj = OcorrenciaRepositorio.ObterPorCodigoClienteDoProtheus(lOcorrencia.Cliente).FirstOrDefault();
            if (!String.IsNullOrEmpty(lOcorrencia.Infraestrutura)) lOcorrencia.InfraestruturaObj = OcorrenciaRepositorio.ObterPorCodigoInfraestruturaDoProtheus(lOcorrencia.Infraestrutura).FirstOrDefault();
            PopularCampos();
            return PartialView("PopUpOcorrencia", lOcorrencia);
        }

        private void PopularCampos()
        {
            Repositorio<Regiao> RegiaoRepositorio = new Repositorio<Regiao>(Repositorio.Contexto);
            Repositorio<Operador> OperadorRepositorio = new Repositorio<Operador>(Repositorio.Contexto);
            Repositorio<Gasoduto> GasodutoRepositorio = new Repositorio<Gasoduto>(Repositorio.Contexto);
            Repositorio<PontoEntrega> PontoEntregaRepositorio = new Repositorio<PontoEntrega>(Repositorio.Contexto);
            Repositorio<Cliente> ClienteRepositorio = new Repositorio<Cliente>(Repositorio.Contexto);

            ViewData.Add("Operadores", OperadorRepositorio.ObterOperadoresDoProtheus());
            ViewData.Add("PontosEntrega", new SelectList(PontoEntregaRepositorio.Listar(), "Id", "Nome"));
            ViewData.Add("Gasodutos", new SelectList(GasodutoRepositorio.Listar(), "Id", "Nome"));
            ViewData.Add("Clientes", new SelectList(ClienteRepositorio.Listar(), "Id", "Nome"));
            ViewData.Add("Regioes", new SelectList(RegiaoRepositorio.Listar(), "Id", "Nome", ViewData["Regiao"]));
        }
    }
}