using Comifer.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Mapping
{
    public class CustomerAddressMap : EntityTypeConfiguration<CustomerAddress>
    {
        public CustomerAddressMap()
        {
            ToTable("CustomerAddress");

            HasKey(t => t.Id);
            Property(t => t.CustomerId).HasColumnName("CustomerId").IsRequired();
            Property(t => t.AddressName).HasColumnName("AddressName").HasMaxLength(255).IsRequired();
            Property(t => t.Number).HasColumnName("Number").IsRequired();
            Property(t => t.Complement).HasColumnName("Complement").HasMaxLength(255);
            Property(t => t.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(255).IsRequired();
            Property(t => t.City).HasColumnName("City").HasMaxLength(255).IsRequired();
            Property(t => t.State).HasColumnName("State").HasMaxLength(255);
            Property(t => t.Country).HasColumnName("Country").HasMaxLength(255);

            HasRequired(t => t.Customer)
                .WithMany(t => t.CustomerAddresses)
                .HasForeignKey(d => d.CustomerId)
                .WillCascadeOnDelete(false);
        }
    }
}
