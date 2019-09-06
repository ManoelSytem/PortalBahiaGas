using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalBahiaGas.Models.ModelDaaper
{
    public class OcorrenciaDapper
    {
        public int Id { get; set; }
        public int Origem { get; set; }
        public int RegistroTurno { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Atendimento { get; set; }
        public DateTime? Conclusao { get; set; }
        public int Status { get; set; }

    }
}