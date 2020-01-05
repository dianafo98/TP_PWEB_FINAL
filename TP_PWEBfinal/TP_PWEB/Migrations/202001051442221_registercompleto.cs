namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registercompleto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizadors", "MetodoPagamento", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilizadors", "MetodoPagamento");
        }
    }
}
