using EF.Configs;
using EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace EF.Data
{
    sealed partial class ContactsBookDbContext : DbContext
    {
        public DbSet<Human> Humans { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public ContactsBookDbContext()
        {
            Database.EnsureCreated();
        }
        
        public ContactsBookDbContext(DbContextOptions<ContactsBookDbContext> options) : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (!ConfigurationService.IsConfigured)
                {
                    // For migrations
                    ConfigurationService.Configure();
                }

                optionsBuilder.UseSqlServer(
                    ConfigurationService.Configuration.GetConnectionString("ContactsBookDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Human>(HumanConfigure);
            modelBuilder.Entity<Phone>(PhoneConfigure);
            modelBuilder.Entity<Contact>(ContactConfigure);
            
            OnModelCreatingPartial(modelBuilder);
        }

        private void HumanConfigure(EntityTypeBuilder<Human> builder)
        {
            builder.ToTable("HUMAN")
                .HasKey(human => human.Id)
                .HasName("HUMAN_PK");

            builder.Property(human => human.Id)
                .HasDefaultValueSql("(newid())");
            
            builder.Property(human => human.Name)
                .HasMaxLength(20);

            builder.Property(human => human.Surname)
                .HasMaxLength(20);

            builder.Property(human => human.Patronymic)
                .HasMaxLength(30);
        }

        private void PhoneConfigure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("PHONE")
                .HasKey(phone => phone.Number)
                .HasName("PHONE_PK");

            builder.Property(phone => phone.Opeator)
                .HasMaxLength(8);
        }

        private void ContactConfigure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("CONTACT")
                .HasKey(contact => new { contact.HumanId, contact.PhoneNumber })
                .HasName("CONTACT_PK");

            builder.Property(contact => contact.Note)
                .HasMaxLength(500);

            builder.HasOne(contact => contact.Human)
                .WithMany()
                .HasForeignKey(contact => contact.HumanId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CONTACT_HUMAN_FK");
            
            builder.HasOne(contact => contact.Phone)
                .WithMany()
                .HasForeignKey(contact => contact.PhoneNumber)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CONTACT_PHONE_FK");
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}