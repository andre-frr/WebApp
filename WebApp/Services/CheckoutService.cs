using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using WebApp.Helpers;
using WebApp.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace WebApp.Services
{
    public class CheckoutService
    {
        private readonly MyOptions _myOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutService(IOptions<MyOptions> myOptions, IHttpContextAccessor httpContextAccessor)
        {
            _myOptions = myOptions.Value;
            _httpContextAccessor = httpContextAccessor;
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
                if (userID == null)
                {
                    var session = _httpContextAccessor.HttpContext?.Session;
                    if (session != null)
                    {
                        session.SetInt32("GuestCartID", carrinho);
                    }
                }

                return new ExecutionResultFactory<carrinhoDTO>().GetSuccessExecutionResult(
                    new carrinhoDTO { userID = userID ?? 0 },
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
        public ExecutionResult<carrinhoDTO> AssociarCarrinho(int userID)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            int? guestCart = session?.GetInt32("GuestCartID");

            if (guestCart == null)
            {
                return new ExecutionResult<carrinhoDTO>()
                {
                    status = false,
                    message = new List<string> { "Guest Cart não encontrado" }
                };
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@cartID", guestCart, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@userID", userID, DbType.Int32, ParameterDirection.Input);

            using (IDbConnection conn = new SqlConnection(_myOptions.ConnString))
            {
                int rowsAffected = conn.Execute(Constants.sp_carrinho_update, parameters, commandType: CommandType.StoredProcedure);

                if (rowsAffected > 0)
                {
                    session.Remove("GuestCartID");

                    return new ExecutionResult<carrinhoDTO>()
                    {
                        status = true,
                        message = new List<string> { "Guest Cart associado com sucesso." }
                    };
                }
                else
                {
                    return new ExecutionResult<carrinhoDTO>()
                    {
                        status = false,
                        message = new List<string> { "Falha ao associar carrinho." }
                    };
                }
            }
        }
    }
}
