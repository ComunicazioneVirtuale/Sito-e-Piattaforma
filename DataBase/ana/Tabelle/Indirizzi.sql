CREATE TABLE [ana].[Indirizzi]
(
	[n_id_luogo] INT NOT NULL PRIMARY KEY, 
    [c_inidirizzo] VARCHAR(100) NULL, 
    [c_comune] VARCHAR(30) NULL, 
    [c_provincia] VARCHAR(2) NULL, 
    [c_cap] VARCHAR(5) NULL, 
    [n_interno] NCHAR(10) NULL, 
    [c_note] VARCHAR(100) NULL 
)
