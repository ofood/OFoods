using System;
using System.Collections.Generic;
using System.Configuration;

namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 表示拦截器参考的配置.
    /// </summary>
    [ConfigurationCollection(typeof(InterceptorRefElement), CollectionType = ConfigurationElementCollectionType.BasicMapAlternate, AddItemName = InterceptorRefElementPropertyName)]
    public class InterceptorRefElementCollection : ConfigurationElementCollection
    {

        #region Constants
        /// <summary>
        /// 此集合中的个人<see cref ="InterceptorRefElement"/>实例的XML名称.
        /// </summary>
        internal const string InterceptorRefElementPropertyName = "interceptorRef";
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
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }

        /// <summary>
        /// 获取用于标识此元素集合的名称
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return InterceptorRefElementPropertyName;
            }
        }

        /// <summary>
        /// 指示<see cref ="ConfigurationElementCollection"/>中是否存在指定的<see cref ="ConfigurationElement"/>.
        /// </summary>
        /// <param name="elementName">要验证的元素的名称.</param>
        /// <returns>
        /// <see langword="true"/> 如果元素存在于集合中; 否则，<see langword ="false"/>.
        /// </returns>
        protected override bool IsElementName(string elementName)
        {
            return (elementName == InterceptorRefElementPropertyName);
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
            return ((InterceptorRefElement)(element)).Name;
        }

        /// <summary>
        /// 创建一个新的 <see cref="InterceptorRefElement"/>.
        /// </summary>
        /// <returns>
        /// 一个新的 <see cref="InterceptorRefElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new InterceptorRefElement();
        }
        #endregion

        #region Indexer
        /// <summary>
        /// 获取指定索引处的<see cref ="InterceptorRefElement"/>.
        /// </summary>
        /// <param name="index">要检索的<see cref ="InterceptorRefElement"/>的索引.</param>
        public InterceptorRefElement this[int index]
        {
            get
            {
                return ((InterceptorRefElement)(base.BaseGet(index)));
            }
        }

        /// <summary>
        /// 使用指定的键获取<see cref ="InterceptorRefElement"/>.
        /// </summary>
        /// <param name="name"><see cref ="InterceptorRefElement"/>检索的关键.</param>
        public InterceptorRefElement this[object name]
        {
            get
            {
                return ((InterceptorRefElement)(base.BaseGet(name)));
            }
        }
        #endregion

        #region Add
        /// <summary>
        /// 将指定的<see cref ="InterceptorRefElement"/>添加到<see cref ="ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="interceptorRef"><see cref ="InterceptorRefElement"/>添加.</param>
        public void Add(InterceptorRefElement interceptorRef)
        {
            base.BaseAdd(interceptorRef);
        }
        #endregion

        #region Remove
        /// <summary>
        /// 从<see cref ="ConfigurationElementCollection"/>中删除指定的<see cref ="InterceptorRefElement"/>.
        /// </summary>
        /// <param name="interceptorRef"><see cref ="InterceptorRefElement"/>删除.</param>
        public void Remove(InterceptorRefElement interceptorRef)
        {
            base.BaseRemove(this.GetElementKey(interceptorRef));
        }
        #endregion

        #region GetItem
        /// <summary>
        /// 获取指定索引处的<see cref ="InterceptorRefElement"/>.
        /// </summary>
        /// <param name="index">The index of the <see cref="InterceptorRefElement"/> to retrieve.</param>
        public InterceptorRefElement GetItemAt(int index)
        {
            return ((InterceptorRefElement)(base.BaseGet(index)));
        }

        /// <summary>
        /// 使用指定的键获取<see cref ="InterceptorRefElement"/>.
        /// </summary>
        /// <param name="name"><see cref ="InterceptorRefElement"/>检索的关键.</param>
        public InterceptorRefElement GetItemByKey(string name)
        {
            return ((InterceptorRefElement)(base.BaseGet(((object)(name)))));
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
