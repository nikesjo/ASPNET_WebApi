using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Contexts;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Education\\aspnet_core\\aspnet_assignment\\ASPNET_WebApi\\Infrastructure\\Data\\webapi_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

        return new DataContext(optionsBuilder.Options);
    }
}
