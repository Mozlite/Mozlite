﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mozlite.Data;
using Mozlite.Utils;

namespace Mozlite.FileProviders
{
    /// <summary>
    /// 媒体文件夹文件提供者。
    /// </summary>
    public class MediaFileProvider : IMediaFileProvider
    {
        private readonly IRepository<StorageFileInfo> _storages;
        private readonly IRepository<MediaFileInfo> _repository;
        private readonly string _mediaRootPath;
        private readonly string _tempRootPath;

        /// <summary>
        /// 初始化类<see cref="MediaFileProvider"/>。
        /// </summary>
        /// <param name="environment">环境变量接口。</param>
        /// <param name="storages">文件存储数据库操作接口。</param>
        /// <param name="repository">数据库操作接口。</param>
        /// <param name="options">配置选项。</param>
        public MediaFileProvider(IHostingEnvironment environment, IRepository<StorageFileInfo> storages, IRepository<MediaFileInfo> repository, IOptions<FileProviderOptions> options)
        {
            _storages = storages;
            _repository = repository;
            //媒体文件存储的物理路径
            var path = options.Value.MediaPath.Trim();
            if (path.StartsWith("~/"))
                path = Path.Combine(environment.WebRootPath, path.Substring(2));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            _mediaRootPath = path;
            //临时文件存储的物理路径
            path = options.Value.TempPath.Trim();
            if (path.StartsWith("~/"))
                path = Path.Combine(environment.WebRootPath, path.Substring(2));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            _tempRootPath = path;
        }

        private string GetMediaPath(StorageFileInfo storage)
        {
            return Path.Combine(_mediaRootPath, storage.Path);
        }

        private string GetTempFileName(string fileName)
        {
            return Path.Combine(_tempRootPath, Cores.Md5(fileName));
        }

        private class InteralMediaFileInfo : IMediaFileInfo
        {
            private readonly FileInfo _info;
            private readonly MediaFileInfo _media;

            public InteralMediaFileInfo(string path, MediaFileInfo media, string md5)
            {
                _info = new FileInfo(path);
                _media = media;
                Md5 = md5;
            }

            public Stream CreateReadStream()
            {
                return new FileStream(PhysicalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 65536, FileOptions.Asynchronous | FileOptions.SequentialScan);
            }

            public bool Exists => _info.Exists;

            public long Length => _info.Length;

            public string PhysicalPath => _info.FullName;

            public string Name => _info.Name;

            public DateTimeOffset LastModified => _info.LastWriteTime;

            public bool IsDirectory => false;

            public string Md5 { get; }

            public string Url => _media.Url;
        }

        /// <inheritdoc />
        public IMediaFileInfo GetFileInfo(Guid mediaId)
        {
            var file = _repository.Find(x => x.Id == mediaId);
            if (file != null)
            {
                var storage = _storages.Find(x => x.FileId == file.FileId);
                return new InteralMediaFileInfo(GetMediaPath(storage), file, storage.Name);
            }
            return null;
        }

        /// <inheritdoc />
        public StorageFile GetStorageFile(Guid mediaId)
        {
            var storage =
                _storages.Query.InnerJoin<MediaFileInfo>((info, fileInfo) => info.FileId == fileInfo.FileId)
                .Where<MediaFileInfo>(x => x.Id == mediaId)
                .SingleOrDefault();
            if (storage == null)
                return null;
            var path = GetMediaPath(storage);
            if (!File.Exists(path))
                return null;
            return new StorageFile(path, storage.ContentType);
        }

        /// <inheritdoc />
        public bool SetTitle(Guid id, string title)
        {
            return _repository.Update(mf => mf.Id == id, new { title });
        }

        /// <inheritdoc />
        public bool MoveTo(Guid id, int directoryId)
        {
            return _repository.Update(mf => mf.Id == id, new { directoryId });
        }

        /// <inheritdoc />
        public IEnumerable<MediaFileInfo> LoadByDirectoryId(string mediaType, int directoryId, int targetId = 0)
        {
            if (targetId == 0)
                return _repository.Load(mf => mf.Type == mediaType && mf.DirectoryId == directoryId);
            return _repository.Load(mf => mf.Type == mediaType && mf.DirectoryId == directoryId && mf.TargetId == targetId);
        }

