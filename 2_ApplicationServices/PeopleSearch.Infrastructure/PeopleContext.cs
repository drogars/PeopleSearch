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

            // TODO: need to setup the join
        }

        public DbSet<Person> People { get; set; }
    }
}
