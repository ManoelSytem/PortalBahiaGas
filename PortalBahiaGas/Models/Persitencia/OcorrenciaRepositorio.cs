using Dapper;
using Oracle.ManagedDataAccess.Client;
using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.ModelDaaper;
using PortalBahiaGas.Models.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalBahiaGas.Models.Persitencia
{
    public class OcorrenciaRepositorio
    {

        private OracleConnection Conexao;
        //private const string ConnectionTeste = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=stark.intranet.bahiagas.com.br)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=AP12HML)));User Id=ap6;Password=ap6;";
        private const string ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=stark.intranet.bahiagas.com.br)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ap12hml)));User Id=RELATORIO_TURNO;Password=relatorio_turno;";
        

        public OcorrenciaRepositorio()
        {
            Conexao = new OracleConnection(ConnectionString);
        }

        public ICollection<Ocorrencia> ListarOcorrencia(DateTime? inicial, DateTime? final, string operador, int? regiao, string cliente, string turno, string TipoOcorrencia, string lOrigem, string lDescricao, string lStatus, string Infraestrutura)
        {

            try
            {
                Conexao.Open();

                if (!inicial.HasValue && !final.HasValue)
                {
                    inicial = DateTime.Now;
                    final = DateTime.Now; 
                }
                    List<Ocorrencia> listaOcorrencia = new List<Ocorrencia>();
                var sql = "Select  \"oco_int_id\" as Id," +
                        "V$DWH_ORIGEM.ID_ORIGEM as Origem," +
                         "V$DWH_TURNO.ID_TURNO as Status," +
                         "\"RegistroTurno_Id\" as RegistroTurno," +
                         "\"oco_dat_inicio\" as Inicio," +
                         "\"oco_dat_atendimento\" as Atendimento," +
                         "\"oco_dat_conclusao\" as Conclusao, " +
                         "\"oco_int_status\" as Status " +
                         "from  \"Ocorrencia\" " +
                          "INNER JOIN V$DWH_ORIGEM " +
                          "on  V$DWH_ORIGEM.ID_ORIGEM = \"Ocorrencia\".\"oco_int_origem\" " +
                          "INNER JOIN  \"RegistroTurno\" " +
                          "on \"RegistroTurno\".\"rtu_int_id\" = \"Ocorrencia\".\"RegistroTurno_Id\" " +
                          "INNER JOIN V$DWH_TURNO " +
                          "ON V$DWH_TURNO.ID_TURNO = \"RegistroTurno\".\"rtu_int_turno\" ";

                if (inicial.HasValue && !final.HasValue)
                {
                    sql += "WHERE TO_DATE(\"Ocorrencia\".\"oco_dat_inicio\",'DD/MM/YY') = TO_DATE(TO_CHAR('" + inicial?.ToString("dd/MM/yyyy") + "'), 'DD/MM/YY')";
                }

                if (!inicial.HasValue && !final.HasValue)
                {
                    sql += "WHERE TO_DATE(\"Ocorrencia\".\"oco_dat_inicio\",'DD/MM/YY') = TO_DATE(TO_CHAR('" + inicial?.ToString("dd/MM/yyyy") + "'), 'DD/MM/YY')";
                }

                if (inicial.HasValue && final.HasValue)
                {
                   sql+= "WHERE TO_DATE(\"Ocorrencia\".\"oco_dat_inicio\",'DD/MM/YY') >= TO_DATE(TO_CHAR('" + inicial?.ToString("dd/MM/yyyy") + "'), 'DD/MM/YY')";
                }

                if (final.HasValue)
                {
                    sql += " AND TO_DATE(\"Ocorrencia\".\"oco_dat_inicio\",'DD/MM/YY') <= TO_DATE(TO_CHAR('" +final?.ToString("dd/MM/yyyy") + "'), 'DD/MM/YY')";
                }

                if (!String.IsNullOrEmpty(operador))
                {
                   sql += "AND \"Ocorrencia\".\"oco_str_atendente\" LIKE '"+operador+ "'";
                    
                }

                if (regiao != null)
                {
                    sql += " AND  \"Ocorrencia\".\"Regiao_Id\" LIKE '"+regiao+"'";
                }

                if (!String.IsNullOrEmpty(cliente))
                {
                    sql += " AND \"Ocorrencia\".\"oco_str_cliente\" LIKE '"+cliente.Trim()+"'";
                }
                if (!String.IsNullOrEmpty(Infraestrutura))
                {
                    sql += " AND \"Ocorrencia\".\"oco_str_infraestrutura\" LIKE '"+Infraestrutura.Trim()+"'";
                }

               
                if (!String.IsNullOrEmpty(turno))
                {
                    sql += " AND V$DWH_TURNO.ID_TURNO.ID_TURNO = " + turno + "";
                }
              
                if (!String.IsNullOrEmpty(TipoOcorrencia))
                {
                    sql += " AND \"Ocorrencia\".\"oco_int_tipo\" = "+TipoOcorrencia + "";
                }

                if (!String.IsNullOrEmpty(lOrigem))
                {
                    sql += " AND \"Ocorrencia\".\"oco_int_origem\"  = " + lOrigem + "";
                }

                if (!String.IsNullOrEmpty(lDescricao))
                {
                   sql += " AND \"Ocorrencia\".\"oco_str_descricao\"  LIKE '%" + lDescricao.Trim()+"%'";
                }

                if (!String.IsNullOrEmpty(lStatus))
                {
                    sql+= " AND \"Ocorrencia\".\"oco_int_status\" = "+lStatus+ "";
                }
                var QueryResult = Conexao.Query<OcorrenciaDapper>(sql).ToList();
                Conexao.Close();
                foreach (OcorrenciaDapper ocorrenciaResult in QueryResult)
                {
                    Ocorrencia ocorrencia = new Ocorrencia();

                    var context = new Repositorio<Ocorrencia>();
                    ocorrencia = context.ObterPorId(ocorrenciaResult.Id);
                    var contextRegistroTurno = new Repositorio<RegistroTurno>();
                    ocorrencia.RegistroTurno = contextRegistroTurno.ObterPorId(ocorrenciaResult.Id);
                   
                    listaOcorrencia.Add(ocorrencia);
                }  

               return listaOcorrencia;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Conexao.Close();
            }
        }
    }
}