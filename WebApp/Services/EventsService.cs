using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using WebApp.Helpers;
using WebApp.Models.DTOs;

namespace WebApp.Services
{
    public class EventsService
    {
        private readonly MyOptions _myOptions;

        public EventsService(IOptions<MyOptions> myOptions)
        {
            _myOptions = myOptions.Value;
        }
        public async Task<List<eventoDTO>> GetEventosAsync()
        {
            using var connection = new SqlConnection(_myOptions.ConnString);
            var eventos = await connection.QueryAsync<eventoDTO>("sp_evento_get", commandType: CommandType.StoredProcedure);
            return eventos.ToList();
        }
        public async Task<eventoDTO> GetEventoByTituloAsync(string titulo)
        {
            var query = "SELECT * FROM Evento WHERE Titulo = @Titulo";
            using var connection = new SqlConnection(_myOptions.ConnString);
            var evento = await connection.QueryFirstOrDefaultAsync<eventoDTO>(query, new { Titulo = titulo });
            return evento;
        }
    }
}