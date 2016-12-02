using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ����С�
    /// </summary>
    public class AddColumnOperation : ColumnOperation
    {
        /// <summary>
        /// �����ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }
        
        /// <summary>
        /// ���
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }
    }
}
