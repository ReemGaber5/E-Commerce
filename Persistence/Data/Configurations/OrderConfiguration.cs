using Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(D => D.Subtotal).HasColumnType("decimal(8,2)");

            builder.HasMany(d => d.Items).WithOne();

            builder.HasOne(d=>d.DeliveryMethod).WithMany().HasForeignKey(d=>d.DeliveryMethodId);

            //one address to one order (one to one )
            builder.OwnsOne(a => a.Address);


        }
    }
}
