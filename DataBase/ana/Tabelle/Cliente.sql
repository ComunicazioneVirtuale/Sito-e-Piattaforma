CREATE TABLE [ana].[Cliente]
(
	[uniq_id_cliente] UNIQUEIDENTIFIER NOT NULL, 
    [c_ragione_sociale] VARCHAR(50) NOT NULL, 
    [c_piva] VARCHAR(17) NOT NULL, 
    [n_id_settore] INT NULL, 
    [n_dipendenti] INT NULL, 
    [c_SDI] VARCHAR(50) NOT NULL, 
    [n_telefono] NUMERIC(10) NOT NULL, 
    [n_id_indirizzo] BIGINT NOT NULL, 
    [c_pec] VARCHAR(50) NULL, 
    [c_email] VARCHAR(50) NULL, 
    [n_capitalesociale] NUMERIC NULL, 
    [n_fatturato] NUMERIC NULL
)
