namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservas", "UtilizadorID", "dbo.Utilizadors");
            DropIndex("dbo.Reservas", new[] { "UtilizadorID" });
            AddColumn("dbo.Reservas", "Utilizador_UtilizadorID", c => c.Int());
            AlterColumn("dbo.Reservas", "UtilizadorID", c => c.String());
            CreateIndex("dbo.Reservas", "Utilizador_UtilizadorID");
            AddForeignKey("dbo.Reservas", "Utilizador_UtilizadorID", "dbo.Utilizadors", "UtilizadorID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "Utilizador_UtilizadorID", "dbo.Utilizadors");
            DropIndex("dbo.Reservas", new[] { "Utilizador_UtilizadorID" });
            AlterColumn("dbo.Reservas", "UtilizadorID", c => c.Int(nullable: false));
            DropColumn("dbo.Reservas", "Utilizador_UtilizadorID");
            CreateIndex("dbo.Reservas", "UtilizadorID");
            AddForeignKey("dbo.Reservas", "UtilizadorID", "dbo.Utilizadors", "UtilizadorID", cascadeDelete: true);
        }
    }
}
