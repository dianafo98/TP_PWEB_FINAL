namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredv13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilizadors", "NomeUsr", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilizadors", "NomeUsr", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
