﻿using ShoppingList.Domain.SeedWork;
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

        public void AddItem(string Description, int Quantity)
        {
            var existingItem = _shoppingListItems.FirstOrDefault(x => x.Description == Description);
            if (existingItem != null)
                existingItem.AddUnits(Quantity);
            else
            {
                var newItem = new ShoppingListItem(Description, Quantity);
                _shoppingListItems.Add(newItem);
            }
        }

        public void RemoveItem(string Description, int Quantity)
        {
            var existingItem = _shoppingListItems.FirstOrDefault(x => x.Description == Description);
            if (existingItem == null)
                throw new ArgumentException("Unable to remove Item. Item not found on Shopping List");

            existingItem.RemoveUnits(Quantity);
        }
    }
}
