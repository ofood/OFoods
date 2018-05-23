using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 表示配置集合，其中包含异常处理程序的一组配置.
    /// </summary>
    [ConfigurationCollection(typeof(ExceptionHandlerElement), CollectionType = global::System.Configuration.ConfigurationElementCollectionType.BasicMap, AddItemName = ExceptionHandlerElementPropertyName)]
    public partial class ExceptionHandlerElementCollection :ConfigurationElementCollection
    {

        #region Constants
        /// <summary>
        /// 此集合中的单个<see cref ="ExceptionHandlerElement"/>实例的XML名称.
        /// </summary>
        internal const string ExceptionHandlerElementPropertyName = "handler";
        #endregion

        #region Overrides
        /// <summary>
        /// 获取<see cref ="ConfigurationElementCollection"/>的类型.
        /// </summary>
        /// <returns>此集合的<see cref ="ConfigurationElementCollectionType"/>.</returns>
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
                return ExceptionHandlerElementPropertyName;
            }
        }

        /// <summary>
        /// 指示<see cref ="ConfigurationElementCollection"/>是否存在指定的<see cref ="ConfigurationElement"/>.
        /// </summary>
        /// <param name="elementName">要验证的元素的名称.</param>
        /// <returns>
        /// <see langword="true"/> 如果元素存在于集合中; 否则, <see langword="false"/>.
        /// </returns>
        protected override bool IsElementName(string elementName)
        {
            return (elementName == ExceptionHandlerElementPropertyName);
        }

        /// <summary>
        /// 获取指定配置元素的元素键.
        /// </summary>
        /// <param name="element"><see cref ="ConfigurationElement"/>返回关键字.</param>
        /// <returns>
        /// An <see cref="object"/> 它充当指定的<see cref ="ConfigurationElement"/>的关键字.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExceptionHandlerElement)(element)).Type;
        }

        /// <summary>
        /// 创建一个新的 <see cref="ExceptionHandlerElement"/>.
        /// </summary>
        /// <returns>
        /// 新的 <see cref="ExceptionHandlerElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ExceptionHandlerElement();
        }
        #endregion

        #region Indexer
        /// <summary>
        /// 获取指定索引处的<see cref ="ExceptionHandlerElement"/>.
        /// </summary>
        /// <param name="index">要检索的<see cref ="ExceptionHandlerElement"/>的索引.</param>
        public ExceptionHandlerElement this[int index]
        {
            get
            {
                return ((ExceptionHandlerElement)(base.BaseGet(index)));
            }
        }

        /// <summary>
        /// 使用指定的键获取<see cref ="ExceptionHandlerElement"/>.
        /// </summary>
        /// <param name="type"><see cref ="ExceptionHandlerElement"/>检索的关键.</param>
        public ExceptionHandlerElement this[object type]
        {
            get
            {
                return ((ExceptionHandlerElement)(base.BaseGet(type)));
            }
        }
        #endregion

        #region Add
        /// <summary>
        /// 将指定的<see cref =“ExceptionHandlerElement”/>添加到<see cref =“ConfigurationElementCollection”/>.
        /// </summary>
        /// <param name="handler">The <see cref="ExceptionHandlerElement"/> to add.</param>
        public void Add(ExceptionHandlerElement handler)
        {
            base.BaseAdd(handler);
        }
        #endregion

        #region Remove
        /// <summary>
        /// 从<see cref ="ConfigurationElementCollection"/>中删除指定的<see cref ="ExceptionHandlerElement"/>.
        /// </summary>
        /// <param name="handler"> <see cref="ExceptionHandlerElement"/> 删除.</param>
        public void Remove(ExceptionHandlerElement handler)
        {
            base.BaseRemove(this.GetElementKey(handler));
        }
        #endregion

        #region GetItem
        /// <summary>
        /// 获取指定索引处的<see cref =“ExceptionHandlerElement”/>.
        /// </summary>
        /// <param name="index">要检索的<see cref =“ExceptionHandlerElement”/>的索引.</param>
        public ExceptionHandlerElement GetItemAt(int index)
        {
            return ((ExceptionHandlerElement)(base.BaseGet(index)));
        }

        /// <summary>
        /// 使用指定的键获取<see cref =“ExceptionHandlerElement”/>.
        /// </summary>
        /// <param name="type"><see cref =“ExceptionHandlerElement”/>检索的关键.</param>
        public ExceptionHandlerElement GetItemByKey(string type)
        {
            return ((ExceptionHandlerElement)(base.BaseGet(((object)(type)))));
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
    /// <summary>
    /// 表示包含消息处理程序的一组配置的配置集合.
    /// </summary>
    [ConfigurationCollection(typeof(HandlerElement), CollectionType = ConfigurationElementCollectionType.BasicMap, AddItemName = HandlerElementPropertyName)]
    public partial class HandlerElementCollection : ConfigurationElementCollection
    {

        #region Constants
        /// <summary>
        /// 此集合中的个人<see cref ="HandlerElement"/>实例的XML名称.
        /// </summary>
        internal const string HandlerElementPropertyName = "handler";
        #endregion

        #region Overrides
        /// <summary>
        /// 获取<see cref ="ConfigurationElementCollection"/>的类型.
        /// </summary>
        /// <returns>The <see cref="ConfigurationElementCollectionType"/> of this collection.</returns>
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
                return HandlerElementPropertyName;
            }
        }

        /// <summary>
        /// 指示<see cref ="ConfigurationElementCollection"/>是否存在指定的<see cref ="ConfigurationElement"/>.
        /// </summary>
        /// <param name="elementName">要验证的元素的名称.</param>
        /// <returns>
        /// <see langword="true"/> 如果元素存在于集合中; 否则, <see langword="false"/>.
        /// </returns>
        protected override bool IsElementName(string elementName)
        {
            return (elementName == HandlerElementPropertyName);
        }

        /// <summary>
        /// 获取指定配置元素的元素键.
        /// </summary>
        /// <param name="element"><see cref ="ConfigurationElement"/>返回关键字.</param>
        /// <returns>
        /// 一个<see cref ="object"/>，它充当指定的<see cref ="ConfigurationElement"/>的键.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((HandlerElement)(element)).Name;
        }

        /// <summary>
        /// 创建一个新的 <see cref="HandlerElement"/>.
        /// </summary>
        /// <returns>
        /// 新的 <see cref="HandlerElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new HandlerElement();
        }
        #endregion

        #region Indexer
        /// <summary>
        /// 获取指定索引处的<see cref ="HandlerElement"/>.
        /// </summary>
        /// <param name="index">要检索的<see cref ="HandlerElement"/>的索引.</param>
        public HandlerElement this[int index]
        {
            get
            {
                return ((HandlerElement)(base.BaseGet(index)));
            }
        }

        /// <summary>
        /// 用指定的键获取<see cref ="HandlerElement"/>.
        /// </summary>
        /// <param name="name"><see cref ="HandlerElement"/>检索的关键.</param>
        public HandlerElement this[object name]
        {
            get
            {
                return ((HandlerElement)(base.BaseGet(name)));
            }
        }
        #endregion

        #region Add
        /// <summary>
        /// 将指定的<see cref ="HandlerElement"/>添加到<see cref ="ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="handler">The <see cref="HandlerElement"/> 添加.</param>
        public void Add(HandlerElement handler)
        {
            base.BaseAdd(handler);
        }
        #endregion

        #region Remove
        /// <summary>
        /// 从<see cref ="ConfigurationElementCollection"/>中删除指定的<see cref ="HandlerElement"/>.
        /// </summary>
        /// <param name="handler"> <see cref="HandlerElement"/> 删除.</param>
        public void Remove(HandlerElement handler)
        {
            base.BaseRemove(this.GetElementKey(handler));
        }
        #endregion

        #region GetItem
        /// <summary>
        /// 获取指定索引处的<see cref ="HandlerElement"/>.
        /// </summary>
        /// <param name="index">要检索的<see cref ="HandlerElement"/>的索引.</param>
        public HandlerElement GetItemAt(int index)
        {
            return ((HandlerElement)(base.BaseGet(index)));
        }

        /// <summary>
        /// 用指定的键获取<see cref ="HandlerElement"/>.
        /// </summary>
        /// <param name="name">The key of the <see cref="HandlerElement"/> to retrieve.</param>
        public HandlerElement GetItemByKey(string name)
        {
            return ((HandlerElement)(base.BaseGet(((object)(name)))));
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
