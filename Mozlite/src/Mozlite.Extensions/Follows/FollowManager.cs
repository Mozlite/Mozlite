using Mozlite.Data;

namespace Mozlite.Extensions.Follows
{
    /// <summary>
    /// �ղع����ࡣ
    /// </summary>
    public class FollowManager : IFollowManager
    {
        private readonly IRepository<Follow> _repository;
        /// <summary>
        /// ��ʼ����<see cref="FollowManager"/>��
        /// </summary>
        /// <param name="repository">���ݿ�����ӿڡ�</param>
        public FollowManager(IRepository<Follow> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// ����ղء�
        /// </summary>
        /// <typeparam name="TTarget">����ģ�����͡�</typeparam>
        /// <param name="follow">�ղ�ʵ����</param>
        /// <returns>������ӽ����</returns>
        public bool AddFollow<TTarget>(Follow follow) where TTarget : IFollowable
        {
            if (
                _repository.Any(
                    x =>
                        x.TargetId == follow.TargetId && x.UserId == follow.UserId &&
                        x.ExtensionName == follow.ExtensionName))
                return true;
            return _repository.BeginTransaction(db =>
            {
                if (!db.Create(follow))
                    return false;
                return db.As<TTarget>().IncreaseBy(x => x.Id == follow.TargetId, x => x.Follows, 1);
            });
        }
    }
}