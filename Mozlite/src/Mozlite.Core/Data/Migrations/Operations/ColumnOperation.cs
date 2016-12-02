using System;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// �в������ʵ����
    /// </summary>
    public class ColumnOperation : MigrationOperation
    {
        /// <summary>
        /// �Ƿ�ΪUnicode�ַ�����
        /// </summary>
        public virtual bool? IsUnicode { get; [param: CanBeNull] set; }

        /// <summary>
        /// ���͡�
        /// </summary>
        public virtual Type ClrType { get; [param: NotNull] set; }

        /// <summary>
        /// �����͡�
        /// </summary>
        public virtual string ColumnType { get; [param: CanBeNull] set; }

        /// <summary>
        /// ��С��
        /// </summary>
        public virtual int? MaxLength { get; [param: CanBeNull] set; }

        /// <summary>
        /// �Ƿ�Ϊ�а汾��
        /// </summary>
        public virtual bool IsRowVersion { get; set; }

        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public virtual bool IsIdentity { get; set; }

        /// <summary>
        /// �Ƿ�ɿա�
        /// </summary>
        public virtual bool IsNullable { get; set; }

        /// <summary>
        /// Ĭ��ֵ��
        /// </summary>
        public virtual object DefaultValue { get; [param: CanBeNull] set; }

        /// <summary>
        /// Ĭ��SQL�ַ�����
        /// </summary>
        public virtual string DefaultValueSql { get; [param: CanBeNull] set; }

        /// <summary>
        /// �����е�ֵ�ַ�����
        /// </summary>
        public virtual string ComputedColumnSql { get; [param: CanBeNull] set; }
    }
}
