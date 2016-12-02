using System;
using System.Linq;
using System.Linq.Expressions;
using Mozlite.Data.Metadata;
using Mozlite.Data.Migrations.Operations;

namespace Mozlite.Data.Migrations.Builders
{
    /// <summary>
    /// ��ӱ�񹹽�ʵ����
    /// </summary>
    /// <typeparam name="TEntity">ʵ�����͡�</typeparam>
    public class CreateTableBuilder<TEntity> : OperationBuilder<CreateTableOperation>
    {
        private readonly IModel _model;
        private readonly IEntityType _entity;

        /// <summary>
        /// ��ʼ����<see cref="CreateTableBuilder{TColumns}"/>
        /// </summary>
        /// <param name="operation">�½����Ĳ���ʵ����</param>
        /// <param name="model">ģ�ͽӿڡ�</param>
        public CreateTableBuilder(
            [NotNull] CreateTableOperation operation,
            [NotNull] IModel model)
            : base(operation)
        {
            Check.NotNull(model, nameof(model));
            _model = model;
            _entity = model.GetEntity(typeof(TEntity));
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <typeparam name="TPrincipal">�����ࡣ</typeparam>
        /// <param name="columns">�ֶΡ�</param>
        /// <param name="principalColumns">�����С�</param>
        /// <param name="onUpdate">����ʱ���Ӧ�Ĳ�����</param>
        /// <param name="onDelete">ɾ��ʱ���Ӧ�Ĳ�����</param>
        /// <param name="action">�����չ������</param>
        /// <returns>����Ǩ�ƹ���ʵ����</returns>
        public virtual CreateTableBuilder<TEntity> ForeignKey<TPrincipal>(
            [NotNull] Expression<Func<TEntity, object>> columns,
            [NotNull] Expression<Func<TPrincipal, object>> principalColumns = null,
            ReferentialAction onUpdate = ReferentialAction.NoAction,
            ReferentialAction onDelete = ReferentialAction.NoAction,
            Action<OperationBuilder<AddForeignKeyOperation>> action = null)
        {
            Check.NotNull(columns, nameof(columns));

            var operation = new AddForeignKeyOperation
            {
                Table = Operation.Table,
                Columns = columns.GetPropertyNames(),
                PrincipalTable = _model.GetTable(typeof(TPrincipal)),
                OnUpdate = onUpdate,
                OnDelete = onDelete
            };
            if (principalColumns == null)
                operation.PrincipalColumns = operation.Columns;
            else
                operation.PrincipalColumns = principalColumns.GetPropertyNames();
            operation.Name = OperationHelper.GetName(NameType.ForeignKey, operation.Table, operation.Columns, operation.PrincipalTable);
            Operation.ForeignKeys.Add(operation);

            action?.Invoke(new OperationBuilder<AddForeignKeyOperation>(operation));
            return this;
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="action">�����չ��Ϣ��</param>
        /// <returns>����Ǩ�ƹ���ʵ����</returns>
        public virtual CreateTableBuilder<TEntity> PrimaryKey(Action<OperationBuilder<AddPrimaryKeyOperation>> action = null)
        {
            var key = _model.GetEntity(typeof(TEntity)).PrimaryKey;
            if (key == null)
                return this;

            var operation = new AddPrimaryKeyOperation
            {
                Table = Operation.Table,
                Columns = key.Properties.Select(p => p.Name).ToArray()
            };
            operation.Name = OperationHelper.GetName(NameType.PrimaryKey, operation.Table);
            Operation.PrimaryKey = operation;

            action?.Invoke(new OperationBuilder<AddPrimaryKeyOperation>(operation));
            return this;
        }

        /// <summary>
        /// ���Ψһ����
        /// </summary>
        /// <param name="columns">�С�</param>
        /// <param name="action">�����չ��Ϣ��</param>
        /// <returns>����Ǩ�ƹ���ʵ����</returns>
        public virtual CreateTableBuilder<TEntity> UniqueConstraint(
            [NotNull] Expression<Func<TEntity, object>> columns,
            Action<OperationBuilder<AddUniqueConstraintOperation>> action = null)
        {
            Check.NotNull(columns, nameof(columns));

            var operation = new AddUniqueConstraintOperation
            {
                Table = Operation.Table,
                Columns = columns.GetPropertyNames()
            };
            operation.Name = OperationHelper.GetName(NameType.UniqueKey, operation.Table, operation.Columns);
            Operation.UniqueConstraints.Add(operation);

            action?.Invoke(new OperationBuilder<AddUniqueConstraintOperation>(operation));
            return this;
        }

        /// <summary>
        /// ����С�
        /// </summary>
        /// <typeparam name="T">�������͡�</typeparam>
        /// <param name="column">�б��ʽ��</param>
        /// <param name="type">�ֶ����͡�</param>
        /// <param name="unicode">�Ƿ�ΪUnicode���롣</param>
        /// <param name="nullable">�Ƿ�Ϊ�ա�</param>
        /// <param name="defaultValue">Ĭ��ֵ��</param>
        /// <param name="defaultValueSql">Ĭ��ֵSQL�ַ�����</param>
        /// <param name="computedColumnSql">�����е�SQL�ַ�����</param>
        /// <param name="action">�����չ��</param>
        /// <returns>���ز���ʵ����</returns>
        public virtual CreateTableBuilder<TEntity> Column<T>(
            [NotNull] Expression<Func<TEntity, T>> column,
            [CanBeNull] string type = null,
            bool? nullable = null,
            bool? unicode = null,
            [CanBeNull] object defaultValue = null,
            [CanBeNull] string defaultValueSql = null,
            [CanBeNull] string computedColumnSql = null,
            Action<OperationBuilder<AddColumnOperation>> action = null)
        {
            Check.NotNull(column, nameof(column));

            var property = _entity.FindProperty(column.GetPropertyAccess().Name);
            var operation = new AddColumnOperation
            {
                Table = Operation.Table,
                Name = property.Name,
                ClrType = property.ClrType,
                ColumnType = type,
                IsUnicode = unicode,
                IsIdentity = property.IsIdentity,
                MaxLength = property.GetSize(),
                IsRowVersion = property.IsRowVersion() ?? false,
                IsNullable = nullable ?? property.IsNullable,
                DefaultValue = defaultValue,
                DefaultValueSql = defaultValueSql,
                ComputedColumnSql = computedColumnSql
            };
            Operation.Columns.Add(operation);

            action?.Invoke(new OperationBuilder<AddColumnOperation>(operation));
            return this;
        }

        /// <summary>
        /// �����չ���ԡ�
        /// </summary>
        /// <param name="name">���ơ�</param>
        /// <param name="value">ֵ��</param>
        /// <returns>�����½���񹹽�ʵ������</returns>
        public new virtual CreateTableBuilder<TEntity> Annotation([NotNull] string name, [NotNull] object value)
            => (CreateTableBuilder<TEntity>)base.Annotation(name, value);
    }
}
