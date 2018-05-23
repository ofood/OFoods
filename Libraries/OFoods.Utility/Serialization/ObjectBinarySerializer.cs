using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OFoods.Utility.Serialization
{
    /// <summary>
    /// 表示二进制序列化.
    /// </summary>
    public class ObjectBinarySerializer : IObjectSerializer
    {
        #region 私有字段
        private readonly BinaryFormatter binaryFormatter = new BinaryFormatter();
        #endregion

        #region IObjectSerializer 成员
        /// <summary>
        /// 将对象序列化为字节流.
        /// </summary>
        /// <typeparam name="TObject">对象的类型.</typeparam>
        /// <param name="obj">要序列化的对象.</param>
        /// <returns>包含序列化数据的字节流.</returns>
        public virtual byte[] Serialize<TObject>(TObject obj)
        {
            byte[] ret = null;
            using (MemoryStream ms = new MemoryStream())
            {
                binaryFormatter.Serialize(ms, obj);
                ret = ms.ToArray();
                ms.Close();
            }
            return ret;
        }
        /// <summary>
        /// 反序列化给定字节流中的对象.
        /// </summary>
        /// <typeparam name="TObject">对象的类型.</typeparam>
        /// <param name="stream">包含对象序列化数据的字节流.</param>
        /// <returns>反序列化的对象.</returns>
        public virtual TObject Deserialize<TObject>(byte[] stream)
        {
            using (MemoryStream ms = new MemoryStream(stream))
            {
                TObject ret = (TObject)binaryFormatter.Deserialize(ms);
                ms.Close();
                return ret;
            }
        }

        #endregion
    }
}
