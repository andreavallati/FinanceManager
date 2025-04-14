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

        public async Task<ApiResponseItem<User>> InsertAsync(User user)
        {
            var model = _mapper.Map<UserDto>(user);
            var apiCall = async () => await _restConnector.PostModelAsync<UserDto, UserDto>("api/users", model);
            var apiResponse = await apiCall.ProcessItemResponse<User, UserDto>(_mapper);
            return apiResponse;
        }
    }
}
