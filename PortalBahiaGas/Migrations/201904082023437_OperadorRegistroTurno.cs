namespace PortalBahiaGas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OperadorRegistroTurno : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "RELATORIO_TURNO.OperadorRegistroTurno",
                c => new
                    {
                        ope_int_id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        rtu_int_id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ort_str_local = c.String(maxLength: 4000, unicode: false),
                        ort_bit_salaControle = c.Decimal(nullable: false, precision: 1, scale: 0),
                    })
                .PrimaryKey(t => new { t.ope_int_id, t.rtu_int_id })
                .ForeignKey("RELATORIO_TURNO.Operador", t => t.ope_int_id, cascadeDelete: true)
                .ForeignKey("RELATORIO_TURNO.RegistroTurno", t => t.rtu_int_id, cascadeDelete: true)
                .Index(t => t.ope_int_id)
                .Index(t => t.rtu_int_id);
            
            AddColumn("RELATORIO_TURNO.RegistroTurno", "rtu_int_turma", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropForeignKey("RELATORIO_TURNO.OperadorRegistroTurno", "rtu_int_id", "RELATORIO_TURNO.RegistroTurno");
            DropForeignKey("RELATORIO_TURNO.OperadorRegistroTurno", "ope_int_id", "RELATORIO_TURNO.Operador");
            DropIndex("RELATORIO_TURNO.OperadorRegistroTurno", new[] { "rtu_int_id" });
            DropIndex("RELATORIO_TURNO.OperadorRegistroTurno", new[] { "ope_int_id" });
            DropColumn("RELATORIO_TURNO.RegistroTurno", "rtu_int_turma");
            DropTable("RELATORIO_TURNO.OperadorRegistroTurno");
        }
    }
}
