using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class OdorizadorMapping : EntityTypeConfiguration<Odorizador>
    {
        public OdorizadorMapping()
        {
            ToTable("Odorizador");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("odo_int_id");
            Property(x => x.Nome).HasColumnName("odo_str_nome").HasColumnType("VARCHAR2").IsRequired();
            Property(x => x.SetPointCcStk).HasColumnName("odo_dec_setPointCcStk").IsRequired();
            Property(x => x.SetPointPsi).HasColumnName("odo_str_setPointPsi").HasColumnType("VARCHAR2").IsRequired();
        }
    }
}