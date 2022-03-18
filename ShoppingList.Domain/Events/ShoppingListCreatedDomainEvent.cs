using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Domain.Events
{
    public class ShoppingListCreatedDomainEvent : INotification
    {
        public AggregatesModel.ShoppingList ShoppingList { get; private set; }
        public ShoppingListCreatedDomainEvent(AggregatesModel.ShoppingList shoppingList)
        {
            ShoppingList = shoppingList;
        }
    }
}
