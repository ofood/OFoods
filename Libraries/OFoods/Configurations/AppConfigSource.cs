using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace OFoods.Configurations
{
    /// <summary>
    /// 表示使用应用程序/ Web配置文件的配置源.
    /// </summary>
    public class AppConfigSource : IConfigSource
    {
        private OFoodsConfigSection config;
        /// <summary>
        /// 表示OFood框架使用的配置节的默认名称.
        /// </summary>
        public const string DefaultConfigSection = "ofood";

        /// <summary>
        /// 初始化<c> AppConfigSource </c>类的新实例.
        /// </summary>
        public AppConfigSource()
        {
            string configSection = DefaultConfigSection;
            try
            {
                object[] apworksConfigAttributes = typeof(OFoodsConfigSection).GetCustomAttributes(false);
                if (apworksConfigAttributes.Any(p => p.GetType().Equals(typeof(System.Xml.Serialization.XmlRootAttribute))))
                {
                    System.Xml.Serialization.XmlRootAttribute xmlRootAttribute = (System.Xml.Serialization.XmlRootAttribute)
                        apworksConfigAttributes.SingleOrDefault(p => p.GetType().Equals(typeof(System.Xml.Serialization.XmlRootAttribute)));
                    if (!string.IsNullOrEmpty(xmlRootAttribute.ElementName) &&
                        !string.IsNullOrWhiteSpace(xmlRootAttribute.ElementName))
                    {
                        configSection = xmlRootAttribute.ElementName;
                    }
                }
            }
            catch // 如果发生任何异常，则禁止使用默认配置节的异常.
            {
            }
            LoadConfig(configSection);
        }
        /// <summary>
        /// 初始化<c> AppConfigSource </ c>类的新实例.
        /// </summary>
        /// <param name="configSectionName">配置节点的名称.</param>
        public AppConfigSource(string configSectionName)
        {
            LoadConfig(configSectionName);
        }

        #region 私有方法
        private void LoadConfig(string configSection)
        {
            config = (OFoodsConfigSection)ConfigurationManager.GetSection(configSection);
        }
        #endregion

        #region IConfigSource 成员
        /// <summary>
        /// 获取<see cref ="OFoodConfigSection"/>类的实例.
        /// </summary>
        public OFoodsConfigSection Config
        {
            get { return config; }
        }

        #endregion
    }
}
