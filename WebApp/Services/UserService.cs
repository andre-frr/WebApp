using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;
using WebApp.Helpers;
using WebApp.Models.DTOs;

namespace WebApp.Services
{
    public class UserService
    {
        private readonly MyOptions _myOptions;
        public UserService(IOptions<MyOptions> myOptions)
        {
            _myOptions = myOptions.Value;
        }

        public ExecutionResult<utilizadorDTO> Insert(utilizadorDTO dto)
        {
            int result;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@nome", dto.nome, DbType.String, ParameterDirection.Input);
            parameters.Add("@apelido", dto.apelido, DbType.String, ParameterDirection.Input);
            parameters.Add("@email", dto.email, DbType.String, ParameterDirection.Input);
            parameters.Add("@pass", dto.pass, DbType.String, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                result = conn.Execute(Constants.sp_utilizador_insert, parameters, commandType: CommandType.StoredProcedure);
            }

            return new ExecutionResultFactory<utilizadorDTO>().GetSuccessExecutionResult(dto, string.Empty);
        }
        public ExecutionResult<List<utilizadorDTO>> Get(string email, string pass)
        {
            List<utilizadorDTO> listc = new List<utilizadorDTO>();

            DynamicParameters parameters = new DynamicParameters();
            if (!string.IsNullOrEmpty(email))
            {
                parameters.Add("@email", email, DbType.String, ParameterDirection.Input);
            }
            if (!string.IsNullOrEmpty(pass))
            {
                parameters.Add("@pass", pass, DbType.String, ParameterDirection.Input);
            }

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                listc = conn.Query<utilizadorDTO>(Constants.sp_utilizador_get, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return new ExecutionResultFactory<List<utilizadorDTO>>().GetSuccessExecutionResult(listc, listc.Any() ? string.Empty : "No data found");
        }
    }
}
