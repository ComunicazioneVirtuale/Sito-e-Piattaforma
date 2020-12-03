CREATE TABLE [ana].[cliente]
(
	[uniq_id_cliente] UNIQUEIDENTIFIER NOT NULL, 
    [c_ragione_sociale] VARCHAR(50) NOT NULL, 
    [c_piva] VARCHAR(11) NOT NULL, 
    [n_id_settore] INT NULL, 
    [n_dipendenti] INT NULL, 
    [c_SDI] VARCHAR(50) NOT NULL, 
    [n_telefono] NUMERIC(10) NOT NULL, 
    [n_id_indirizzo] BIGINT NOT NULL, 
    [c_pec] VARCHAR(50) NULL, 
    [c_email] VARCHAR(50) NULL, 
    [n_capitalesociale] NUMERIC NULL, 
    [n_fatturato] NUMERIC NULL, 
    [n_id_sede_legale] BIGINT NOT NULL, 
    [n_id_sede_operativa] BIGINT NULL
)
