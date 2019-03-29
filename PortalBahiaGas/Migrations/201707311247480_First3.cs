namespace PortalBahiaGas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("RELATORIO_TURNO.Ocorrencia", "oco_str_outro", c => c.String());
            AddColumn("RELATORIO_TURNO.Pendencia", "jus_str_outro", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("RELATORIO_TURNO.Pendencia", "jus_str_outro");
            DropColumn("RELATORIO_TURNO.Ocorrencia", "oco_str_outro");
        }
    }
}
