﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.Mvc;

namespace Mozlite.TagHelpers.Common
{
    /// <summary>
    /// 分页标签。
    /// </summary> 
    public class PageTagHelper : ViewContextableTagHelper
    {
        private const string ActionAttributeName = "x-action";
        private const string ControllerAttributeName = "x-controller";
        private const string AreaAttributeName = "x-area";
        private const string FragmentAttributeName = "x-fragment";
        private const string HostAttributeName = "x-host";
        private const string ProtocolAttributeName = "x-protocol";
        private const string RouteAttributeName = "x-route";
        private const string RouteValuesDictionaryName = "x-all-route-data";
        private const string RouteValuesPrefix = "x-route-";
        private const string HrefAttributeName = "x-href";
        private const string DataAttributeName = "x-data";
        private const string FactorAttributeName = "x-factor";
        private const string BorderAttributeName = "x-border";
        private IDictionary<string, string> _routeValues;

        /// <summary>
        /// 初始化类<see cref="PageTagHelper"/>。
        /// </summary>
        /// <param name="generator"><see cref="IHtmlGenerator"/>接口。</param>
        public PageTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        /// <summary>
        /// 排序。
        /// </summary>
        public override int Order => -1000;

        /// <summary>
        /// HTML辅助接口。
        /// </summary>
        protected IHtmlGenerator Generator { get; }

        /// <summary>
        /// 试图名称。
        /// </summary>
        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        /// <summary>
        /// 控制器名称。
        /// </summary>
        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }

        /// <summary>
        /// 区域名称。
        /// </summary>
        [HtmlAttributeName(AreaAttributeName)]
        public string Area { get; set; }

        /// <summary>
        /// 协议如：http:或者https:等。
        /// </summary>
        [HtmlAttributeName(ProtocolAttributeName)]
        public string Protocol { get; set; }

        /// <summary>
        /// 主机名称。
        /// </summary>
        [HtmlAttributeName(HostAttributeName)]
        public string Host { get; set; }

        /// <summary>
        /// URL片段。
        /// </summary>
        [HtmlAttributeName(FragmentAttributeName)]
        public string Fragment { get; set; }

        /// <summary>
        /// 路由配置名称。
        /// </summary>
        [HtmlAttributeName(RouteAttributeName)]
        public string Route { get; set; }

        /// <summary>
        /// 是否有边框。
        /// </summary>
        [HtmlAttributeName(BorderAttributeName)]
        public bool Border { get; set; }

        /// <summary>
        /// 路由对象列表。
        /// </summary>
        [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                if (_routeValues == null)
                {
                    _routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                return _routeValues;
            }
            set
            {
                _routeValues = value;
            }
        }

        /// <summary>
        /// 分页数据对象。
        /// </summary>
        [HtmlAttributeName(DataAttributeName)]
        public IPageEnumerable Data { get; set; }

        /// <summary>
        /// 链接地址。
        /// </summary>
        [HtmlAttributeName(HrefAttributeName)]
        public string Href { get; set; }

        /// <summary>
        /// 显示页码数量。
        /// </summary>
        [HtmlAttributeName(FactorAttributeName)]
        public int Factor { get; set; } = 9;

        /// <summary>
        /// 初始化当前标签上下文。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        public override void Init(TagHelperContext context)
        {
            base.Init(context);
            Area = Area ?? ViewContext.GetAreaName();
            Controller = Controller ?? ViewContext.GetControllerName();
            Action = Action ?? ViewContext.GetActionName();
        }

        private Func<int, TagBuilder> _createAnchor;
        /// <summary>
        /// 访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Data = Data ?? ViewContext.ViewData.Model as IPageEnumerable;
            if (Data == null || Data.Pages == 0)
            {
                output.SuppressOutput();
                return;
            }
            if (context.AllAttributes.ContainsName(HrefAttributeName))
            {
                _createAnchor = page =>
                {
                    var tagBuilder = new TagBuilder("a");
                    tagBuilder.MergeAttribute("href", Href.Replace("$page;", page.ToString()));
                    return tagBuilder;
                };
            }
            else
            {
                IDictionary<string, object> routeValues = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                if (_routeValues != null && _routeValues.Count > 0)
                {
                    foreach (var routeValue in _routeValues)
                    {
                        routeValues.Add(routeValue.Key, routeValue.Value);
                    }
                }

                if (Area != null)
                    routeValues["area"] = Area;

                if (Route == null)
                {
                    _createAnchor = page =>
                    {
                        routeValues["page"] = page;
                        return Generator.GenerateActionLink(
                            ViewContext,
                            linkText: string.Empty,
                            actionName: Action,
                            controllerName: Controller,
                            protocol: Protocol,
                            hostname: Host,
                            fragment: Fragment,
                            routeValues: routeValues,
                            htmlAttributes: null);
                    };
                }
                else
                {
                    _createAnchor = page =>
                    {
                        routeValues["page"] = page;
                        return Generator.GenerateRouteLink(
                            ViewContext,
                            linkText: string.Empty,
                            routeName: Route,
                            protocol: Protocol,
                            hostName: Host,
                            fragment: Fragment,
                            routeValues: routeValues,
                            htmlAttributes: null);
                    };
                }
            }

            var builder = new TagBuilder("ul");
            builder.AddCssClass("pagination");
            if (!Border)
                builder.AddCssClass("borderless");
            int endIndex;
            var startIndex = Cores.GetRange(Data.Page, Data.Pages, Factor, out endIndex);
            if (Data.Page > 1)
                builder.InnerHtml.AppendHtml(CreateLink(Data.Page - 1, Resources.LastPage, title: Resources.LastPage));
            if (startIndex > 1)
                builder.InnerHtml.AppendHtml(CreateLink(1, "1"));
            if (startIndex > 2)
                builder.InnerHtml.AppendHtml("<li><span>…</span></li>");
            for (int i = startIndex; i < endIndex; i++)
            {
                builder.InnerHtml.AppendHtml(CreateLink(i, i.ToString(), "active"));
            }
            if (endIndex < Data.Pages)
                builder.InnerHtml.AppendHtml("<li><span>…</span></li>");
            if (endIndex <= Data.Pages)
                builder.InnerHtml.AppendHtml(CreateLink(Data.Pages, Data.Pages.ToString()));
            if (Data.Page < Data.Pages)
                builder.InnerHtml.AppendHtml(CreateLink(Data.Page + 1, Resources.NextPage, title: Resources.NextPage));
            output.TagName = "ul";
            output.MergeAttributes(builder);
            output.Content.AppendHtml(builder.InnerHtml);
        }

        private TagBuilder CreateLink(int pageIndex, string innerHtml, string className = null, string title = null)
        {
            var li = new TagBuilder("li");
            var anchor = _createAnchor(pageIndex);
            anchor.MergeAttribute("title", title ?? string.Format(Resources.NumberPage, pageIndex));
            if (Data.Page == pageIndex)
            {
                li.AddCssClass(className);
                anchor.MergeAttribute("href", "javascript:;", true);
            }
            anchor.InnerHtml.AppendHtml(innerHtml);
            li.InnerHtml.AppendHtml(anchor);
            return li;
        }
    }
}