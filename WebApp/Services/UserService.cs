using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
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

            return new ExecutionResultFactory<utilizadorDTO>().GetSuccessExecutionResult(dto, result > 0 ? string.Empty : "Error inserting the user.");
        }

        public ExecutionResult<List<utilizadorDTO>> Get(string email, string pass)
        {
            List<utilizadorDTO> listc = new List<utilizadorDTO>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userID", null, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@email", string.IsNullOrEmpty(email) ? null : email, DbType.String, ParameterDirection.Input);
            parameters.Add("@pass", string.IsNullOrEmpty(pass) ? null : pass, DbType.String, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                listc = conn.Query<utilizadorDTO>(Constants.sp_utilizador_get, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return new ExecutionResultFactory<List<utilizadorDTO>>().GetSuccessExecutionResult(listc, listc.Any() ? string.Empty : "No data found.");
        }

        public utilizadorDTO GetUserById(int userID)
        {
            utilizadorDTO user = null;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userID", userID, DbType.Int32, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                user = conn.QueryFirstOrDefault<utilizadorDTO>(Constants.sp_utilizador_get, parameters, commandType: CommandType.StoredProcedure);
            }

            return user;
        }

        public ExecutionResult<utilizadorDTO> UpdateUser(utilizadorDTO dto)
        {
            int result;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userID", dto.userID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@nome", dto.nome, DbType.String, ParameterDirection.Input);
            parameters.Add("@apelido", dto.apelido, DbType.String, ParameterDirection.Input);
            parameters.Add("@email", dto.email, DbType.String, ParameterDirection.Input);
            parameters.Add("@dt_nascimento", dto.dt_nascimento, DbType.Date, ParameterDirection.Input);
            parameters.Add("@morada", dto.morada, DbType.String, ParameterDirection.Input);
            parameters.Add("@nif", dto.nif, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@cidade", dto.cidade, DbType.String, ParameterDirection.Input);
            parameters.Add("@cod_postal", dto.cod_postal, DbType.String, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                result = conn.Execute(Constants.sp_utilizador_update, parameters, commandType: CommandType.StoredProcedure);
            }

            return new ExecutionResultFactory<utilizadorDTO>().GetSuccessExecutionResult(dto, result > 0 ? string.Empty : "Error updating the user.");
        }

        public ExecutionResult<bool> UpdatePassword(int userID, string newPass)
        {
            int result;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userID", userID, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@newPassword", newPass, DbType.String, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                result = conn.Execute(Constants.sp_utilizador_update, parameters, commandType: CommandType.StoredProcedure);
            }

            return new ExecutionResultFactory<bool>().GetSuccessExecutionResult(result > 0, result > 0 ? "Password updated successfully." : "Error updating the password.");
        }
    }
}
