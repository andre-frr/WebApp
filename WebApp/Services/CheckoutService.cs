using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using WebApp.Helpers;
using WebApp.Models.DTOs;

namespace WebApp.Services
{
    public class CheckoutService
    {
        private readonly MyOptions _myOptions;

        public CheckoutService(IOptions<MyOptions> myOptions)
        {
            _myOptions = myOptions.Value;
        }

        public ExecutionResult<carrinhoDTO> AbrirCarrinho(int? userID)
        {
            int carrinho;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@userID", userID, DbType.Int32, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {

                carrinho = conn.QuerySingleOrDefault<int>(Constants.sp_carrinho_create, parameters, commandType: CommandType.StoredProcedure);
            }

            if (carrinho > 0)
            {

                return new ExecutionResultFactory<carrinhoDTO>().GetSuccessExecutionResult(
                    new carrinhoDTO { userID = userID ?? 0},
                    "Carrinho criado com sucesso."
                );
            }
            else
            {
                return new ExecutionResult<carrinhoDTO>()
                {
                    status = false,
                    message = new List<string> { "Erro ao criar o carrinho." }
                };
            }
        }
    }
}
