using Mozlite.Data.Migrations.Operations;

namespace Mozlite.Data.Migrations.Builders
{
    /// <summary>
    /// �޸Ĳ�������ʵ����
    /// </summary>
    /// <typeparam name="TOperation">�������͡�</typeparam>
    public class AlterOperationBuilder<TOperation> : OperationBuilder<TOperation>
        where TOperation : MigrationOperation, IAlterMigrationOperation
    {
        /// <summary>
        /// ��ʼ����<see cref="AlterOperationBuilder{TOperation}"/>��
        /// </summary>
        /// <param name="operation">����ʵ������</param>
        public AlterOperationBuilder([NotNull] TOperation operation)
            : base(operation)
        {
        }

        /// <summary>
        /// �����չʵ����
        /// </summary>
        /// <param name="name">���ơ�</param>
        /// <param name="value">ֵ��</param>
        /// <returns>���ص�ǰ��������ʵ������</returns>
        public new virtual AlterOperationBuilder<TOperation> Annotation(
                [NotNull] string name,
                [NotNull] object value)
            => (AlterOperationBuilder<TOperation>)base.Annotation(name, value);
        
        /// <summary>
        /// ���ԭ����չʵ����
        /// </summary>
        /// <param name="name">���ơ�</param>
        /// <param name="value">ֵ��</param>
        /// <returns>���ص�ǰ��������ʵ������</returns>
        public virtual AlterOperationBuilder<TOperation> OldAnnotation(
            [NotNull] string name,
            [NotNull] object value)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(value, nameof(value));

            Operation.OldAnnotations.AddAnnotation(name, value);

            return this;
        }
    }
}
