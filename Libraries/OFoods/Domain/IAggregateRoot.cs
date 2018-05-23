using OFoods.Domain.Entities;
using System;

namespace OFoods.Domain
{
    /// <summary>
    /// 代表聚合根.
    /// </summary>
    public interface IAggregateRoot<TKey>: IEntity<TKey>
        where TKey : IEquatable<TKey>
    {

    }
    /// <summary>
    /// 代表聚合根.
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<Guid>, IEntity
    {

    }
}
