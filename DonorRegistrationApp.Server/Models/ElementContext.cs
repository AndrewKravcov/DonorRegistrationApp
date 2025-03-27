using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DonorRegistrationApp.Server.Models
{
    public class ElementContext : DbContext
    {
        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General);
        public ElementContext(DbContextOptions<ElementContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Element>()
                .Property(b => b.dateCreated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Element>()
                .Property(b => b.elementProperties)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, jsonSerializerOptions),
                    v => JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(v, jsonSerializerOptions));
        }

        public DbSet<Element> Elements { get; set; } = null!;
    }
}
