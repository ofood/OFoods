using Globalstech.Domain.Model;
using OFoods.Domain.Repositories;

namespace Globalstech.Domain.Repositories
{
    /// <summary>
    /// 表示用于“用户角色关系”聚合根的仓储接口。
    /// </summary>
    public interface IUserRoleRepository: IRepository<UserRole>
    {
        #region 方法
        /// <summary>
        /// 根据指定的用户，获取该用户所属的角色。
        /// </summary>
        /// <param name="user">用户。</param>
        /// <returns>角色。</returns>
        Role GetRoleForUser(User user);
        #endregion
    }
}
