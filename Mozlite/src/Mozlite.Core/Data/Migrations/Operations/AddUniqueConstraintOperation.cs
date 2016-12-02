using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ���Ψһ��������
    /// </summary>
    public class AddUniqueConstraintOperation : MigrationOperation
    {
        /// <summary>
        /// ���
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }

        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }

        /// <summary>
        /// ����С�
        /// </summary>
        public virtual string[] Columns { get; [param: NotNull] set; }
    }
}
