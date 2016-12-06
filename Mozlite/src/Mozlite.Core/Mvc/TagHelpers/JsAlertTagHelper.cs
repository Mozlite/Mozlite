﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using Mozlite.Mvc.Messages;
using Newtonsoft.Json;

namespace Mozlite.Mvc.TagHelpers
{
    /// <summary>
    /// 警告窗口。
    /// </summary>
    [HtmlTargetElement("jsalert")]
    public class JsAlertTagHelper : ViewContextableTagHelper
    {
        /// <summary>
        /// 访问并呈现当前标签实例。
        /// </summary>
        /// <param name="context">当前HTML标签上下文，包含当前HTML相关信息。</param>
        /// <param name="output">当前标签输出实例，用于呈现标签相关信息。</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var message = ViewContext.ViewData["BsMessage"] as BsMessage;
            if (string.IsNullOrWhiteSpace(message?.Message))
            {
                output.SuppressOutput();
                return;
            }

            output.TagName = "script";
            output.Attributes.Add("type", "text/javascript");
            output.Content.Clear();
            output.Content.AppendHtml(
                $"$(function(){{$alert({JsonConvert.SerializeObject(new {message = message.Message, type = message.Type?.ToString().ToLower()})});}});");
        }
    }
}