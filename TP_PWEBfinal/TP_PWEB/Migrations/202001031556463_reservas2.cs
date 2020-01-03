namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservas2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Estacaos", "Longitude", c => c.String(nullable: false));
            AddColumn("dbo.Estacaos", "Latitude", c => c.String(nullable: false));
            DropColumn("dbo.Estacaos", "Localizacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Estacaos", "Localizacao", c => c.String(nullable: false));
            DropColumn("dbo.Estacaos", "Latitude");
            DropColumn("dbo.Estacaos", "Longitude");
        }
    }
}
