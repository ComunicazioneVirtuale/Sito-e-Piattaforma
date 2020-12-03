CREATE TABLE [cst].[interviste]
(
	[uniq_id_cliente] UNIQUEIDENTIFIER NOT NULL, 
    [n_sito] INT NOT NULL, 
    [c_url_sito] VARCHAR(50) NULL, 
    [n_erp] INT NOT NULL, 
    [c_url_erp] VARCHAR(50) NULL, 
    [n_campagne_social] INT NOT NULL, 
    [n_ecommerce] INT NOT NULL, 
    [c_note] VARCHAR(8000) NULL 
)
