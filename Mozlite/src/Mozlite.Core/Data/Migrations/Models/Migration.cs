﻿using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Models
{
    /// <summary>
    /// 数据库实例。
    /// </summary>
    [Table("$__Migrations")]
    public class Migration
    {
        /// <summary>
        /// 迁移类型。
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 版本。
        /// </summary>
        public int Version { get; set; }
    }
}