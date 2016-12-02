
namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �޸��������ơ�
    /// </summary>
    public class RenameSequenceOperation : MigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }

        /// <summary>
        /// �ܹ���
        /// </summary>
        public virtual string Schema { get; [param: CanBeNull] set; }

        /// <summary>
        /// �����ơ�
        /// </summary>
        public virtual string NewName { get; [param: CanBeNull] set; }

        /// <summary>
        /// �¼ܹ���
        /// </summary>
        public virtual string NewSchema { get; [param: CanBeNull] set; }
    }
}
