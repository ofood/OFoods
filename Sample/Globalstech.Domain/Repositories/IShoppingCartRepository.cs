using Globalstech.Domain.Model;
using OFoods.Domain.Repositories;

namespace Globalstech.Domain.Repositories
{
    /// <summary>
    /// 表示用于“购物篮”聚合根的仓储接口。
    /// </summary>
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        /// <summary>
        /// 根据指定的用户，查找该用户的购物篮。
        /// </summary>
        /// <param name="user">用户。</param>
        /// <returns>购物篮。</returns>
        ShoppingCart FindShoppingCartByUser(User user);
    }
}
