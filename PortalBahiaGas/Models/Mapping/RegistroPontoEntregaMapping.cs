using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class RegistroPontoEntregaMapping : EntityTypeConfiguration<RegistroPontoEntrega>
    {
        public RegistroPontoEntregaMapping()
        {
            ToTable("RegistroPontoEntrega");
            HasKey(x => x.Id);
            HasRequired<RegistroTurno>(x => x.RegistroTurno);
            HasRequired<PontoEntrega>(x => x.PontoEntrega);
            Property(x => x.Id).HasColumnName("rpe_int_id");
            Property(x => x.Hora).HasColumnName("rpe_dat_hora");
            Property(x => x.PressaoEntrada).HasColumnName("rpe_dec_pressaoEntrada");
            Property(x => x.PressaoSaida).HasColumnName("rpe_dec_pressaoSaida");
            Property(x => x.VazaoEntrada).HasColumnName("rpe_dec_vazaoEntrada");
            Property(x => x.VazaoSaida).HasColumnName("rpe_dec_vazaoSaida");
            Property(x => x.Desvio).HasColumnName("rpe_dec_desvio");
            Property(x => x.Penalidade).HasColumnName("rpe_bit_penalidade");
        }
    }
}