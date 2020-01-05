namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reserva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservas", "UtilizadorNome", c => c.String());
            AddColumn("dbo.Reservas", "EstacaoNome", c => c.String());
            AddColumn("dbo.Reservas", "HoraReserva", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservas", "HoraReserva");
            DropColumn("dbo.Reservas", "EstacaoNome");
            DropColumn("dbo.Reservas", "UtilizadorNome");
        }
    }
}
