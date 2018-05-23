using System;
namespace OFoods.Domain.DTO
{
    /// <summary>
    /// 可用于发送/接收名称/值（或键/值）对.
    /// </summary>
    [Serializable]
    public class NameValueDto : NameValueDto<string>
    {
        /// <summary>
        /// 创建新的 <see cref="NameValueDto"/>.
        /// </summary>
        public NameValueDto()
        {

        }

        /// <summary>
        /// 创建新的 <see cref="NameValueDto"/>.
        /// </summary>
        public NameValueDto(string name, string value)
            : base(name, value)
        {

        }

        /// <summary>
        /// 创建新的 <see cref="NameValueDto"/>.
        /// </summary>
        /// <param name="nameValue">A <see cref="NameValue"/> object to get it's name and value</param>
        public NameValueDto(NameValue nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }

    /// <summary>
    /// 可用于发送/接收名称/值（或键/值）对.
    /// </summary>
    [Serializable]
    public class NameValueDto<T> : NameValue<T>
    {
        /// <summary>
        /// 创建新的 <see cref="NameValueDto"/>.
        /// </summary>
        public NameValueDto()
        {

        }

        /// <summary>
        /// 创建新的 <see cref="NameValueDto"/>.
        /// </summary>
        public NameValueDto(string name, T value)
            : base(name, value)
        {

        }

        /// <summary>
        /// 创建新的 <see cref="NameValueDto"/>.
        /// </summary>
        /// <param name="nameValue">一个<see cref ="NameValue"/>对象来获取它的名字和值</param>
        public NameValueDto(NameValue<T> nameValue)
            : this(nameValue.Name, nameValue.Value)
        {

        }
    }
}
