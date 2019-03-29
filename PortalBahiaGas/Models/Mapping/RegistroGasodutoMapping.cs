using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class RegistroGasodutoMapping : EntityTypeConfiguration<RegistroGasoduto>
    {
        public RegistroGasodutoMapping()
        {
            ToTable("RegistroGasoduto");
            HasKey(x => x.Id);
            HasRequired<RegistroTurno>(x => x.RegistroTurno);
            HasRequired<Gasoduto>(x => x.Gasoduto);
            Property(x => x.Id).HasColumnName("rga_int_id");
            Property(x => x.Pressao).HasColumnName("rga_dec_pressao");
            Property(x => x.Vazao).HasColumnName("rga_dec_vazao");
            //HasRequired<Operador>(x => x.Operador);
            //Property(x => x.Hora).HasColumnName("rga_dat_hora");
        }
    }
}