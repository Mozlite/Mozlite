
namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ɾ���ܹ���
    /// </summary>
    public class DropSchemaOperation : MigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }
    }
}
