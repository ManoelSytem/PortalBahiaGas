namespace PortalBahiaGas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "RELATORIO_TURNO.Ocorrencia",
                c => new
                    {
                        oco_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        oco_str_operador = c.String(maxLength: 4000, unicode: false),
                        oco_str_cliente = c.String(maxLength: 4000, unicode: false),
                        oco_str_infraestrutura = c.String(maxLength: 4000, unicode: false),
                        oco_str_atendente = c.String(nullable: false, maxLength: 4000, unicode: false),
                        oco_str_descricao = c.String(maxLength: 4000, unicode: false),
                        oco_str_justificativa = c.String(maxLength: 4000, unicode: false),
                        oco_str_chamado = c.String(maxLength: 4000, unicode: false),
                        oco_str_nomeContato = c.String(maxLength: 4000, unicode: false),
                        oco_str_telefone = c.String(maxLength: 4000, unicode: false),
                        oco_str_protocolo = c.String(maxLength: 4000, unicode: false),
                        oco_str_tecnico = c.String(maxLength: 4000, unicode: false),
                        oco_dec_pressao = c.Decimal(precision: 18, scale: 2),
                        oco_dec_vazao = c.Decimal(precision: 18, scale: 2),
                        oco_dat_hora = c.DateTime(),
                        oco_dat_inicio = c.DateTime(),
                        oco_dat_atendimento = c.DateTime(),
                        oco_dat_conclusao = c.DateTime(),
                        oco_int_status = c.Decimal(precision: 10, scale: 0),
                        oco_int_origem = c.Decimal(precision: 10, scale: 0),
                        oco_int_local = c.Decimal(precision: 10, scale: 0),
                        oco_int_modalidade = c.Decimal(precision: 10, scale: 0),
                        oco_int_tipo = c.Decimal(nullable: false, precision: 10, scale: 0),
                        oco_int_tipoMedicao = c.Decimal(precision: 10, scale: 0),
                        Gasoduto_Id = c.Decimal(precision: 10, scale: 0),
                        PontoEntrega_Id = c.Decimal(precision: 10, scale: 0),
                        Regiao_Id = c.Decimal(precision: 10, scale: 0),
                        RegistroTurno_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.oco_int_id)
                .ForeignKey("RELATORIO_TURNO.Gasoduto", t => t.Gasoduto_Id)
                .ForeignKey("RELATORIO_TURNO.PontoEntrega", t => t.PontoEntrega_Id)
                .ForeignKey("RELATORIO_TURNO.Regiao", t => t.Regiao_Id)
                .ForeignKey("RELATORIO_TURNO.RegistroTurno", t => t.RegistroTurno_Id, cascadeDelete: true)
                .Index(t => t.Gasoduto_Id)
                .Index(t => t.PontoEntrega_Id)
                .Index(t => t.Regiao_Id)
                .Index(t => t.RegistroTurno_Id);
            
            CreateTable(
                "RELATORIO_TURNO.Gasoduto",
                c => new
                    {
                        gas_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        gas_str_nome = c.String(nullable: false, maxLength: 4000, unicode: false),
                    })
                .PrimaryKey(t => t.gas_int_id);
            
            CreateTable(
                "RELATORIO_TURNO.PontoEntrega",
                c => new
                    {
                        pte_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        pte_str_nome = c.String(nullable: false, maxLength: 4000, unicode: false),
                    })
                .PrimaryKey(t => t.pte_int_id);
            
            CreateTable(
                "RELATORIO_TURNO.Regiao",
                c => new
                    {
                        reg_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        reg_str_nome = c.String(nullable: false, maxLength: 4000, unicode: false),
                    })
                .PrimaryKey(t => t.reg_int_id);
            
            CreateTable(
                "RELATORIO_TURNO.RegistroTurno",
                c => new
                    {
                        rtu_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        rtu_int_turno = c.Decimal(nullable: false, precision: 10, scale: 0),
                        rtu_bit_bloqueado = c.Decimal(nullable: false, precision: 1, scale: 0),
                        rtu_str_operadorMedicao = c.String(maxLength: 4000, unicode: false),
                        rtu_dat_data = c.DateTime(nullable: false),
                        rtu_dat_horaMedicao = c.DateTime(),
                        rtu_str_usuarioCriacao = c.String(),
                        rtu_dat_criacao = c.DateTime(),
                        rtu_str_usuarioAlteracao = c.String(),
                        rtu_dat_alteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.rtu_int_id);
            
            CreateTable(
                "RELATORIO_TURNO.Operador",
                c => new
                    {
                        ope_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ope_str_codigoProtheus = c.String(maxLength: 4000, unicode: false),
                    })
                .PrimaryKey(t => t.ope_int_id);
            
            CreateTable(
                "RELATORIO_TURNO.Pendencia",
                c => new
                    {
                        jus_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        jus_str_cliente = c.String(maxLength: 4000, unicode: false),
                        jus_str_Infraestrutura = c.String(maxLength: 4000, unicode: false),
                        jus_str_atendente = c.String(maxLength: 4000, unicode: false),
                        jus_str_descricao = c.String(maxLength: 4000, unicode: false),
                        jus_str_justificativa = c.String(maxLength: 4000, unicode: false),
                        jus_str_nomeContato = c.String(maxLength: 4000, unicode: false),
                        jus_str_tecnico = c.String(maxLength: 4000, unicode: false),
                        jus_dat_inicio = c.DateTime(),
                        jus_dat_atendimento = c.DateTime(),
                        jus_dat_conclusao = c.DateTime(),
                        jus_int_status = c.Decimal(precision: 10, scale: 0),
                        jus_int_local = c.Decimal(precision: 10, scale: 0),
                        Gasoduto_Id = c.Decimal(precision: 10, scale: 0),
                        PontoEntrega_Id = c.Decimal(precision: 10, scale: 0),
                        Regiao_Id = c.Decimal(precision: 10, scale: 0),
                        RegistroTurno_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.jus_int_id)
                .ForeignKey("RELATORIO_TURNO.Gasoduto", t => t.Gasoduto_Id)
                .ForeignKey("RELATORIO_TURNO.PontoEntrega", t => t.PontoEntrega_Id)
                .ForeignKey("RELATORIO_TURNO.Regiao", t => t.Regiao_Id)
                .ForeignKey("RELATORIO_TURNO.RegistroTurno", t => t.RegistroTurno_Id, cascadeDelete: true)
                .Index(t => t.Gasoduto_Id)
                .Index(t => t.PontoEntrega_Id)
                .Index(t => t.Regiao_Id)
                .Index(t => t.RegistroTurno_Id);
            
            CreateTable(
                "RELATORIO_TURNO.RegistroCliente",
                c => new
                    {
                        rcl_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        rcl_dat_hora = c.DateTime(),
                        rcl_dec_pressaoEntrada = c.Decimal(precision: 18, scale: 2),
                        rcl_dec_pressaoSaida = c.Decimal(precision: 18, scale: 2),
                        rcl_dec_vazaoInstantanea = c.Decimal(precision: 18, scale: 2),
                        rcl_dec_vazaoAcumulada = c.Decimal(precision: 18, scale: 2),
                        Cliente_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RegistroTurno_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.rcl_int_id)
                .ForeignKey("RELATORIO_TURNO.Cliente", t => t.Cliente_Id, cascadeDelete: true)
                .ForeignKey("RELATORIO_TURNO.RegistroTurno", t => t.RegistroTurno_Id, cascadeDelete: true)
                .Index(t => t.Cliente_Id)
                .Index(t => t.RegistroTurno_Id);
            
            CreateTable(
                "RELATORIO_TURNO.Cliente",
                c => new
                    {
                        cli_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        cli_str_nome = c.String(nullable: false, maxLength: 4000, unicode: false),
                        cli_str_idProtheus = c.String(nullable: false, maxLength: 8, unicode: false),
                    })
                .PrimaryKey(t => t.cli_int_id);
            
            CreateTable(
                "RELATORIO_TURNO.RegistroGasoduto",
                c => new
                    {
                        rga_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        rga_dec_pressao = c.Decimal(precision: 18, scale: 2),
                        rga_dec_vazao = c.Decimal(precision: 18, scale: 2),
                        Gasoduto_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RegistroTurno_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.rga_int_id)
                .ForeignKey("RELATORIO_TURNO.Gasoduto", t => t.Gasoduto_Id, cascadeDelete: true)
                .ForeignKey("RELATORIO_TURNO.RegistroTurno", t => t.RegistroTurno_Id, cascadeDelete: true)
                .Index(t => t.Gasoduto_Id)
                .Index(t => t.RegistroTurno_Id);
            
            CreateTable(
                "RELATORIO_TURNO.RegistroOdorizador",
                c => new
                    {
                        rod_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        rod_int_statusComunicacao = c.Decimal(nullable: false, precision: 10, scale: 0),
                        rod_int_pressaoTqOdor = c.Decimal(precision: 10, scale: 0),
                        rod_dec_pumpDisp = c.Decimal(precision: 18, scale: 2),
                        Odorizador_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RegistroTurno_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.rod_int_id)
                .ForeignKey("RELATORIO_TURNO.Odorizador", t => t.Odorizador_Id, cascadeDelete: true)
                .ForeignKey("RELATORIO_TURNO.RegistroTurno", t => t.RegistroTurno_Id, cascadeDelete: true)
                .Index(t => t.Odorizador_Id)
                .Index(t => t.RegistroTurno_Id);
            
            CreateTable(
                "RELATORIO_TURNO.Odorizador",
                c => new
                    {
                        odo_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        odo_str_nome = c.String(nullable: false, maxLength: 4000, unicode: false),
                        odo_dec_setPointCcStk = c.Decimal(nullable: false, precision: 18, scale: 2),
                        odo_str_setPointPsi = c.String(nullable: false, maxLength: 4000, unicode: false),
                    })
                .PrimaryKey(t => t.odo_int_id);
            
            CreateTable(
                "RELATORIO_TURNO.RegistroPontoEntrega",
                c => new
                    {
                        rpe_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        rpe_dat_hora = c.DateTime(),
                        rpe_dec_pressaoEntrada = c.Decimal(precision: 18, scale: 2),
                        rpe_dec_pressaoSaida = c.Decimal(precision: 18, scale: 2),
                        rpe_dec_vazaoEntrada = c.Decimal(precision: 18, scale: 2),
                        rpe_dec_vazaoSaida = c.Decimal(precision: 18, scale: 2),
                        rpe_dec_desvio = c.Decimal(precision: 18, scale: 2),
                        rpe_bit_penalidade = c.Decimal(precision: 1, scale: 0),
                        PontoEntrega_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RegistroTurno_Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.rpe_int_id)
                .ForeignKey("RELATORIO_TURNO.PontoEntrega", t => t.PontoEntrega_Id, cascadeDelete: true)
                .ForeignKey("RELATORIO_TURNO.RegistroTurno", t => t.RegistroTurno_Id, cascadeDelete: true)
                .Index(t => t.PontoEntrega_Id)
                .Index(t => t.RegistroTurno_Id);
            
            CreateTable(
                "RELATORIO_TURNO.Origem",
                c => new
                    {
                        ori_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        ori_str_nome = c.String(nullable: false, maxLength: 4000, unicode: false),
                    })
                .PrimaryKey(t => t.ori_int_id);
            
            CreateTable(
                "RELATORIO_TURNO.Usuario",
                c => new
                    {
                        usu_int_id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        usu_str_nome = c.String(nullable: false, maxLength: 4000, unicode: false),
                        usu_str_login = c.String(nullable: false, maxLength: 4000, unicode: false),
                        usu_str_senha = c.String(maxLength: 4000, unicode: false),
                        usu_int_perfil = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.usu_int_id);
            
            CreateTable(
                "RELATORIO_TURNO.tOperadorRegistroTurno",
                c => new
                    {
                        rtu_int_id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ope_int_id = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.rtu_int_id, t.ope_int_id })
                .ForeignKey("RELATORIO_TURNO.RegistroTurno", t => t.rtu_int_id, cascadeDelete: true)
                .ForeignKey("RELATORIO_TURNO.Operador", t => t.ope_int_id, cascadeDelete: true)
                .Index(t => t.rtu_int_id)
                .Index(t => t.ope_int_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("RELATORIO_TURNO.Ocorrencia", "RegistroTurno_Id", "RELATORIO_TURNO.RegistroTurno");
            DropForeignKey("RELATORIO_TURNO.RegistroPontoEntrega", "RegistroTurno_Id", "RELATORIO_TURNO.RegistroTurno");
            DropForeignKey("RELATORIO_TURNO.RegistroPontoEntrega", "PontoEntrega_Id", "RELATORIO_TURNO.PontoEntrega");
            DropForeignKey("RELATORIO_TURNO.RegistroOdorizador", "RegistroTurno_Id", "RELATORIO_TURNO.RegistroTurno");
            DropForeignKey("RELATORIO_TURNO.RegistroOdorizador", "Odorizador_Id", "RELATORIO_TURNO.Odorizador");
            DropForeignKey("RELATORIO_TURNO.RegistroGasoduto", "RegistroTurno_Id", "RELATORIO_TURNO.RegistroTurno");
            DropForeignKey("RELATORIO_TURNO.RegistroGasoduto", "Gasoduto_Id", "RELATORIO_TURNO.Gasoduto");
            DropForeignKey("RELATORIO_TURNO.RegistroCliente", "RegistroTurno_Id", "RELATORIO_TURNO.RegistroTurno");
            DropForeignKey("RELATORIO_TURNO.RegistroCliente", "Cliente_Id", "RELATORIO_TURNO.Cliente");
            DropForeignKey("RELATORIO_TURNO.Pendencia", "RegistroTurno_Id", "RELATORIO_TURNO.RegistroTurno");
            DropForeignKey("RELATORIO_TURNO.Pendencia", "Regiao_Id", "RELATORIO_TURNO.Regiao");
            DropForeignKey("RELATORIO_TURNO.Pendencia", "PontoEntrega_Id", "RELATORIO_TURNO.PontoEntrega");
            DropForeignKey("RELATORIO_TURNO.Pendencia", "Gasoduto_Id", "RELATORIO_TURNO.Gasoduto");
            DropForeignKey("RELATORIO_TURNO.tOperadorRegistroTurno", "ope_int_id", "RELATORIO_TURNO.Operador");
            DropForeignKey("RELATORIO_TURNO.tOperadorRegistroTurno", "rtu_int_id", "RELATORIO_TURNO.RegistroTurno");
            DropForeignKey("RELATORIO_TURNO.Ocorrencia", "Regiao_Id", "RELATORIO_TURNO.Regiao");
            DropForeignKey("RELATORIO_TURNO.Ocorrencia", "PontoEntrega_Id", "RELATORIO_TURNO.PontoEntrega");
            DropForeignKey("RELATORIO_TURNO.Ocorrencia", "Gasoduto_Id", "RELATORIO_TURNO.Gasoduto");
            DropIndex("RELATORIO_TURNO.tOperadorRegistroTurno", new[] { "ope_int_id" });
            DropIndex("RELATORIO_TURNO.tOperadorRegistroTurno", new[] { "rtu_int_id" });
            DropIndex("RELATORIO_TURNO.RegistroPontoEntrega", new[] { "RegistroTurno_Id" });
            DropIndex("RELATORIO_TURNO.RegistroPontoEntrega", new[] { "PontoEntrega_Id" });
            DropIndex("RELATORIO_TURNO.RegistroOdorizador", new[] { "RegistroTurno_Id" });
            DropIndex("RELATORIO_TURNO.RegistroOdorizador", new[] { "Odorizador_Id" });
            DropIndex("RELATORIO_TURNO.RegistroGasoduto", new[] { "RegistroTurno_Id" });
            DropIndex("RELATORIO_TURNO.RegistroGasoduto", new[] { "Gasoduto_Id" });
            DropIndex("RELATORIO_TURNO.RegistroCliente", new[] { "RegistroTurno_Id" });
            DropIndex("RELATORIO_TURNO.RegistroCliente", new[] { "Cliente_Id" });
            DropIndex("RELATORIO_TURNO.Pendencia", new[] { "RegistroTurno_Id" });
            DropIndex("RELATORIO_TURNO.Pendencia", new[] { "Regiao_Id" });
            DropIndex("RELATORIO_TURNO.Pendencia", new[] { "PontoEntrega_Id" });
            DropIndex("RELATORIO_TURNO.Pendencia", new[] { "Gasoduto_Id" });
            DropIndex("RELATORIO_TURNO.Ocorrencia", new[] { "RegistroTurno_Id" });
            DropIndex("RELATORIO_TURNO.Ocorrencia", new[] { "Regiao_Id" });
            DropIndex("RELATORIO_TURNO.Ocorrencia", new[] { "PontoEntrega_Id" });
            DropIndex("RELATORIO_TURNO.Ocorrencia", new[] { "Gasoduto_Id" });
            DropTable("RELATORIO_TURNO.tOperadorRegistroTurno");
            DropTable("RELATORIO_TURNO.Usuario");
            DropTable("RELATORIO_TURNO.Origem");
            DropTable("RELATORIO_TURNO.RegistroPontoEntrega");
            DropTable("RELATORIO_TURNO.Odorizador");
            DropTable("RELATORIO_TURNO.RegistroOdorizador");
            DropTable("RELATORIO_TURNO.RegistroGasoduto");
            DropTable("RELATORIO_TURNO.Cliente");
            DropTable("RELATORIO_TURNO.RegistroCliente");
            DropTable("RELATORIO_TURNO.Pendencia");
            DropTable("RELATORIO_TURNO.Operador");
            DropTable("RELATORIO_TURNO.RegistroTurno");
            DropTable("RELATORIO_TURNO.Regiao");
            DropTable("RELATORIO_TURNO.PontoEntrega");
            DropTable("RELATORIO_TURNO.Gasoduto");
            DropTable("RELATORIO_TURNO.Ocorrencia");
        }
    }
}
