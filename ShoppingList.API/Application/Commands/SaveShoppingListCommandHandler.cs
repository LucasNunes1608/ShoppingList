using MediatR;
using ShoppingList.Domain.AggregatesModel;

namespace ShoppingList.API.Application.Commands
{
    public class SaveShoppingListCommandHandler : IRequestHandler<SaveShoppingListCommand, bool>
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMediator _mediator;

        public SaveShoppingListCommandHandler(IShoppingListRepository shoppingListRepository, IMediator mediator)
        {
            _shoppingListRepository = shoppingListRepository ?? throw new ArgumentNullException(nameof(shoppingListRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<bool> Handle(SaveShoppingListCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = new Domain.AggregatesModel.ShoppingList(request.Title);

            foreach (var item in request.ShoppingListItems)
            {
                shoppingList.AddItem(item.Description, item.Quantity, item.isCompleted);
            }

            _shoppingListRepository.Add(shoppingList);

            return _shoppingListRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
