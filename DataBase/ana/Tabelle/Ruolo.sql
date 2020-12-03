CREATE TABLE [ana].[ruolo]
(
	[n_id_ruolo] INT NOT NULL , 
    [descrizione] VARCHAR(50) NOT NULL, 
    [n_pan_cliente] INT NULL DEFAULT 1, 
    [n_pan_commerciale] INT NULL, 
    [n_pan_admin] INT NULL, 
    [n_pan_sql] INT NULL, 
    CONSTRAINT [PK_Ruolo] PRIMARY KEY ([n_id_ruolo])
)
