using MediatR;

namespace ShoppingList.API.Application.Commands
{
    public class UpdateShoppingListCommand : IRequest<bool>
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<UpdateShoppingListItemDTO> ShoppingListItems { get; private set; }
        public UpdateShoppingListCommand(int id, string title, IEnumerable<UpdateShoppingListItemDTO> shoppingListItems)
        {
            Id = id;
            Title = title;
            ShoppingListItems = shoppingListItems;
        }
        public record UpdateShoppingListItemDTO
        {
            public string Description { get; init; }
            public int Quantity { get; init; }
            public bool IsCompleted { get; init; }
        }
    }
    
}
