using TextConverterApp.Models;
using Microsoft.EntityFrameworkCore;


namespace TextConverterApp.Data;

public class ConversionsDbContext : DbContext
{
    public ConversionsDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<ConversionModel> Conversions { get; set; }
    public DbSet<UserModel> Users { get; set; }
}