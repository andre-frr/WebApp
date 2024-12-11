using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WebApp.Helpers;
using WebApp.Models.DTOs;
using WebApp.Models.ViewModels.Home;

namespace WebApp.Services
{
    public class HomeService
    {
        private readonly MyOptions _myOptions;

        public HomeService(IOptions<MyOptions> myOptions)
        {
            _myOptions = myOptions.Value;
        }

        public List<HomeEventViewModel> GetEventos()
        {
            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                var eventos = conn.Query<eventoDTO>(Constants.sp_evento_get,commandType: CommandType.StoredProcedure).ToList();

                return eventos.Select(e => new HomeEventViewModel
                {
                    titulo = e.titulo,
                    imagem = e.imagem,
                    data_hora = e.data_hora,
                    local_evento = e.local_evento
                }).ToList();
            }
        }
    }
}