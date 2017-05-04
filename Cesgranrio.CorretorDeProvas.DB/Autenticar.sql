USE [Cesgranrio.CorretorDeProvas]
GO
/****** Object:  StoredProcedure [dbo].[Autenticar]    Script Date: 5/4/2017 7:22:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wanderley Mayhe Junior <juniormayhe@gmail.com>
-- Create date: 2017-04-27
-- Description:	Verfica credenciais do usuário elaborador ou professor e retorna ID e grupo
-- EXEC Autenticar '26140486408', 'DF3E1D81C4809D5CE4958D1D5DE7925B'
-- =============================================
ALTER PROCEDURE [dbo].[Autenticar]
	@UsuarioCPF varchar(11),
	@UsuarioSenha varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @usuarioID as int
	DECLARE @grupoID as int

	SELECT @usuarioID = u.UsuarioID , @grupoID = u.GrupoID
	FROM Usuario u
	INNER JOIN Grupo g on u.GrupoID = g.GrupoID
	WHERE u.UsuarioCPF = @UsuarioCPF 
	AND u.UsuarioSenha = @UsuarioSenha

	SELECT ISNULL(@usuarioID, 0) as UsuarioID, ISNULL(@grupoID, 0) AS GrupoID
END
