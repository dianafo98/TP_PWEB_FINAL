namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservas1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilizadors", "ID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilizadors", "ID");
        }
    }
}
