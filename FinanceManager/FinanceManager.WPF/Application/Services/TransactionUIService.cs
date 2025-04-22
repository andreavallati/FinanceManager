using AutoMapper;
using FinanceManager.Shared.Application.Dtos;
using FinanceManager.Shared.Application.Responses;
using FinanceManager.WPF.Application.Interfaces.Connectors;
using FinanceManager.WPF.Application.Interfaces.Services;
using FinanceManager.WPF.Domain.Models;
using FinanceManager.WPF.Extensions;

namespace FinanceManager.WPF.Application.Services
{
    public class TransactionUIService : ITransactionUIService
    {
        private readonly IRestConnector _restConnector;
        private readonly IMapper _mapper;

        public TransactionUIService(IRestConnector restConnector, IMapper mapper)
        {
            _restConnector = restConnector ?? throw new ArgumentNullException(nameof(restConnector));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponseItems<Transaction>> GetByUserIdAsync(long userId)
        {
            var apiCall = async () => await _restConnector.GetModelsAsync<TransactionDto>($"api/transactions/{userId}");
            var apiResponse = await apiCall.ProcessItemsResponse<Transaction, TransactionDto>(_mapper);
            return apiResponse;
        }
    }
}
