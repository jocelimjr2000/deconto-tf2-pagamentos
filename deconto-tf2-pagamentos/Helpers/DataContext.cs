using deconto_tf2_pagamentos.Models;
using Microsoft.EntityFrameworkCore;

namespace deconto_tf2_pagamentos.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("Database");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Pagamento> Pagamentos { get; set; }
    }
}
