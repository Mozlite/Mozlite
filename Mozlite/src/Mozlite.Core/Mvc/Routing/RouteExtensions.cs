﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Mozlite.Mvc.Controllers;

namespace Mozlite.Mvc.Routing
{
    /// <summary>
    /// 路由扩展类。
    /// </summary>
    public static class RouteExtensions
    {
        /// <summary>
        /// 添加<see cref="UserControllerBase"/>和<see cref="AdminControllerBase"/>为积累控制器的导航。
        /// </summary>
        /// <param name="builder">MVC构建实例对象。</param>
        /// <returns>返回MVC构建实例对象。</returns>
        public static IMvcBuilder AddControllerRoutes(this IMvcBuilder builder)
        {
            var routes = new ControllerRouteCollection();
            ControllerFeature feature = new ControllerFeature();
            builder.PartManager.PopulateFeature(feature);
            foreach (var info in feature.Controllers)
            {
                var area = info.GetCustomAttribute<AreaAttribute>(true);
                if (typeof(UserControllerBase).GetTypeInfo().IsAssignableFrom(info))
                    routes.AddRoute(RouteType.User, info.Name, area?.RouteValue);
                else if (typeof(AdminControllerBase).GetTypeInfo().IsAssignableFrom(info))
                    routes.AddRoute(RouteType.Backend, info.Name, area?.RouteValue);
            }
            builder.Services.AddTransient(typeof(ControllerRouteCollection), service => routes);
            return builder;
        }

        /// <summary>
        /// 路由地址。
        /// </summary>
        /// <param name="routeBuilder">路由实例化接口。</param>
        /// <param name="name">名称。</param>
        /// <param name="template">模板。</param>
        /// <returns>返回当前路由实例化对象。</returns>
        public static IRouteBuilder MapLowerCaseRoute(this IRouteBuilder routeBuilder, string name, string template)
        {
            routeBuilder.MapLowerCaseRoute(name, template, (object)null);
            return routeBuilder;
        }

        /// <summary>
        /// 路由地址。
        /// </summary>
        /// <param name="routeBuilder">路由实例化接口。</param>
        /// <param name="name">名称。</param>
        /// <param name="template">模板。</param>
        /// <param name="defaults">默认值。</param>
        /// <returns>返回当前路由实例化对象。</returns>
        public static IRouteBuilder MapLowerCaseRoute(this IRouteBuilder routeBuilder, string name, string template, object defaults)
        {
            return routeBuilder.MapLowerCaseRoute(name, template, defaults, (object)null);
        }

        /// <summary>
        /// 路由地址。
        /// </summary>
        /// <param name="routeBuilder">路由实例化接口。</param>
        /// <param name="name">名称。</param>
        /// <param name="template">模板。</param>
        /// <param name="defaults">默认值。</param>
        /// <param name="constraints">约束表达式。</param>
        /// <returns>返回当前路由实例化对象。</returns>
        public static IRouteBuilder MapLowerCaseRoute(this IRouteBuilder routeBuilder, string name, string template, object defaults, object constraints)
        {
            return routeBuilder.MapLowerCaseRoute(name, template, defaults, constraints, (object)null);
        }

        /// <summary>
        /// 路由地址。
        /// </summary>
        /// <param name="routeBuilder">路由实例化接口。</param>
        /// <param name="name">名称。</param>
        /// <param name="template">模板。</param>
        /// <param name="defaults">默认值。</param>
        /// <param name="constraints">约束表达式。</param>
        /// <param name="dataTokens">其他数据。</param>
        /// <returns>返回当前路由实例化对象。</returns>
        public static IRouteBuilder MapLowerCaseRoute(this IRouteBuilder routeBuilder, string name, string template, object defaults, object constraints, object dataTokens)
        {
            var requiredService = routeBuilder.ServiceProvider.GetRequiredService<IInlineConstraintResolver>();
            routeBuilder.Routes.Add(new LowerCaseRoute(routeBuilder.DefaultHandler, name, template, new RouteValueDictionary(defaults), (IDictionary<string, object>)new RouteValueDictionary(constraints), new RouteValueDictionary(dataTokens), requiredService));
            return routeBuilder;
        }
    }
}