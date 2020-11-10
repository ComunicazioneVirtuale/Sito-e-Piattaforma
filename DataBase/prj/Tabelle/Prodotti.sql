CREATE TABLE [prj].[Prodotti]
(
	[n_id_prodotto] BIGINT NOT NULL PRIMARY KEY, 
    [c_titolo] VARCHAR(50) NULL, 
    [c_descrizione] VARCHAR(200) NULL, 
    [n_id_tipologia] INT NULL, 
    [n_id_settore] INT NULL, 
    [n_costo] NUMERIC(18, 2) NULL
)
