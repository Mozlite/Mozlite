using System;
using System.Collections.Generic;

namespace Mozlite.Extensions.Searching
{
    /// <summary>
    /// ����ʵ���ṩ�߻��ࡣ
    /// </summary>
    /// <typeparam name="TModel">ģ�����͡�</typeparam>
    public abstract class SearchIndexProvider<TModel> : ISearchIndexProvider
        where TModel : class, ISearchable, new()
    {
        /// <inheritdoc />
        public Type Model => typeof(TModel);

        /// <inheritdoc />
        public virtual string ProviderName => Model.FullName;

        /// <inheritdoc />
        public abstract string Summarized(SearchEntry entry);

        /// <inheritdoc />
        public abstract IEnumerable<string> Indexed(SearchEntry entry);
    }
}