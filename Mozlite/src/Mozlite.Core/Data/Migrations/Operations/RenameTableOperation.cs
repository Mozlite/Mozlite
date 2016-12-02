using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �޸ı�����ơ�
    /// </summary>
    public class RenameTableOperation : MigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }
        
        /// <summary>
        /// �����ơ�
        /// </summary>
        public virtual ITable NewTable { get; [param: CanBeNull] set; }
    }
}
