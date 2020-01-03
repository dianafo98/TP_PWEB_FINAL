namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservasroles : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Postoes", name: "UtilizadorID_UtilizadorID", newName: "Utilizador_UtilizadorID");
            RenameIndex(table: "dbo.Postoes", name: "IX_UtilizadorID_UtilizadorID", newName: "IX_Utilizador_UtilizadorID");
            AddColumn("dbo.Postoes", "UtilizadorID", c => c.String());
            AddColumn("dbo.Reservas", "Custo", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservas", "Custo");
            DropColumn("dbo.Postoes", "UtilizadorID");
            RenameIndex(table: "dbo.Postoes", name: "IX_Utilizador_UtilizadorID", newName: "IX_UtilizadorID_UtilizadorID");
            RenameColumn(table: "dbo.Postoes", name: "Utilizador_UtilizadorID", newName: "UtilizadorID_UtilizadorID");
        }
    }
}
