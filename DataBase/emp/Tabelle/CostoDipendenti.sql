CREATE TABLE [dbo].[costo_dipendenti]
(
	[uniq_id_dipendente] UNIQUEIDENTIFIER NOT NULL, 
    [RAL] NUMERIC(18,3) NOT NULL, 
    [p_contributi] DECIMAL(5,2) NOT NULL, 
    [n_stipendio] NUMERIC(18,3) NULL, 
    [n_costo_orario] DECIMAL(5,2) NULL 
)
