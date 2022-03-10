using ShoppingList.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Domain.AggregatesModel
{
    public class ShoppingListItem : Entity
    {
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public bool IsCompleted { get; private set; }

        public ShoppingListItem(string Description, int Quantity)
        {
            if (Quantity < 0)
                throw new ArgumentOutOfRangeException("Quantity", "Quantity cannot be less than 0");

            this.Description = Description;
            this.Quantity = Quantity;
            IsCompleted = false;
        }

        public void SetDescription(string Description)
        {
            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Description cannot be Null, Empty or WhiteSpace", "Description");

            this.Description = Description.Trim();
        }

        public void AddUnits(int Quantity)
        {
            if (Quantity < 0)
                throw new ArgumentOutOfRangeException("Quantity", "Quantity cannot be less than 0");

            this.Quantity += Quantity;
        }

        public void RemoveUnits(int Quantity)
        {
            if(this.Quantity - Quantity <= 0)
                    throw new ArgumentOutOfRangeException("Quantity", "Cannot remove more units than the current unit quantity");

            this.Quantity -= Quantity;
        }

    }
}
