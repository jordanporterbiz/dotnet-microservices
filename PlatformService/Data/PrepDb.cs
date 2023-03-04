namespace PlatformService.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }

        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if (!isProduction)
            {
                Console.WriteLine("--> Seeding Data...");

                context.Platforms.AddRange(
                    new Models.Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We are in Production!");
            }
        }
    }
}