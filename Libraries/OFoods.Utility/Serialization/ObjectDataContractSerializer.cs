using System;
using System.IO;
using System.Runtime.Serialization;

namespace OFoods.Utility.Serialization
{
    /// <summary>
    /// 表示数据协定序列化程序.
    /// </summary>
    public class ObjectDataContractSerializer : IObjectSerializer
    {
        #region IObjectSerializer<TObject> 成员
        /// <summary>
        /// 将对象序列化为字节流.
        /// </summary>
        /// <typeparam name="TObject">对象的类型.</typeparam>
        /// <param name="obj">要序列化的对象.</param>
        /// <returns>包含序列化数据的字节流.</returns>
        public virtual byte[] Serialize<TObject>(TObject obj)
        {
            Type graphType = obj.GetType();
            DataContractSerializer js = new DataContractSerializer(graphType);
            byte[] ret = null;
            using (MemoryStream ms = new MemoryStream())
            {
                js.WriteObject(ms, obj);
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
            DataContractSerializer js = new DataContractSerializer(typeof(TObject));
            using (MemoryStream ms = new MemoryStream(stream))
            {
                TObject ret = (TObject)js.ReadObject(ms);
                ms.Close();
                return ret;
            }
        }

        #endregion
    }
}
