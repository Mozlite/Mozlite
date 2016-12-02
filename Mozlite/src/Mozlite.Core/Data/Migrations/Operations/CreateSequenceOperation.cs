using System;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ������������
    /// </summary>
    public class CreateSequenceOperation : SequenceOperation
    {
        /// <summary>
        /// �ܹ���
        /// </summary>
        public virtual string Schema { get; [param: CanBeNull] set; }

        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }

        /// <summary>
        /// ��ǰ���͡�
        /// </summary>
        public virtual Type ClrType { get; [param: NotNull] set; }

        /// <summary>
        /// ��ʼֵ��
        /// </summary>
        public virtual long StartValue { get; set; } = 1L;
    }
}
