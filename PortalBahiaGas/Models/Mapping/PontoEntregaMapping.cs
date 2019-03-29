using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class PontoEntregaMapping : EntityTypeConfiguration<PontoEntrega>
    {
        public PontoEntregaMapping()
        {
            ToTable("PontoEntrega");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("pte_int_id");
            Property(x => x.Nome).HasColumnName("pte_str_nome").HasColumnType("VARCHAR2").IsRequired();
        }
    }

}