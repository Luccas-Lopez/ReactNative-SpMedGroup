USE SP_MedGroup;
GO

INSERT INTO tipoUsuario(nomeTipo)
VALUES ('Administrador'), ('Médico'), ('Paciente')
GO

INSERT INTO usuario(idTipo,nome,email,senha)
VALUES 
(3,'Adriana','adriana@email.com','111'),(3,'Saulo','saulo@email.com','222'),
(3,'Maria','maria@email.com','333'),(3,'Renato','renato@email.com','444'),
(3,'João','joao@email.com','555'),(3,'Bruno','bruno@email.com','666'),
(3,'Mariana','mariana@email.com','777'),(2,'Ricardo Lemos','ricardo.lemos@spmedicalgroup.com.br','888'),(2,'Roberto Possarle','roberto.possarle@spmedicalgroup.com.br','999'),
(2,'Helena Strada','helena.souza@spmedicalgroup.com.br','101010'),(1,'Lucas ADM','lucasadm@email.com.br','111111')
GO

INSERT INTO situacaoConsulta(situacaoConsulta)
VALUES ('AGENDADA'),('REALIZADA'),('CANCELADA')
GO

INSERT INTO especializacao (nomeEspecializacao)
VALUES 
('Acupuntura'), ('Anestesiologia'), ('Angiologia'), ('Cardiologia'), ('Cirurgia Cardiovascular'),
('Cirurgia da Mão'), ('Cirurgia do Aparelho Digestivo'), ('Cirurgia Geral'), ('Cirurgia Pediátrica'), 
('Cirurgia Plástica'), ('Cirurgia Torácica'), ('Cirurgia Vascular'), ('Dermatologia'), ('Radioterapia'), 
('Urologia'), ('Pediatria'), ('Psiquiatria');
GO

INSERT INTO instituicao(endereco, nomeFantasia, razaoSocial, cnpj)
VALUES ('Av. Barão Limeira, 532, São Paulo, SP','Clinica Possarle','SP Medical Group','86.400.902/0001-30')
GO

INSERT INTO medico(idUsuario,idInstituicao,idEspecializacao,nomeMedico,crm)
VALUES ('8','1','2','Ricardo Lemos', '54356-SP'),('9','1','17','Roberto Possarle', '54365-SP'),('10','1','16','Helena Strada', '65463-SP')
GO

INSERT INTO paciente (idUsuario, dataNascimento, telefone, rg, cpf,endereco)
VALUES 
(1, '10/12/1983', '11 3456-7654', '43522543-5', '94839859000', 'Rua Estado de Israel 240, São Paulo, Estado de São Paulo, 04022-000'), 
(6, '07/11/2001', '11 98765-6543', '32654345-7', '73556944057', 'Av. Paulista, 1578 - Bela Vista, São Paulo - SP, 01310-200'), 
(7, '10/10/1978', '11 97208-4453', '54636525-3', '16839338002', 'Av. Ibirapuera - Indianópolis, 2927,  São Paulo - SP, 04029-200'), 
(8, '10/12/1985', '11 3456-6543', '54366362-5', '14332654765', 'Rua Vitória, 120 - Vila Sao Jorge, Barueri - SP, 06402-030'), 
(9, '08/07/1975', '11 7656-6377', '53254444-1', '91305348010', 'Rua Ver. Geraldo de Camargo, 66 - Santa Luzia, Ribeirão Pires - SP, 09405-380'), 
(10, '03/09/1972', '11 95436-8769', '54566266-7', '79799299004', 'Rua Alameda dos Arapanés, 945 - Indianópolis, São Paulo - SP, 04524-001'), 
(11, '03/05/2018', NULL, '54566266-8', '13771913039', 'Rua Sao Antonio, 232 - Vila Universal, Barueri - SP, 06407-140')
GO

INSERT INTO consulta (idPaciente,idMedico,idSituacaoConsulta,dataConsulta,descricao)
VALUES
(7, 3, 2, '01/12/20 15:00', 'paciente ok'),
(2, 1, 2, '19/06/2021 10:00', NULL), 
(3, 1, 2, '07/02/2021 11:00', 'paciente ok'), 
(2, 2, 2, '24/08/2021 11:00', 'paciente ok'), 
(4, 3, 3, '02/07/2019 11:00', NULL), 
(7, 3, 1, '03/10/2020 21:00', NULL), 
(4, 2, 1, '03/09/2020 11:00', NULL);
GO







