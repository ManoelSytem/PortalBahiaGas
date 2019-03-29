using PortalBahiaGas.Models.Entidade.Enuns;
using System;

namespace PortalBahiaGas.Models.Entidade
{
    public class Pendencia : AEntidade
    {
        public virtual RegistroTurno RegistroTurno { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual Gasoduto Gasoduto { get; set; }
        public virtual PontoEntrega PontoEntrega { get; set; }
        public virtual Cliente ClienteObj { get; set; }
        public String Cliente { get; set; }
        public virtual Infraestrutura InfraestruturaObj { get; set; }
        public String CodigoSST { get; set; }
        public String Infraestrutura { get; set; }
        public String Atendente { get; set; }
        public String Descricao { get; set; }
        public String Justificativa { get; set; }
        public String NomeContato { get; set; }
        public String Tecnico { get; set; }
        public String RegiaoStr { get { return (Regiao != null) ? Regiao.Nome : string.Empty; } }
        public String Outro { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Atendimento { get; set; }
        public DateTime? Conclusao { get; set; }
        public EStatus? Status { get; set; }
        public ELocal? Local { get; set; }
        public EOrigemPendencia? Origem { get; set; }
    }
}