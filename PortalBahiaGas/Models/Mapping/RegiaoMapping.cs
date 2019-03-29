using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class RegiaoMapping : EntityTypeConfiguration<Regiao>
    {
        public RegiaoMapping()
        {
            ToTable("Regiao");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("reg_int_id");
            Property(x => x.Nome).HasColumnName("reg_str_nome").HasColumnType("VARCHAR2").IsRequired();
        }
    }
}