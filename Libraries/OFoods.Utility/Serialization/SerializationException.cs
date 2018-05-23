﻿using System;
using System.Runtime.InteropServices;

namespace OFoods.Utility.Serialization
{
    /// <summary>
    /// 表示序列化/反序列化对象时发生的错误.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof(_Exception))]
    public class SerializationException : Exception
    {
        #region 构造函数
        /// <summary>
        /// 初始化<c> SerializationException </ c>类的新实例.
        /// </summary>
        public SerializationException() : base() { }
        /// <summary>
        /// 使用指定的错误消息初始化<c> SerializationException </ c>类的新实例.
        /// </summary>
        /// <param name="message">描述错误的消息.</param>
        public SerializationException(string message) : base(message) { }
        /// <summary>
        /// 使用指定的错误消息和导致此异常的内部异常初始化<c> SerializationException </ c>类的新实例.
        /// </summary>
        /// <param name="message">描述错误的消息.</param>
        /// <param name="innerException">内部异常是导致此异常的原因.</param>
        public SerializationException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// 使用指定的字符串格式化程序和用于格式化描述错误的消息的参数初始化<c> SerializationException </ c>类的新实例.
        /// </summary>
        /// <param name="format">用于格式化错误消息的字符串格式器.</param>
        /// <param name="args">格式化程序用于生成错误消息的参数.</param>
        public SerializationException(string format, params object[] args) : base(string.Format(format, args)) { }
        #endregion
    }
}
