using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mozlite.Data.Internal;

namespace Mozlite.Data
{
    /// <summary>
    /// ���ݿ�ӿڡ�
    /// </summary>
    public interface IDatabase : IExecutor
    {
        /// <summary>
        /// ��־�ӿڡ�
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// ����һ������ִ�С�
        /// </summary>
        /// <param name="executor">����ִ�еķ�����</param>
        /// <param name="timeout">�ȴ�����ִ�������ʱ�䣨����Ϊ��λ����Ĭ��ֵΪ 30 �롣</param>
        /// <param name="cancellationToken">ȡ����ʶ��</param>
        /// <returns>��������ʵ������</returns>
        Task<bool> BeginTransactionAsync(Func<ITransaction, Task<bool>> executor, int timeout = 30, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// ����һ������ִ�С�
        /// </summary>
        /// <param name="executor">����ִ�еķ�����</param>
        /// <param name="timeout">�ȴ�����ִ�������ʱ�䣨����Ϊ��λ����Ĭ��ֵΪ 30 �롣</param>
        /// <returns>��������ʵ������</returns>
        bool BeginTransaction(Func<ITransaction, bool> executor, int timeout = 30);
    }
}