﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OFoods.Utility.Extensions;

namespace OFoods.Domain.Entities
{
    /// <summary>
    /// 可持久化到数据库的数据模型基类
    /// </summary>
    /// <typeparam name="TKey">主键数据类型</typeparam>
    public abstract class EntityBase<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 初始化一个<see cref="EntityBase{TKey}"/>类型的新实例
        /// </summary>
        protected EntityBase()
        {
            if (typeof(TKey) == typeof(Guid))
            {
                ID = CombHelper.NewComb().CastTo<TKey>();
            }
        }

        #region 属性

        /// <summary>
        /// 获取或设置 实体唯一标识，主键
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("主键")]
        public TKey ID { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 判断两个实体是否是同一数据记录的实体
        /// </summary>
        /// <param name="obj">要比较的实体信息</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            //if (obj == null)
            //{
            //    return false;
            //}
            //EntityBase<TKey> entity = obj as EntityBase<TKey>;
            //if (entity == null)
            //{
            //    return false;
            //}
            //return entity.Id.Equals(Id);
            return Equals(obj as EntityBase<TKey>);
        }
        /// <summary>
        /// 根据实体标示符判断实体是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(EntityBase<TKey> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(ID, other.ID))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }

            return false;
        }
        private static bool IsTransient(EntityBase<TKey> obj)
        {
            return obj != null && Equals(obj.ID, default(int));
        }
        /// <summary>
        /// 获取当前实例的Type
        /// </summary>
        /// <returns></returns>
        private Type GetUnproxiedType()
        {
            return GetType();
        }
        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:System.Object"/> 的哈希代码。<br/>
        /// 如果<c>Id</c>为<c>null</c>则返回0，
        /// 如果不为<c>null</c>则返回<c>Id</c>对应的哈希值
        /// </returns>
        public override int GetHashCode()
        {
            if (ID == null)
            {
                return 0;
            }
            return ID.ToString().GetHashCode();
        }

        #endregion
    }
}
