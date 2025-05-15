public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Moto> Motos { get; set; }
    public DbSet<Filial> Filiais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filial>()
            .HasMany(f => f.Motos)
            .WithOne(m => m.Filial)
            .HasForeignKey(m => m.FilialId);
    }
}
