CREATE DATABASE SP_MedGroup;
GO

USE SP_MedGroup;
GO

CREATE TABLE tipoUsuario(
 idTipo TINYINT PRIMARY KEY IDENTITY,
 nomeTipo VARCHAR(100) NOT NULL UNIQUE
);
GO

CREATE TABLE usuario(
 idUsuario INT PRIMARY KEY IDENTITY, 
 idTipo TINYINT FOREIGN KEY REFERENCES tipoUsuario(idTipo),
 nome VARCHAR(150) NOT NULL,
 email VARCHAR(256) NOT NULL UNIQUE,
 senha VARCHAR(25) NOT NULL
);
GO

CREATE TABLE situacaoConsulta(
idSituacaoConsulta TINYINT PRIMARY KEY IDENTITY (1,1),
situacaoConsulta VARCHAR (15)
);
GO

CREATE TABLE especializacao(
 idEspecializacao TINYINT PRIMARY KEY IDENTITY,
 nomeEspecializacao VARCHAR(100) NOT NULL UNIQUE
);
GO


CREATE TABLE instituicao (
 idInstituicao SMALLINT PRIMARY KEY IDENTITY,
 nomeFantasia VARCHAR(200) NOT NULL,
 cnpj VARCHAR(20) NOT NULL UNIQUE,
 razaoSocial VARCHAR(200) NOT NULL UNIQUE,
 endereco VARCHAR(200) NOT NULL UNIQUE
);
GO

CREATE TABLE medico (
idMedico SMALLINT PRIMARY KEY IDENTITY (1,1),
idUsuario INT FOREIGN KEY REFERENCES Usuario(idUsuario),
idInstituicao SMALLINT FOREIGN KEY REFERENCES Instituicao (idInstituicao),
idEspecializacao TINYINT FOREIGN KEY REFERENCES especializacao (idEspecializacao),
nomeMedico VARCHAR (200) NOT NULL,
crm VARCHAR (20) UNIQUE NOT NULL
);
GO

CREATE TABLE paciente(
 idPaciente INT PRIMARY KEY IDENTITY,
 idUsuario INT FOREIGN KEY REFERENCES usuario(idUsuario),
 dataNascimento DATE NOT NULL,
 telefone VARCHAR(20) UNIQUE,
 rg VARCHAR(15) NOT NULL UNIQUE,
 cpf VARCHAR(15) NOT NULL UNIQUE,
 endereco VARCHAR (256) NOT NULL
);
GO

CREATE TABLE consulta(
idConsulta INT PRIMARY KEY IDENTITY (1,1),
idPaciente INT FOREIGN KEY REFERENCES Paciente (idPaciente),
idMedico SMALLINT FOREIGN KEY REFERENCES Medico (idMedico),
idSituacaoConsulta TINYINT FOREIGN KEY REFERENCES situacaoConsulta (idSituacaoConsulta),
dataConsulta DATETIME NOT NULL,
descricao VARCHAR (256)
);
GO

CREATE TABLE imagemUsuario(
 id INT PRIMARY KEY IDENTITY(1,1),
 idUsuario INT FOREIGN KEY REFERENCES usuario(idUsuario),
 binario VARBINARY(MAX) NOT NULL,
 mimeType VARCHAR(30) NOT NULL,
 nomeArquivo VARCHAR(250) NOT NULL,
 data_inclusao DATETIME DEFAULT GETDATE() NULL
);
GO
