using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using WebApp.Helpers;
using WebApp.Models.DTOs;

namespace WebApp.Services
{
    public class HomeService
    {
        private readonly MyOptions _myOptions;
        public HomeService(IOptions<MyOptions> myOptions)
        {
            _myOptions = myOptions.Value;
        }
        public ExecutionResult<eventoDTO> Get(int id)
        {
            eventoDTO evento = null;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id, DbType.Int32, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                evento = conn.QueryFirstOrDefault<eventoDTO>(Constants.sp_evento_get,parameters,commandType: CommandType.StoredProcedure);
            }
             return new ExecutionResultFactory<utilizadorDTO>().GetSuccessExecutionResult(id, string.Empty);
        }
    }
}

