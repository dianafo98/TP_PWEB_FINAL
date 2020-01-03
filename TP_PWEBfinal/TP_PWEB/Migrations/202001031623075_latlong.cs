namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latlong : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Estacaos", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Estacaos", "Latitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Estacaos", "Latitude", c => c.String(nullable: false));
            AlterColumn("dbo.Estacaos", "Longitude", c => c.String(nullable: false));
        }
    }
}
