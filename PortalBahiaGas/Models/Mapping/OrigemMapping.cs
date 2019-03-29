using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class OrigemMapping : EntityTypeConfiguration<Origem>
    {
        public OrigemMapping()
        {
            ToTable("Origem");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("ori_int_id");
            Property(x => x.Nome).HasColumnName("ori_str_nome").HasColumnType("VARCHAR2").IsRequired();
        }
    }
}