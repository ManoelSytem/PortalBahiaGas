using PortalBahiaGas.Models;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Entidade.Enuns;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PortalBahiaGas.Controllers
{
    public class RegistroTurnoController : Controller
    {
        private Repositorio<RegistroTurno> TurnoRepositorio = new Repositorio<RegistroTurno>();
        private Repositorio<Operador> OperadorRepositorio;
        private Repositorio<Gasoduto> GasodutoRepositorio;
        private Repositorio<PontoEntrega> PontoEntregaRepositorio;
        private Repositorio<Cliente> ClienteRepositorio;
        //private Repositorio<Infraestrutura> InfraestruturaRepositorio;
        private Repositorio<Regiao> RegiaoRepositorio;
        private Repositorio<Ocorrencia> OcorrenciaRepositorio;
        private Repositorio<Pendencia> PendenciaRepositorio;
        private Repositorio<Odorizador> OdorizadorRepositorio;
      
 
        public ActionResult Index(Int32 Id = 0)
        {
            if (Session["UsuarioLogado"] == null)
                return RedirectToAction("Index", "Login");

            RegistroTurno lRegistroTurno;

            if (Id != 0)
            {
                ViewData["acao"] = EAcao.Editar;
                lRegistroTurno = TurnoRepositorio.ObterPorId(Id);
            }
            else
            {
                ViewData["acao"] = EAcao.Inserir;
                if (TurnoRepositorio.Listar().Count() > 0)
                {
                    DateTime? lData = TurnoRepositorio.Listar().Max(x => x.Data);
                    ETurno? lTurno = TurnoRepositorio.Listar(x => x.Data == lData).Max(x => x.Turno);

                    switch (lTurno)
                    {
                        case ETurno.De7as15:
                            lTurno = ETurno.De15as23;
                            break;
                        case ETurno.De15as23:
                            lTurno = ETurno.De23as7;
                            break;
                        case ETurno.De23as7:
                            lData = lData.Value.AddDays(1);
                            lTurno = ETurno.De7as15;
                            break;
                    }

                    lRegistroTurno = new RegistroTurno()
                    {
                        Data = lData.Value,
                        Turno = lTurno.Value,
                        Turma = ETurma.D
                        
                    };
                }
                else
                {
                    lRegistroTurno = new RegistroTurno()
                    {
                        Data = DateTime.Today,
                        Turno = ETurno.De7as15
                    };
                }
            }
            PopularCampos();
            return View(lRegistroTurno);
        }

        public JsonResult Cadastrar(FormCollection pFormulario)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                switch (pFormulario["pagina"])
                {
                    case "turno":
                        lRetorno.Objeto = new { Id = CadastrarTurno(pFormulario).Id };
                        break;
                    case "gasoduto":
                        lRetorno.Objeto = new { Id = CadastrarGasoduto(pFormulario).Id };
                        break;
                    case "pontoEntrega":
                        lRetorno.Objeto = new { Id = CadastrarPontoEntrega(pFormulario).Id };
                        break;
                    case "cliente":
                        lRetorno.Objeto = new { Id = CadastrarCliente(pFormulario).Id };
                        break;
                    case "odorizador":
                        lRetorno.Objeto = new { Id = CadastrarOdorizador(pFormulario).Id };
                        break;
                    case "ocorrencia":
                        lRetorno.Objeto = new { Id = CadastrarOcorrencia(pFormulario).Id };
                        break;
                    case "pendencia":
                        lRetorno.Objeto = new { Id = CadastrarPendencia(pFormulario).Id };
                        break;
                }
                lRetorno.Mensagem = "Registro salvo com sucesso!";
            }
            catch (Exception ex)
            {
                lRetorno.Erro = true;
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
            }
            return Json(lRetorno);
        }

        public ActionResult ObterAbas(FormCollection pFormulario)
        {
            string aba = string.Empty;
            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);
            GasodutoRepositorio = new Repositorio<Gasoduto>(TurnoRepositorio.Contexto);
            PontoEntregaRepositorio = new Repositorio<PontoEntrega>(TurnoRepositorio.Contexto);
            ClienteRepositorio = new Repositorio<Cliente>(TurnoRepositorio.Contexto);
            RegiaoRepositorio = new Repositorio<Regiao>(TurnoRepositorio.Contexto);
            OcorrenciaRepositorio = new Repositorio<Ocorrencia>(TurnoRepositorio.Contexto);
            OdorizadorRepositorio = new Repositorio<Odorizador>(TurnoRepositorio.Contexto);
            PendenciaRepositorio = new Repositorio<Pendencia>(TurnoRepositorio.Contexto);

            RegistroTurno lRegistroTurno = TurnoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["id"]));
            switch (pFormulario["aba"])
            {
                case "abaTurno":
                    aba = "Turno";
                    break;
                case "abaGasoduto":
                    aba = "Gasoduto";
                    if (lRegistroTurno.RegistrosGasoduto.Count() == 0)
                        GasodutoRepositorio.Listar().ToList().ForEach(x => lRegistroTurno.RegistrosGasoduto.Add(new RegistroGasoduto()
                        {
                            RegistroTurno = lRegistroTurno,
                            Gasoduto = x
                        }));
                    break;
                case "abaPontoEntrega":
                    aba = "PontoEntrega";
                    RegistroTurno lRegistroTurnoAnterior = TurnoRepositorio.Listar(x => x.Turno == ETurno.De7as15 && x.Data == lRegistroTurno.Data).FirstOrDefault();
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
                        foreach (var item in lRegistroTurno.RegistrosPontoEntrega)
                        {
                            if (item.PontoEntrega != null && lRegistrosPontosEntrega.Any(y => y.PontoEntrega.Id == item.PontoEntrega.Id))
                                item.VazaoEntrada = lRegistrosPontosEntrega.FirstOrDefault(y => y.PontoEntrega.Id == item.PontoEntrega.Id).VazaoEntrada;
                        }
                    break;
                case "abaCliente":
                    aba = "Cliente";
                    if (lRegistroTurno.RegistrosCliente.Count() == 0)
                        ClienteRepositorio.Listar().ToList().ForEach(x => lRegistroTurno.RegistrosCliente.Add(new RegistroCliente()
                        {
                            RegistroTurno = lRegistroTurno,
                            Cliente = x
                        }));
                    break;
                case "abaOdorizador":
                    aba = "Odorizador";
                    if (lRegistroTurno.RegistrosOdorizador.Count() == 0)
                        OdorizadorRepositorio.Listar().ToList().ForEach(x => lRegistroTurno.RegistrosOdorizador.Add(new RegistroOdorizador()
                        {
                            RegistroTurno = lRegistroTurno,
                            Odorizador = x
                        }));
                    break;
                case "abaOcorrencia":
                    aba = "Ocorrencia";
                    lRegistroTurno.OutrasOcorrencias.AddRange(OcorrenciaRepositorio.Listar(x => x.Status != EStatus.Concluído && x.RegistroTurno.Id != lRegistroTurno.Id));
                    break;
                case "abaPendencia":
                    aba = "Pendencia";
                    lRegistroTurno.Pendencias = PendenciaRepositorio.Listar(x => x.Status != EStatus.Concluído || x.RegistroTurno.Id == lRegistroTurno.Id).ToList();
                    break;
            }
            PopularCampos();
            return PartialView(aba, lRegistroTurno);
        }

        #region[Turno]
        private RegistroTurno CadastrarTurno(FormCollection pFormulario)
        {
            RegistroTurno lRegistroTurno = new RegistroTurno();
            Usuario lUsuario = (Usuario)Session["UsuarioLogado"];
            Operador lOperador;
            List<Operador> Operadores = new List<Operador>();
            lRegistroTurno.Bloqueado = true;
            TryUpdateModel<RegistroTurno>(lRegistroTurno, pFormulario);
            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);

            ValidarTurno(pFormulario);

            foreach (var item in OperadorRepositorio.ObterOperadoresDoProtheus(pFormulario.GetValue("CodigoProtheus").AttemptedValue.Replace(",false", "").Replace("false,", "")))
            {
                lOperador = OperadorRepositorio.Listar(x => x.CodigoProtheus == item.CodigoProtheus).FirstOrDefault();
                if (lOperador == null) lOperador = OperadorRepositorio.Adicionar(item);
                Operadores.Add(lOperador);
            }

            if (lRegistroTurno.Id == 0)
            {
                lRegistroTurno.DataCriacao = DateTime.Now;
                lRegistroTurno.UsuarioCriacao = lUsuario.Login;
                lRegistroTurno.Turma = lRegistroTurno.ObterTurma(pFormulario.GetValue("Turma").AttemptedValue);
                Operadores.ForEach(x => lRegistroTurno.OperadorRegistroTurno.Add(new OperadorRegistroTurno() { Operador = x, Local = x.Localidade}));
                string[] lSalaControl = pFormulario.GetValues("SalaControle");
                foreach (var opRegTurno in lRegistroTurno.OperadorRegistroTurno)
                {
                    foreach(string slControl in lSalaControl)
                        if (opRegTurno.Operador.CodigoProtheus == slControl)
                            opRegTurno.SalaControle = true;
                }

                lRegistroTurno = TurnoRepositorio.Adicionar(lRegistroTurno);
            }
            else
            {
                lRegistroTurno = TurnoRepositorio.Listar(x => x.Id == lRegistroTurno.Id).FirstOrDefault();
                lRegistroTurno.DataAlteracao = DateTime.Now;
                lRegistroTurno.UsuarioAlteracao = lUsuario.Login;
                lRegistroTurno.Bloqueado = true;
                lRegistroTurno.Turma =  lRegistroTurno.ObterTurma(pFormulario.GetValue("Turma").AttemptedValue);
                lRegistroTurno.OperadorRegistroTurno.Clear();
                Operadores.ForEach(x => lRegistroTurno.OperadorRegistroTurno.Add(new OperadorRegistroTurno() { Operador = x, Local = x.Localidade}));
                string[] lSalaControl = pFormulario.GetValues("SalaControle");
                foreach (var opRegTurno in lRegistroTurno.OperadorRegistroTurno)
                {
                    foreach (string slControl in lSalaControl)
                        if (opRegTurno.Operador.CodigoProtheus == slControl)
                            opRegTurno.SalaControle = true;
                }
                lRegistroTurno = TurnoRepositorio.Editar(lRegistroTurno);
            }

            return lRegistroTurno;
        }

        private ETurma? ObterTurma(string attemptedValue)
        {
            throw new NotImplementedException();
        }

        private void ValidarTurno(FormCollection pFormulario)
        {
            StringBuilder lMensagem = new StringBuilder();
            RegistroTurno lRegistroTurno = new RegistroTurno();
            TryUpdateModel<RegistroTurno>(lRegistroTurno, pFormulario);

            if (TurnoRepositorio.VerificarExistencia(x => x.Data == lRegistroTurno.Data && x.Turno == lRegistroTurno.Turno && x.Id != lRegistroTurno.Id)) lMensagem.AppendLine("Turno já cadastrado.");
            //if (pFormulario.GetValues("item") == null) lMensagem.AppendLine("Informe os operadores do turno.");
            if (!pFormulario.GetValues("CodigoProtheus").Any(x => x != "false")) lMensagem.AppendLine("Informe os operadores do turno.");
            if (pFormulario.GetValue("CodigoProtheus").AttemptedValue.Replace(",false", "").Replace("false,", "").Split(',').Count() != 4) lMensagem.AppendLine("O registro de turno deve possuir 4 operadores.");
            if (pFormulario.GetValue("SalaControle").AttemptedValue.Replace(",false", "").Replace("false,", "").Split(',').Count() > 1) lMensagem.AppendLine("O registro de turno deve possuir somente 1 operador para sala de controle.");
            if (lRegistroTurno.ObterTurma(pFormulario.GetValue("Turma").AttemptedValue).Equals(ETurma.D)) lMensagem.AppendLine("Turma não informada no registro de turno.");
            validarPeriodoTurno(lRegistroTurno, lMensagem, null);
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());




        }
        #endregion

        #region[Gasoduto]
        private RegistroTurno CadastrarGasoduto(FormCollection pFormulario)
        {
            GasodutoRepositorio = new Repositorio<Gasoduto>(TurnoRepositorio.Contexto);
            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);
            RegistroTurno lRegistroTurno = TurnoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["IdRegistroTurno"]));
            lRegistroTurno.OperadorMedicao = pFormulario["OperadorMedicao"];
            lRegistroTurno.HoraMedicao = Convert.ToDateTime(lRegistroTurno.Data.ToShortDateString() + " " + pFormulario["HoraMedicao"]);
            for (int i = 0; i < pFormulario.GetValues("IdGasoduto").Length; i++)
            {
                if (lRegistroTurno.RegistrosGasoduto.Any(x => x.Id == Convert.ToInt32(pFormulario.GetValues("IdGasoduto")[i])) && !pFormulario.GetValues("IdGasoduto")[i].Equals("0"))
                    foreach (RegistroGasoduto item in lRegistroTurno.RegistrosGasoduto.Where(x => x.Id == Convert.ToInt32(pFormulario.GetValues("IdGasoduto")[i])))
                    {
                        item.Id = Convert.ToInt32(pFormulario.GetValues("IdGasoduto")[i]);
                        item.RegistroTurno = lRegistroTurno;
                        item.Gasoduto = GasodutoRepositorio.ObterPorId(Convert.ToInt32(pFormulario.GetValues("Gasoduto")[i]));
                        item.Vazao = (String.IsNullOrEmpty(pFormulario.GetValues("Vazao")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("Vazao")[i]);
                        item.Pressao = (String.IsNullOrEmpty(pFormulario.GetValues("Pressao")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("Pressao")[i]);
                    }
                else
                    lRegistroTurno.RegistrosGasoduto.Add(new RegistroGasoduto()
                    {
                        Id = Convert.ToInt32(pFormulario.GetValues("IdGasoduto")[i]),
                        RegistroTurno = lRegistroTurno,
                        Gasoduto = GasodutoRepositorio.ObterPorId(Convert.ToInt32(pFormulario.GetValues("Gasoduto")[i])),
                        Vazao = (String.IsNullOrEmpty(pFormulario.GetValues("Vazao")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("Vazao")[i]),
                        Pressao = (String.IsNullOrEmpty(pFormulario.GetValues("Pressao")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("Pressao")[i])
                    });
            }
            ValidarGasoduto(lRegistroTurno);
            return TurnoRepositorio.Editar(lRegistroTurno);
        }

        private void ValidarGasoduto(RegistroTurno pRegistroTurno)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (String.IsNullOrEmpty(pRegistroTurno.OperadorMedicao)) lMensagem.AppendLine("Informe o operador da medição.");
            if (pRegistroTurno.HoraMedicao == null) lMensagem.AppendLine("Informe a hora da medição.");
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());
        }
        #endregion

        #region[Ponto de entrega]
        private RegistroTurno CadastrarPontoEntrega(FormCollection pFormulario)
        {
            
            PontoEntregaRepositorio = new Repositorio<PontoEntrega>(TurnoRepositorio.Contexto);
            RegistroTurno lRegistroTurno = TurnoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["IdRegistroTurno"]));
            lRegistroTurno.FatorCorrecao = Convert.ToDecimal(pFormulario["FatorCorrecao"]);
            for (int i = 0; i < pFormulario.GetValues("PontoEntrega").Length; i++)
            {
                if (lRegistroTurno.RegistrosPontoEntrega.Any(x => x.Id == Convert.ToInt32(pFormulario.GetValues("IdPontoEntrega")[i])) && !pFormulario.GetValues("IdPontoEntrega")[i].Equals("0"))
                    foreach (RegistroPontoEntrega item in lRegistroTurno.RegistrosPontoEntrega.Where(x => x.Id == Convert.ToInt32(pFormulario.GetValues("IdPontoEntrega")[i])))
                    {

                        item.Id = Convert.ToInt32(pFormulario.GetValues("IdPontoEntrega")[i]);
                        item.RegistroTurno = lRegistroTurno;
                        item.PontoEntrega = PontoEntregaRepositorio.ObterPorId(Convert.ToInt32(pFormulario.GetValues("PontoEntrega")[i]));
                        item.Hora = (String.IsNullOrEmpty(pFormulario.GetValues("Hora")[i])) ? null : (DateTime?)Convert.ToDateTime(lRegistroTurno.Data.ToShortDateString() + " " + pFormulario.GetValues("Hora")[i]);
                        item.PressaoEntrada = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoEntrada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PressaoEntrada")[i]);
                        item.PressaoSaida = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoSaida")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PressaoSaida")[i]);
                        item.VazaoEntrada = (String.IsNullOrEmpty(pFormulario.GetValues("VazaoEntrada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoEntrada")[i]);
                        item.VazaoSaida = (String.IsNullOrEmpty(pFormulario.GetValues("VazaoSaida")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoSaida")[i]);
                        item.Desvio = CalcularDesvio(lRegistroTurno.Turno, item.VazaoEntrada, item.VazaoSaida, item.Hora);
                        item.Penalidade = CalcularPenalidade(lRegistroTurno.Turno, item.Desvio);
                    }
                else
                    
                    lRegistroTurno.RegistrosPontoEntrega.Add(new RegistroPontoEntrega()
                    {
                  

                        Id = Convert.ToInt32(pFormulario.GetValues("IdPontoEntrega")[i]),
                        RegistroTurno = lRegistroTurno,
                        PontoEntrega = PontoEntregaRepositorio.ObterPorId(Convert.ToInt32(pFormulario.GetValues("PontoEntrega")[i])),
                        Hora = (String.IsNullOrEmpty(pFormulario.GetValues("Hora")[i])) ? null : (DateTime?)Convert.ToDateTime(lRegistroTurno.Data.ToShortDateString() + " " + pFormulario.GetValues("Hora")[i]),
                        PressaoEntrada = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoEntrada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PressaoEntrada")[i]),
                        PressaoSaida = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoSaida")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PressaoSaida")[i]),
                        VazaoEntrada = (String.IsNullOrEmpty(pFormulario.GetValues("VazaoEntrada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoEntrada")[i]),
                        VazaoSaida = (String.IsNullOrEmpty(pFormulario.GetValues("VazaoSaida")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoSaida")[i]),
                        Desvio = CalcularDesvio(
                            lRegistroTurno.Turno,
                            (String.IsNullOrEmpty(pFormulario.GetValues("VazaoEntrada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoEntrada")[i]),
                            (String.IsNullOrEmpty(pFormulario.GetValues("VazaoSaida")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoSaida")[i]),
                            (String.IsNullOrEmpty(pFormulario.GetValues("Hora")[i])) ? null : (DateTime?)Convert.ToDateTime(lRegistroTurno.Data.ToShortDateString() + " " + pFormulario.GetValues("Hora")[i])),
                        Penalidade = CalcularPenalidade(lRegistroTurno.Turno, CalcularDesvio(
                            lRegistroTurno.Turno,
                            (String.IsNullOrEmpty(pFormulario.GetValues("VazaoEntrada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoEntrada")[i]),
                            (String.IsNullOrEmpty(pFormulario.GetValues("VazaoSaida")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoSaida")[i]),
                            (String.IsNullOrEmpty(pFormulario.GetValues("Hora")[i])) ? null : (DateTime?)Convert.ToDateTime(lRegistroTurno.Data.ToShortDateString() + " " + pFormulario.GetValues("Hora")[i])))
                    }

                    );
            }

            return TurnoRepositorio.Editar(lRegistroTurno);
        }

        private Decimal? CalcularDesvio(ETurno pTurno, Decimal? pVazaoProgramada, Decimal? pVazaoRetirada, DateTime? pHora)
        {
            Decimal? lDesvio = 0;
            if (pHora.HasValue && pVazaoProgramada > 0 && pVazaoRetirada.HasValue)
                if (pTurno == ETurno.De23as7) lDesvio = (Convert.ToInt32(Convert.ToDecimal(pVazaoRetirada / pVazaoProgramada) * 100)) - 100;
                else lDesvio = Convert.ToInt32(Convert.ToDecimal((pVazaoRetirada / ((pVazaoProgramada / 24) * (Convert.ToDecimal(pHora.Value.Hour) + (Convert.ToDecimal(pHora.Value.Minute) / 60))) - 1) * 100));

            return lDesvio;
        }

        private Boolean? CalcularPenalidade(ETurno pTurno, Decimal? pDesvio)
        {
            Boolean? lPenalidade = null;
            if (pTurno == ETurno.De23as7)
                if (pDesvio < -10 || pDesvio > 5) lPenalidade = true;
                else lPenalidade = false;
            return lPenalidade;
        }

        #endregion

        #region[Cliente]
        private RegistroTurno CadastrarCliente(FormCollection pFormulario)
        {
            ClienteRepositorio = new Repositorio<Cliente>(TurnoRepositorio.Contexto);
            RegistroTurno lRegistroTurno = TurnoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["IdRegistroTurno"]));
            for (int i = 0; i < pFormulario.GetValues("Cliente").Length; i++)
            {
                if (lRegistroTurno.RegistrosCliente.Any(x => x.Id == Convert.ToInt32(pFormulario.GetValues("IdCliente")[i])) && !pFormulario.GetValues("IdCliente")[i].Equals("0"))
                    foreach (RegistroCliente item in lRegistroTurno.RegistrosCliente.Where(x => x.Id == Convert.ToInt32(pFormulario.GetValues("IdCliente")[i])))
                    {
                        item.Id = Convert.ToInt32(pFormulario.GetValues("IdCliente")[i]);
                        item.RegistroTurno = lRegistroTurno;
                        item.Cliente = ClienteRepositorio.ObterPorId(Convert.ToInt32(pFormulario.GetValues("Cliente")[i]));
                        item.Hora = (String.IsNullOrEmpty(pFormulario.GetValues("Hora")[i])) ? null : (DateTime?)Convert.ToDateTime(lRegistroTurno.Data.ToShortDateString() + " " + pFormulario.GetValues("Hora")[i]);
                        item.PressaoEntrada = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoEntrada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PressaoEntrada")[i]);
                        item.PressaoSaida = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoSaida")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PressaoSaida")[i]);
                        item.VazaoAcumulada = (String.IsNullOrEmpty(pFormulario.GetValues("VazaoAcumulada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoAcumulada")[i]);
                        item.VazaoInstantanea = (String.IsNullOrEmpty(pFormulario.GetValues("VazaoInstantanea")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoInstantanea")[i]);
                    }
                else
                    lRegistroTurno.RegistrosCliente.Add(new RegistroCliente()
                    {
                        Id = Convert.ToInt32(pFormulario.GetValues("IdCliente")[i]),
                        RegistroTurno = lRegistroTurno,
                        Cliente = ClienteRepositorio.ObterPorId(Convert.ToInt32(pFormulario.GetValues("Cliente")[i])),
                        Hora = (String.IsNullOrEmpty(pFormulario.GetValues("Hora")[i])) ? null : (DateTime?)Convert.ToDateTime(lRegistroTurno.Data.ToShortDateString() + " " + pFormulario.GetValues("Hora")[i]),
                        PressaoEntrada = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoEntrada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PressaoEntrada")[i]),
                        PressaoSaida = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoSaida")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PressaoSaida")[i]),
                        VazaoAcumulada = (String.IsNullOrEmpty(pFormulario.GetValues("VazaoAcumulada")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoAcumulada")[i]),
                        VazaoInstantanea = (String.IsNullOrEmpty(pFormulario.GetValues("VazaoInstantanea")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("VazaoInstantanea")[i])
                    });
            }

            return TurnoRepositorio.Editar(lRegistroTurno);
        }
        #endregion

        #region[Odorizador]
        private RegistroTurno CadastrarOdorizador(FormCollection pFormulario)
        {
            OdorizadorRepositorio = new Repositorio<Odorizador>(TurnoRepositorio.Contexto);
            RegistroTurno lRegistroTurno = TurnoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["IdRegistroTurno"]));
            for (int i = 0; i < pFormulario.GetValues("Odorizador").Length; i++)
            {
                if (lRegistroTurno.RegistrosOdorizador.Any(x => x.Id == Convert.ToInt32(pFormulario.GetValues("IdOdorizador")[i])) && !pFormulario.GetValues("IdOdorizador")[i].Equals("0"))
                    foreach (RegistroOdorizador item in lRegistroTurno.RegistrosOdorizador.Where(x => x.Id == Convert.ToInt32(pFormulario.GetValues("IdOdorizador")[i])))
                    {
                        item.Id = Convert.ToInt32(pFormulario.GetValues("IdOdorizador")[i]);
                        item.RegistroTurno = lRegistroTurno;
                        item.Odorizador = OdorizadorRepositorio.ObterPorId(Convert.ToInt32(pFormulario.GetValues("Odorizador")[i]));
                        item.StatusComunicacao = String.IsNullOrEmpty(pFormulario.GetValues("item.StatusComunicacao")[i]) ? null : (EStatusComunicacao?)Enum.Parse(typeof(EStatusComunicacao), pFormulario.GetValues("item.StatusComunicacao")[i]);
                        item.PumpDisp = (String.IsNullOrEmpty(pFormulario.GetValues("PumpDisp")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PumpDisp")[i]);
                        item.PressaoTqOdor = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoTqOdor")[i])) ? null : (Int32?)Convert.ToInt32(pFormulario.GetValues("PressaoTqOdor")[i]);
                    }
                else
                    lRegistroTurno.RegistrosOdorizador.Add(new RegistroOdorizador()
                    {
                        Id = Convert.ToInt32(pFormulario.GetValues("IdOdorizador")[i]),
                        RegistroTurno = lRegistroTurno,
                        Odorizador = OdorizadorRepositorio.ObterPorId(Convert.ToInt32(pFormulario.GetValues("Odorizador")[i])),
                        StatusComunicacao = String.IsNullOrEmpty(pFormulario.GetValues("item.StatusComunicacao")[i]) ? null : (EStatusComunicacao?)Enum.Parse(typeof(EStatusComunicacao), pFormulario.GetValues("item.StatusComunicacao")[i]),
                        PumpDisp = (String.IsNullOrEmpty(pFormulario.GetValues("PumpDisp")[i])) ? null : (Decimal?)Convert.ToDecimal(pFormulario.GetValues("PumpDisp")[i]),
                        PressaoTqOdor = (String.IsNullOrEmpty(pFormulario.GetValues("PressaoTqOdor")[i])) ? null : (Int32?)Convert.ToInt32(pFormulario.GetValues("PressaoTqOdor")[i])
                    });
            }

            return TurnoRepositorio.Editar(lRegistroTurno);
        }
        #endregion

        #region[Ocorrência]
        public ActionResult PopUpOcorrencia(FormCollection pFormulario)
        {
            Ocorrencia lOcorrencia = null;
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

        public JsonResult ObterChamado(FormCollection pFormulario)
        {
            Retorno lRetorno = new Retorno();
            try
            {
                lRetorno.Objeto = TurnoRepositorio.ObterChamadoDeQueryExterna(pFormulario["Chamado"]);
            }
            catch (Exception ex)
            {
                lRetorno.Erro = true;
                lRetorno.Mensagem = ex.Message;
                lRetorno.PilhaErro = ex.StackTrace;
            }
            return Json(lRetorno);
        }

        private RegistroTurno CadastrarOcorrencia(FormCollection pFormulario)
        {
            OcorrenciaRepositorio = new Repositorio<Ocorrencia>(TurnoRepositorio.Contexto);
            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);
            GasodutoRepositorio = new Repositorio<Gasoduto>(TurnoRepositorio.Contexto);
            PontoEntregaRepositorio = new Repositorio<PontoEntrega>(TurnoRepositorio.Contexto);
            ClienteRepositorio = new Repositorio<Cliente>(TurnoRepositorio.Contexto);
            //InfraestruturaRepositorio = new Repositorio<Infraestrutura>(TurnoRepositorio.Contexto);
            RegiaoRepositorio = new Repositorio<Regiao>(TurnoRepositorio.Contexto);

            RegistroTurno lRegistroTurno = TurnoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["IdRegistroTurno"]));
            Ocorrencia lOcorrencia = OcorrenciaRepositorio.ObterPorId(Convert.ToInt32(pFormulario["Id"]));

            Ocorrencia lModel = new Ocorrencia()
            {
                Id = Convert.ToInt32(pFormulario["Id"] ?? "0"),
                Chamado = pFormulario["Chamado"],
                Atendente = pFormulario["Atendente"],
                NomeContato = pFormulario["NomeContato"],
                Telefone = pFormulario["Telefone"],
                Protocolo = pFormulario["Protocolo"],
                Descricao = pFormulario["Descricao"],
                Justificativa = pFormulario["Justificativa"],
                Cliente = pFormulario["SelectIdCliente"],
                Infraestrutura = pFormulario["SelectIdInfraestrutura"],
                PontoEntrega = (String.IsNullOrEmpty(pFormulario["PontoEntrega.Id"])) ? null : PontoEntregaRepositorio.ObterPorId(Convert.ToInt32(pFormulario["PontoEntrega.Id"])),
                Regiao = (String.IsNullOrEmpty(pFormulario["Regiao.Id"])) ? null : RegiaoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["Regiao.Id"])),
                Operador = pFormulario["Operador"],
                Inicio = String.IsNullOrEmpty(pFormulario["Inicio"]) ? null : (DateTime?)Convert.ToDateTime(pFormulario["Inicio"]),
                Atendimento = String.IsNullOrEmpty(pFormulario["Atendimento"]) ? null : (DateTime?)Convert.ToDateTime(pFormulario["Atendimento"]),
                Conclusao = String.IsNullOrEmpty(pFormulario["Conclusao"]) ? null : (DateTime?)Convert.ToDateTime(pFormulario["Conclusao"]),
                Pressao = Convert.ToDecimal(String.IsNullOrEmpty(pFormulario["Pressao"]) ? "0" : pFormulario["Pressao"]),
                Vazao = Convert.ToDecimal(String.IsNullOrEmpty(pFormulario["Vazao"]) ? "0" : pFormulario["Vazao"]),
                Status = ObterStatus(pFormulario["Inicio"], pFormulario["Atendimento"], pFormulario["Conclusao"]),
                Origem = String.IsNullOrEmpty(pFormulario["Origem"]) ? null : (EOrigem?)Enum.Parse(typeof(EOrigem), pFormulario["Origem"]),
                Tipo = String.IsNullOrEmpty(pFormulario["Tipo"]) ? null : (ETipoOcorrencia?)Enum.Parse(typeof(ETipoOcorrencia), pFormulario["Tipo"]),
                Local = String.IsNullOrEmpty(pFormulario["Local"]) ? null : (ELocal?)Enum.Parse(typeof(ELocal), pFormulario["Local"]),
                Modalidade = String.IsNullOrEmpty(pFormulario["Modalidade"]) ? null : (EModalidade?)Enum.Parse(typeof(EModalidade), pFormulario["Modalidade"]),
                Outro = pFormulario["Outro"]
            };


            ValidarOcorrencia(lModel, pFormulario, lRegistroTurno);
            if (lOcorrencia == null)
            {
                lModel.RegistroTurno = lRegistroTurno;
                lRegistroTurno.Ocorrencias.Clear();
                lRegistroTurno.Ocorrencias.Add(lModel);
            }
            else
            {
                if (lOcorrencia.Id == lRegistroTurno.Id)
                {
                    foreach (Ocorrencia item in lRegistroTurno.Ocorrencias.Where(x => x.Id == lOcorrencia.Id))
                    {
                        item.Chamado = lModel.Chamado;
                        item.Origem = lModel.Origem;
                        item.Cliente = lModel.Cliente;
                        item.Infraestrutura = lModel.Infraestrutura;
                        item.Inicio = lModel.Inicio;
                        item.Atendimento = lModel.Atendimento;
                        item.Conclusao = lModel.Conclusao;
                        item.Status = lModel.Status;
                        item.Atendente = lModel.Atendente;
                        item.PontoEntrega = lModel.PontoEntrega;
                        item.NomeContato = lModel.NomeContato;
                        item.Telefone = lModel.Telefone;
                        item.Pressao = lModel.Pressao;
                        item.Vazao = lModel.Vazao;
                        item.Modalidade = lModel.Modalidade;
                        item.Protocolo = lModel.Protocolo;
                        item.Descricao = lModel.Descricao;
                        item.Justificativa = lModel.Justificativa;
                        item.Regiao = lModel.Regiao;
                        item.Origem = lModel.Origem;
                        item.Tipo = lModel.Tipo;
                        item.Local = lModel.Local;
                        item.Operador = lModel.Operador;
                        item.Outro = lModel.Outro;
                    }
                }
                else
                {
                    lOcorrencia.Chamado = lModel.Chamado;
                    lOcorrencia.Origem = lModel.Origem;
                    lOcorrencia.Cliente = lModel.Cliente;
                    lOcorrencia.Infraestrutura = lModel.Infraestrutura;
                    lOcorrencia.Inicio = lModel.Inicio;
                    lOcorrencia.Atendimento = lModel.Atendimento;
                    lOcorrencia.Conclusao = lModel.Conclusao;
                    lOcorrencia.Status = lModel.Status;
                    lOcorrencia.Atendente = lModel.Atendente;
                    lOcorrencia.PontoEntrega = lModel.PontoEntrega;
                    lOcorrencia.NomeContato = lModel.NomeContato;
                    lOcorrencia.Telefone = lModel.Telefone;
                    lOcorrencia.Pressao = lModel.Pressao;
                    lOcorrencia.Vazao = lModel.Vazao;
                    lOcorrencia.Modalidade = lModel.Modalidade;
                    lOcorrencia.Protocolo = lModel.Protocolo;
                    lOcorrencia.Descricao = lModel.Descricao;
                    lOcorrencia.Justificativa = lModel.Justificativa;
                    lOcorrencia.Regiao = lModel.Regiao;
                    lOcorrencia.Origem = lModel.Origem;
                    lOcorrencia.Tipo = lModel.Tipo;
                    lOcorrencia.Local = lModel.Local;
                    lOcorrencia.Operador = lModel.Operador;
                    OcorrenciaRepositorio.Editar(lOcorrencia);
                    lOcorrencia.Outro = lModel.Outro;
                }
            }

            lRegistroTurno = TurnoRepositorio.Editar(lRegistroTurno);
            lRegistroTurno.OutrasOcorrencias.AddRange(OcorrenciaRepositorio.Listar(x => x.Status != EStatus.Concluído && x.RegistroTurno.Id != lRegistroTurno.Id));

            return lRegistroTurno;
        }

        private void ValidarOcorrencia(Ocorrencia pOcorrencia, FormCollection pFormulario, RegistroTurno lRegistroTurno)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (pOcorrencia.Origem == null) lMensagem.AppendLine("Informe a Origem.");
            if (pOcorrencia.Local == null) lMensagem.AppendLine("Informe o Local.");
            if ((ELocal.Cliente.Equals(pOcorrencia.Local) && String.IsNullOrEmpty(pFormulario["SelectIdCliente"]))) lMensagem.AppendLine("Ao selecionar o local Cliente, é necessário que este seja pesquisado na base do Protheus.");
            if ((ELocal.Infraestrutura.Equals(pOcorrencia.Local) && String.IsNullOrEmpty(pFormulario["SelectIdInfraestrutura"]))) lMensagem.AppendLine("Ao selecionar o local Infraestrutura, é necessário que este seja pesquisado na base do Protheus.");
            if ((ELocal.Outro.Equals(pOcorrencia.Local) && String.IsNullOrEmpty(pFormulario["Outro"]))) lMensagem.AppendLine("Ao selecionar o local Outro, é necessário Preencher o campo Outro.");
            if (pOcorrencia.Tipo == null) lMensagem.AppendLine("Informe o Tipo.");
            if (String.IsNullOrEmpty(pOcorrencia.Atendente)) lMensagem.AppendLine("Informe o Atendente.");
            if (pOcorrencia.Inicio == null) lMensagem.AppendLine("Informe a hora de início.");
            if (String.IsNullOrEmpty(pOcorrencia.Descricao)) lMensagem.AppendLine("Informe a Descrição.");
            if (pOcorrencia.Conclusao != null && String.IsNullOrEmpty(pOcorrencia.Justificativa)) lMensagem.AppendLine("Informe a justificativa de conclusão.");
            validarPeriodoTurno(lRegistroTurno, lMensagem, pOcorrencia);
            validarPeriodoOcorrencia(pOcorrencia, lMensagem);



        }
        #endregion

        #region[Pendencia]
        public ActionResult PopUpPendencia(FormCollection pFormulario)
        {
            Pendencia lPendencia = null;
            PendenciaRepositorio = new Repositorio<Pendencia>(TurnoRepositorio.Contexto);
            lPendencia = PendenciaRepositorio.ObterPorId(Convert.ToInt32(pFormulario["Id"])) ?? new Pendencia()
            {
                RegistroTurno = TurnoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["idRegistroTurno"]))
            };
            if (!String.IsNullOrEmpty(lPendencia.Cliente)) lPendencia.ClienteObj = PendenciaRepositorio.ObterPorCodigoClienteDoProtheus(lPendencia.Cliente).FirstOrDefault();
            if (!String.IsNullOrEmpty(lPendencia.Infraestrutura)) lPendencia.InfraestruturaObj = PendenciaRepositorio.ObterPorCodigoInfraestruturaDoProtheus(lPendencia.Infraestrutura).FirstOrDefault();

            PopularCampos();
            return PartialView("PopUpPendencia", lPendencia);
        }


        [HttpGet]
        public JsonResult ObterTotalMetroCubico(double vazaoEntrada)
        {
            try
            {
                List<string> VazaoEntradaRetiradaTotal = new List<string>();
                string vazaoEntradaTotal = String.Format("{0:0,0}", vazaoEntrada);
                VazaoEntradaRetiradaTotal.Add(vazaoEntradaTotal);

                return Json(VazaoEntradaRetiradaTotal, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { msg = e.Message, erro = true });
            }
        }

        private RegistroTurno CadastrarPendencia(FormCollection pFormulario)
        {
            PendenciaRepositorio = new Repositorio<Pendencia>(TurnoRepositorio.Contexto);
            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);
            GasodutoRepositorio = new Repositorio<Gasoduto>(TurnoRepositorio.Contexto);
            PontoEntregaRepositorio = new Repositorio<PontoEntrega>(TurnoRepositorio.Contexto);
            ClienteRepositorio = new Repositorio<Cliente>(TurnoRepositorio.Contexto);
            //InfraestruturaRepositorio = new Repositorio<Infraestrutura>(TurnoRepositorio.Contexto);
            RegiaoRepositorio = new Repositorio<Regiao>(TurnoRepositorio.Contexto);

            RegistroTurno lRegistroTurno = TurnoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["IdRegistroTurno"]));
            Pendencia lPendencia = PendenciaRepositorio.ObterPorId(Convert.ToInt32(pFormulario["Id"]));

            Pendencia lModel = new Pendencia()
            {
                Id = Convert.ToInt32(pFormulario["Id"] ?? "0"),
                Atendente = pFormulario["Atendente"],
                NomeContato = pFormulario["NomeContato"],
                Descricao = pFormulario["Descricao"],
                Justificativa = pFormulario["Justificativa"],
                Cliente = pFormulario["SelectIdCliente"],
                Infraestrutura = pFormulario["SelectIdInfraestrutura"],
                PontoEntrega = (String.IsNullOrEmpty(pFormulario["PontoEntrega.Id"])) ? null : PontoEntregaRepositorio.ObterPorId(Convert.ToInt32(pFormulario["PontoEntrega.Id"])),
                Regiao = (String.IsNullOrEmpty(pFormulario["Regiao.Id"])) ? null : RegiaoRepositorio.ObterPorId(Convert.ToInt32(pFormulario["Regiao.Id"])),
                Inicio = String.IsNullOrEmpty(pFormulario["Inicio"]) ? null : (DateTime?)Convert.ToDateTime(pFormulario["Inicio"]),
                Atendimento = String.IsNullOrEmpty(pFormulario["Inicio"]) ? null : (DateTime?)Convert.ToDateTime(pFormulario["Inicio"]),
                Conclusao = String.IsNullOrEmpty(pFormulario["Conclusao"]) ? null : (DateTime?)Convert.ToDateTime(pFormulario["Conclusao"]),
                Status = ObterStatus(pFormulario["Inicio"], pFormulario["Atendimento"], pFormulario["Conclusao"]),
                Local = String.IsNullOrEmpty(pFormulario["Local"]) ? null : (ELocal?)Enum.Parse(typeof(ELocal), pFormulario["Local"]),
                Origem = String.IsNullOrEmpty(pFormulario["Origem"]) ? null : (EOrigemPendencia?)Enum.Parse(typeof(EOrigemPendencia), pFormulario["Origem"]),
                CodigoSST = pFormulario["CodigoSST"],
                Outro = pFormulario["Outro"]
            };
            ValidarPendencia(lModel, pFormulario);
            if (lPendencia == null)
            {
                lModel.RegistroTurno = lRegistroTurno;
                lRegistroTurno.Pendencias.Add(lModel);
            }
            else
            {
                if (lPendencia.Id == lRegistroTurno.Id)
                {
                    foreach (Pendencia item in lRegistroTurno.Pendencias.Where(x => x.Id == lPendencia.Id))
                    {
                        item.Cliente = lModel.Cliente;
                        item.Inicio = lModel.Inicio;
                        item.Atendimento = lModel.Inicio;
                        item.Conclusao = lModel.Conclusao;
                        item.Status = lModel.Status;
                        item.Atendente = lModel.Atendente;
                        item.PontoEntrega = lModel.PontoEntrega;
                        item.NomeContato = lModel.NomeContato;
                        item.Descricao = lModel.Descricao;
                        item.Justificativa = lModel.Justificativa;
                        item.Regiao = lModel.Regiao;
                        item.Local = lModel.Local;
                        item.Origem = lModel.Origem;
                        item.CodigoSST = lModel.CodigoSST;
                        item.Outro = lModel.Outro;
                    }
                }
                else
                {
                    lPendencia.Cliente = lModel.Cliente;
                    lPendencia.Inicio = lModel.Inicio;
                    lPendencia.Atendimento = lModel.Inicio;
                    lPendencia.Conclusao = lModel.Conclusao;
                    lPendencia.Status = lModel.Status;
                    lPendencia.Atendente = lModel.Atendente;
                    lPendencia.PontoEntrega = lModel.PontoEntrega;
                    lPendencia.NomeContato = lModel.NomeContato;
                    lPendencia.Descricao = lModel.Descricao;
                    lPendencia.Justificativa = lModel.Justificativa;
                    lPendencia.Regiao = lModel.Regiao;
                    lPendencia.Local = lModel.Local;
                    lPendencia.Origem = lModel.Origem;
                    lPendencia.CodigoSST = lModel.CodigoSST;
                    PendenciaRepositorio.Editar(lPendencia);
                    lPendencia.Outro = lModel.Outro;
                }
            }
            lRegistroTurno = TurnoRepositorio.Editar(lRegistroTurno);
            lRegistroTurno.FatorCorrecao = Convert.ToDecimal(pFormulario["FatorCorrecao"]);
            return lRegistroTurno;
        }

        private void ValidarPendencia(Pendencia pPendencia, FormCollection pFormulario)
        {
            StringBuilder lMensagem = new StringBuilder();
            if (pPendencia.Origem == null) lMensagem.AppendLine("Informe a Origem.");
            if (pPendencia.Local == null) lMensagem.AppendLine("Informe o Local.");
            if ((ELocal.Cliente.Equals(pPendencia.Local) && String.IsNullOrEmpty(pFormulario["SelectIdCliente"]))) lMensagem.AppendLine("Ao selecionar o local Cliente, é necessário que este seja pesquisado na base do Protheus.");
            if ((ELocal.Infraestrutura.Equals(pPendencia.Local) && String.IsNullOrEmpty(pFormulario["SelectIdInfraestrutura"]))) lMensagem.AppendLine("Ao selecionar o local Infraestrutura, é necessário que este seja pesquisado na base do Protheus.");
            if ((ELocal.Outro.Equals(pPendencia.Local) && String.IsNullOrEmpty(pFormulario["Outro"]))) lMensagem.AppendLine("Ao selecionar o local Outro, é necessário Preencher o campo Outro.");
            if (String.IsNullOrEmpty(pPendencia.Atendente)) lMensagem.AppendLine("Informe o Atendente.");
            if (pPendencia.Inicio == null) lMensagem.AppendLine("Informe a hora de início.");
            if (String.IsNullOrEmpty(pPendencia.Descricao)) lMensagem.AppendLine("Informe a Descrição.");
            if (pPendencia.Conclusao != null && String.IsNullOrEmpty(pPendencia.Justificativa)) lMensagem.AppendLine("Informe a justificativa de conclusão.");
            validarPeriodoPendencia(pPendencia, lMensagem);
            if (!String.IsNullOrEmpty(lMensagem.ToString())) throw new Exception(lMensagem.ToString());
        }
        #endregion

        private void PopularCampos()
        {
            OperadorRepositorio = new Repositorio<Operador>(TurnoRepositorio.Contexto);
            GasodutoRepositorio = new Repositorio<Gasoduto>(TurnoRepositorio.Contexto);
            PontoEntregaRepositorio = new Repositorio<PontoEntrega>(TurnoRepositorio.Contexto);
            ClienteRepositorio = new Repositorio<Cliente>(TurnoRepositorio.Contexto);
            //InfraestruturaRepositorio = new Repositorio<Infraestrutura>(TurnoRepositorio.Contexto);
            RegiaoRepositorio = new Repositorio<Regiao>(TurnoRepositorio.Contexto);

            ViewData.Add("Operadores", OperadorRepositorio.ObterOperadoresDoProtheus());
            ViewData.Add("PontosEntrega", new SelectList(PontoEntregaRepositorio.Listar(), "Id", "Nome"));
            ViewData.Add("Gasodutos", new SelectList(GasodutoRepositorio.Listar(), "Id", "Nome"));
            ViewData.Add("Clientes", new SelectList(ClienteRepositorio.Listar(), "Id", "Nome"));
            //ViewData.Add("Infraestruturas", new SelectList(InfraestruturaRepositorio.Listar(), "Id", "Nome"));
            ViewData.Add("Regioes", new SelectList(RegiaoRepositorio.Listar(), "Id", "Nome"));
        }

        private EStatus ObterStatus(String pInicio, String pAtendimento, String pConclusao)
        {
            if (!String.IsNullOrEmpty(pConclusao)) return EStatus.Concluído;
            if (!String.IsNullOrEmpty(pAtendimento)) return EStatus.Iniciado;

            return EStatus.Pendente;
        }

        private void validarPeriodoTurno(RegistroTurno pRegistroTurno, StringBuilder pMensagem, Ocorrencia mOcorrencia)
        {

            DateTime hoje = DateTime.Now;
            DateTime inicioTurno;
            DateTime fimTurno;

            // Testa se está no modo inclusão
            if (pRegistroTurno.Id == 0)
            {
                inicioTurno = pRegistroTurno.Data;
                fimTurno = pRegistroTurno.Data;

                if (pRegistroTurno.Turno.Equals(ETurno.De7as15))
                {
                    inicioTurno = inicioTurno.AddHours(7);
                    fimTurno = fimTurno.AddHours(15);
                }
                else if (pRegistroTurno.Turno.Equals(ETurno.De15as23))
                {
                    inicioTurno = inicioTurno.AddHours(15);
                    fimTurno = fimTurno.AddHours(23);
                }
                else
                {
                    inicioTurno = inicioTurno.AddHours(23);
                    fimTurno = fimTurno.AddDays(1);
                    fimTurno = fimTurno.AddHours(7);
                }

                if (DateTime.Compare(inicioTurno, hoje) == 1 || DateTime.Compare(fimTurno, hoje) == -1)
                {
                    pMensagem.AppendLine("A data informada é inválida!");
                    pMensagem.AppendLine("Turno selecionado: " + inicioTurno + " ás " + fimTurno);
                    pMensagem.AppendLine("Data/hora atual   = " + hoje);
                }
            }
            else
            {
                inicioTurno = pRegistroTurno.Data;
                fimTurno = pRegistroTurno.Data;

                if (pRegistroTurno.Turno.Equals(ETurno.De7as15))
                {
                    inicioTurno = inicioTurno.AddHours(7);
                    fimTurno = fimTurno.AddHours(15);
                }
                else if (pRegistroTurno.Turno.Equals(ETurno.De15as23))
                {
                    inicioTurno = inicioTurno.AddHours(15);
                    fimTurno = fimTurno.AddHours(23);
                }
                else
                {
                    inicioTurno = inicioTurno.AddHours(23);
                    fimTurno = fimTurno.AddDays(1);
                    fimTurno = fimTurno.AddHours(7);
                }

                if(mOcorrencia != null) {
                if (DateTime.Compare(inicioTurno, Convert.ToDateTime(mOcorrencia.Inicio)) == 1 || DateTime.Compare(fimTurno, Convert.ToDateTime(mOcorrencia.Inicio)) == -1)
                {
                    pMensagem.AppendLine("A data/ocorrência ou hora/ocorrência é inválida!");
                    pMensagem.AppendLine("Turno selecionado: " + inicioTurno + " ás " + fimTurno);
                    pMensagem.AppendLine("Data e hora da ocorrência: " + mOcorrencia.Inicio);
                        throw new Exception(pMensagem.ToString());
                    }
                }
            }

        }

        private void validarPeriodoOcorrencia(Ocorrencia pOcorrencia, StringBuilder pMensagem)
        {

            if (pOcorrencia.Atendimento != null && (DateTime.Compare((DateTime)pOcorrencia.Inicio, (DateTime)pOcorrencia.Atendimento) == 1))
            {
                pMensagem.AppendLine("A data Atendimento deve ser posterior à data Início!");
            }

            if (pOcorrencia.Atendimento != null && pOcorrencia.Conclusao != null && (DateTime.Compare((DateTime)pOcorrencia.Conclusao, (DateTime)pOcorrencia.Atendimento) == -1))
            {
                pMensagem.AppendLine("A data Conclusão deve ser posterior à data Atendimento!");
            }

        }

        private void validarPeriodoPendencia(Pendencia pPendencia, StringBuilder pMensagem)
        {

            if (pPendencia.Conclusao != null && (DateTime.Compare((DateTime)pPendencia.Inicio, (DateTime)pPendencia.Conclusao) == 1))
            {
                pMensagem.AppendLine("A data Conslusão deve ser posterior à data Início!");
            }

        }

    }
}