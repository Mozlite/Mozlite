﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Mozlite.Extensions.Identity;

namespace Mozlite.Extensions.Security
{
    /// <summary>
    /// 服务扩展类。
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// 添加用户服务。
        /// </summary>
        /// <param name="services">服务集合。</param>
        /// <param name="action">用户服务操作。</param>
        /// <returns>返回服务集合实例对象。</returns>
        public static IServiceCollection AddIdentity(this IServiceCollection services, Action<IdentityBuilder> action = null)
        {
            var builder = services.AddIdentity<User, Role>()
                .AddIdentityStores<UserStore, RoleStore>()
                .AddDefaultTokenProviders();
            action?.Invoke(builder);
            return services;
        }
    }
}