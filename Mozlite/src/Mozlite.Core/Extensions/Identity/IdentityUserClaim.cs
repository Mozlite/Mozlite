﻿using Mozlite.Data.Metadata;

namespace Mozlite.Extensions.Identity
{
    /// <summary>
    /// 用户声明实例类。
    /// </summary>
    [Table("Users_Claims")]
    public class IdentityUserClaim
    {
        /// <summary>
        /// 标识ID。
        /// </summary>
        [Identity]
        public int Id { get; set; }

        /// <summary>
        /// 用户ID。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 声明类型。
        /// </summary>
        [Size(256)]
        public virtual string ClaimType { get; set; }

        /// <summary>
        /// 声明值。
        /// </summary>
        public virtual string ClaimValue { get; set; }
    }
}