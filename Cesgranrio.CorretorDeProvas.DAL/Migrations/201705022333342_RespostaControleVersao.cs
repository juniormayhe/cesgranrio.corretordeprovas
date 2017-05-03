namespace Cesgranrio.CorretorDeProvas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RespostaControleVersao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resposta", "RespostaControleVersao", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resposta", "RespostaControleVersao");
        }
    }
}
