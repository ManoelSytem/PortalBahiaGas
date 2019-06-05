using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class OperadorMapping : EntityTypeConfiguration<Operador>
    {
        public OperadorMapping()
        {
            ToTable("Operador");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("ope_int_id");
            Ignore(x => x.Nome);
            Ignore(x => x.Localidade);
            Property(x => x.CodigoProtheus).HasColumnName("ope_str_codigoProtheus").HasColumnType("VARCHAR2");
            HasMany(x => x.OperadorRegistroTurno);
            Property(x => x.SalaControle)
              .HasColumnName("ope_bit_sala_controle");
            
        }
    }
}