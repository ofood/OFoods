using System.Configuration;
using System.ComponentModel;
namespace OFoods.Configurations.Elements
{
    /// <summary>
    /// 表示事件或快照序列化器的配置.
    /// </summary>
    public partial class SerializerElement : ConfigurationElement
    {

        #region IsReadOnly override
        /// <summary>
        /// 获取一个值，指示元素是否是只读的.
        /// </summary>
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion

        #region EventSerializer属性
        /// <summary>
        /// <see cref ="EventSerializer"/>属性的XML名称.
        /// </summary>
        internal const string EventSerializerPropertyName = "eventSerializer";

        /// <summary>
        /// 获取或设置事件序列化程序的配置.
        /// </summary>
        [Description("The configuration for the event serializer.")]
        [ConfigurationProperty(EventSerializerPropertyName, IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        public virtual EventSerializerElement EventSerializer
        {
            get
            {
                return ((EventSerializerElement)(base[EventSerializerPropertyName]));
            }
            set
            {
                base[EventSerializerPropertyName] = value;
            }
        }
        #endregion

        #region 快照序列化器属性
        /// <summary>
        /// <see cref ="SnapshotSerializer"/>属性的XML名称.
        /// </summary>
        internal const string SnapshotSerializerPropertyName = "snapshotSerializer";

        /// <summary>
        /// 获取或设置快照序列化程序的配置.
        /// </summary>
        [Description("快照序列化程序的配置.")]
        [ConfigurationProperty(SnapshotSerializerPropertyName, IsRequired = true, IsKey = false, IsDefaultCollection = false)]
        public virtual SnapshotSerializerElement SnapshotSerializer
        {
            get
            {
                return ((SnapshotSerializerElement)(base[SnapshotSerializerPropertyName]));
            }
            set
            {
                base[SnapshotSerializerPropertyName] = value;
            }
        }
        #endregion
    }
}
