using ShoppingList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Domain.AggregatesModel
{
    public interface IShoppingListRepository : IRepository<ShoppingList>
    {
        ShoppingList Add(ShoppingList shoppingList);

        void Update(ShoppingList shoppingList);

        Task<ShoppingList> GetAsync(int shoppingListId);
        void Remove(int shoppingListId);
    }
}
