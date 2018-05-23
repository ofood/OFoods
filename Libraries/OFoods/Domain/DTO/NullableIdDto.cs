using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Domain.DTO
{
    /// <summary>
    /// 这个DTO可以直接使用（或继承）将可为空的Id值传递给应用程序服务方法.
    /// </summary>
    /// <typeparam name="TId">Id的类型</typeparam>
    [Serializable]
    public class NullableIdDto<TId>
        where TId:struct
    {
        public TId? Id { get; set; }

        public NullableIdDto()
        {

        }

        public NullableIdDto(TId? id)
        {
            Id = id;
        }
    }
    [Serializable]
    public class NullableIdDto : NullableIdDto<int>
    {
        public NullableIdDto()
        {

        }

        public NullableIdDto(int? id)
            : base(id)
        {

        }
    }
}
