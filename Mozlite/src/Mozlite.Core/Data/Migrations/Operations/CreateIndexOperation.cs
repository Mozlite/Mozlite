using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �½�����������
    /// </summary>
    public class CreateIndexOperation : MigrationOperation
    {
        /// <summary>
        /// �Ƿ�Ψһ��
        /// </summary>
        public virtual bool IsUnique { get; set; }

        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }
        
        /// <summary>
        /// ���
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }

        /// <summary>
        /// ����С�
        /// </summary>
        public virtual string[] Columns { get; [param: NotNull] set; }
    }
}
