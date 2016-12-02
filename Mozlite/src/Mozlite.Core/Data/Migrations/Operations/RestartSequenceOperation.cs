
namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ���¿�ʼ���������
    /// </summary>
    public class RestartSequenceOperation : MigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }

        /// <summary>
        /// �ܹ���
        /// </summary>
        public virtual string Schema { get; [param: CanBeNull] set; }

        /// <summary>
        /// ��ʼֵ��
        /// </summary>
        public virtual long StartValue { get; set; } = 1L;
    }
}
