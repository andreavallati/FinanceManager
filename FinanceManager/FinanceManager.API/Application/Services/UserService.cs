using AutoMapper;
using FinanceManager.API.Application.Interfaces.Repositories;
using FinanceManager.API.Application.Interfaces.Services;
using FinanceManager.API.Domain.Entities;
using FinanceManager.Shared.Application.Dtos;
using FinanceManager.Shared.Exceptions;
using FluentValidation;

namespace FinanceManager.API.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IValidator<User> _validator;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IValidator<User> validator,
                           IUserRepository repository,
                           IMapper mapper)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(entities);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> GetByLoginInfoAsync(string email, string password)
        {
            var entity = await _repository.GetByLoginInfoAsync(email, password);
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> InsertAsync(UserDto user)
        {
            var entity = _mapper.Map<User>(user);

            var validationResult = _validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var insertResult = await _repository.InsertAsync(entity);
            await _repository.CommitChangesAsync();
            return _mapper.Map<UserDto>(insertResult);
        }
    }
}