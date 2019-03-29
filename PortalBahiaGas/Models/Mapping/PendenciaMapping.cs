using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class PendenciaMapping : EntityTypeConfiguration<Pendencia>
    {
        public PendenciaMapping()
        {
            ToTable("Pendencia");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("jus_int_id");
            HasRequired<RegistroTurno>(x => x.RegistroTurno);
            HasOptional<PontoEntrega>(x => x.PontoEntrega);
            HasOptional<Regiao>(x => x.Regiao);
            HasOptional<Gasoduto>(x => x.Gasoduto);
            Property(x => x.Cliente).HasColumnName("jus_str_cliente").HasColumnType("VARCHAR2");
            Property(x => x.Infraestrutura).HasColumnName("jus_str_Infraestrutura").HasColumnType("VARCHAR2");
            Property(x => x.Atendente).HasColumnName("jus_str_atendente").HasColumnType("VARCHAR2");
            Property(x => x.Descricao).HasColumnName("jus_str_descricao").HasColumnType("VARCHAR2");
            Property(x => x.Justificativa).HasColumnName("jus_str_justificativa").HasColumnType("VARCHAR2");
            Property(x => x.Tecnico).HasColumnName("jus_str_tecnico").HasColumnType("VARCHAR2");
            Property(x => x.NomeContato).HasColumnName("jus_str_nomeContato").HasColumnType("VARCHAR2");
            Property(x => x.Inicio).HasColumnName("jus_dat_inicio");
            Property(x => x.Atendimento).HasColumnName("jus_dat_atendimento");
            Property(x => x.Conclusao).HasColumnName("jus_dat_conclusao");
            Property(x => x.Status).HasColumnName("jus_int_status");
            Property(x => x.Local).HasColumnName("jus_int_local");
            Property(x => x.Origem).HasColumnName("jus_int_origem");
            Property(x => x.CodigoSST).HasColumnName("jus_int_codigoSST");
            Property(x => x.Outro).HasColumnName("jus_str_outro");
            Ignore(x => x.ClienteObj);
            Ignore(x => x.InfraestruturaObj);
        }
    }
}