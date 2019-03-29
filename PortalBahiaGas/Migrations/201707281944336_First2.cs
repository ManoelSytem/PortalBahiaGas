namespace PortalBahiaGas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("RELATORIO_TURNO.Pendencia", "jus_int_codigoSST", c => c.String());
            AddColumn("RELATORIO_TURNO.Pendencia", "jus_int_origem", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("RELATORIO_TURNO.Pendencia", "jus_int_origem");
            DropColumn("RELATORIO_TURNO.Pendencia", "jus_int_codigoSST");
        }
    }
}
