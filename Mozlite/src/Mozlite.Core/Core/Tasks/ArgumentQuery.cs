﻿using Mozlite.Data;

namespace Mozlite.Core.Tasks
{
    /// <summary>
    /// 参数查询实例。
    /// </summary>
    public class ArgumentQuery : QueryBase<TaskArgument>
    {
        /// <summary>
        /// 扩展名称。
        /// </summary>
        public string ExtensionName { get; set; }

        /// <summary>
        /// 状态。
        /// </summary>
        public ArgumentStatus? Status { get; set; }

        /// <summary>
        /// 任务ID。
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 初始化查询上下文。
        /// </summary>
        /// <param name="context">查询上下文。</param>
        protected internal override void Init(IQueryContext<TaskArgument> context)
        {
            context.Where(x => x.ExtensionName == ExtensionName);
            if (Status != null)
                context.Where(x => x.Status == Status);
            if (TaskId > 0)
                context.Where(x => x.TaskId == TaskId);
        }
    }
}