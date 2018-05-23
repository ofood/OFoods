using System.ComponentModel;
namespace OFoods.Configurations.Fluent
{
    public interface IConfigurator<TContainer>
    {
        /// <summary>
        /// 配置容器.
        /// </summary>
        /// <returns>已配置的容器.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        TContainer Configure();
    }
}
