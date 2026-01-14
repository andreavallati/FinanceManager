using AutoMapper;
using FinanceManager.Shared.Application.Requests;
using FinanceManager.Shared.Application.Responses;
using FinanceManager.WPF.Application.Interfaces.Connectors;
using FinanceManager.WPF.Application.Interfaces.Services;
using FinanceManager.WPF.Extensions;

namespace FinanceManager.WPF.Application.Services
{
    public class AuthUIService : IAuthUIService
    {
        private readonly IRestConnector _restConnector;
        private readonly IMapper _mapper;

        public AuthUIService(IRestConnector restConnector, IMapper mapper)
        {
            _restConnector = restConnector ?? throw new ArgumentNullException(nameof(restConnector));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponseItem<TokenResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var apiCall = async () => await _restConnector.PostModelAsync<TokenResponse, LoginRequest>("api/auth/login", loginRequest);
            var apiResponse = await apiCall.ProcessItemResponse<TokenResponse, TokenResponse>(_mapper);
            return apiResponse;
        }
    }
}