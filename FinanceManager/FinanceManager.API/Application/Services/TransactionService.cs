using AutoMapper;
using FinanceManager.API.Application.Interfaces.Repositories;
using FinanceManager.API.Application.Interfaces.Services;
using FinanceManager.Shared.Application.Dtos;

namespace FinanceManager.API.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository repository,
                                  IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TransactionDto>> GetByUserIdAsync(long userId)
        {
            var entities = await _repository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TransactionDto>>(entities);
        }
    }
}
