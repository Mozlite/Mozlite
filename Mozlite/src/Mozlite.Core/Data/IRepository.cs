﻿using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Mozlite.Data.Internal;

namespace Mozlite.Data
{
    /// <summary>
    /// 实体数据库操作接口。
    /// </summary>
    /// <typeparam name="TModel">实体模型。</typeparam>
    public interface IRepository<TModel> : IRepositoryBase<TModel>
    {
        /// <summary>
        /// 开启一个事务并执行。
        /// </summary>
        /// <param name="executor">事务执行的方法。</param>
        /// <param name="timeout">等待命令执行所需的时间（以秒为单位）。默认值为 30 秒。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回事务实例对象。</returns>
        Task<bool> BeginTransactionAsync(Func<ITransactionRepository<TModel>, Task<bool>> executor, int timeout = 30, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// 开启一个事务并执行。
        /// </summary>
        /// <param name="executor">事务执行的方法。</param>
        /// <param name="timeout">等待命令执行所需的时间（以秒为单位）。默认值为 30 秒。</param>
        /// <returns>返回事务实例对象。</returns>
        bool BeginTransaction(Func<ITransactionRepository<TModel>, bool> executor, int timeout = 30);

        /// <summary>
        /// 实例化一个查询实例。
        /// </summary>
        IQueryable<TModel> Query { get; }

        /// <summary>
        /// 分页获取实例列表。
        /// </summary>
        /// <param name="query">查询实例。</param>
        /// <param name="countExpression">返回总记录数的表达式,用于多表拼接过滤重复记录数。</param>
        /// <returns>返回分页实例列表。</returns>
        TQuery Load<TQuery>(TQuery query, Expression<Func<TModel, object>> countExpression = null) where TQuery : QueryBase<TModel>;

        /// <summary>
        /// 分页获取实例列表。
        /// </summary>
        /// <param name="query">查询实例。</param>
        /// <param name="countExpression">返回总记录数的表达式,用于多表拼接过滤重复记录数。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回分页实例列表。</returns>
        Task<TQuery> LoadAsync<TQuery>(TQuery query, Expression<Func<TModel, object>> countExpression = null, CancellationToken cancellationToken = new CancellationToken()) where TQuery : QueryBase<TModel>;
    }
}