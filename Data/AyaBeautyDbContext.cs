
using AyaBeauty.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AyaBeauty.API.Data
{
public class AyaBeautyDbContext:DbContext
    {
     public AyaBeautyDbContext(DbContextOptions<AyaBeautyDbContext> options)
     :base(options){}
     public DbSet<HomeContent> HomeContent {get;set;}
     public DbSet<AboutContent> AboutContent {get;set;}
     public DbSet<Testimonial> Testimonials {get;set;}
     public DbSet<GalleryImage> GalleryImages {get;set;}
     public DbSet<ContactInfo> ContactInfo { get; set; } 
     public DbSet<Product> Products { get; set; }  
     public DbSet<Service> Services { get; set; }  
     
   protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HomeContent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Subtitle).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ImageFileName).IsRequired().HasMaxLength(300);
                entity.Property(e => e.ButtonPrimary).HasMaxLength(100);
                entity.Property(e => e.ButtonSecondary).HasMaxLength(100);
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<AboutContent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SectionTitle).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PhilosophyTitle).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Paragraph1).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Paragraph2).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Paragraph3).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.ImageFileName).IsRequired().HasMaxLength(300);
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
});


          modelBuilder.Entity<Testimonial>(entity =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.ClientName).IsRequired().HasMaxLength(200);
    entity.Property(e => e.Quote).IsRequired().HasMaxLength(1000);
    entity.Property(e => e.ProfileImageFileName).IsRequired().HasMaxLength(300);
    entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
});


   modelBuilder.Entity<GalleryImage>(entity =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Src).IsRequired().HasMaxLength(300);
    entity.Property(e => e.Alt).IsRequired().HasMaxLength(200);
    entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
    entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
    entity.Property(e => e.CategoryLabel).IsRequired().HasMaxLength(100);
    entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
});


   modelBuilder.Entity<ContactInfo>(entity =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.SectionTitle).IsRequired().HasMaxLength(200);
    entity.Property(e => e.SectionDescription).IsRequired().HasMaxLength(500);
    entity.Property(e => e.AddressLine1).IsRequired().HasMaxLength(200);
    entity.Property(e => e.AddressLine2).IsRequired().HasMaxLength(200);
    entity.Property(e => e.Phone1).HasMaxLength(50);
    entity.Property(e => e.Phone2).HasMaxLength(50);
    entity.Property(e => e.Email).HasMaxLength(200);
    entity.Property(e => e.HoursWeekdays).HasMaxLength(100);
    entity.Property(e => e.HoursWeekdaysTime).HasMaxLength(100);
    entity.Property(e => e.HoursSunday).HasMaxLength(100);
    entity.Property(e => e.InstagramUrl).HasMaxLength(300);
    entity.Property(e => e.FacebookUrl).HasMaxLength(300);
    entity.Property(e => e.WhatsappNumber).HasMaxLength(50);
    entity.Property(e => e.MapLatitude).HasMaxLength(50);
    entity.Property(e => e.MapLongitude).HasMaxLength(50);
    entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
});


   modelBuilder.Entity<Product>(entity =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
    entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
    entity.Property(e => e.CategoryLabel).IsRequired().HasMaxLength(100);
    entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
    entity.Property(e => e.OldPrice).HasColumnType("decimal(10,2)");
    entity.Property(e => e.Image).IsRequired().HasMaxLength(300);
    entity.Property(e => e.Badge).HasMaxLength(50);
    entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
});


    modelBuilder.Entity<Service>(entity =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
    entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
    entity.Property(e => e.ImageFileName).IsRequired().HasMaxLength(300);
    entity.Property(e => e.OldPrice).HasColumnType("decimal(10,2)");
    entity.Property(e => e.NewPrice).HasColumnType("decimal(10,2)");
    entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
});
        }
    } 
}