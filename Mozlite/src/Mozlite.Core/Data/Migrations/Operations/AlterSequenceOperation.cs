using Mozlite.Core;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �޸����������
    /// </summary>
    public class AlterSequenceOperation : SequenceOperation, IAlterMigrationOperation
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
        /// ԭ����������ԡ�
        /// </summary>
        public virtual SequenceOperation OldSequence { get; [param: NotNull] set; } = new SequenceOperation();

        IMutableAnnotatable IAlterMigrationOperation.OldAnnotations => OldSequence;
    }
}
