﻿using PortalBahiaGas.Models.Entidade.Enuns;
using System;
using System.Collections.Generic;


namespace PortalBahiaGas.Models.Entidade
{
    public class RegistroTurno : AEntidade
    {
        public RegistroTurno()
        {
            RegistrosGasoduto = new List<RegistroGasoduto>();
            RegistrosPontoEntrega = new List<RegistroPontoEntrega>();
            RegistrosCliente = new List<RegistroCliente>();
            RegistrosOdorizador = new List<RegistroOdorizador>();
            Ocorrencias = new List<Ocorrencia>();
            Pendencias = new List<Pendencia>();
            OutrasOcorrencias = new List<Ocorrencia>();
            Operadores = new List<Operador>();
            OperadorRegistroTurno = new List<OperadorRegistroTurno>();
        }

        public ETurno Turno { get; set; }
        public ETurma? Turma { get; set; }
        public virtual ICollection<Operador> Operadores { get; protected set; }
        public virtual ICollection<RegistroGasoduto> RegistrosGasoduto { get; protected set; }
        public virtual ICollection<RegistroPontoEntrega> RegistrosPontoEntrega { get; protected set; }
        public virtual ICollection<RegistroCliente> RegistrosCliente { get; protected set; }
        public virtual ICollection<RegistroOdorizador> RegistrosOdorizador { get; protected set; }
        public virtual ICollection<Ocorrencia> Ocorrencias { get; protected set; }
        public virtual ICollection<Pendencia> Pendencias { get; set; }
        public virtual ICollection<OperadorRegistroTurno> OperadorRegistroTurno { get; set; }
        public List<Ocorrencia> OutrasOcorrencias { get; protected set; }
        public bool Bloqueado { get; set; }
        public String OperadorMedicao { get; set; }
        public DateTime Data { get; set; }
        public DateTime? HoraMedicao { get; set; }
        public String UsuarioCriacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public String UsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Decimal? FatorCorrecao { get; set; }

        public ETurma? ObterTurma(string codigo)
        {
            ETurma turma = ETurma.D;
            switch (codigo)
            {
                case "1":
                    turma = ETurma.Z;
                    break;
                case "2":
                    turma = ETurma.A;
                    break;
                case "3":
                    turma = ETurma.B;
                    break;
                case "4":
                    turma = ETurma.C;
                    break;
                case "5":
                    turma = ETurma.D;
                    break;
                case "6":
                    turma = ETurma.E;
                    break;
            }
            return turma;
        }




    }

}