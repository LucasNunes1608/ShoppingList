using ShoppingList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Domain.AggregatesModel
{
    public class ShoppingList : Entity, IAggregateRoot
    {
        private readonly List<ShoppingListItem> _shoppingListItems;
        public IReadOnlyCollection<ShoppingListItem> ShoppingListItems => _shoppingListItems;

        public string Title { get; private set; }

        public ShoppingList(string title)
        {
            _shoppingListItems = new List<ShoppingListItem>();
            Title = title;
        }

        public void AddItem(string Description, int Quantity, bool isCompleted)
        {
            var existingItem = _shoppingListItems.FirstOrDefault(x => x.Description == Description);
            if (existingItem != null)
                throw new ArgumentException("Unable to add Item. Item already exists.");
            else
            {
                var newItem = new ShoppingListItem(Description, Quantity, isCompleted);
                _shoppingListItems.Add(newItem);
            }
        }

        public void UpdateItem(string Description, int Quantity, bool isCompleted)
        {
            var existingItem = _shoppingListItems.FirstOrDefault(x => x.Description == Description);
            if (existingItem == null)
                throw new ArgumentException("Unable to update Item. Item not found on Shopping List");

            existingItem.SetUnits(Quantity);
            existingItem.SetDescription(Description);
            existingItem.SetCompletion(isCompleted);
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetShoppingList(IEnumerable<ShoppingListItem> shoppingListItems)
        {
            foreach (var item in shoppingListItems)
            {
                if (_shoppingListItems.Any(i => i.Description == item.Description))
                {
                    UpdateItem(item.Description, item.Quantity, item.IsCompleted);
                }
                else
                {
                    AddItem(item.Description, item.Quantity, item.IsCompleted);
                }
            }

            var query = from i in _shoppingListItems
                        where !(from ni in shoppingListItems
                                select ni.Description)
                                .Contains(i.Description)
                        select i;
            foreach (var item in query.ToList())
            {
                _shoppingListItems.Remove(item);
            }
        }
    }
}
