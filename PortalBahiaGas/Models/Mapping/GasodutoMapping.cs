using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class GasodutoMapping : EntityTypeConfiguration<Gasoduto>
    {
        public GasodutoMapping()
        {
            ToTable("Gasoduto");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("gas_int_id");
            Property(x => x.Nome).HasColumnName("gas_str_nome").HasColumnType("VARCHAR2").IsRequired();
        }
    }
}