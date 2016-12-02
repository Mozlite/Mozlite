using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ɾ��������
    /// </summary>
    public class DropIndexOperation : MigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }

        /// <summary>
        /// ���
        /// </summary>
        public virtual ITable Table { get; [param: CanBeNull] set; }
    }
}
