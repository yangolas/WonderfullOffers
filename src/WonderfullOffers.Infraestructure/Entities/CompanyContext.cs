using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WonderfullOffers.Infraestructure.Entities.Models;

namespace WonderfullOffers.Infraestructure.Entities;
public class CompanyContext:DbContext
{
    public DbSet<AmazonOfferEntity> Amazon { get; set; }
    public DbSet<DouglasOfferEntity> Douglas { get; set; }
    public DbSet<DruniOfferEntity> Druni { get; set; }
    public DbSet<MaquillaliaOfferEntity> Maquillalia { get; set; }
    public DbSet<PrimorOfferEntity> Primor { get; set; }
    public DbSet<SheinOfferEntity> Shein { get; set; }
    public DbSet<SpringfieldOfferEntity> Springfield { get; set; }
    public DbSet<StradivariusOfferEntity> Stradivarius { get; set; }
    public DbSet<WomensecretOfferEntity> Womensecret { get; set; }
    public DbSet<ZalandoOfferEntity> Zalando { get; set; }
    public CompanyContext() { }
    public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-9NH5142;Initial Catalog=WonderfullOffer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}