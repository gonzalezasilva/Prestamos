using Grpc.Core;
using WebApplicationPrestamos.DataAccess;
using WebApplicationPrestamos.Services;
using Microsoft.Extensions.Hosting;

namespace WebApplicationPrestamos.Protos
{
    public class ReturnLoanService : ReturnLoan.ReturnLoanBase

    {
        private readonly ILogger<ReturnLoanService> _logger;
        private readonly ILoanService _loanService ;
       

        public ReturnLoanService(ILogger<ReturnLoanService> logger)
        {
            _logger = logger;
         
           
        }

        public override async Task<ReturnLoanReply> ReturnLoan(ReturnLoanRequest request,
            ServerCallContext context)

        {
            
            _loanService.Return(request.Id);
            return await Task.FromResult(new ReturnLoanReply
            {    Resultado = "Hello "
            });
        }
    

    }
}
