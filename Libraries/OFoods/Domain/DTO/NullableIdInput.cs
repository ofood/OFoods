namespace OFoods.Domain.DTO
{
    /// <summary>
    /// 这个DTO可以直接使用（或继承）将可为空的Id值传递给应用程序服务方法.
    /// </summary>
    /// <typeparam name="TId">Id的类型</typeparam>
    public class NullableIdInput<TId>
        where TId : struct
    {
        public TId? Id { get; set; }

        public NullableIdInput()
        {

        }

        public NullableIdInput(TId? id)
        {
            Id = id;
        }
    }
    public class NullableIdInput : NullableIdInput<int>
    {
        public NullableIdInput()
        {

        }

        public NullableIdInput(int? id)
            : base(id)
        {

        }
    }
}
