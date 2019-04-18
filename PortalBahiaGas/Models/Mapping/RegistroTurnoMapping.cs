using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class RegistroTurnoMapping : EntityTypeConfiguration<RegistroTurno>
    {
        public RegistroTurnoMapping()
        {
            ToTable("RegistroTurno");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("rtu_int_id");
            Property(x => x.Data).HasColumnName("rtu_dat_data").IsRequired();
            Property(x => x.Turno).HasColumnName("rtu_int_turno").IsRequired();
            Property(x => x.Bloqueado).HasColumnName("rtu_bit_bloqueado").IsRequired();
            Property(x => x.HoraMedicao).HasColumnName("rtu_dat_horaMedicao").IsOptional();
            Property(x => x.FatorCorrecao).HasColumnName("rtu_dec_fatorCorrecao").IsOptional();
            Property(x => x.OperadorMedicao).HasColumnName("rtu_str_operadorMedicao").HasColumnType("VARCHAR2").IsOptional();
            HasMany(x => x.RegistrosGasoduto);
            HasMany(x => x.RegistrosPontoEntrega);
            HasMany(x => x.RegistrosCliente);
            HasMany(x => x.RegistrosOdorizador);
            HasMany(x => x.Ocorrencias);
            HasMany(x => x.Pendencias);
            HasMany(x => x.OperadorRegistroTurno);
            HasMany<Operador>(x => x.Operadores)
                .WithMany(x => x.RegistrosTurno)
                .Map(x =>
                {
                    x.ToTable("tOperadorRegistroTurno");
                    x.MapLeftKey("rtu_int_id");
                    x.MapRightKey("ope_int_id");
                });
            Ignore(x => x.OutrasOcorrencias);
            Property(x => x.Turma).HasColumnName("rtu_int_turma").IsOptional();
            Property(x => x.UsuarioCriacao).HasColumnName("rtu_str_usuarioCriacao");
            Property(x => x.UsuarioAlteracao).HasColumnName("rtu_str_usuarioAlteracao");
            Property(x => x.DataCriacao).HasColumnName("rtu_dat_criacao");
            Property(x => x.DataAlteracao).HasColumnName("rtu_dat_alteracao");
        }
    }
}