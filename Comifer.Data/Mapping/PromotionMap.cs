using Comifer.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Mapping
{
    public class PromotionMap : EntityTypeConfiguration<Promotion>
    {
        public PromotionMap()
        {
            ToTable("Promotion");

            HasKey(t => t.Id);
            Property(t => t.ProductId).HasColumnName("ProductId").IsRequired();
            Property(t => t.ExpiresOn).HasColumnName("ExpiresOn");
            Property(t => t.Value).HasColumnName("Value");
            Property(t => t.Percentage).HasColumnName("Percentage");
            Property(t => t.Status).HasColumnName("Status").IsRequired();

            HasRequired(t => t.Product)
                .WithMany(t => t.Promotions)
                .HasForeignKey(d => d.ProductId)
                .WillCascadeOnDelete(false);
        }
    }
}
