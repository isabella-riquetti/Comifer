using Comifer.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Mapping
{
    public class ProductParentMap : EntityTypeConfiguration<ProductParent>
    {
        public ProductParentMap()
        {
            ToTable("ProductParent");

            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.CategoryId).HasColumnName("CategoryId");
            Property(t => t.BrandId).HasColumnName("BrandId").IsRequired();
            Property(t => t.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            Property(t => t.Code).HasColumnName("Code").HasMaxLength(255).IsRequired();

            HasOptional(t => t.Category)
                .WithMany(t => t.ProductParents)
                .HasForeignKey(d => d.CategoryId)
                .WillCascadeOnDelete(false);
            HasRequired(t => t.Brand)
                .WithMany(t => t.ProductParents)
                .HasForeignKey(d => d.BrandId)
                .WillCascadeOnDelete(false);
        }
    }
}
