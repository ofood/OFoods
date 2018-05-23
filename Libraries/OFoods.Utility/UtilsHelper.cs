using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace OFoods.Utility
{
    /// <summary>
    /// 实用方法类库.
    /// </summary>
    public static class UtilsHelper
    {
        #region 私有变量
        private const int InitialPrime = 23;
        private const int FactorPrime = 29;
        #endregion
        /// <summary>
        /// 将给定的对象转换为给定的强类型.
        /// </summary>
        public static T ConvertType<T>(object value)
        {
            if (value == null)
            {
                return default(T);
            }

            var typeConverter1 = TypeDescriptor.GetConverter(typeof(T));
            if (typeConverter1.CanConvertFrom(value.GetType()))
            {
                return (T)typeConverter1.ConvertFrom(value);
            }

            var typeConverter2 = TypeDescriptor.GetConverter(value.GetType());
            if (typeConverter2.CanConvertTo(typeof(T)))
            {
                return (T)typeConverter2.ConvertTo(value, typeof(T));
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// 根据对象的每个属性的给定哈希代码数组获取哈希代码.
        /// </summary>
        /// <param name="hashCodesForProperties">来自对象每个属性的哈希代码数组.</param>
        /// <returns>哈希码.</returns>
        public static int GetHashCode(params int[] hashCodesForProperties)
        {
            unchecked
            {
                int hash = InitialPrime;
                foreach (var code in hashCodesForProperties)
                    hash = hash * FactorPrime + code;
                return hash;
            }
        }
        /// <summary>
        /// 用指定长度生成一个由<see cref ="System.String"/>值表示的唯一标识符.
        /// </summary>
        /// <param name="length">要生成的标识符的长度.</param>
        /// <returns>由<see cref ="System.String"/>值表示的唯一标识符.</returns>
        public static string GetUniqueIdentifier(int length)
        {
            int maxSize = length;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            // Unique identifiers cannot begin with 0-9
            if (result[0] >= '0' && result[0] <= '9')
            {
                return GetUniqueIdentifier(length);
            }
            return result.ToString();
        }
    }
}