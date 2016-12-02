using System;
using System.Linq.Expressions;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// SQL��������
    /// </summary>
    public class SqlOperation : MigrationOperation
    {
        /// <summary>
        /// SQL�ַ�����
        /// </summary>
        public virtual string Sql { get; [param: NotNull] set; }

        /// <summary>
        /// ��ӻ���µ�ʵ�塣
        /// </summary>
        public object Instance { get; set; }

        /// <summary>
        /// ģ�����͡�
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// �������ʽ��
        /// </summary>
        public Expression Expression { get; set; }
    }
}
