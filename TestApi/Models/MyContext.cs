using Microsoft.EntityFrameworkCore;
namespace TestApi.Models;

public class MyContext : DbContext
{
    public DbSet<EFUser> Users { get; set; } = null!;
    private readonly string _connectString;
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
        

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("Db");
            optionsBuilder.UseNpgsql(connectionString);
        }

        //this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
   
}