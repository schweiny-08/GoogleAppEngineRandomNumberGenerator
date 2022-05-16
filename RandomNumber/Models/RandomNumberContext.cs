using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RandomNumber.Models
{
    public class RandomNumberContext : DbContext
    {
        public RandomNumberContext(DbContextOptions<RandomNumberContext> options) : base(options)
        {
        }

        public DbSet<GeneratedNumber> GeneratedNumbers { get; set; } = null!;
    }
}
