using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Domain.SeedWork
{
    public interface IRepository <T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
