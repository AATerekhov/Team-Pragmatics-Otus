using Domain.Entities;

namespace Services.Repositories.Abstractions
{
    public interface IRepository<T, TPrimaryKey>
        where T : IEntity<TPrimaryKey>
    {

    }
}
