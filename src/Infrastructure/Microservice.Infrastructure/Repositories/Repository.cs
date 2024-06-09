using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microservice.Application.Contracts.Repositories;
using Microservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Infrastructure.Repositories;

public class Repository(DbContext dbContext) : IRepository
{
    public async Task<Entity> Create(Entity entity)
    {
        var dbEntity = await dbContext.Entities.AddAsync(entity);

        await dbContext.SaveChangesAsync();

        return dbEntity.Entity;
    }

    public async Task<Entity> Get(ISpecification<Entity> specification)
    {
        var entity = await dbContext
            .Entities
            .WithSpecification(specification)
            .FirstOrDefaultAsync();

        if (entity != null) return entity;

        // Error handling
        throw new NotImplementedException();
    }

    public async Task<Entity> Update(int id, Entity entity)
    {
        var entityFromDb = await dbContext
            .Entities
            .FirstOrDefaultAsync(record => record.Id == id);

        if (entityFromDb != null)
        {
            // update the entity
            // entityFromDb.Property = entityFromDb.Column

            await dbContext.SaveChangesAsync();

            return entityFromDb;
        }

        // Error handling
        return new Entity();
    }
}