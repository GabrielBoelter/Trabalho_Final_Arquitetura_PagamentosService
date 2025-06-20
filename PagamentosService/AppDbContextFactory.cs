using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PagamentosService.Data;

namespace PagamentosService.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=pagamentos.db");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}