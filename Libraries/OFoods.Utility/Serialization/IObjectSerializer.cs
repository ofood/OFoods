namespace OFoods.Utility.Serialization
{
    /// <summary>
    /// 表示实现的类是对象序列化器.
    /// </summary>
    public interface IObjectSerializer
    {
        /// <summary>
        /// 将对象序列化为字节流.
        /// </summary>
        /// <typeparam name="TObject">对象的类型.</typeparam>
        /// <param name="obj">要序列化的对象.</param>
        /// <returns>包含序列化数据的字节流.</returns>
        byte[] Serialize<TObject>(TObject obj);
        /// <summary>
        /// 反序列化给定字节流中的对象.
        /// </summary>
        /// <typeparam name="TObject">对象的类型.</typeparam>
        /// <param name="stream">包含对象序列化数据的字节流.</param>
        /// <returns>反序列化的对象.</returns>
        TObject Deserialize<TObject>(byte[] stream);
    }
}
