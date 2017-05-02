
-- Script usado apenas para carga inicial para apoio durante desenvolvimento
BEGIN TRAN

PRINT 'Realizando carga inicial...'
--elaborador
IF NOT EXISTS(SELECT 1 FROM Usuario WHERE UsuarioCPF='26140486408')
	insert into Usuario(UsuarioCPF, UsuarioSenha, GrupoID) values ('26140486408',CONVERT(VARCHAR(32), HashBytes('MD5', 'elaborador'), 2), 1)
--professor
IF NOT EXISTS(SELECT 1 FROM Usuario WHERE UsuarioCPF='18842743070')
	insert into Usuario(UsuarioCPF, UsuarioSenha, GrupoID) values ('18842743070',CONVERT(VARCHAR(32), HashBytes('MD5', 'professor'), 2), 2)
	--professor
IF NOT EXISTS(SELECT 1 FROM Usuario WHERE UsuarioCPF='58446167581')
	insert into Usuario(UsuarioCPF, UsuarioSenha, GrupoID) values ('58446167581',CONVERT(VARCHAR(32), HashBytes('MD5', 'professor'), 2), 2)
--candidatos
--IF NOT EXISTS(SELECT 1 FROM Usuario WHERE UsuarioCPF='53137376190')
--	insert into Usuario(UsuarioCPF, UsuarioSenha, GrupoID) values ('53137376190',CONVERT(VARCHAR(32), HashBytes('MD5', 'candidato'), 2), 3)
--IF NOT EXISTS(SELECT 1 FROM Usuario WHERE UsuarioCPF='62222851394')
--	insert into Usuario(UsuarioCPF, UsuarioSenha, GrupoID) values ('62222851394',CONVERT(VARCHAR(32), HashBytes('MD5', 'candidato'), 2), 3)
--IF NOT EXISTS(SELECT 1 FROM Usuario WHERE UsuarioCPF='90797173048')
--	insert into Usuario(UsuarioCPF, UsuarioSenha, GrupoID) values ('90797173048',CONVERT(VARCHAR(32), HashBytes('MD5', 'candidato'), 2), 3)

if @@ERROR=0 begin
	print 'OK'
	commit
end
else begin
	print 'falha'
	rollback
end