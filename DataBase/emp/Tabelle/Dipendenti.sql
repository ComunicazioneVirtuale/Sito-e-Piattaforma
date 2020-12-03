CREATE TABLE [dbo].[dipendenti]
(
	[uniq_id_dipendente] UNIQUEIDENTIFIER NOT NULL,
	[uniq_id_utente] UNIQUEIDENTIFIER NOT NULL, 
    [n_id_ruolo] INT NOT NULL, 
    CONSTRAINT [PK_Dipendenti] PRIMARY KEY ([uniq_id_dipendente]) 
)
