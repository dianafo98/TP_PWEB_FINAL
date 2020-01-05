namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilizadors", "DataNasc", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilizadors", "DataNasc", c => c.DateTime(nullable: false));
        }
    }
}
