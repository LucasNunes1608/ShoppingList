using MediatR;
using ShoppingList.API.Extensions;
using ShoppingList.Domain.AggregatesModel;

namespace ShoppingList.API.Application.Commands
{
    public class UpdateShoppingListCommandHandler : IRequestHandler<UpdateShoppingListCommand, bool>
    {
        private readonly IShoppingListRepository _shoppingListRepository;

        public UpdateShoppingListCommandHandler(IShoppingListRepository shoppingListRepository)
        {
            _shoppingListRepository = shoppingListRepository ?? throw new ArgumentNullException(nameof(shoppingListRepository));
        }

        public async Task<bool> Handle(UpdateShoppingListCommand request, CancellationToken cancellationToken)
        {
            var listToUpdate = await _shoppingListRepository.GetAsync(request.Id);
            if (listToUpdate == null)
                return false;

            listToUpdate.SetTitle(request.Title);
            listToUpdate.SetShoppingList(request.ShoppingListItems.ToShoppingListItems());

            _shoppingListRepository.Update(listToUpdate);
            return await _shoppingListRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
