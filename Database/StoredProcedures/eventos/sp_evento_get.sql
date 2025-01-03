CREATE PROCEDURE [dbo].[sp_evento_get]
AS
BEGIN
    SELECT 
        titulo,
        imagem,
        tipo,
        classificacao,
        data_hora,
        local_evento,
        descricao,
        preco,
        lotacao
    FROM Evento
    ORDER BY data_hora ASC
END