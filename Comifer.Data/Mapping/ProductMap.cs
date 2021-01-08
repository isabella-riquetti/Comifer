using Comifer.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Product");

            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.BrandId).HasColumnName("BrandId").IsRequired();
            Property(t => t.ProductParentId).HasColumnName("ProductParentId");
            Property(t => t.ProductGroupId).HasColumnName("ProductGroupId");
            Property(t => t.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            Property(t => t.Code).HasColumnName("Code").HasMaxLength(255).IsRequired();
            Property(t => t.Supply).HasColumnName("Supply").IsRequired();
            Property(t => t.Cost).HasColumnName("Cost");
            Property(t => t.Price).HasColumnName("Price");
            Property(t => t.Weight).HasColumnName("Weight");

            HasRequired(t => t.Brand)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.BrandId)
                .WillCascadeOnDelete(false);
            HasOptional(t => t.ProductParent)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.ProductParentId)
                .WillCascadeOnDelete(false);
        }
    }
}