        /// <inheritdoc />
        public IEnumerable<MediaFileInfo> LoadByTargetId(string mediaType, int targetId, int? directoryId = null)
        {
            if (directoryId == null)
                return _repository.Load(mf => mf.Type == mediaType && mf.TargetId == targetId);
            return _repository.Load(mf => mf.Type == mediaType && mf.TargetId == targetId && mf.DirectoryId == directoryId.Value);
        }

        /// <inheritdoc />
        public async Task<string> DownloadAsync(string url, bool isUnique, string mediaType, int targetId = 0, int directoryId = 0, string extension = null)
        {
            var path = GetTempFileName(url);
            extension = await HttpHelper.DownloadAsync(url, path) ?? extension;
            if (extension == null)
                return null;
            var info = new FileInfo(path);
            return await CreateAsync(path, extension, null, isUnique, null, info.Length, mediaType, targetId, directoryId);
        }

        /// <inheritdoc />
        public async Task<string> UploadAsync(IFormFile file, bool isUnique, string mediaType, int targetId = 0, int directoryId = 0)
        {
            var path = GetTempFileName(Guid.NewGuid().ToString());
            if (!await file.SaveToAsync(path))
                return null;
            return await CreateAsync(path, Path.GetExtension(file.FileName), file.FileName, isUnique, file.ContentType, file.Length, mediaType, targetId, directoryId);
        }

        /// <inheritdoc />
        public async Task<string> UploadAsync(IFormFile file, string mediaFileName, string mediaType = null, int targetId = 0, int directoryId = 0)
        {
            var path = GetTempFileName(Guid.NewGuid().ToString());
            if (!await file.SaveToAsync(path))
                return null;

            var mediaId = Path.GetFileNameWithoutExtension(mediaFileName).AsGuid();
            if (mediaId != null)
            {
                var media = await _repository.FindAsync(f => f.Id == mediaId.Value);
                if (media != null)
                    return await CreateAsync(media, path, file);
            }
            return await CreateAsync(path, Md5(path), Path.GetExtension(file.FileName), file.FileName, file.ContentType, file.Length, mediaType, targetId, directoryId);
        }

        /// <summary>
        /// 添加临时文件到数据库中，并转移到文件存储库中。
        /// </summary>
        /// <param name="path">临时文件的物理路径。</param>
        /// <param name="isUnique">判断每个物理文件和媒体文件信息是否一一对应。</param>
        /// <param name="extension">扩展名称。</param>
        /// <param name="title">标题。</param>
        /// <param name="contentType">内容类型。</param>
        /// <param name="length">大小。</param>
        /// <param name="mediaType">媒体类型。</param>
        /// <param name="targetId">所属实例Id。</param>
        /// <param name="directoryId">文件目录Id。</param>
        /// <returns>返回当前文件的访问地址，如果失败返回<c>null</c>。</returns>
        private async Task<string> CreateAsync(string path, string extension, string title, bool isUnique, string contentType, long length, string mediaType, int targetId, int directoryId)
        {
            var md5 = Md5(path);
            var storage = _storages.Find(s => s.Name == md5);
            if (storage == null)
                return await CreateAsync(path, md5, extension, title, contentType, length, mediaType, targetId, directoryId);
            File.Delete(path);
            return await CreateAsync(storage, extension, title, isUnique, mediaType, targetId, directoryId);
        }

        /// <summary>
        /// 上传一个新的文件替换原来的物理文件。
        /// </summary>
        /// <param name="mediaFile">媒体文件实例对象。</param>
        /// <param name="path">临时文件物理路径。</param>
        /// <param name="file">表单文件实例对象。</param>
        /// <returns>返回文件的访问地址，如果不成功则返回<c>null</c>。</returns>
        private async Task<string> CreateAsync(MediaFileInfo mediaFile, string path, IFormFile file)
        {
            var md5 = Md5(path);
            var storage = await _storages.FindAsync(s => s.Name == md5);
            if (storage != null)
            {
                mediaFile.FileId = storage.FileId;
                mediaFile.Length = storage.Length;
                if (await _repository.UpdateAsync(f => f.Id == mediaFile.Id, new { mediaFile.Length, mediaFile.FileId }))
                    return mediaFile.Url;
                return null;
            }
            storage = new StorageFileInfo();
            if (await _repository.BeginTransactionAsync(async db =>
            {
                var storages = db.As<StorageFileInfo>();
                storage.Name = md5;
                storage.Length = file.Length;
                storage.ContentType = file.ContentType;
                if (await storages.CreateAsync(storage))
                {
                    mediaFile.Length = file.Length;
                    mediaFile.FileId = storage.FileId;
                    return await db.UpdateAsync(f => f.Id == mediaFile.Id, new { mediaFile.Length, mediaFile.FileId });
                }
                return false;
            }))
            {
                StoredFile(path, storage);
                return mediaFile.Url;
            }
            File.Delete(path);
            return null;
        }

