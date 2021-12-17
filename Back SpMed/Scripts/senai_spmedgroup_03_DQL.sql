USE SP_MedGroup;
GO

SELECT * FROM tipoUsuario;
GO

SELECT * FROM usuario;
GO

SELECT * FROM especializacao;
GO

SELECT * FROM instituicao;
GO

SELECT * FROM paciente;
GO

SELECT * FROM medico;
GO

SELECT * FROM situacaoConsulta;
GO

SELECT * FROM consulta;
GO

SELECT U.nome AS Paciente, UMED.nome AS Medico, E.nomeEspecializacao AS Especialização, CONVERT(varchar, C.dataConsulta, 103) AS Data,descricao AS Situação FROM consulta C
INNER JOIN paciente P
ON C.idPaciente = P.idPaciente
INNER JOIN medico M
ON C.idMedico = M.idMedico
INNER JOIN usuario U
ON P.idUsuario = U.idUsuario
INNER JOIN usuario UMED
ON M.idUsuario = UMED.idUsuario
INNER JOIN especializacao E
ON M.idEspecializacao = E.idEspecializacao
INNER JOIN situacaoConsulta S
ON C.idSituacaoConsulta = S.idSituacaoConsulta;
GO

SELECT COUNT(idUsuario) [Numero De Usuarios] FROM usuario;
GO

--Função - Especialização

CREATE FUNCTION MED_ESPC(@nomeEspec VARCHAR(100))
RETURNS TABLE
AS
RETURN
(
 SELECT @nomeEspec AS Especialização, COUNT(idEspecializacao) [Numero De Médicos] FROM especializacao
 WHERE nomeEspecializacao LIKE '%'+ @nomeEspec +'%'
)
GO

SELECT * FROM MED_ESPC('Cardiologia');
GO

--_______________________________________________________________________________________________________


CREATE PROCEDURE IDADE
@nome VARCHAR(100)
AS
BEGIN
 SELECT U.nome, DATEDIFF(YEAR, P.dataNascimento, GETDATE()) AS Idade  FROM paciente P
 INNER JOIN usuario U
 ON P.idUsuario = U.idUsuario
 WHERE U.nome = @nome
END
GO

exec IDADE 'Mariana'
