SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wanderley Mayhe Junior <juniormayhe@gmail.com>
-- Create date: 05/05/2017
-- Description:	Cria respostas simuladas
-- =============================================
CREATE PROCEDURE CriarResposta
		@UsuarioID int,
        @CandidatoID int,
        @QuestaoID int,
        @RespostaImagem image,
        @RespostaGradeEscolhida int,
        @RespostaNota decimal(5,2),
        @RespostaNotaConcluida bit
AS
BEGIN
	
	SET NOCOUNT OFF;

	INSERT INTO [dbo].[Resposta]
           ([UsuarioID]
           ,[CandidatoID]
           ,[QuestaoID]
           ,[RespostaImagem]
           ,[RespostaGradeEscolhida]
           ,[RespostaNota]
           ,[RespostaNotaConcluida])
     VALUES
           (@UsuarioID
           ,@CandidatoID
           ,@QuestaoID
           ,@RespostaImagem
           ,@RespostaGradeEscolhida
           ,@RespostaNota
           ,@RespostaNotaConcluida)


END
GO
