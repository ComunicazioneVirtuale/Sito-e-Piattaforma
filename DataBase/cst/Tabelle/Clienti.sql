CREATE TABLE [cst].[Clienti]
(
	[uniq_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [uniq_id_commerciale] UNIQUEIDENTIFIER NOT NULL, 
    [uniq_id_responsabile] UNIQUEIDENTIFIER NOT NULL, 
    [d_registrazione] DATETIME NOT NULL DEFAULT getdate(), 
    [d_registrazione_effettiva] DATETIME NULL
)
