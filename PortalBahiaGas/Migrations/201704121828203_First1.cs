namespace PortalBahiaGas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("RELATORIO_TURNO.RegistroOdorizador", "rod_int_statusComunicacao", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("RELATORIO_TURNO.RegistroOdorizador", "rod_int_statusComunicacao", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
    }
}
