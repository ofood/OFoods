using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 表示拦截合同的配置.
    /// </summary>
    [ConfigurationCollection(typeof(InterceptContractElement), CollectionType = ConfigurationElementCollectionType.BasicMapAlternate, AddItemName = InterceptContractElementPropertyName)]
    public  class InterceptContractElementCollection : ConfigurationElementCollection
    {

        #region Constants
        /// <summary>
        /// 此集合中的个人<see cref ="InterceptContractElement"/>实例的XML名称.
        /// </summary>
        internal const string InterceptContractElementPropertyName = "contract";
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
                return InterceptContractElementPropertyName;
            }
        }

        /// <summary>
        /// 指示<see cref ="ConfigurationElementCollection"/>是否存在指定的<see cref ="ConfigurationElement"/>.
        /// </summary>
        /// <param name="elementName">要验证的元素的名称.</param>
        /// <returns>
        /// <see langword="true"/> 如果元素存在于集合中; 除此以外， <see langword="false"/>.
        /// </returns>
        protected override bool IsElementName(string elementName)
        {
            return (elementName == InterceptContractElementPropertyName);
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
            return ((InterceptContractElement)(element)).Type;
        }

        /// <summary>
        /// 创建一个新的<see cref ="InterceptContractElement"/>.
        /// </summary>
        /// <returns>
        /// 新的 <see cref="InterceptContractElement"/>.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new InterceptContractElement();
        }
        #endregion

        #region Indexer
        /// <summary>
        /// 获取指定索引处的<see cref ="InterceptContractElement"/>.
        /// </summary>
        /// <param name="index">要检索的<see cref ="InterceptContractElement"/>的索引.</param>
        public InterceptContractElement this[int index]
        {
            get
            {
                return ((InterceptContractElement)(base.BaseGet(index)));
            }
        }

        /// <summary>
        /// 使用指定的键获取<see cref ="InterceptContractElement"/>.
        /// </summary>
        /// <param name="type"><see cref ="InterceptContractElement"/>检索的关键.</param>
        public InterceptContractElement this[object type]
        {
            get
            {
                return ((InterceptContractElement)(base.BaseGet(type)));
            }
        }
        #endregion

        #region Add
        /// <summary>
        /// 将指定的<see cref ="InterceptContractElement"/>添加到<see cref ="ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="contract"><see cref="InterceptContractElement"/> 添加.</param>
        public void Add(InterceptContractElement contract)
        {
            base.BaseAdd(contract);
        }
        #endregion

        #region Remove
        /// <summary>
        /// 从<see cref ="ConfigurationElementCollection"/>中删除指定的<see cref ="InterceptContractElement"/>.
        /// </summary>
        /// <param name="contract"><see cref ="InterceptContractElement"/>删除.</param>
        public void Remove(InterceptContractElement contract)
        {
            base.BaseRemove(this.GetElementKey(contract));
        }
        #endregion

        #region GetItem
        /// <summary>
        /// 获取指定索引处的<see cref ="InterceptContractElement"/>.
        /// </summary>
        /// <param name="index">要检索的<see cref ="InterceptContractElement"/>的索引.</param>
        public InterceptContractElement GetItemAt(int index)
        {
            return ((InterceptContractElement)(base.BaseGet(index)));
        }

        /// <summary>
        /// 使用指定的键获取<see cref ="InterceptContractElement"/>.
        /// </summary>
        /// <param name="type"><see cref ="InterceptContractElement"/>检索的关键.</param>
        public InterceptContractElement GetItemByKey(string type)
        {
            return ((InterceptContractElement)(base.BaseGet(((object)(type)))));
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
