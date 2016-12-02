using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mozlite.Core.Tasks;
using Mozlite.Data;
using Mozlite.Data.Metadata;
using Mozlite.FileProviders;

namespace Mozlite.Extensions.Tasks
{
    /// <summary>
    /// �ɼ�����̨����
    /// </summary>
    public class ImageDownloaderTask : TaskService
    {
        private readonly ILogger<ImageDownloaderTask> _logger;
        private readonly IEnumerable<IImageDownloader> _downloaders;

        /// <summary>
        /// ��ʼ����<see cref="ImageDownloaderTask"/>��
        /// </summary>
        /// <param name="logger">��־�ӿڡ�</param>
        /// <param name="downloaders">�������б�</param>
        /// <param name="database">���ݿ�ӿڡ�</param>
        /// <param name="sqlHelper">SQL�����ӿڡ�</param>
        /// <param name="model">ģ�͹���ӿڡ�</param>
        /// <param name="provider">ý���ļ��ṩ�ߡ�</param>
        public ImageDownloaderTask(ILogger<ImageDownloaderTask> logger, IEnumerable<IImageDownloader> downloaders, IDatabase database, ISqlHelper sqlHelper, IModel model, IMediaFileProvider provider)
        {
            _logger = logger;
            foreach (var downloader in downloaders)
            {
                var current = downloader as ImageDownloader;
                if (current == null)
                    continue;
                current.Database = database;
                current.SqlHelper = sqlHelper;
                current.Model = model;
                current.Provider = provider;
            }
            _downloaders = downloaders;
        }

        /// <summary>
        /// ���ơ�
        /// </summary>
        public override string Name => "ͼƬ������";

        /// <summary>
        /// ������
        /// </summary>
        public override string Description => "���ݴ�����򣬻�����ݿ��е�һЩͼƬ�ֶε�Զ��ͼƬ���ص����ط������С�";

        /// <summary>
        /// ִ�м��ʱ�䡣
        /// </summary>
        public override TaskInterval Interval => 30;

        /// <summary>
        /// ִ�з�����
        /// </summary>
        /// <param name="argument">������</param>
        public override async Task ExecuteAsync(Argument argument)
        {
            foreach (var downloader in _downloaders)
            {
                await Task.Delay(30 * 1000);
                try
                {
                    await downloader.DownloadAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(3, $"������[{downloader.GetType().FullName}]���ִ���:{ex.Message}", ex);
                }
            }
        }
    }
}