namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class login1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Utilizadors", "Perfil_TipoPerfilID", "dbo.TipoPerfils");
            DropIndex("dbo.Utilizadors", new[] { "Perfil_TipoPerfilID" });
            AddColumn("dbo.Estacaos", "CustoPMinuto", c => c.Single(nullable: false));
            AddColumn("dbo.Estacaos", "NPostos", c => c.Int(nullable: false));
            AddColumn("dbo.Reservas", "DataReserva", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservas", "ReservaConfirmada", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reservas", "PostoID_PostoID", c => c.Int());
            CreateIndex("dbo.Reservas", "PostoID_PostoID");
            AddForeignKey("dbo.Reservas", "PostoID_PostoID", "dbo.Postoes", "PostoID");
            DropColumn("dbo.Utilizadors", "Perfil_TipoPerfilID");
            DropTable("dbo.TipoPerfils");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TipoPerfils",
                c => new
                    {
                        TipoPerfilID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.TipoPerfilID);
            
            AddColumn("dbo.Utilizadors", "Perfil_TipoPerfilID", c => c.Int());
            DropForeignKey("dbo.Reservas", "PostoID_PostoID", "dbo.Postoes");
            DropIndex("dbo.Reservas", new[] { "PostoID_PostoID" });
            DropColumn("dbo.Reservas", "PostoID_PostoID");
            DropColumn("dbo.Reservas", "ReservaConfirmada");
            DropColumn("dbo.Reservas", "DataReserva");
            DropColumn("dbo.Estacaos", "NPostos");
            DropColumn("dbo.Estacaos", "CustoPMinuto");
            CreateIndex("dbo.Utilizadors", "Perfil_TipoPerfilID");
            AddForeignKey("dbo.Utilizadors", "Perfil_TipoPerfilID", "dbo.TipoPerfils", "TipoPerfilID");
        }
    }
}
