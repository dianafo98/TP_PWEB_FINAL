namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredv12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilizadors", "DataNasc", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilizadors", "DataNasc", c => c.DateTime());
        }
    }
}
