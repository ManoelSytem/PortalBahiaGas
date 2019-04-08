
using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class OperadorRegistroTurnoMapping : EntityTypeConfiguration<OperadorRegistroTurno>
    {
        public OperadorRegistroTurnoMapping()
        {
            ToTable("OperadorRegistroTurno");

            HasKey(x => new { x.IdOperador, x.IdRegistroTurno });

            HasRequired(t => t.Operador)
                .WithMany(t => t.OperadorRegistroTurno)
                .HasForeignKey(t => t.IdOperador);

            HasRequired(t => t.RegistroTurno)
                .WithMany(t => t.OperadorRegistroTurno)
                .HasForeignKey(t => t.IdRegistroTurno);

            Property(x => x.IdRegistroTurno)
                .HasColumnName("rtu_int_id");

            Property(x => x.IdOperador)
                .HasColumnName("ope_int_id");

            Property(x => x.Local)
                .HasColumnName("ort_str_local")
                .HasColumnType("VARCHAR2");

            Property(x => x.SalaControle)
                .HasColumnName("ort_bit_salaControle");
        }
    }
}