namespace ChikaProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HumanDbContext : DbContext
    {
        public HumanDbContext()
            : base("name=HumanDbContext")
        {
        }

        public virtual DbSet<HumanTb> HumanTbs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HumanTb>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<HumanTb>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<HumanTb>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
