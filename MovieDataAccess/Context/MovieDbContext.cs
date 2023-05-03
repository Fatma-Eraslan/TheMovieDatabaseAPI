using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieDataAccess.Configurations;
using MovieEntities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataAccess.Context
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieRating> MoviesRatings { get; set; }    


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = "Server=localhost;Database=movieDb;Integrated Security=True;";//sil!

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().HasMany(m => m.Genres).WithMany(g => g.Movies);//*-*
            modelBuilder.Entity<MovieRating>().HasKey(x => new { x.UserId, x.MovieId });
            modelBuilder.ApplyConfiguration(new MovieConfiguration());

            //modelBuilder.Entity<Movie>(entity =>
            //{
            //    entity.ToTable("Movies");
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Title).HasMaxLength(255).IsRequired();         

            //    entity.Property(e => e.PosterPath).HasMaxLength(255).IsRequired();
            //});
        }
    }
}
