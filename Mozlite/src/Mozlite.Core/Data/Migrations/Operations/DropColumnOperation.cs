using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ɾ���С�
    /// </summary>
    public class DropColumnOperation : MigrationOperation
    {
        /// <summary>
        /// ��ʼ����<see cref="DropColumnOperation"/>��
        /// </summary>
        public DropColumnOperation()
        {
            IsDestructiveChange = true;
        }

        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }
        
        /// <summary>
        /// ���
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }
    }
}
