namespace Cesgranrio.CorretorDeProvas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Candidato",
            //    c => new
            //        {
            //            CandidatoID = c.Int(nullable: false, identity: true),
            //            CandidatoNome = c.String(unicode: false),
            //            CandidatoCPF = c.String(unicode: false),
            //        })
            //    .PrimaryKey(t => t.CandidatoID);
            
            //CreateTable(
            //    "dbo.Resposta",
            //    c => new
            //        {
            //            RespostaID = c.Int(nullable: false, identity: true),
            //            UsuarioID = c.Int(nullable: false),
            //            CandidatoID = c.Int(nullable: false),
            //            QuestaoID = c.Int(nullable: false),
            //            RespostaImagem = c.Binary(storeType: "image"),
            //            RespostaFidelidadeAoTema = c.Decimal(nullable: false, precision: 5, scale: 2),
            //            RespostaOrganizacaoDeIdeias = c.Decimal(nullable: false, precision: 5, scale: 2),
            //            RespostaNivelDeLinguagem = c.Decimal(nullable: false, precision: 5, scale: 2),
            //            RespostaDominioDasRegras = c.Decimal(nullable: false, precision: 5, scale: 2),
            //        })
            //    .PrimaryKey(t => t.RespostaID)
            //    .ForeignKey("dbo.Questao", t => t.QuestaoID)
            //    .ForeignKey("dbo.Usuario", t => t.UsuarioID)
            //    .ForeignKey("dbo.Candidato", t => t.CandidatoID)
            //    .Index(t => t.UsuarioID)
            //    .Index(t => t.CandidatoID)
            //    .Index(t => t.QuestaoID);
            
            //CreateTable(
            //    "dbo.Questao",
            //    c => new
            //        {
            //            QuestaoID = c.Int(nullable: false, identity: true),
            //            QuestaoNumero = c.Int(nullable: false),
            //            QuestaoEnunciado = c.String(nullable: false, maxLength: 500, unicode: false),
            //            QuestaoGradeFidelidadeAoTema = c.Decimal(nullable: false, precision: 5, scale: 2),
            //            QuestaoGradeOrganizacaoIdeias = c.Decimal(nullable: false, precision: 5, scale: 2),
            //            QuestaoGradeNivelDeLinguagem = c.Decimal(nullable: false, precision: 5, scale: 2),
            //            QuestaoGradeDominioDasRegras = c.Decimal(nullable: false, precision: 5, scale: 2),
            //        })
            //    .PrimaryKey(t => t.QuestaoID);
            
            //CreateTable(
            //    "dbo.Usuario",
            //    c => new
            //        {
            //            UsuarioID = c.Int(nullable: false, identity: true),
            //            UsuarioCPF = c.String(nullable: false, maxLength: 11, unicode: false),
            //            UsuarioSenha = c.String(nullable: false, maxLength: 50, unicode: false),
            //            GrupoID = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.UsuarioID)
            //    .ForeignKey("dbo.Grupo", t => t.GrupoID)
            //    .Index(t => t.GrupoID);
            
            //CreateTable(
            //    "dbo.Grupo",
            //    c => new
            //        {
            //            GrupoID = c.Int(nullable: false, identity: true),
            //            GrupoNome = c.String(nullable: false, maxLength: 100, unicode: false),
            //        })
            //    .PrimaryKey(t => t.GrupoID);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Resposta", "CandidatoID", "dbo.Candidato");
            //DropForeignKey("dbo.Resposta", "UsuarioID", "dbo.Usuario");
            //DropForeignKey("dbo.Usuario", "GrupoID", "dbo.Grupo");
            //DropForeignKey("dbo.Resposta", "QuestaoID", "dbo.Questao");
            //DropIndex("dbo.Usuario", new[] { "GrupoID" });
            //DropIndex("dbo.Resposta", new[] { "QuestaoID" });
            //DropIndex("dbo.Resposta", new[] { "CandidatoID" });
            //DropIndex("dbo.Resposta", new[] { "UsuarioID" });
            //DropTable("dbo.Grupo");
            //DropTable("dbo.Usuario");
            //DropTable("dbo.Questao");
            //DropTable("dbo.Resposta");
            //DropTable("dbo.Candidato");
        }
    }
}
