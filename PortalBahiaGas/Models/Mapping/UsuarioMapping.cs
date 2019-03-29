
using PortalBahiaGas.Models.Entidade;
using System.Data.Entity.ModelConfiguration;

namespace PortalBahiaGas.Models.Mapping
{
    public class UsuarioMapping : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMapping()
        {
            ToTable("Usuario");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("usu_int_id");
            Property(x => x.Nome).HasColumnName("usu_str_nome").HasColumnType("VARCHAR2").IsRequired();
            Property(x => x.Login).HasColumnName("usu_str_login").HasColumnType("VARCHAR2").IsRequired();
            Property(x => x.Senha).HasColumnName("usu_str_senha").HasColumnType("VARCHAR2").IsOptional();
            Property(x => x.Perfil).HasColumnName("usu_int_perfil").IsRequired();
            Ignore(x => x.ConfirmaSenha);
        }
    }
}