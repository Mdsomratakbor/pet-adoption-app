using Microsoft.EntityFrameworkCore;
using PetAdoption.Api.Data;

namespace PetAdoption.Api.Utilities
{
    public static class ApplyMigration
    {
       public  static void ApplyDbMigration(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PetContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
                dbContext.Database.Migrate();
        }
    }
}
