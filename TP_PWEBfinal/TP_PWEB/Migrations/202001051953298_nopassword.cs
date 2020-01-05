namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nopassword : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Empresas", "Password");
            DropColumn("dbo.Empresas", "ConfirmPassword");
            DropColumn("dbo.Utilizadors", "Password");
            DropColumn("dbo.Utilizadors", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utilizadors", "ConfirmPassword", c => c.String());
            AddColumn("dbo.Utilizadors", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Empresas", "ConfirmPassword", c => c.String());
            AddColumn("dbo.Empresas", "Password", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
