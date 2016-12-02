using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �������Ƹ��Ĳ�����
    /// </summary>
    public class RenameIndexOperation : MigrationOperation
    {
        /// <summary>
        /// ԭ�����ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }

        /// <summary>
        /// �����ơ�
        /// </summary>
        public virtual string NewName { get; [param: NotNull] set; }
        
        /// <summary>
        /// ������ơ�
        /// </summary>
        public virtual ITable Table { get; [param: CanBeNull] set; }
    }
}
