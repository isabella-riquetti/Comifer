using Comifer.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Category");

            HasKey(t => t.Id);
            Property(t => t.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
        }
    }
}
