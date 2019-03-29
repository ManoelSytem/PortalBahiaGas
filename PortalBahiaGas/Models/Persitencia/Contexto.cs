using PortalBahiaGas.Models.Mapping;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PortalBahiaGas.Models.Persistencia
{
    public class Contexto : DbContext
    {
        public Contexto() : base("OracleDbContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("RELATORIO_TURNO");
            Database.SetInitializer(new CreateDatabaseIfNotExists<Contexto>());

            modelBuilder.Configurations.Add(new OcorrenciaMapping());
            modelBuilder.Configurations.Add(new OperadorMapping());
            modelBuilder.Configurations.Add(new RegistroClienteMapping());
            modelBuilder.Configurations.Add(new RegistroGasodutoMapping());
            modelBuilder.Configurations.Add(new RegistroPontoEntregaMapping());
            modelBuilder.Configurations.Add(new RegistroTurnoMapping());
            modelBuilder.Configurations.Add(new OrigemMapping());
            modelBuilder.Configurations.Add(new UsuarioMapping());
            modelBuilder.Configurations.Add(new GasodutoMapping());
            modelBuilder.Configurations.Add(new PontoEntregaMapping());
            modelBuilder.Configurations.Add(new RegiaoMapping());
            modelBuilder.Configurations.Add(new ClienteMapping());
            modelBuilder.Configurations.Add(new PendenciaMapping());
            modelBuilder.Configurations.Add(new OdorizadorMapping());
            modelBuilder.Configurations.Add(new RegistroOdorizadorMapping());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
