using EF.Configs;
using EF.Entities;
using EF.Providers.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace EF.Data
{
    public partial class ContactsBookDbContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public ContactsBookDbContext()
        {
        }

        public ContactsBookDbContext(DbContextOptions<ContactsBookDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.Config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(ContactConfigure);
            modelBuilder.Entity<Phone>(PhoneConfigure);
            OnModelCreatingPartial(modelBuilder);
        }

        private void ContactConfigure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("CONTACT")
                .HasKey(c => c.Id)
                .HasName("CONTACT_PK");
            
            builder.Property(c => c.Id)
                .HasDefaultValueSql("(newid())");
            
            builder.Property(c => c.Name)
                .HasMaxLength(20);

            builder.Property(c => c.Surname)
                .HasMaxLength(20);

            builder.Property(c => c.Patronymic)
                .HasMaxLength(30);
        }
        
        private void PhoneConfigure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Phone")
                .HasKey(p => p.Number)
                .HasName("PHONE_PK");

            builder.Property(p => p.Number)
                .HasMaxLength(10);

            builder.Property(p => p.Note)
                .HasMaxLength(500);

            builder.HasOne(p => p.ContactNavigation)
                .WithMany(c => c.Phones)
                .HasForeignKey(p => p.ContactId)
                .HasPrincipalKey(c => c.Id)
                .HasConstraintName("PHONE_CONTACT_FK");
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}