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

        public async Task<eventoDTO> GetEventoByTituloAsync(string titulo)
        {
            using var connection = new SqlConnection(_myOptions.ConnString);

            var evento = await connection.QueryFirstOrDefaultAsync<eventoDTO>(Constants.sp_evento_get1,new { Titulo = titulo },commandType: CommandType.StoredProcedure);

            return evento;
        }
    }
}