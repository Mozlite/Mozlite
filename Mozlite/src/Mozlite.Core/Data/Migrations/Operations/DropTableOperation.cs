using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ɾ�����
    /// </summary>
    public class DropTableOperation : MigrationOperation
    {
        /// <summary>
        /// ��ʼ����<see cref="DropTableOperation"/>��
        /// </summary>
        public DropTableOperation()
        {
            IsDestructiveChange = true;
        }

        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }
    }
}
