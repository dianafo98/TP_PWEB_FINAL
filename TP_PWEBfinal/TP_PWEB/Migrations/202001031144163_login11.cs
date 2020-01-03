namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class login11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizadors", "Dinheiro", c => c.Double(nullable: false));
            AlterColumn("dbo.Utilizadors", "PerfilID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilizadors", "PerfilID", c => c.Int(nullable: false));
            DropColumn("dbo.Utilizadors", "Dinheiro");
        }
    }
}
