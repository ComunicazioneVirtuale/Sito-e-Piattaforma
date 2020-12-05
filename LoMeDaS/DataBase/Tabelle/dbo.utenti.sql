CREATE TABLE [dbo].[Utenti]
(
	[id_utente] UNIQUEIDENTIFIER NOT NULL DEFAULT newsequentialid() ,
    [email] VARCHAR(255) NOT NULL, 
    [password] NVARCHAR(200) NOT NULL, 
    [password_salt] NVARCHAR(200) NOT NULL, 
    [data_registrazione] DATETIME NULL, 
    PRIMARY KEY ([id_utente])
)

GO


CREATE UNIQUE INDEX [IX_utenti_email] ON [dbo].[utenti] ([email]);
