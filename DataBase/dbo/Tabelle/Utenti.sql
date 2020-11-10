CREATE TABLE [dbo].[Utenti]
(
	[uniq_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY NONCLUSTERED, 
    [c_email] VARCHAR(50) NULL, 
    [c_password] VARCHAR(50) NULL, 
    [c_half_password] VARCHAR(50) NOT NULL, 
    [d_registrazione] DATETIME NOT NULL, 
    [n_id_ruolo] INT NULL
)