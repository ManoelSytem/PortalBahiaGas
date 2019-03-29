using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class RegistroOdorizadorMapping : EntityTypeConfiguration<RegistroOdorizador>
    {
        public RegistroOdorizadorMapping()
        {
            ToTable("RegistroOdorizador");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("rod_int_id");
            HasRequired<RegistroTurno>(x => x.RegistroTurno);
            HasRequired<Odorizador>(x => x.Odorizador);
            Property(x => x.StatusComunicacao).HasColumnName("rod_int_statusComunicacao").IsOptional();
            Property(x => x.PressaoTqOdor).HasColumnName("rod_int_pressaoTqOdor");
            Property(x => x.PumpDisp).HasColumnName("rod_dec_pumpDisp");
        }
    }
}