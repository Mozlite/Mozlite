﻿using Microsoft.AspNetCore.Identity;
using Mozlite.Extensions.Identity;
using Mozlite.Data;

namespace Mozlite.Extensions.Security.Data
{
    /// <summary>
    /// 用户数据存储。
    /// </summary>
    public class UserStore : IdentityUserStore<User, Role, UserClaim, UserRole, UserLogin, UserToken>
    {
        /// <summary>
        /// 初始化类<see cref="UserStore"/>。
        /// </summary>
        /// <param name="users">用户数据操作接口实例。</param>
        /// <param name="userClaims">用户声明数据操作接口实例。</param>
        /// <param name="userRoles">用户组数据操作接口实例。</param>
        /// <param name="userTokens">用户标识数据操作接口实例。</param>
        /// <param name="roles">用户组数据操作接口实例。</param>
        /// <param name="userLogins">用户登陆数据操作接口实例。</param>
        /// <param name="describer">错误描述实例。</param>
        public UserStore(IRepository<User> users, IRepository<UserClaim> userClaims, IRepository<UserRole> userRoles, IRepository<UserToken> userTokens, IRepository<Role> roles, IRepository<UserLogin> userLogins, IdentityErrorDescriber describer = null) : base(users, userClaims, userRoles, userTokens, roles, userLogins, describer)
        {
        }
    }
}