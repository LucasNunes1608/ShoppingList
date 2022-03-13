using MediatR;
using ShoppingList.Domain.AggregatesModel;

namespace ShoppingList.API.Application.Commands
{
    public class RemoveShoppingListCommandHandler : IRequestHandler<RemoveShoppingListCommand, bool>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public RemoveShoppingListCommandHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository ?? throw new ArgumentNullException(nameof(shoppingListRepository));
        }

        public async Task<bool> Handle(RemoveShoppingListCommand request, CancellationToken cancellationToken)
        {
            var listToRemove = await _shoppingListRepository.GetAsync(request.Id);
            if (listToRemove == null)
            {
                return false;
            }

            _shoppingListRepository.Remove(listToRemove);

            return await _shoppingListRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
