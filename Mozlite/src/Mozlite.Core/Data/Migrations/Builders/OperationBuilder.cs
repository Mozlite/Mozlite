using Mozlite.Data.Migrations.Operations;

namespace Mozlite.Data.Migrations.Builders
{
    /// <summary>
    /// ��������ʵ������
    /// </summary>
    /// <typeparam name="TOperation">�������͡�</typeparam>
    public class OperationBuilder<TOperation>
        where TOperation : MigrationOperation
    {
        /// <summary>
        /// ��ʼ����<see cref="OperationBuilder{TOperation}"/>��
        /// </summary>
        /// <param name="operation">����ʵ����</param>
        public OperationBuilder([NotNull] TOperation operation)
        {
            Check.NotNull(operation, nameof(operation));

            Operation = operation;
        }

        /// <summary>
        /// ��ǰ����ʵ����
        /// </summary>
        protected virtual TOperation Operation { get; }

        /// <summary>
        /// �����չʵ����
        /// </summary>
        /// <param name="name">���ơ�</param>
        /// <param name="value">ֵ��</param>
        /// <returns>���ص�ǰ��������ʵ������</returns>
        public virtual OperationBuilder<TOperation> Annotation(
            [NotNull] string name,
            [NotNull] object value)
        {
            Check.NotEmpty(name, nameof(name));
            Check.NotNull(value, nameof(value));

            Operation.AddAnnotation(name, value);

            return this;
        }
    }
}
