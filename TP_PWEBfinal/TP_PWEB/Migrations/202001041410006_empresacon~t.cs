namespace TP_PWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empresacont : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        EmpresaID = c.Int(nullable: false, identity: true),
                        ID = c.String(),
                        NomeEmpresa = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PerfilID = c.String(),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.EmpresaID);
            
            AddColumn("dbo.Estacaos", "EmpresaNome", c => c.String());
            AddColumn("dbo.Estacaos", "Empresa_EmpresaID", c => c.Int());
            CreateIndex("dbo.Estacaos", "Empresa_EmpresaID");
            AddForeignKey("dbo.Estacaos", "Empresa_EmpresaID", "dbo.Empresas", "EmpresaID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estacaos", "Empresa_EmpresaID", "dbo.Empresas");
            DropIndex("dbo.Estacaos", new[] { "Empresa_EmpresaID" });
            DropColumn("dbo.Estacaos", "Empresa_EmpresaID");
            DropColumn("dbo.Estacaos", "EmpresaNome");
            DropTable("dbo.Empresas");
        }
    }
}
