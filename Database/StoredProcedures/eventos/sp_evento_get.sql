CREATE PROCEDURE [dbo].[sp_evento_get]
AS
BEGIN
    SELECT 
        titulo,
        imagem,
        data_hora,
        local_evento
    FROM Evento
    ORDER BY data_hora ASC
END