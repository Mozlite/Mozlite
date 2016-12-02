using System;
using Mozlite.Data;

namespace Mozlite.Extensions
{
    /// <summary>
    /// ���������ࡣ
    /// </summary>
    /// <typeparam name="TModel">ģ�����͡�</typeparam>
    public abstract class ObjectManager<TModel> : IObjectManager<TModel> where TModel : ExtendObjectBase
    {
        /// <summary>
        /// ���ݿ�����ӿڡ�
        /// </summary>
        protected readonly IRepository<TModel> Database;
        /// <summary>
        /// ��ʼ����<see cref="ObjectManager{TModel}"/>��
        /// </summary>
        /// <param name="repository">���ݿ�����ӿڡ�</param>
        protected ObjectManager(IRepository<TModel> repository)
        {
            Database = repository;
        }

        /// <summary>
        /// �ж�ʵ���Ƿ��ظ���
        /// </summary>
        /// <param name="model">ģ��ʵ����</param>
        /// <returns>�����жϽ����</returns>
        public virtual bool IsDulicate(TModel model)
        {
            return false;
        }

        /// <summary>
        /// ����ʵ����
        /// </summary>
        /// <param name="model">ģ��ʵ������</param>
        /// <returns>����ִ�н����</returns>
        public virtual DataResult Save(TModel model)
        {
            if (IsDulicate(model))
                return DataAction.Duplicate;
            if (model.Id > 0)
            {
                model.UpdatedDate = DateTime.Now;
                return DataResult.FromResult(Database.Update(model), DataAction.Updated);
            }
            return DataResult.FromResult(Database.Create(model), DataAction.Created);
        }

        /// <summary>
        /// ��ȡģ��ʵ����
        /// </summary>
        /// <param name="id">ʵ��Id��</param>
        /// <returns>����ģ��ʵ������</returns>
        public TModel Get(int id)
        {
            return Database.Find(x => x.Id == id);
        }

        /// <summary>
        /// ��ȡģ��ʵ����
        /// </summary>
        /// <param name="guid">Guid��</param>
        /// <returns>����ģ��ʵ������</returns>
        public TModel Get(Guid guid)
        {
            return Database.Find(x => x.Guid == guid);
        }

        /// <summary>
        /// ��ȡģ��ʵ����
        /// </summary>
        /// <param name="key">Ψһ����</param>
        /// <returns>����ģ��ʵ������</returns>
        public TModel Get(string key)
        {
            return Database.Find(x => x.Key == key);
        }

        /// <summary>
        /// ɾ��ʵ������
        /// </summary>
        /// <param name="id">Id��</param>
        /// <returns>����ɾ�������</returns>
        public virtual DataResult Delete(int id)
        {
            return DataResult.FromResult(Database.Delete(x => x.Id == id), DataAction.Deleted);
        }

        /// <summary>
        /// ɾ��ʵ������
        /// </summary>
        /// <param name="ids">Id���ϡ�</param>
        /// <returns>����ɾ�������</returns>
        public virtual DataResult Delete(int[] ids)
        {
            return DataResult.FromResult(Database.Delete(x => x.Id.Included(ids)), DataAction.Deleted);
        }

        /// <summary>
        /// ɾ��ʵ������
        /// </summary>
        /// <param name="ids">Id���ϣ��ԡ�,���ָ</param>
        /// <returns>����ɾ�������</returns>
        public DataResult Delete(string ids)
        {
            return Delete(ids.SplitToInt32());
        }
    }
}