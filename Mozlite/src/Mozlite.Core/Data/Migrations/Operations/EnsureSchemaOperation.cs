
namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ȷ�ϼܹ�������
    /// </summary>
    public class EnsureSchemaOperation : MigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }
    }
}
