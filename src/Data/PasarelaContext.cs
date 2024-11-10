using Microsoft.EntityFrameworkCore;


public class PasarelaContext : DbContext
{
    public PasarelaContext(DbContextOptions<PasarelaContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=PasarelaPagosDB.db");
    }

    public DbSet<Recibo> Recibos { get; set; }
}
