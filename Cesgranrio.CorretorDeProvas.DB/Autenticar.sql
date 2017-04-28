SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Wanderley Mayhe Junior <juniormayhe@gmail.com>
-- Create date: 2017-04-27
-- Description:	Verfica credenciais do usuário elaborador ou professor
-- EXEC Autenticar '26140486408', 'DF3E1D81C4809D5CE4958D1D5DE7925B'
-- =============================================
ALTER PROCEDURE Autenticar
	@UsuarioCPF varchar(11),
	@UsuarioSenha varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @EXISTE as bit
	
	SELECT @EXISTE = 1 
	FROM Usuario 
	WHERE UsuarioCPF = @UsuarioCPF 
	AND UsuarioSenha = @UsuarioSenha

	SELECT CASE WHEN @EXISTE=1 THEN '1' ELSE '0' END AS Existe

END
GO
