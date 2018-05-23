using System;
namespace OFoods.Domain.DTO
{
    [Serializable]
    public class EntityDto: EntityDto<Guid>,IEntityDto
    {
        public EntityDto()
        {

        }
        public EntityDto(Guid id):base(id)
        {

        }
    }
    /// <summary>
    /// 为基于实体的DTO实现公共属性.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键的类型</typeparam>
    [Serializable]
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }

        public EntityDto()
        {

        }
        public EntityDto(TPrimaryKey id)
        {
            Id = id;
        }
    }
}
