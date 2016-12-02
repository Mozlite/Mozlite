using Mozlite.Core;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �޸����ݿ������
    /// </summary>
    public class AlterDatabaseOperation : MigrationOperation, IAlterMigrationOperation
    {
        /// <summary>
        /// ԭ���ݿ��һЩ�������ԡ�
        /// </summary>
        public virtual Annotatable OldDatabase { get; } = new Annotatable();

        IMutableAnnotatable IAlterMigrationOperation.OldAnnotations => OldDatabase;
    }
}
