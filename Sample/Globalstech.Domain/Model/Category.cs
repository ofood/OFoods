using OFoods.Domain;
using System;


namespace Globalstech.Domain.Model
{
    /// <summary>
    /// 表示商品类别的对象。
    /// </summary>
    public class Category : IAggregateRoot
    {
        /// <summary>
        /// 初始化一个<c>Category</c>类型的实例。
        /// </summary>
        public Category() { }
        /// <summary>
        /// 初始化一个<c>Category</c>类型的实例。
        /// </summary>
        /// <param name="name">商品分类的名称。</param>
        /// <param name="description">商品分类的描述信息。</param>
        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置商品分类的名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置商品分类的描述信息。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 返回表示当前Object的字符串。
        /// </summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
