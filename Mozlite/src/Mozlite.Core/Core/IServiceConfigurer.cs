using Microsoft.Extensions.DependencyInjection;

namespace Mozlite.Core
{
    /// <summary>
    /// �������ýӿڡ�
    /// </summary>
    public interface IServiceConfigurer : IService
    {
        /// <summary>
        /// ���÷��񷽷���
        /// </summary>
        /// <param name="services">���񼯺�ʵ����</param>
        void ConfigureServices(IServiceCollection services);
    }
}