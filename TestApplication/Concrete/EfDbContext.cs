namespace TestApplication.Concrete
{
    using System;
    using System.Data.Entity;
    using TestApplication.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EfDbContext : DbContext
    {
        public EfDbContext()
            : base("name=Lannisters")
        {
        }

        public virtual DbSet<Lannister> Lannisters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lannister>()
                .HasMany(e => e.Table1)
                .WithOptional(e => e.Table2)
                .HasForeignKey(e => e.ParentId);
        }
    }
}
