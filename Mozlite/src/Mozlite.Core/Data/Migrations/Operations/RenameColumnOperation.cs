using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �޸������ơ�
    /// </summary>
    public class RenameColumnOperation : MigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }

        /// <summary>
        /// ���
        /// </summary>
        public virtual ITable Table { get; [param: CanBeNull] set; }
        /// <summary>
        /// �����ơ�
        /// </summary>
        public virtual string NewName { get; [param: NotNull] set; }
    }
}
