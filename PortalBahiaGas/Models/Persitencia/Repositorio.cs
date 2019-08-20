using Oracle.ManagedDataAccess.Client;
using PortalBahiaGas.Models.Entidade;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PortalBahiaGas.Models.Persistencia
{
    public class Repositorio<T> : IDisposable where T : AEntidade
    {
        public Contexto Contexto { get; private set; }

        private bool Disposed = false;

        public Repositorio(Contexto pContexto = null)
        {
            Contexto = pContexto ?? new Contexto();
        }

        public T ObterPorId(int pId)
        {
            return Contexto.Set<T>().Find(pId);
        }

        public ICollection<T> Listar()
        {
            return Contexto.Set<T>().ToList();
        }

        public ICollection<T> Listar(Expression<Func<T, bool>> pExpressao)
        {
            return Contexto.Set<T>().Where(pExpressao.Compile()).ToList();
        }

        public ICollection<RegistroTurno> ListarPesquisa(DateTime? inicio, DateTime? fim)
        {
            return Contexto.Set<RegistroTurno>().Where(dt => dt.Data >= inicio && dt.Data <= fim).ToList();
        }

        public ICollection<T> ListarPorExpressao(Expression<Func<T, bool>> pExpressao)
        {
            return Contexto.Set<T>().Take(50).OrderByDescending(x => x.Id).ToList();
        }

        public Boolean VerificarExistencia(Expression<Func<T, bool>> pExpressao)
        {
            return Contexto.Set<T>().Any(pExpressao);
        }

        public T Adicionar(T pObjeto)
        {
            T lRetorno = Contexto.Set<T>().Add(pObjeto);
            Contexto.SaveChanges();
            return lRetorno;
        }

        public void Remover(T pObjeto)
        {
            
            Contexto.Set<T>().Remove(pObjeto);
            Contexto.SaveChanges();
        }

        public T Editar(T pObjeto)
        {
            Contexto.Entry(pObjeto).State = EntityState.Modified;
            Contexto.SaveChanges();
            return pObjeto;
        }

        public ICollection<T> Adicionar(ICollection<T> pObjetos)
        {
            ICollection<T> lRetorno = new List<T>();
            foreach (var lObjeto in pObjetos)
                pObjetos.Add(Contexto.Set<T>().Add(lObjeto));

            Contexto.SaveChanges();
            return lRetorno;
        }

        public void Remover(ICollection<T> pObjetos)
        {
            foreach (var lObjeto in pObjetos)
            {
                Contexto.Entry(lObjeto).State = EntityState.Deleted;
                Contexto.Set<T>().Remove(lObjeto);
            }

            Contexto.SaveChanges();
        }

        public ICollection<T> Editar(ICollection<T> pObjetos)
        {
            foreach (var lObjeto in pObjetos)
                Contexto.Entry(lObjeto).State = EntityState.Modified;

            Contexto.SaveChanges();
            return pObjetos;
        }

        //public T AtualizarFilhos(Int32 pId, params String[] pFilhos)
        //{
        //    T lRetorno = null;
        //    foreach (String lFilho in pFilhos)
        //        lRetorno = Contexto.Set<T>().Include(lFilho).FirstOrDefault(x => x.Id == pId);

        //    return lRetorno;
        //}

        public T AdicionarFilhos(T pObjeto)
        {
            return Contexto.Set<T>().Attach(pObjeto);
        }

        protected virtual void Dispose(bool pDispose)
        {
            if (!this.Disposed)
            {
                if (pDispose)
                {
                    Contexto.Dispose();
                }
            }
            this.Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Chamado ObterChamadoDeQueryExterna(String lNumeroChamado)
        {
            String lQuery = "SELECT " +
                                "A1_NREDUZ AS CODCLIENTE " +
                                ",TRIM(A1.A1_ENDENT) || ', ' || TRIM(A1.A1_BAIRRO) || ', ' || TRIM(A1.A1_MUN) || ' - ' || TRIM(A1.A1_EST) AS ENDERECO " +
                                ",TRIM(UD.UD_OBS) AS OBSERVACAO " +
                            "FROM AP6.SUC010 UC " +
                            "INNER JOIN AP6.SUD010 UD ON UD.UD_CODIGO = UC.UC_CODIGO " +
                                "AND UC.UC_FILIAL = UD.UD_FILIAL " +
                                "AND UC.D_E_L_E_T_ = ' ' " +
                                "AND UD.D_E_L_E_T_ = ' ' " +
                            "INNER JOIN AP6.SA1010 A1 ON UC.UC_CHAVE = A1.A1_COD || A1.A1_LOJA  " +
                            "WHERE UC.UC_CODIGO = :CHAMADO";

            OracleDataReader lDR;
            Chamado lChamado = new Chamado();
            OracleConnection lOracleCon = (OracleConnection)Contexto.Database.Connection;
            lOracleCon.Open();
            try
            {
                OracleCommand lComando = new OracleCommand(lQuery, lOracleCon);
                using (lComando)
                {
                    lComando.Parameters.Add(new OracleParameter("CHAMADO", lNumeroChamado));
                    lDR = lComando.ExecuteReader();
                    while (lDR.Read())
                    {
                        lChamado.CodCliente = lDR["CODCLIENTE"].ToString();
                        lChamado.Endereco = lDR["ENDERECO"].ToString();
                        lChamado.Observacao = lDR["OBSERVACAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lOracleCon.Close();
            }

            return lChamado;
        }

        public List<Cliente> ObterPorNomeClienteDoProtheus(String Nome)
        {
            StringBuilder lQuery = new StringBuilder();
            lQuery.Append("SELECT DISTINCT FANTASIA, ID_LOJA, ID_CLIENTE FROM AP6.VW_CLIENTE_COMPLETO WHERE ROWNUM <= 200 AND Id_Situacao <> 'I' and Id_Situacao <> 'S' ");

            foreach (string lPalavra in Nome.ToUpper().Split(' '))
            {
                lQuery.Append(" AND UPPER(FANTASIA) LIKE '%" + lPalavra.Trim() + "%' ");
            }

            OracleDataReader lDR;
            List<Cliente> lClientes = new List<Cliente>();
            OracleConnection lOracleCon = (OracleConnection)Contexto.Database.Connection;
            lOracleCon.Open();
            try
            {
                OracleCommand lComando = new OracleCommand(lQuery.ToString(), lOracleCon);
                using (lComando)
                {
                    lDR = lComando.ExecuteReader();
                    while (lDR.Read())
                    {
                        lClientes.Add(new Cliente()
                        {
                            Nome = lDR["FANTASIA"].ToString().Trim(),
                            IdProtheus = lDR["ID_LOJA"].ToString().Trim() + lDR["ID_CLIENTE"].ToString().Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lOracleCon.Close();
            }

            return lClientes;
        }

        public List<Cliente> ObterPorNomeInfraEstruturaDoProtheus(String Nome)
        {
            StringBuilder lQuery = new StringBuilder();
            lQuery.Append("SELECT DISTINCT ZA_COD, ZA_DESC FROM AP6.SZA010 WHERE D_E_L_E_T_ = ' ' AND ROWNUM <= 200 ");

            foreach (string lPalavra in Nome.ToUpper().Split(' '))
            {
                lQuery.Append(" AND UPPER(ZA_DESC) LIKE '%" + lPalavra.Trim() + "%' ");
            }

            OracleDataReader lDR;
            List<Cliente> lClientes = new List<Cliente>();
            OracleConnection lOracleCon = (OracleConnection)Contexto.Database.Connection;
            lOracleCon.Open();
            try
            {
                OracleCommand lComando = new OracleCommand(lQuery.ToString(), lOracleCon);
                using (lComando)
                {
                    lDR = lComando.ExecuteReader();
                    while (lDR.Read())
                    {
                        lClientes.Add(new Cliente()
                        {
                            Nome = lDR["ZA_DESC"].ToString().Trim(),
                            IdProtheus = lDR["ZA_COD"].ToString().Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lOracleCon.Close();
            }

            return lClientes;
        }

        public List<Cliente> ObterPorCodigoClienteDoProtheus(String Codigo)
        {
            String lQuery = "SELECT DISTINCT FANTASIA, ID_LOJA, ID_CLIENTE FROM AP6.VW_CLIENTE_COMPLETO WHERE ID_LOJA || ID_CLIENTE = '" + Codigo.Trim() + "'";

            OracleDataReader lDR;
            List<Cliente> lClientes = new List<Cliente>();
            OracleConnection lOracleCon = (OracleConnection)Contexto.Database.Connection;
            lOracleCon.Open();
            try
            {
                OracleCommand lComando = new OracleCommand(lQuery, lOracleCon);
                using (lComando)
                {
                    lDR = lComando.ExecuteReader();
                    while (lDR.Read())
                    {
                        lClientes.Add(new Cliente()
                        {
                            Nome = lDR["FANTASIA"].ToString().Trim(),
                            IdProtheus = lDR["ID_LOJA"].ToString().Trim() + lDR["ID_CLIENTE"].ToString().Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lOracleCon.Close();
            }

            return lClientes;
        }

        public List<Infraestrutura> ObterPorCodigoInfraestruturaDoProtheus(String Codigo)
        {
            String lQuery = "SELECT ZA_COD, ZA_DESC FROM AP6.SZA010 WHERE D_E_L_E_T_ = ' ' AND ZA_COD = '" + Codigo.Trim() + "' order by ZA_DESC";

            OracleDataReader lDR;
            List<Infraestrutura> lInfraestruturas = new List<Infraestrutura>();
            OracleConnection lOracleCon = (OracleConnection)Contexto.Database.Connection;
            lOracleCon.Open();
            try
            {
                OracleCommand lComando = new OracleCommand(lQuery, lOracleCon);
                using (lComando)
                {
                    lDR = lComando.ExecuteReader();
                    while (lDR.Read())
                    {
                        lInfraestruturas.Add(new Infraestrutura()
                        {
                            Nome = lDR["ZA_DESC"].ToString().Trim(),
                            IdProtheus = lDR["ZA_COD"].ToString().Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lOracleCon.Close();
            }

            return lInfraestruturas;
        }

        public List<Operador> ObterOperadoresDoProtheus()
        {
            String lQuery = "Select Ra.Ra_Filial || Ra.Ra_Mat As Codigo, Ra_Nome As Nome, Case Ra.Ra_Filial When '01' Then 'SALVADOR' When '03' Then 'FEIRA DE SANTANA' When '04' Then 'CAMAÇARI' ELSE 'OUTRO' END AS LOCALIDADE From Ap6.Sra010 Ra Where D_E_L_E_T_ = ' ' And Ra.Ra_Cc = '1012' And Ra.Ra_Turrev = 'S' And (Ra.Ra_Sitfolh = ' ' Or Ra.Ra_Sitfolh = 'F' Or Ra.Ra_Sitfolh = 'A') Order by LOCALIDADE, NOME";
            //String lQuery = "SELECT RA.RA_FILIAL || RA.RA_MAT AS CODIGO, RA_NOME AS NOME FROM AP6.SRA010 RA WHERE D_E_L_E_T_ = ' ' AND RA.RA_CC = '1012' AND RA.RA_TURREV = 'S' and (RA.RA_SITFOLH = ' ' OR RA.RA_SITFOLH = 'F')";

            OracleDataReader lDR;
            List<Operador> lClientes = new List<Operador>();
            OracleConnection lOracleCon = (OracleConnection)Contexto.Database.Connection;
            lOracleCon.Open();
            try
            {
                OracleCommand lComando = new OracleCommand(lQuery, lOracleCon);
                using (lComando)
                {
                    lDR = lComando.ExecuteReader();
                    while (lDR.Read())
                    {
                        lClientes.Add(new Operador()
                        {
                            CodigoProtheus = lDR["CODIGO"].ToString().Trim(),
                            Localidade = lDR["LOCALIDADE"].ToString().Trim(),
                            Nome = lDR["NOME"].ToString().Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lOracleCon.Close();
            }

            return lClientes;
        }

        public List<Operador> ObterOperadoresDoRM()
        {
            string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=bgasxl01.intranet.bahiagas.com.br)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ap12hml)));User Id=rm;Password=rm";

            String lQuery = $@"SELECT	PFUNC.CHAPA AS CODIGO,
                         PFUNC.NOME AS NOME,
                         CASE
                         WHEN PFUNC.CODFILIAL = '01' THEN 'SALVADOR'
                         WHEN PFUNC.CODFILIAL = '03' THEN 'FEIRA DE SANTANA'
                         WHEN PFUNC.CODFILIAL = '04' THEN 'CAMAÇARI'
                         ELSE 'OUTRO'
                         END LOCALIDADE
                         FROM rm.PFUNC
                         INNER JOIN rm.PFRATEIOFIXO ON PFUNC.CODCOLIGADA = PFRATEIOFIXO.CODCOLIGADA
                           AND PFUNC.CHAPA = PFRATEIOFIXO.CHAPA
                           AND PFRATEIOFIXO.CODCCUSTO = '1012'
                           WHERE PFUNC.CODSITUACAO IN('A','F','P','T') 
                           AND PFUNC.JORNADAMENSAL = 10800
                         ORDER BY LOCALIDADE";
            //String lQuery = "SELECT RA.RA_FILIAL || RA.RA_MAT AS CODIGO, RA_NOME AS NOME FROM AP6.SRA010 RA WHERE D_E_L_E_T_ = ' ' AND RA.RA_CC = '1012' AND RA.RA_TURREV = 'S' and (RA.RA_SITFOLH = ' ' OR RA.RA_SITFOLH = 'F')";

            OracleDataReader lDR;
            List<Operador> lClientes = new List<Operador>();
            OracleConnection lOracleCon = new OracleConnection();
            lOracleCon.ConnectionString = connString;
            lOracleCon.Open();
            try
            {
                OracleCommand lComando = new OracleCommand(lQuery, lOracleCon);
                using (lComando)
                {
                    lDR = lComando.ExecuteReader();
                    while (lDR.Read())
                    {
                        lClientes.Add(new Operador()
                        {
                            CodigoProtheus = lDR["CODIGO"].ToString().Trim(),
                            Localidade = lDR["LOCALIDADE"].ToString().Trim(),
                            Nome = lDR["NOME"].ToString().Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lOracleCon.Close();
            }

            return lClientes;
        }

        public List<Operador> ObterOperadoresDoRM(String pCodigos)
        {
            string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=bgasxl01.intranet.bahiagas.com.br)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ap12hml)));User Id=rm;Password=rm";

            String lQuery = $@"SELECT PFUNC.CHAPA AS Codigo,
                            PFUNC.NOME AS Nome,  
                            CASE
                            WHEN PFUNC.CODFILIAL = '01' THEN 'SALVADOR'
                            WHEN PFUNC.CODFILIAL = '03' THEN 'FEIRA DE SANTANA'
                            WHEN PFUNC.CODFILIAL = '04' THEN 'CAMAÇARI'
                            ELSE 'OUTRO'
                            END LOCALIDADE
                            FROM rm.PFUNC
                            INNER JOIN rm.PFRATEIOFIXO ON PFUNC.CODCOLIGADA = PFRATEIOFIXO.CODCOLIGADA 
                            AND PFUNC.CHAPA = PFRATEIOFIXO.CHAPA
                            AND PFRATEIOFIXO.CODCCUSTO='1012'
                            WHERE PFUNC.CODSITUACAO IN ('A','F','P','T') AND PFUNC.CHAPA IN("+pCodigos+")  AND PFUNC.JORNADAMENSAL = 10800 ORDER BY LOCALIDADE";

            OracleDataReader lDR;
            List<Operador> lClientes = new List<Operador>();
            OracleConnection lOracleCon = new OracleConnection();
            lOracleCon.ConnectionString = connString;
            lOracleCon.Open();
            try
            {
                OracleCommand lComando = new OracleCommand(lQuery, lOracleCon);
                using (lComando)
                {
                    lDR = lComando.ExecuteReader();
                    while (lDR.Read())
                    {
                        lClientes.Add(new Operador()
                        {
                            CodigoProtheus = lDR["CODIGO"].ToString().Trim(),
                            Localidade = lDR["LOCALIDADE"].ToString().Trim(),
                            Nome = lDR["NOME"].ToString().Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lOracleCon.Close();
            }

            return lClientes;
        }

        public List<Operador> ObterOperadoresDoProtheus(String pCodigos)
        {
            String lQuery = "SELECT RA.RA_FILIAL || RA.RA_MAT AS CODIGO, RA_NOME AS NOME,  Case Ra.Ra_Filial When '01' Then 'SALVADOR' When '03' Then 'FEIRA DE SANTANA' When '04' Then 'CAMAÇARI' ELSE 'OUTRO' END AS LOCALIDADE FROM AP6.SRA010 RA WHERE D_E_L_E_T_ = ' ' AND RA.RA_CC = '1012' AND RA.RA_TURREV = 'S'  and (RA.RA_SITFOLH = ' ' OR RA.RA_SITFOLH = 'F' Or Ra.Ra_Sitfolh = 'A') AND  RA.RA_FILIAL || RA.RA_MAT IN(" + pCodigos + ")";

            OracleDataReader lDR;
            List<Operador> lClientes = new List<Operador>();
            OracleConnection lOracleCon = (OracleConnection)Contexto.Database.Connection;
            lOracleCon.Open();
            try
            {
                OracleCommand lComando = new OracleCommand(lQuery, lOracleCon);
                using (lComando)
                {
                    lDR = lComando.ExecuteReader();
                    while (lDR.Read())
                    {
                        lClientes.Add(new Operador()
                        {
                            CodigoProtheus = lDR["CODIGO"].ToString().Trim(),
                            Localidade = lDR["LOCALIDADE"].ToString().Trim(),
                            Nome = lDR["NOME"].ToString().Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                lOracleCon.Close();
            }

            return lClientes;
        }
    }
}
