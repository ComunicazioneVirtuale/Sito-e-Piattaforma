CREATE TABLE [prj].[ordine_prodotto]
(
	[uniq_id_ordine] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [uniq_id_cliente] UNIQUEIDENTIFIER NOT NULL, 
    [n_id_prodotto] INT NOT NULL, 
    [n_costo] DECIMAL(18, 2) NOT NULL, 
    [n_sconto] DECIMAL(5, 2) NULL, 
    [d_attivazione] DATETIME NULL DEFAULT getdate(), 
    [d_scadenza] DATETIME NULL
)
