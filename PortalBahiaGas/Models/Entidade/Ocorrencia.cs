using PortalBahiaGas.Models.Entidade.Enuns;
using System;

namespace PortalBahiaGas.Models.Entidade
{
    public class Ocorrencia : AEntidade
    {
        public virtual RegistroTurno RegistroTurno { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual Gasoduto Gasoduto { get; set; }
        public virtual String Operador { get; set; }
        public virtual PontoEntrega PontoEntrega { get; set; }
        public string Cliente { get; set; }
        public Cliente ClienteObj { get; set; }
        public string Infraestrutura { get; set; }
        public Infraestrutura InfraestruturaObj { get; set; }
        public String Atendente { get; set; }
        public String Descricao { get; set; }
        public String Justificativa { get; set; }
        public String Chamado { get; set; }
        public String NomeContato { get; set; }
        public String Telefone { get; set; }
        public String Protocolo { get; set; }
        public String Tecnico { get; set; }
        public String RegiaoStr { get { return (Regiao != null) ? Regiao.Nome : string.Empty; } }
        public String Outro { get; set; }
        public Decimal? Pressao { get; set; }
        public Decimal? Vazao { get; set; }
        public DateTime? Hora { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Atendimento { get; set; }
        public DateTime? Conclusao { get; set; }
        public EStatus? Status { get; set; }
        public EOrigem? Origem { get; set; }
        public ELocal? Local { get; set; }
        public EModalidade? Modalidade { get; set; }
        public ETipoOcorrencia? Tipo { get; set; }
        public ETipoMedicao? TipoMedicao { get; set; }
    }
}