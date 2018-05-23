using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// InterceptMethodElement实例的集合.
    /// </summary>
    [ConfigurationCollection(typeof(InterceptMethodElement), CollectionType = ConfigurationElementCollectionType.BasicMapAlternate, AddItemName = InterceptMethodElementCollection.InterceptMethodElementPropertyName)]
    public class InterceptMethodElementCollection:ConfigurationElementCollection
    {
        #region Constants
        /// <summary>
        /// 此集合中的个人<see cref ="InterceptMethodElement"/>实例的XML名称.
        /// </summary>
        internal const string InterceptMethodElementPropertyName = "method";
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
                return InterceptMethodElementPropertyName;
            }
        }

        /// <summary>
        /// 指示<see cref ="ConfigurationElementCollection"/>是否存在指定的<see cref ="ConfigurationElement"/>.
        /// </summary>
        /// <param name="elementName">要验证的元素的名称.</param>
        /// <returns>
        /// <see langword="true"/> 如果元素存在于集合中; 否则，<see langword ="false"/>.
        /// </returns>
        protected override bool IsElementName(string elementName)
        {
            return (elementName == InterceptMethodElementPropertyName);
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
            return ((InterceptMethodElement)(element)).Signature;
        }

        /// <summary>
        /// 创建一个新的<see cref ="InterceptMethodElement"/>.
        /// </summary>
        /// <returns>
        /// 新的<see cref ="InterceptMethodElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new InterceptMethodElement();
        }
        #endregion

        #region Indexer
        /// <summary>
        /// 获取指定索引处的<see cref ="InterceptMethodElement"/>.
        /// </summary>
        /// <param name="index">要检索的<see cref ="InterceptMethodElement"/>的索引.</param>
        public InterceptMethodElement this[int index]
        {
            get
            {
                return ((InterceptMethodElement)(base.BaseGet(index)));
            }
        }

        /// <summary>
        /// 使用指定的键获取<see cref ="InterceptMethodElement"/>.
        /// </summary>
        /// <param name="signature"><see cref ="InterceptMethodElement"/>检索的关键.</param>
        public InterceptMethodElement this[object signature]
        {
            get
            {
                return ((InterceptMethodElement)(base.BaseGet(signature)));
            }
        }
        #endregion

        #region Add
        /// <summary>
        /// 将指定的<see cref ="InterceptMethodElement"/>添加到<see cref ="ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="method"><see cref ="InterceptMethodElement"/>添加.</param>
        public void Add(InterceptMethodElement method)
        {
            base.BaseAdd(method);
        }
        #endregion

        #region Remove
        /// <summary>
        /// 从<see cref ="ConfigurationElementCollection"/>中删除指定的<see cref ="InterceptMethodElement"/>.
        /// </summary>
        /// <param name="method"><see cref ="InterceptMethodElement"/>删除.</param>
        public void Remove(InterceptMethodElement method)
        {
            base.BaseRemove(this.GetElementKey(method));
        }
        #endregion

        #region GetItem
        /// <summary>
        /// 获取指定索引处的<see cref ="InterceptMethodElement"/>.
        /// </summary>
        /// <param name="index">要检索的<see cref ="InterceptMethodElement"/>的索引.</param>
        public InterceptMethodElement GetItemAt(int index)
        {
            return ((InterceptMethodElement)(base.BaseGet(index)));
        }

        /// <summary>
        /// 使用指定的键获取<see cref ="InterceptMethodElement"/>.
        /// </summary>
        /// <param name="signature"><see cref ="InterceptMethodElement"/>检索的关键.</param>
        public InterceptMethodElement GetItemByKey(string signature)
        {
            return ((InterceptMethodElement)(base.BaseGet(((object)(signature)))));
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
