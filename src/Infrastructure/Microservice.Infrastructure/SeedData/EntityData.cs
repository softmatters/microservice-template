using Microservice.Domain.Entities;

namespace Microservice.Infrastructure.SeedData;

internal static class EntityData
{
    public static IList<Entity> Seed()
    {
        return
        [
            new()
            {
                Id = 1
                // add more properties as needed
            },
            new()
            {
                Id = 2,
                // add more properties as needed
            }
        ];
    }
}