        /// <summary>
        /// 已经存在物理文件，添加媒体文件信息。
        /// </summary>
        /// <param name="storage">物理文件存储信息。</param>
        /// <param name="extension">扩展名。</param>
        /// <param name="title">标题。</param>
        /// <param name="isUnique">判断每个物理文件和媒体文件信息是否一一对应。</param>
        /// <param name="mediaType">媒体类型。</param>
        /// <param name="targetId">所属实例Id。</param>
        /// <param name="directoryId">文件目录Id。</param>
        /// <returns>返回当前文件的访问地址，如果失败返回<c>null</c>。</returns>
        private async Task<string> CreateAsync(StorageFileInfo storage, string extension, string title, bool isUnique, string mediaType, int targetId, int directoryId)
        {
            var mediaFile = await _repository.FindAsync(m => m.FileId == storage.FileId);
            if (mediaFile != null && isUnique)
                return mediaFile.Url;
            mediaFile = new MediaFileInfo();
            mediaFile.Id = Guid.NewGuid();
            mediaFile.Extension = extension;
            mediaFile.Type = mediaType;
            mediaFile.TargetId = targetId;
            mediaFile.DirectoryId = directoryId;
            mediaFile.Length = storage.Length;
            mediaFile.FileId = storage.FileId;
            mediaFile.Title = title;
            if (await _repository.CreateAsync(mediaFile))
                return mediaFile.Url;
            return null;
        }

        /// <summary>
        /// 从临时文件中的文件全新添加文件信息。
        /// </summary>
        /// <param name="path">临时文件的物理路径。</param>
        /// <param name="md5">文件Md5值。</param>
        /// <param name="extension">扩展名称。</param>
        /// <param name="title">标题。</param>
        /// <param name="contentType">内容类型。</param>
        /// <param name="length">大小。</param>
        /// <param name="mediaType">媒体类型。</param>
        /// <param name="targetId">所属实例Id。</param>
        /// <param name="directoryId">文件目录Id。</param>
        /// <param name="mediaId">媒体文件的Id。</param>
        /// <returns>返回当前文件的访问地址，如果失败返回<c>null</c>。</returns>
        private async Task<string> CreateAsync(string path, string md5, string extension, string title, string contentType, long length, string mediaType, int targetId, int directoryId, Guid? mediaId = null)
        {
            var storage = new StorageFileInfo();
            var mediaFile = new MediaFileInfo();
            mediaFile.Id = mediaId ?? Guid.NewGuid();
            if (await _repository.BeginTransactionAsync(async db =>
            {
                var storages = db.As<StorageFileInfo>();
                storage.Name = md5;
                storage.Length = length;
                storage.ContentType = contentType ?? ContentType.GetContentType(extension);
                if (await storages.CreateAsync(storage))
                {
                    mediaFile.Title = title;
                    mediaFile.Extension = extension;
                    mediaFile.Type = mediaType;
                    mediaFile.TargetId = targetId;
                    mediaFile.DirectoryId = directoryId;
                    mediaFile.Length = length;
                    mediaFile.FileId = storage.FileId;
                    return await db.CreateAsync(mediaFile);
                }
                return false;
            }))
            {
                StoredFile(path, storage);
                return mediaFile.Url;
            }
            File.Delete(path);
            return null;
        }

        private void StoredFile(string path, StorageFileInfo storage)
        {
            var mediaPath = GetMediaPath(storage);
            if (File.Exists(mediaPath))
            {//文件已经存在，但是数据库重建就会出现重复文件的事情
                File.Delete(path);
                return;
            }
            var dir = Path.GetDirectoryName(mediaPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.Move(path, mediaPath);
        }

        /// <summary>
        /// 获取文件的MD5值。
        /// </summary>
        /// <param name="path">文件的物理路径。</param>
        /// <returns>返回MD5值。</returns>
        private string Md5(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var md5 = MD5.Create();
                return md5.ComputeHash(fs).ToHexString();
            }
        }
    }
}