using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Domain.DTO
{
    public interface IEntityDto : IEntityDto<Guid>
    {

    }
    /// <summary>
    /// 定义基于实体的DTO的公共属性.
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntityDto<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
