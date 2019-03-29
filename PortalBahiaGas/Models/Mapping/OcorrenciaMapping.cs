using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class OcorrenciaMapping : EntityTypeConfiguration<Ocorrencia>
    {
        public OcorrenciaMapping()
        {
            ToTable("Ocorrencia");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("oco_int_id");
            HasRequired<RegistroTurno>(x => x.RegistroTurno);
            HasOptional<PontoEntrega>(x => x.PontoEntrega);
            HasOptional<Regiao>(x => x.Regiao);
            HasOptional<Gasoduto>(x => x.Gasoduto);
            Property(x => x.Cliente).HasColumnName("oco_str_cliente").HasColumnType("VARCHAR2");
            Property(x => x.Infraestrutura).HasColumnName("oco_str_infraestrutura").HasColumnType("VARCHAR2");
            Property(x => x.Operador).HasColumnName("oco_str_operador").HasColumnType("VARCHAR2");
            Property(x => x.Atendente).HasColumnName("oco_str_atendente").HasColumnType("VARCHAR2").IsRequired();
            Property(x => x.Descricao).HasColumnName("oco_str_descricao").HasColumnType("VARCHAR2");
            Property(x => x.Justificativa).HasColumnName("oco_str_justificativa").HasColumnType("VARCHAR2");
            Property(x => x.Protocolo).HasColumnName("oco_str_protocolo").HasColumnType("VARCHAR2");
            Property(x => x.Tecnico).HasColumnName("oco_str_tecnico").HasColumnType("VARCHAR2");
            Property(x => x.NomeContato).HasColumnName("oco_str_nomeContato").HasColumnType("VARCHAR2");
            Property(x => x.Telefone).HasColumnName("oco_str_telefone").HasColumnType("VARCHAR2");
            Property(x => x.Chamado).HasColumnName("oco_str_chamado").HasColumnType("VARCHAR2");
            Property(x => x.Inicio).HasColumnName("oco_dat_inicio");
            Property(x => x.Origem).HasColumnName("oco_int_origem");
            Property(x => x.Local).HasColumnName("oco_int_local");
            Property(x => x.Tipo).HasColumnName("oco_int_tipo").IsRequired();
            Property(x => x.TipoMedicao).HasColumnName("oco_int_tipoMedicao");
            Property(x => x.Atendimento).HasColumnName("oco_dat_atendimento");
            Property(x => x.Conclusao).HasColumnName("oco_dat_conclusao");
            Property(x => x.Hora).HasColumnName("oco_dat_hora");
            Property(x => x.Pressao).HasColumnName("oco_dec_pressao");
            Property(x => x.Vazao).HasColumnName("oco_dec_vazao");
            Property(x => x.Status).HasColumnName("oco_int_status");
            Property(x => x.Modalidade).HasColumnName("oco_int_modalidade");
            Property(x => x.Outro).HasColumnName("oco_str_outro");
            Ignore(x => x.ClienteObj);
            Ignore(x => x.InfraestruturaObj);
        }
    }
}