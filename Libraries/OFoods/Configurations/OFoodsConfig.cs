using System;
using System.Configuration;
using System.Xml;

namespace OFoods.Configuration
{
    /// <summary>
    /// ����һ��OFoodsConfig
    /// </summary>
    public partial class OFoodsConfig : IConfigurationSectionHandler
    {
        /// <summary>
        /// �������ýڴ������.
        /// </summary>
        /// <param name="parent">Parent object.</param>
        /// <param name="configContext">Configuration context object.</param>
        /// <param name="section">Section XML node.</param>
        /// <returns>The created section handler object.</returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new OFoodsConfig();

            var startupNode = section.SelectSingleNode("Startup");
            config.IgnoreStartupTasks = GetBool(startupNode, "IgnoreStartupTasks");
           
            var redisCachingNode = section.SelectSingleNode("RedisCaching");
            config.RedisCachingEnabled = GetBool(redisCachingNode, "Enabled");
            config.RedisCachingConnectionString = GetString(redisCachingNode, "ConnectionString");

            var userAgentStringsNode = section.SelectSingleNode("UserAgentStrings");
            config.UserAgentStringsPath = GetString(userAgentStringsNode, "databasePath");
            config.CrawlerOnlyUserAgentStringsPath = GetString(userAgentStringsNode, "crawlersOnlyDatabasePath");

            var supportPreviousNopcommerceVersionsNode = section.SelectSingleNode("SupportPreviousNopcommerceVersions");
            config.SupportPreviousNopcommerceVersions = GetBool(supportPreviousNopcommerceVersionsNode, "Enabled");
            
            var webFarmsNode = section.SelectSingleNode("WebFarms");
            config.MultipleInstancesEnabled = GetBool(webFarmsNode, "MultipleInstancesEnabled");
            config.RunOnAzureWebApps = GetBool(webFarmsNode, "RunOnAzureWebApps");

            var azureBlobStorageNode = section.SelectSingleNode("AzureBlobStorage");
            config.AzureBlobStorageConnectionString = GetString(azureBlobStorageNode, "ConnectionString");
            config.AzureBlobStorageContainerName = GetString(azureBlobStorageNode, "ContainerName");
            config.AzureBlobStorageEndPoint = GetString(azureBlobStorageNode, "EndPoint");

            var installationNode = section.SelectSingleNode("Installation");
            config.DisableSampleDataDuringInstallation = GetBool(installationNode, "DisableSampleDataDuringInstallation");
            config.UseFastInstallationService = GetBool(installationNode, "UseFastInstallationService");
            config.PluginsIgnoredDuringInstallation = GetString(installationNode, "PluginsIgnoredDuringInstallation");

            return config;
        }

        private string GetString(XmlNode node, string attrName)
        {
            return SetByXElement<string>(node, attrName, Convert.ToString);
        }

        private bool GetBool(XmlNode node, string attrName)
        {
            return SetByXElement<bool>(node, attrName, Convert.ToBoolean);
        }

        private T SetByXElement<T>(XmlNode node, string attrName, Func<string, T> converter)
        {
            if (node == null || node.Attributes == null) return default(T);
            var attr = node.Attributes[attrName];
            if (attr == null) return default(T);
            var attrVal = attr.Value;
            return converter(attrVal);
        }

        /// <summary>
        ///ָʾ�Ƿ������������
        /// </summary>
        public bool IgnoreStartupTasks { get; private set; }

        /// <summary>
        /// ʹ���û������ַ��������ݿ�·��
        /// </summary>
        public string UserAgentStringsPath { get; private set; }

        /// <summary>
        ///��ʹ�������û������ַ��������ݿ�·��
        /// </summary>
        public string CrawlerOnlyUserAgentStringsPath { get; private set; }



        /// <summary>
        /// ָʾ�Ƿ�Ӧ��ʹ��Redis���������л��棨������Ĭ�ϵ��ڴ��л��棩
        /// </summary>
        public bool RedisCachingEnabled { get; private set; }
        /// <summary>
        /// Redis�����ַ����� ������Redis����ʱʹ��
        /// </summary>
        public string RedisCachingConnectionString { get; private set; }



        /// <summary>
        /// ָ���Ƿ�Ӧ��֧����ǰ��nopCommerce�汾����������΢������ܣ�
        /// </summary>
        public bool SupportPreviousNopcommerceVersions { get; private set; }



        /// <summary>
        /// ָʾվ���Ƿ��ڶ��ʵ�������е�ֵ�����磬Web�������ж��ʵ����Windows Azure�ȣ���
        /// �����������Azure�ϵ���ʹ��һ��ʵ�����벻Ҫ������
        /// </summary>
        public bool MultipleInstancesEnabled { get; private set; }

        /// <summary>
        /// ָʾվ���Ƿ���Windows Azure Web Apps�����е�ֵ
        /// </summary>
        public bool RunOnAzureWebApps { get; private set; }

        /// <summary>
        /// Azure BLOB�洢�������ַ���
        /// </summary>
        public string AzureBlobStorageConnectionString { get; private set; }
        /// <summary>
        /// Azure BLOB�洢����������
        /// </summary>
        public string AzureBlobStorageContainerName { get; private set; }
        /// <summary>
        /// Azure BLOB�洢���յ�
        /// </summary>
        public string AzureBlobStorageEndPoint { get; private set; }


        /// <summary>
        /// һ��ֵ��ָʾ�����Ƿ�����ڰ�װ�����а�װ��������
        /// </summary>
        public bool DisableSampleDataDuringInstallation { get; private set; }
        /// <summary>
        /// Ĭ������£�������Ӧʼ������Ϊ��False�����������ڸ߼��û���
        /// </summary>
        public bool UseFastInstallationService { get; private set; }
        /// <summary>
        /// ��nopCommerce��װ�ڼ���ԵĲ���б�
        /// </summary>
        public string PluginsIgnoredDuringInstallation { get; private set; }
    }
}
