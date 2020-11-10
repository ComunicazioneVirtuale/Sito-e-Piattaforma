CREATE TABLE [cst].[Cliente_Orari]
(
	[uniq_id_cliente] UNIQUEIDENTIFIER NOT NULL, 
    [c_giorno] VARCHAR(50) NOT NULL, 
    [d_apertura] TIME NULL, 
    [d_chiusura] TIME NULL, 
    [d_pausa] TIME NULL 
)
