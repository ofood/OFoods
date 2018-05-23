using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 表示包含用于异常处理的一组配置的配置集合.
    /// </summary>
    [ConfigurationCollection(typeof(ExceptionElement), CollectionType = ConfigurationElementCollectionType.BasicMap, AddItemName = ExceptionElementPropertyName)]
    public partial class ExceptionElementCollection : ConfigurationElementCollection
    {

        #region Constants
        /// <summary>
        /// 此集合中的个人<see cref =“ExceptionElement”/>实例的XML名称.
        /// </summary>
        internal const string ExceptionElementPropertyName = "exception";
        #endregion

        #region Overrides
        /// <summary>
        /// 获取<see cref =“ConfigurationElementCollection”/>的类型.
        /// </summary>
        /// <returns>此集合的<see cref =“ConfigurationElementCollectionType”/>.</returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        /// <summary>
        /// 获取用于标识此元素集合的名称
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return ExceptionElementPropertyName;
            }
        }

        /// <summary>
        /// 指示在<see cref =“ConfigurationElementCollection”/>中是否存在指定的<see cref =“ConfigurationElement”/>.
        /// </summary>
        /// <param name="elementName">要验证的元素的名称.</param>
        /// <returns>
        /// <see langword="true"/> 如果元素存在于集合中; 否则, <see langword="false"/>.
        /// </returns>
        protected override bool IsElementName(string elementName)
        {
            return (elementName == ExceptionElementPropertyName);
        }

        /// <summary>
        /// 获取指定配置元素的元素键.
        /// </summary>
        /// <param name="element"><see cref =“ConfigurationElement”/>返回关键字.</param>
        /// <returns>
        /// 一个<see cref =“object”/>，它充当指定的<see cref =“ConfigurationElement”/>的键.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExceptionElement)(element)).Type;
        }

        /// <summary>
        /// 创建一个新的 <see cref="ExceptionElement"/>.
        /// </summary>
        /// <returns>
        /// 新的 <see cref="ExceptionElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ExceptionElement();
        }
        #endregion

        #region Indexer
        /// <summary>
        /// 获取指定索引处的<see cref =“ExceptionElement”/>.
        /// </summary>
        /// <param name="index">要检索的<see cref =“ExceptionElement”/>的索引.</param>
        public ExceptionElement this[int index]
        {
            get
            {
                return ((ExceptionElement)(base.BaseGet(index)));
            }
        }

        /// <summary>
        /// 使用指定的键获取<see cref =“ExceptionElement”/>.
        /// </summary>
        /// <param name="type"><see cref =“ExceptionElement”/>检索的关键.</param>
        public ExceptionElement this[object type]
        {
            get
            {
                return ((ExceptionElement)(base.BaseGet(type)));
            }
        }
        #endregion

        #region Add
        /// <summary>
        /// 将指定的<see cref =“ExceptionElement”/>添加到<see cref =“ConfigurationElementCollection”/>.
        /// </summary>
        /// <param name="exception"> <see cref="ExceptionElement"/> 添加.</param>
        public void Add(ExceptionElement exception)
        {
            base.BaseAdd(exception);
        }
        #endregion

        #region Remove
        /// <summary>
        /// 从<see cref =“ConfigurationElementCollection”/>中删除指定的<see cref =“ExceptionElement”/>.
        /// </summary>
        /// <param name="exception"> <see cref="ExceptionElement"/>删除.</param>
        public void Remove(ExceptionElement exception)
        {
            base.BaseRemove(this.GetElementKey(exception));
        }
        #endregion

        #region GetItem
        /// <summary>
        /// 获取指定索引处的<see cref =“ExceptionElement”/>.
        /// </summary>
        /// <param name="index">要检索的<see cref =“ExceptionElement”/>的索引.</param>
        public ExceptionElement GetItemAt(int index)
        {
            return ((ExceptionElement)(base.BaseGet(index)));
        }

        /// <summary>
        /// 使用指定的键获取<see cref =“ExceptionElement”/>.
        /// </summary>
        /// <param name="type"><see cref =“ExceptionElement”/>检索的关键.</param>
        public ExceptionElement GetItemByKey(string type)
        {
            return ((ExceptionElement)(base.BaseGet(((object)(type)))));
        }
        #endregion

        #region IsReadOnly override
        /// <summary>
        /// 获取一个值，指示元素是否是只读的.
        /// </summary>
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
    }
}
