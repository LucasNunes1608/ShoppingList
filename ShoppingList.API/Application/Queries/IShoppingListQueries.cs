namespace ShoppingList.API.Application.Queries
{
    public interface IShoppingListQueries
    {
        public Task<ShoppingList> GetShoppingListAsync(int id);
    }
}
