using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class ClienteMapping : EntityTypeConfiguration<Cliente>
    {
        public ClienteMapping()
        {
            ToTable("Cliente");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("cli_int_id");
            Property(x => x.Nome).HasColumnName("cli_str_nome").HasColumnType("VARCHAR2").IsRequired();
            Property(x => x.IdProtheus).HasColumnName("cli_str_idProtheus").HasColumnType("VARCHAR2").HasMaxLength(8).IsRequired();
        }
    }
}