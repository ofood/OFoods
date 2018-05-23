using System;
using System.IO;
using NSXmlSerialization = System.Xml.Serialization;

namespace OFoods.Utility.Serialization
{
    /// <summary>
    /// Xml序列化器.
    /// </summary>
    public class ObjectXmlSerializer : IObjectSerializer
    {
        #region IObjectSerializer 成员
        /// <summary>
        /// 将对象序列化为字节流.
        /// </summary>
        /// <typeparam name="TObject">对象的类型.</typeparam>
        /// <param name="obj">要序列化的对象.</param>
        /// <returns>包含序列化数据的字节流.</returns>
        public virtual byte[] Serialize<TObject>(TObject obj)
        {
            Type graphType = obj.GetType();
            NSXmlSerialization.XmlSerializer xmlSerializer = new NSXmlSerialization.XmlSerializer(graphType);
            byte[] ret = null;
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj);
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
            NSXmlSerialization.XmlSerializer xmlSerializer = new NSXmlSerialization.XmlSerializer(typeof(TObject));
            using (MemoryStream ms = new MemoryStream(stream))
            {
                TObject ret = (TObject)xmlSerializer.Deserialize(ms);
                ms.Close();
                return ret;
            }
        }

        #endregion
    }
}
