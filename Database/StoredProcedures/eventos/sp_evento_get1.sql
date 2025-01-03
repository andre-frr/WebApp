CREATE PROCEDURE [dbo].[sp_evento_get1]
	    @Titulo NVARCHAR(255)
AS
BEGIN
    SELECT * FROM Evento WHERE Titulo = @Titulo;
END;