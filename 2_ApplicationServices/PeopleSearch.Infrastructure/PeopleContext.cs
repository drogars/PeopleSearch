using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Infrastructure
{
    public class PeopleContext : DbContext
    {

        public PeopleContext() : base("name=People")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PeopleContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Interests)
                .WithMany()
                .Map(x => x.ToTable("PersonInterestJoin"));
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Interest> Interests { get; set; }
    }
}
