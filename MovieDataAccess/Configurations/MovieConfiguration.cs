using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieEntities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataAccess.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
           builder.HasKey(x=>x.Id);
            builder.HasIndex(x=>x.Title).IsUnique();
            builder.ToTable("Movies");
            builder.Property(x=>x.PosterPath).HasMaxLength(250);

        }
    }
}
