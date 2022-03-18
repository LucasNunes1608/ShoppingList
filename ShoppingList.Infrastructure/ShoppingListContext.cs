using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.SeedWork;
using ShoppingList.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Infrastructure
{
    public class ShoppingListContext : DbContext, IUnitOfWork
    {
        public DbSet<Domain.AggregatesModel.ShoppingList> ShoppingLists { get; set; }
        private readonly IMediator _mediator;

        public ShoppingListContext(DbContextOptions<ShoppingListContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainsEventAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShoppingListEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingListItemEntityTypeConfiguration());
        }
    }
}
