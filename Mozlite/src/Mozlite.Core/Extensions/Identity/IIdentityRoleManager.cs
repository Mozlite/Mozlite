﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Mozlite.Extensions.Identity
{
    /// <summary>
    /// 用户组管理接口。
    /// </summary>
    /// <typeparam name="TRole">用户组类型。</typeparam>
    public interface IIdentityRoleManager<TRole>
        where TRole : IdentityRole, new()
    {
        /// <summary>
        /// 添加用户组声明。
        /// </summary>
        /// <param name="roleId">用户组ID。</param>
        /// <param name="claim">声明实例对象。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回添加任务实例。</returns>
        Task AddClaimAsync(int roleId, Claim claim, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 新建用户组。
        /// </summary>
        /// <param name="role">用户组实例对象。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回新建结果。</returns>
        Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 删除用户组。
        /// </summary>
        /// <param name="roleId">用户组ID。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回删除结果。</returns>
        Task<IdentityResult> DeleteAsync(int roleId, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// 查询用户组。
        /// </summary>
        /// <param name="normalizedRoleName">用户组名称。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回用户组实例对象。</returns>
        Task<TRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 查询用户组。
        /// </summary>
        /// <param name="roleId">用户组ID。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回用户组实例对象。</returns>
        Task<TRole> FindByIdAsync(int roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 查询用户组。
        /// </summary>
        /// <param name="roleName">用户组名称。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回用户组实例对象。</returns>
        Task<TRole> FindByRoleNameAsync(string roleName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 获取当前用户组的声明列表实例。
        /// </summary>
        /// <param name="roleId">用户组ID。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回用户组声明列表实例。</returns>
        Task<IList<Claim>> GetClaimsAsync(int roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 移除当前用户组的一个声明实例。
        /// </summary>
        /// <param name="roleId">用户组ID。</param>
        /// <param name="claim">声明实例。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回移除声明的任务。</returns>
        Task RemoveClaimAsync(int roleId, Claim claim, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 更新用户组。
        /// </summary>
        /// <param name="role">用户组实例对象。</param>
        /// <param name="cancellationToken">取消标志。</param>
        /// <returns>返回更新结果。</returns>
        Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken = default(CancellationToken));
    }
}