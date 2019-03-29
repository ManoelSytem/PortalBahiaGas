using PortalBahiaGas.Models.Entidade;
using PortalBahiaGas.Models.Entidade.Enuns;
using System;
using System.Collections.Generic;

namespace PortalBahiaGas.Util
{
    public static class Utilitario
    {
        private static readonly Dictionary<ETurno, Int16> LimiteTurno = new Dictionary<ETurno, Int16>() {
            { ETurno.De7as15, 17 },
            { ETurno.De15as23,1 },
            { ETurno.De23as7, 9 }
        };

        public static Boolean VerificarLimiteEdicaoTurno(ETurno pTurno, DateTime pData, Usuario pUsuario)
        {
            if (pUsuario.Perfil == EPerfil.Administrador) return true;
            if (pTurno == ETurno.De15as23 || pTurno == ETurno.De23as7) pData = pData.AddDays(1);
            pData = pData.AddHours(LimiteTurno[pTurno]);
            return pData > DateTime.Now;
        }
    }
}