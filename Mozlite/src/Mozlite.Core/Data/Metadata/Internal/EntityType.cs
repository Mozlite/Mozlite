﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Mozlite.Core;
using Mozlite.Extensions;

namespace Mozlite.Data.Metadata.Internal
{
    /// <summary>
    /// 实体类型。
    /// </summary>
    public class EntityType : Annotatable, IEntityType
    {
        private readonly Dictionary<Ignore, IEnumerable<IProperty>> _ignores = new Dictionary<Ignore, IEnumerable<IProperty>>();
        private readonly SortedDictionary<string, Property> _properties;
        /// <summary>
        /// 初始化类<see cref="EntityType"/>。
        /// </summary>
        /// <param name="type">当前类型实例。</param>
        public EntityType(Type type)
        {
            _properties = new SortedDictionary<string, Property>(StringComparer.OrdinalIgnoreCase);
            foreach (var info in type.GetProperties(BindingFlags.Instance|BindingFlags.Public))
            {
                if (info.CanRead && info.CanWrite)
                {
                    _properties.Add(info.Name, new Property(info, this));
                }
            }
            ClrType = type;
            Name = type.DisplayName();
            var properties = _properties.Values
                        .Where(property => property.IsPrimaryKey)
                        .ToList();
            if (properties.Count == 0 && Identity != null)
            {
                Identity.IsPrimaryKey = true;
                properties = new List<Property> { Identity };
            }
            if (properties.Count > 0)
                PrimaryKey = new Key(properties);
        }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 类型。
        /// </summary>
        public Type ClrType { get; }

        /// <summary>
        /// 获取主键。
        /// </summary>
        public IKey PrimaryKey { get; }

        /// <summary>
        /// 自增长列。
        /// </summary>
        public Property Identity { get; set; }

        IProperty IEntityType.Identity => Identity;

        /// <summary>
        /// 通过名称查找属性实例。
        /// </summary>
        /// <param name="name">属性名称。</param>
        /// <returns>返回属性实例对象。</returns>
        public IProperty FindProperty(string name)
        {
            Property property;
            _properties.TryGetValue(name, out property);
            return property;
        }

        /// <summary>
        /// 根据忽略特性获取属性集合。
        /// </summary>
        /// <param name="ignore">忽略特性。</param>
        /// <returns>返回属性集合。</returns>
        public IEnumerable<IProperty> FindProperties(Ignore ignore)
        {
            IEnumerable<IProperty> properties;
            if (_ignores.TryGetValue(ignore, out properties))
                return properties;
            properties = _properties.Values.Where(property =>
            {
                Ignore? current = null;
                if (property.IsIdentity)
                    current = Ignore.Upsert;
                else
                    current = property.PropertyInfo.GetCustomAttribute<IgnoreAttribute>()?.Ignore;
                return (current & ignore) != ignore;
            });
            _ignores[ignore] = properties;
            return properties;
        }

        /// <summary>
        /// 获取当前类型的所有属性列表。
        /// </summary>
        /// <returns>所有属性列表。</returns>
        public IEnumerable<IProperty> GetProperties()
        {
            return _properties.Values;
        }

        /// <summary>
        /// 从数据库读取器中读取当前实例对象。
        /// </summary>
        /// <typeparam name="TModel">模型类型。</typeparam>
        /// <param name="reader">数据库读取器。</param>
        /// <returns>返回当前模型实例对象。</returns>
        public TModel Read<TModel>(DbDataReader reader)
        {
            var model = Activator.CreateInstance<TModel>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var property = FindProperty(reader.GetName(i));
                if (property != null)
                {
                    var value = reader.GetValue(i);
                    if (value == DBNull.Value)
                        value = null;
                    property.Set(model, value);
                }
                else if (model is ExtendObjectBase)
                {
                    ((ExtendObjectBase)(object)model)[reader.GetName(i)] = reader.GetValue(i)?.ToString();
                }
            }
            return model;
        }
    }
}