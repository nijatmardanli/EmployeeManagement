using MediatR;
using System.Transactions;

namespace EM.Application.Behaviours.Transaction
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ITransactionalRequest
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                TResponse response = await next();
                transactionScope.Complete();
                return response;
            }
            catch
            {
                throw;
            }
            finally
            {
                transactionScope.Dispose();
            }
        }
    }
}