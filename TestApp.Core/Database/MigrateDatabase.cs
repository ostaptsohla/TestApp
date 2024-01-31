using Microsoft.EntityFrameworkCore;

namespace TestApp.Database;

public static class SetupDatabase
{
    public static IServiceProvider MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            context.Database.Migrate();
        }

        return serviceProvider;
    }
}