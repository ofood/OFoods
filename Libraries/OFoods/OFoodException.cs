using System;
using System.Runtime.Serialization;


namespace OFoods
{
    /// <summary>
    /// OFood框架异常类
    /// </summary>
    [Serializable]
    public class OFoodsException : Exception
    {
        /// <summary>
        /// 初始化<see cref="OFoodException"/>类的新实例
        /// </summary>
        public OFoodsException()
        { }

        /// <summary>
        /// 使用指定错误消息初始化<see cref="OFoodException"/>类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息</param>
        public OFoodsException(string message)
            : base(message)
        { }

        /// <summary>
        /// 使用异常消息与一个内部异常实例化一个<see cref="OFoodException"/>类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在<see cref="OFoodException"/>内部的异常实例</param>
        public OFoodsException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// 使用可序列化数据实例化一个<see cref="OFoodException"/>类的新实例
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        protected OFoodsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        /// <summary>
        /// 使用指定的字符串格式化程序和用于格式化描述错误的消息的参数初始化<c> OFoodsException </ c>类的新实例.
        /// </summary>
        /// <param name="format">用于格式化错误消息的字符串格式器.</param>
        /// <param name="args">格式化程序用于生成错误消息的参数.</param>
        public OFoodsException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
