using Comifer.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Mapping
{
    public class FileMap : EntityTypeConfiguration<File>
    {
        public FileMap()
        {
            ToTable("File");

            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ReferId).HasColumnName("ReferId").IsRequired();
            Property(t => t.TableName).HasColumnName("TableName").HasMaxLength(255).IsRequired();
            Property(t => t.Type).HasColumnName("Type").HasMaxLength(255).IsRequired();
            Property(t => t.Priority).HasColumnName("Priority").IsRequired();
            Property(t => t.FileBytes).HasColumnName("FileBytes").IsRequired();
            Property(t => t.FileName).HasColumnName("FileName").IsRequired();
            Property(t => t.MIME).HasColumnName("MIME").IsRequired();
        }
    }
}
