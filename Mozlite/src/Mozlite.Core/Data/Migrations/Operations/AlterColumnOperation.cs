using Mozlite.Core;
using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �޸��С�
    /// </summary>
    public class AlterColumnOperation : ColumnOperation, IAlterMigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }
        
        /// <summary>
        /// ���
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }

        /// <summary>
        /// ԭ���е��������ԡ�
        /// </summary>
        public virtual ColumnOperation OldColumn { get; [param: NotNull] set; } = new ColumnOperation();

        IMutableAnnotatable IAlterMigrationOperation.OldAnnotations => OldColumn;
    }
}
