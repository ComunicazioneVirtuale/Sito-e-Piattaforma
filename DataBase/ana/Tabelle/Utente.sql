CREATE TABLE [ana].[Utente]
(
	[uniq_id_utente] UNIQUEIDENTIFIER NOT NULL, 
    [c_codice_fiscale] VARCHAR(15) NOT NULL, 
    [c_nome] VARCHAR(50) NULL, 
    [c_cognome] VARCHAR(50) NULL, 
    [d_nascita] DATE NULL, 
    [id_residenza] BIGINT NULL, 
    [id_domicilio] BIGINT NULL, 
    [c_carta_idenità] VARCHAR(20) NULL, 
    [c_patente] VARCHAR(20) NULL, 
    [n_telefono] NUMERIC(10) NULL, 
    [c_iban] VARCHAR(27) NULL
) 