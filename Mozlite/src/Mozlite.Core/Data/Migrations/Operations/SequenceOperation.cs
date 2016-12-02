
namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ���������
    /// </summary>
    public class SequenceOperation : MigrationOperation
    {
        /// <summary>
        /// ������
        /// </summary>
        public virtual int IncrementBy { get; set; } = 1;

        /// <summary>
        /// ���ֵ��
        /// </summary>
        public virtual long? MaxValue { get; [param: CanBeNull] set; }

        /// <summary>
        /// ��Сֵ��
        /// </summary>
        public virtual long? MinValue { get; [param: CanBeNull] set; }

        /// <summary>
        /// �Ƿ�ѭ����
        /// </summary>
        public virtual bool IsCyclic { get; set; }
    }
}
