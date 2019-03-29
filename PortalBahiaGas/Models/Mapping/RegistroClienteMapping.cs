using PortalBahiaGas.Models.Entidade;
using System;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class RegistroClienteMapping : EntityTypeConfiguration<RegistroCliente>
    {
        public RegistroClienteMapping()
        {
            ToTable("RegistroCliente");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("rcl_int_id");
            HasRequired<RegistroTurno>(x => x.RegistroTurno);
            HasRequired<Cliente>(x => x.Cliente);
            Property(x => x.Hora).HasColumnName("rcl_dat_hora");
            Property(x => x.PressaoEntrada).HasColumnName("rcl_dec_pressaoEntrada");
            Property(x => x.PressaoSaida).HasColumnName("rcl_dec_pressaoSaida");
            Property(x => x.VazaoInstantanea).HasColumnName("rcl_dec_vazaoInstantanea");
            Property(x => x.VazaoAcumulada).HasColumnName("rcl_dec_vazaoAcumulada");
        }
    }
}