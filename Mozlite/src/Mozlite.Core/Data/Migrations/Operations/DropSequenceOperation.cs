
namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ɾ�����������
    /// </summary>
    public class DropSequenceOperation : MigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }

        /// <summary>
        /// �ܹ���
        /// </summary>
        public virtual string Schema { get; [param: CanBeNull] set; }
    }
}
