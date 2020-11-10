CREATE TABLE [prj].[Ordini]
(
	[uniq_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [uniq_id_cliente] UNIQUEIDENTIFIER NOT NULL, 
    [n_totale] DECIMAL(18, 2) NOT NULL, 
    [d_ordine] DATETIME NOT NULL DEFAULT getdate()
)
