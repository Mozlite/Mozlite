using Mozlite.Core;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �޸�Ǩ�Ʋ�����
    /// </summary>
    public interface IAlterMigrationOperation
    {
        /// <summary>
        /// ԭ����չʵ����
        /// </summary>
        IMutableAnnotatable OldAnnotations { get; }
    }
}
