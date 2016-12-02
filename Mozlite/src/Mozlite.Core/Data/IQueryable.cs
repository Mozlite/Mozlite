﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Mozlite.Data
{
    /// <summary>
    /// 查询实例。
    /// </summary>
    /// <typeparam name="TModel">实体类型。</typeparam>
    public interface IQueryable<TModel> : IQueryContext<TModel>
    {        /// <summary>
        /// 设置表格关联。
        /// </summary>
        /// <typeparam name="TForeign">拼接类型。</typeparam>
        /// <param name="onExpression">关联条件表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> InnerJoin<TForeign>(Expression<Func<TModel, TForeign, bool>> onExpression);

        /// <summary>
        /// 设置表格关联。
        /// </summary>
        /// <typeparam name="TPrimary">主键所在的模型类型。</typeparam>
        /// <typeparam name="TForeign">外键所在的模型类型。</typeparam>
        /// <param name="onExpression">关联条件表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> InnerJoin<TPrimary, TForeign>(Expression<Func<TPrimary, TForeign, bool>> onExpression);

        /// <summary>
        /// 设置选择列。
        /// </summary>
        /// <typeparam name="TEntity">模型类型。</typeparam>
        /// <param name="field">列表达式。</param>
        /// <param name="alias">别名。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> Select<TEntity>(Expression<Func<TEntity, object>> field, string alias);

        /// <summary>
        /// 设置选择列。
        /// </summary>
        /// <typeparam name="TEntity">模型类型。</typeparam>
        /// <param name="fields">列表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> Select<TEntity>(Expression<Func<TEntity, object>> fields);

        /// <summary>
        /// 设置选择列。
        /// </summary>
        /// <param name="fields">列表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> Select(Expression<Func<TModel, object>> fields);

        /// <summary>
        /// 设置选择列。
        /// </summary>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> Select();

        /// <summary>
        /// 添加条件表达式。
        /// </summary>
        /// <typeparam name="TEntity">模型类型。</typeparam>
        /// <param name="expression">条件表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> Where<TEntity>(Expression<Predicate<TEntity>> expression);

        /// <summary>
        /// 添加条件表达式。
        /// </summary>
        /// <param name="where">条件语句。</param>
        /// <returns>返回当前查询上下文。</returns>
        new IQueryable<TModel> Where(string where);

        /// <summary>
        /// 添加条件表达式。
        /// </summary>
        /// <param name="expression">条件表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> Where(Expression<Predicate<TModel>> expression);

        /// <summary>
        /// 添加排序规则。
        /// </summary>
        /// <typeparam name="TEntity">模型类型。</typeparam>
        /// <param name="expression">列名称表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> OrderBy<TEntity>(Expression<Func<TEntity, object>> expression);

        /// <summary>
        /// 添加排序规则。
        /// </summary>
        /// <typeparam name="TEntity">模型类型。</typeparam>
        /// <param name="expression">列名称表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> OrderByDescending<TEntity>(Expression<Func<TEntity, object>> expression);

        /// <summary>
        /// 添加排序规则。
        /// </summary>
        /// <param name="expression">列名称表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> OrderBy(Expression<Func<TModel, object>> expression);

        /// <summary>
        /// 添加排序规则。
        /// </summary>
        /// <param name="expression">列名称表达式。</param>
        /// <returns>返回当前查询实例对象。</returns>
        new IQueryable<TModel> OrderByDescending(Expression<Func<TModel, object>> expression);

        /// <summary>
        /// 查询数据库返回<paramref name="size"/>项结果。
        /// </summary>
        /// <param name="size">返回的记录数。</param>
        /// <returns>返回数据列表。</returns>
        IEnumerable<TModel> AsEnumerable(int size);

        /// <summary>
        /// 查询数据库返回结果。
        /// </summary>
        /// <returns>返回数据列表。</returns>
        TModel SingleOrDefault();

        /// <summary>
        /// 查询数据库返回结果。
        /// </summary>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回数据列表。</returns>
        Task<TModel> SingleOrDefaultAsync(CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// 查询数据库返回结果。
        /// </summary>
        /// <param name="pageIndex">页码。</param>
        /// <param name="pageSize">每页显示的记录数。</param>
        /// <param name="count">分页总记录数计算列。</param>
        /// <returns>返回数据列表。</returns>
        IPageEnumerable<TModel> AsEnumerable(int pageIndex, int pageSize, Expression<Func<TModel, object>> count = null);

        /// <summary>
        /// 查询数据库返回结果。
        /// </summary>
        /// <returns>返回数据列表。</returns>
        IEnumerable<TModel> AsEnumerable();

        /// <summary>
        /// 查询数据库返回<paramref name="size"/>项结果。
        /// </summary>
        /// <param name="size">返回的记录数。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回数据列表。</returns>
        Task<IEnumerable<TModel>> AsEnumerableAsync(int size,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 查询数据库返回结果。
        /// </summary>
        /// <param name="pageIndex">页码。</param>
        /// <param name="pageSize">每页显示的记录数。</param>
        /// <param name="count">分页总记录数计算列。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回数据列表。</returns>
        Task<IPageEnumerable<TModel>> AsEnumerableAsync(int pageIndex, int pageSize, Expression<Func<TModel, object>> count = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 查询数据库返回结果。
        /// </summary>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回数据列表。</returns>
        Task<IEnumerable<TModel>> AsEnumerableAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 递归查询子集数据。
        /// </summary>
        /// <param name="expression">条件表达式。</param>
        /// <returns>返回数据实例列表。</returns>
        IEnumerable<TModel> RecurseChildren(Expression<Predicate<TModel>> expression);

        /// <summary>
        /// 递归查询父级数据。
        /// </summary>
        /// <param name="expression">条件表达式。</param>
        /// <returns>返回数据实例列表。</returns>
        IEnumerable<TModel> RecurseParents(Expression<Predicate<TModel>> expression);

        /// <summary>
        /// 递归查询子集数据。
        /// </summary>
        /// <param name="expression">条件表达式。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回数据实例列表。</returns>
        Task<IEnumerable<TModel>> RecurseChildrenAsync(Expression<Predicate<TModel>> expression, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 递归查询父级数据。
        /// </summary>
        /// <param name="expression">条件表达式。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回数据实例列表。</returns>
        Task<IEnumerable<TModel>> RecurseParentsAsync(Expression<Predicate<TModel>> expression, CancellationToken cancellationToken = default(CancellationToken));
    }
}