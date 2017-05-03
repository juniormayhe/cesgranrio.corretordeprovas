namespace Cesgranrio.CorretorDeProvas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resposta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resposta", "RespostaGradeFidelidadeAoTema", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AddColumn("dbo.Resposta", "RespostaGradeOrganizacaoIdeias", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AddColumn("dbo.Resposta", "RespostaGradeNivelDeLinguagem", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AddColumn("dbo.Resposta", "RespostaGradeDominioDasRegras", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            DropColumn("dbo.Resposta", "RespostaFidelidadeAoTema");
            DropColumn("dbo.Resposta", "RespostaOrganizacaoDeIdeias");
            DropColumn("dbo.Resposta", "RespostaNivelDeLinguagem");
            DropColumn("dbo.Resposta", "RespostaDominioDasRegras");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resposta", "RespostaDominioDasRegras", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AddColumn("dbo.Resposta", "RespostaNivelDeLinguagem", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AddColumn("dbo.Resposta", "RespostaOrganizacaoDeIdeias", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            AddColumn("dbo.Resposta", "RespostaFidelidadeAoTema", c => c.Decimal(nullable: false, precision: 5, scale: 2));
            DropColumn("dbo.Resposta", "RespostaGradeDominioDasRegras");
            DropColumn("dbo.Resposta", "RespostaGradeNivelDeLinguagem");
            DropColumn("dbo.Resposta", "RespostaGradeOrganizacaoIdeias");
            DropColumn("dbo.Resposta", "RespostaGradeFidelidadeAoTema");
        }
    }
}
