using InsideTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InsideTest.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
