using PortalBahiaGas.Models.Entidade.Enuns;
using System;

namespace PortalBahiaGas.Models.Entidade
{
    public class Usuario : AEntidade
    {
        public String Nome { get; set; }
        public String Login { get; set; }
        public String Senha { get; set; }
        public String ConfirmaSenha { get; set; }
        public EPerfil? Perfil { get; set; }

        public override string ToString()
        {
            return this.Nome;
        }
    }
}