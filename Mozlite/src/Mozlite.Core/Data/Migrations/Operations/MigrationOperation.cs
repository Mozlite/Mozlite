using Mozlite.Core;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// Ǩ�����ݲ������ࡣ
    /// </summary>
    public abstract class MigrationOperation : Annotatable
    {
        /// <summary>
        /// �����Ƿ����ɲ�����ص��ƻ���
        /// </summary>
        public virtual bool IsDestructiveChange { get; set; }
    }
}
