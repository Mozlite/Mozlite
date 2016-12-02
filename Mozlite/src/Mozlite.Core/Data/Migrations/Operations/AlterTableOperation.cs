using Mozlite.Core;
using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �޸ı��
    /// </summary>
    public class AlterTableOperation : MigrationOperation, IAlterMigrationOperation
    {
        /// <summary>
        /// ������ơ�
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }

        /// <summary>
        /// ԭ����������ԡ�
        /// </summary>
        public virtual Annotatable OldTable { get; [param: NotNull] set; } = new Annotatable();

        IMutableAnnotatable IAlterMigrationOperation.OldAnnotations => OldTable;
    }
}
