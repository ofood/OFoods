using System;

namespace OFoods.Domain.Entities
{
    /// <summary>
    /// 数据模型接口
    /// </summary>
    public interface IEntity:IEntity<Guid>
    {
        
    }
    /// <summary>
    /// 数据模型接口
    /// </summary>
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 获取 实体唯一标识，主键
        /// </summary>
        TKey ID { get; set; }
    }
}