using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// ������������
    /// </summary>
    public class AddForeignKeyOperation : MigrationOperation
    {
        /// <summary>
        /// ���ơ�
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }
        
        /// <summary>
        /// ���
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }

        /// <summary>
        /// ����С�
        /// </summary>
        public virtual string[] Columns { get; [param: NotNull] set; }
        
        /// <summary>
        /// ������
        /// </summary>
        public virtual ITable PrincipalTable { get; [param: NotNull] set; }

        /// <summary>
        /// �����С�
        /// </summary>
        public virtual string[] PrincipalColumns { get; [param: NotNull] set; }

        /// <summary>
        /// ��������ʱ��Ĳ�����
        /// </summary>
        public virtual ReferentialAction OnUpdate { get; set; }

        /// <summary>
        /// ����ɾ��ʱ��Ĳ�����
        /// </summary>
        public virtual ReferentialAction OnDelete { get; set; }
    }
}
