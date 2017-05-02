USE [Cesgranrio.CorretorDeProvas]
GO
/****** Object:  StoredProcedure [dbo].[LimparRespostas]    Script Date: 5/2/2017 9:03:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wanderley Mayhe Junior <juniormayhe@gmail.com>
-- Create date: 2017-04-27
-- Description:	zera tabela de respostas para nova carga
-- =============================================
ALTER PROCEDURE [dbo].[LimparRespostas]

AS
BEGIN
	
	SET NOCOUNT ON;

    TRUNCATE TABLE Resposta;
	DELETE FROM Candidato;
	DBCC CHECKIDENT('Candidato', RESEED, 0);
END
