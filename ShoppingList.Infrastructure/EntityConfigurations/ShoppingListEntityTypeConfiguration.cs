using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingList.Domain.AggregatesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Infrastructure.EntityConfigurations
{
    public class ShoppingListEntityTypeConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.ShoppingList>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.ShoppingList> builder)
        {
            builder.ToTable("ShoppingLists");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired();

            builder.HasMany(sl => sl.ShoppingListItems)
                .WithOne()                
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
