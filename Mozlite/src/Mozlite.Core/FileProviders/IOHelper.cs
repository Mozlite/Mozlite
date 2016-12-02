using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Mozlite.FileProviders
{
    /// <summary>
    /// �ļ����������ࡣ
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IOHelper
    {
        /// <summary>
        /// ��ȡ�����ļ����ݡ�
        /// </summary>
        /// <param name="path">�ļ�������·����</param>
        /// <returns>�����ļ������ַ�����</returns>
        public static string ReadText(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// ��ȡ�����ļ����ݡ�
        /// </summary>
        /// <param name="path">�ļ�������·����</param>
        /// <returns>�����ļ������ַ�����</returns>
        public static async Task<string> ReadTextAsync(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        /// <summary>
        /// �����ļ����ݡ�
        /// </summary>
        /// <param name="path">�ļ�������·����</param>
        /// <param name="text"></param>
        /// <returns>����д������ʵ������</returns>
        public static async Task SaveTextAsync(string path, string text)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    await sw.WriteAsync(text);
                }
            }
        }

        /// <summary>
        /// ���ļ������浽�ļ��С�
        /// </summary>
        /// <param name="stream">��ǰ�ļ�����</param>
        /// <param name="path">�ļ�������·����</param>
        /// <returns>���ر�������</returns>
        public static async Task SaveToAsync(this Stream stream, string path)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var size = 409600;
                var buffer = new byte[size];
                var current = await stream.ReadAsync(buffer, 0, size);
                while (current > 0)
                {
                    await fs.WriteAsync(buffer, 0, current);
                    current = await stream.ReadAsync(buffer, 0, size);
                }
            }
        }

        /// <summary>
        /// ��ȡ�ļ�������·����
        /// </summary>
        /// <param name="env">����ʵ������</param>
        /// <param name="baseWebRootPath">����WebRoot�ļ��е��ļ����·����</param>
        /// <returns>���ص�ǰ�ļ�������·����</returns>
        public static string MapPath(this IHostingEnvironment env, string baseWebRootPath)
        {
            return Path.Combine(env.WebRootPath, baseWebRootPath);
        }
    }
}