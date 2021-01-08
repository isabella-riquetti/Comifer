using Comifer.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Mapping
{
    public class BrandMap : EntityTypeConfiguration<Brand>
    {
        public BrandMap()
        {
            ToTable("Brand");

            HasKey(t => t.Id);
            Property(t => t.ProviderId).HasColumnName("ProviderId").IsRequired();
            Property(t => t.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            Property(t => t.SiteUrl).HasColumnName("SiteUrl").HasMaxLength(500);

            HasRequired(t => t.Provider)
                .WithMany(t => t.Brands)
                .HasForeignKey(d => d.ProviderId)
                .WillCascadeOnDelete(false);
        }
    }
}
