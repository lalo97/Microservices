﻿using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            Console.WriteLine($"---> Prep Population...");

            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            
            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

            var platforms = grpcClient.ReturnAllPlatforms();

            var repo = serviceScope.ServiceProvider.GetService<ICommandRepo>();

            SeedData(repo, platforms);
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine($"---> Seeding new platforms...");

            foreach (var platform in platforms)
            {
                if (!repo.ExternalPlatformExists(platform.ExternalID))
                {
                    repo.CreatePlatform(platform);
                }

                repo.SaveChanges();

            }
        }
    }
}
