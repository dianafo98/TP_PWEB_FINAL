namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class login : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizadors", "NomeUsr", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Utilizadors", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Utilizadors", "ConfirmPassword", c => c.String());
            DropColumn("dbo.Utilizadors", "NomeUtilizador");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utilizadors", "NomeUtilizador", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Utilizadors", "ConfirmPassword");
            DropColumn("dbo.Utilizadors", "Password");
            DropColumn("dbo.Utilizadors", "NomeUsr");
        }
    }
}
