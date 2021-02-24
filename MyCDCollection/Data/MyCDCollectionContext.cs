using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCDCollection.Models;


namespace MyCDCollection.Data
{
    public class MyCDCollectionContext : DbContext
    {

        public MyCDCollectionContext(DbContextOptions<MyCDCollectionContext> options)
            : base(options)
        {

        }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<CD> CD { get; set; }

        public DbSet<CDLended> LendedCDS {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .HasMany<CD>(s => s.CDS)
                .WithOne(g => g.Artist)
                .HasForeignKey(g => g.ArtistNameID)
                .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CD>()

            .HasOne<CDLended>(s => s.Name)
            .WithOne(b => b.CDs)
            .HasForeignKey<CDLended>(b => b.CDSID)
            .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
