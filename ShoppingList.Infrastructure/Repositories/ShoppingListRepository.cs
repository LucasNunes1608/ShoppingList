﻿using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.AggregatesModel;
using ShoppingList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Infrastructure.Repositories
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly ShoppingListContext _context;

        public ShoppingListRepository(ShoppingListContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Domain.AggregatesModel.ShoppingList Add(Domain.AggregatesModel.ShoppingList shoppingList)
        {
            return _context.ShoppingLists.Add(shoppingList).Entity;
        }

        public async Task<Domain.AggregatesModel.ShoppingList> GetAsync(int shoppingListId)
        {
            var shoppingList = await _context
                                        .ShoppingLists
                                        .FirstOrDefaultAsync(sl => sl.Id == shoppingListId);
            if (shoppingList == null)
            {
                shoppingList = _context
                                .ShoppingLists
                                .Local
                                .FirstOrDefault(sl => sl.Id == shoppingListId);
            }
            if(shoppingList != null)
            {
                await _context
                        .Entry(shoppingList)
                        .Collection(i => i.ShoppingListItems).LoadAsync();
            }

            return shoppingList;
        }

        public void Remove(Domain.AggregatesModel.ShoppingList shoppingList)
        {
            foreach (var item in shoppingList.ShoppingListItems)
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            _context.Entry(shoppingList).State = EntityState.Deleted;
        }

        public void Update(Domain.AggregatesModel.ShoppingList shoppingList)
        {
                var query = from i in _context.Entry(shoppingList).Entity.ShoppingListItems
                            where !(from ni in shoppingList.ShoppingListItems
                                    select ni.Description)
                                    .Contains(i.Description)
                            select i;
            foreach (var item in query.ToList())
            {
                _context.Entry(item).State = EntityState.Deleted;
            }
            _context.Entry(shoppingList).State = EntityState.Modified;
        }
    }
}
