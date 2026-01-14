using AutoMapper;
using FinanceManager.Shared.Application.Dtos;
using FinanceManager.Shared.Application.Responses;
using FinanceManager.WPF.Application.Interfaces.Connectors;
using FinanceManager.WPF.Application.Interfaces.Services;
using FinanceManager.WPF.Domain.Models;
using FinanceManager.WPF.Extensions;

namespace FinanceManager.WPF.Application.Services
{
    public class UserUIService : IUserUIService
    {
        private readonly IRestConnector _restConnector;
        private readonly IMapper _mapper;

        public UserUIService(IRestConnector restConnector, IMapper mapper)
        {
            _restConnector = restConnector ?? throw new ArgumentNullException(nameof(restConnector));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponseItems<User>> GetAllAsync()
        {
            var apiCall = async () => await _restConnector.GetModelsAsync<UserDto>("api/users");
            var apiResponse = await apiCall.ProcessItemsResponse<User, UserDto>(_mapper);
            return apiResponse;
        }

        public async Task<ApiResponseItem<User>> RegisterAsync(User user)
        {
            var model = _mapper.Map<UserDto>(user);
            var apiCall = async () => await _restConnector.PostModelAsync<UserDto, UserDto>("api/users/register", model);
            var apiResponse = await apiCall.ProcessItemResponse<User, UserDto>(_mapper);
            return apiResponse;
        }
    }
}
