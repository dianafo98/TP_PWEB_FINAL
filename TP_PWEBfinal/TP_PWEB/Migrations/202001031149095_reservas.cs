namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservas", "EstacaoReservada_EstacaoID", "dbo.Estacaos");
            DropForeignKey("dbo.Reservas", "PostoID_PostoID", "dbo.Postoes");
            DropForeignKey("dbo.Reservas", "UtilizadorID_UtilizadorID", "dbo.Utilizadors");
            DropIndex("dbo.Reservas", new[] { "EstacaoReservada_EstacaoID" });
            DropIndex("dbo.Reservas", new[] { "PostoID_PostoID" });
            DropIndex("dbo.Reservas", new[] { "UtilizadorID_UtilizadorID" });
            RenameColumn(table: "dbo.Reservas", name: "EstacaoReservada_EstacaoID", newName: "EstacaoID");
            RenameColumn(table: "dbo.Reservas", name: "PostoID_PostoID", newName: "PostoID");
            RenameColumn(table: "dbo.Reservas", name: "UtilizadorID_UtilizadorID", newName: "UtilizadorID");
            AlterColumn("dbo.Reservas", "Duracao", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Reservas", "EstacaoID", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservas", "PostoID", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservas", "UtilizadorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservas", "UtilizadorID");
            CreateIndex("dbo.Reservas", "EstacaoID");
            CreateIndex("dbo.Reservas", "PostoID");
            AddForeignKey("dbo.Reservas", "EstacaoID", "dbo.Estacaos", "EstacaoID", cascadeDelete: true);
            AddForeignKey("dbo.Reservas", "PostoID", "dbo.Postoes", "PostoID", cascadeDelete: true);
            AddForeignKey("dbo.Reservas", "UtilizadorID", "dbo.Utilizadors", "UtilizadorID", cascadeDelete: true);
            DropColumn("dbo.Reservas", "CustoPrevisto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservas", "CustoPrevisto", c => c.Single(nullable: false));
            DropForeignKey("dbo.Reservas", "UtilizadorID", "dbo.Utilizadors");
            DropForeignKey("dbo.Reservas", "PostoID", "dbo.Postoes");
            DropForeignKey("dbo.Reservas", "EstacaoID", "dbo.Estacaos");
            DropIndex("dbo.Reservas", new[] { "PostoID" });
            DropIndex("dbo.Reservas", new[] { "EstacaoID" });
            DropIndex("dbo.Reservas", new[] { "UtilizadorID" });
            AlterColumn("dbo.Reservas", "UtilizadorID", c => c.Int());
            AlterColumn("dbo.Reservas", "PostoID", c => c.Int());
            AlterColumn("dbo.Reservas", "EstacaoID", c => c.Int());
            AlterColumn("dbo.Reservas", "Duracao", c => c.String(nullable: false));
            RenameColumn(table: "dbo.Reservas", name: "UtilizadorID", newName: "UtilizadorID_UtilizadorID");
            RenameColumn(table: "dbo.Reservas", name: "PostoID", newName: "PostoID_PostoID");
            RenameColumn(table: "dbo.Reservas", name: "EstacaoID", newName: "EstacaoReservada_EstacaoID");
            CreateIndex("dbo.Reservas", "UtilizadorID_UtilizadorID");
            CreateIndex("dbo.Reservas", "PostoID_PostoID");
            CreateIndex("dbo.Reservas", "EstacaoReservada_EstacaoID");
            AddForeignKey("dbo.Reservas", "UtilizadorID_UtilizadorID", "dbo.Utilizadors", "UtilizadorID");
            AddForeignKey("dbo.Reservas", "PostoID_PostoID", "dbo.Postoes", "PostoID");
            AddForeignKey("dbo.Reservas", "EstacaoReservada_EstacaoID", "dbo.Estacaos", "EstacaoID");
        }
    }
}
