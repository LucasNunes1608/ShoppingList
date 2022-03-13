using MediatR;

namespace ShoppingList.API.Application.Commands
{
    public class RemoveShoppingListCommand : IRequest<bool>
    {
        public int Id { get; private set; }

        public RemoveShoppingListCommand(int id)
        {
            Id = id;
        }
    }
}
