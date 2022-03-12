using ShoppingList.API.Application.Commands;

namespace ShoppingList.API.Models
{
    public class ShoppingListItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public bool IsCompleted { get; set; }
        
    }
}
