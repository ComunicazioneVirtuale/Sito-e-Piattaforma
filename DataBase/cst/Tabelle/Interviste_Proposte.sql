CREATE TABLE [cst].[interviste_proposte]
(
	[uniq_id_cliente] UNIQUEIDENTIFIER NOT NULL, 
    [n_sito] BIGINT NULL, 
    [n_erp] BIGINT NULL, 
    [n_campagne_social] BIGINT NULL, 
    [n_ecommerce] BIGINT NULL, 
    [c_note] VARCHAR(8000) NULL 
)
