﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Mozlite.Extensions.Identity
{
    /// <summary>
    /// 用户管理接口。
    /// </summary>
    /// <typeparam name="TUser">当前用户类。</typeparam>
    public interface IIdentityUserManager<TUser> where TUser : IdentityUser, new()
    {
        /// <summary>
        /// 新建用户，不包含密码。
        /// </summary>
        /// <param name="user">当前用户实例。</param>
        /// <param name="roleName">用户组名称。</param>
        /// <returns>返回新建结果。</returns>
        Task<IdentityResult> CreateAsync(TUser user, string roleName);

        /// <summary>
        /// 新建用户。
        /// </summary>
        /// <param name="user">当前用户实例。</param>
        /// <param name="password">用户密码。</param>
        /// <param name="roleName">用户组名称。</param>
        /// <returns>返回新建结果。</returns>
        Task<IdentityResult> CreateAsync(TUser user, string password, string roleName);

        /// <summary>
        /// 新建用户。
        /// </summary>
        /// <param name="user">当前用户实例。</param>
        /// <param name="password">用户密码。</param>
        /// <param name="roles">用户组名称。</param>
        /// <returns>返回新建结果。</returns>
        Task<IdentityResult> CreateAsync(TUser user, string password, IEnumerable<string> roles);

        /// <summary>
        /// 获取当前用户声明的所有用户列表。
        /// </summary>
        /// <param name="claim">当前用户声明实例。</param>
        /// <returns>返回当前用户声明的所有用户列表。</returns>
        Task<IList<TUser>> GetUsersForClaimAsync(Claim claim);

        /// <summary>
        /// 获取当前用户组的所有用户列表。
        /// </summary>
        /// <param name="roleName">当前用户组名称。</param>
        /// <returns>返回当前用户组的所有用户列表。</returns>
        Task<IList<TUser>> GetUsersInRoleAsync(string roleName);

        /// <summary>
        /// 异步修改密码。
        /// </summary>
        /// <param name="user">当前用户实例。</param>
        /// <param name="currentPassword">当前密码。</param>
        /// <param name="newPassword">新密码。</param>
        /// <returns>返回修改结果。</returns>
        Task<IdentityResult> ChangePasswordAsync(TUser user, string currentPassword, string newPassword);

        /// <summary>
        /// 重置密码。
        /// </summary>
        /// <param name="user">用户实例。</param>
        /// <param name="token">用户验证标志。</param>
        /// <param name="newPassword">用户新密码。</param>
        /// <returns>返回重置结果。</returns>
        Task<IdentityResult> ResetPasswordAsync(TUser user, string token, string newPassword);

        /// <summary>
        /// 重置密码。
        /// </summary>
        /// <param name="user">用户实例。</param>
        /// <param name="newPassword">用户新密码。</param>
        /// <returns>返回重置结果。</returns>
        Task<IdentityResult> ResetPasswordAsync(TUser user, string newPassword);

        /// <summary>
        /// 通过Id查找用户。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <returns>返回当前用户实例。</returns>
        Task<TUser> FindByIdAsync(string userId);

        /// <summary>
        /// 通过Id查找用户。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <returns>返回当前用户实例。</returns>
        Task<TUser> FindByIdAsync(int userId);

        /// <summary>
        /// 通过名称查找用户。
        /// </summary>
        /// <param name="userName">用户名称。</param>
        /// <returns>返回当前用户实例。</returns>
        Task<TUser> FindByNameAsync(string userName);

        /// <summary>
        /// 通过电子邮件查找用户。
        /// </summary>
        /// <param name="email">电子邮件。</param>
        /// <returns>返回当前用户实例。</returns>
        Task<TUser> FindByEmailAsync(string email);

        /// <summary>
        /// 通过登陆类型查找用户。
        /// </summary>
        /// <param name="loginProvider">登陆类型。</param>
        /// <param name="providerKey">登陆唯一键。</param>
        /// <returns>返回当前用户实例。</returns>
        Task<TUser> FindByLoginAsync(string loginProvider, string providerKey);

        /// <summary>
        /// 登陆成功后写入登陆IP和登陆时间。
        /// </summary>
        /// <param name="userName">登陆用户名称。</param>
        /// <returns>返回设置是否成功。</returns>
        bool SignInSuccess(string userName);

        /// <summary>
        /// 登陆成功后写入登陆IP和登陆时间。
        /// </summary>
        /// <param name="userName">登陆用户名称。</param>
        /// <returns>返回设置是否成功。</returns>
        Task<bool> SignInSuccessAsync(string userName);

        /// <summary>
        /// 锁定用户。
        /// </summary>
        /// <param name="userIds">用户Id集合。</param>
        /// <param name="lockoutEnd">锁定过期时间。</param>
        /// <returns>返回锁定结果。</returns>
        bool LockoutUsers(int[] userIds, DateTime lockoutEnd);

        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="userIds">用户Id集合。</param>
        /// <returns>返回删除结果。</returns>
        bool DeleteUsers(int[] userIds);
        
        /// <summary>
        /// 锁定用户。
        /// </summary>
        /// <param name="userIds">用户Id集合。</param>
        /// <param name="lockoutEnd">锁定过期时间。</param>
        /// <returns>返回锁定结果。</returns>
        Task<bool> LockoutUsersAsync(int[] userIds, DateTime lockoutEnd);

        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="userIds">用户Id集合。</param>
        /// <returns>返回删除结果。</returns>
        Task<bool> DeleteUsersAsync(int[] userIds);

        /// <summary>
        /// 添加用户登录实例。
        /// </summary>
        /// <param name="user">当前用户。</param>
        /// <param name="login">登陆信息。</param>
        /// <returns>返回操作结果。</returns>
        Task<IdentityResult> AddLoginAsync(TUser user, UserLoginInfo login);

        /// <summary>
        /// 确认邮件地址。
        /// </summary>
        /// <param name="user">当前用户。</param>
        /// <param name="token">确认邮件生成的字符串。</param>
        /// <returns>返回确认结果。</returns>
        Task<IdentityResult> ConfirmEmailAsync(TUser user, string token);

        /// <summary>
        /// 判断当前用户的邮件是否已经确认。
        /// </summary>
        /// <param name="user">当前用户。</param>
        /// <returns>返回判断结果。</returns>
        Task<bool> IsEmailConfirmedAsync(TUser user);

        /// <summary>
        /// 获取验证提供者名称列表。
        /// </summary>
        /// <param name="user">当前用户。</param>
        /// <returns>返回验证提供者名称列表。</returns>
        Task<IList<string>> GetValidTwoFactorProvidersAsync(TUser user);

        /// <summary>
        /// 生成验证标识字符串。
        /// </summary>
        /// <param name="user">当前用户。</param>
        /// <param name="tokenProvider">验证提供者。</param>
        /// <returns>返回生产的代码字符串。</returns>
        Task<string> GenerateTwoFactorTokenAsync(TUser user, string tokenProvider);

        /// <summary>
        /// 获取当前用户的电子邮件地址。
        /// </summary>
        /// <param name="user">当前用户。</param>
        /// <returns>当前用户的电子邮件地址。</returns>
        Task<string> GetEmailAsync(TUser user);

        /// <summary>
        /// 获取电话号码。
        /// </summary>
        /// <param name="user">当前用户。</param>
        /// <returns>电话号码。</returns>
        Task<string> GetPhoneNumberAsync(TUser user);

        /// <summary>
        /// 获取当前登录用户。
        /// </summary>
        /// <returns>返回当前登录用户实例。</returns>
        Task<TUser> GetUserAsync();

        /// <summary>
        /// 获取当前登录用户。
        /// </summary>
        /// <returns>返回当前登录用户实例。</returns>
        TUser GetUser();
    }
}