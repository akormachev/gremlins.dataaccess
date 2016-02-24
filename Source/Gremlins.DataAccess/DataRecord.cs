using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace Gremlins.DataAccess
{
    public sealed class DataRecord
	{

		#region Private fields

		private readonly IDataReader _reader;

		private readonly IDictionary<string, int> _fields;

		private readonly EntityMapperCollection _mappers;

		private readonly DataReflectorCollection _reflectors;

		private readonly DataCache _cache;

		private string _currentPath;

		private readonly object _bag;

		#endregion

		#region Constructors

		/// <summary>
		/// Create instance of <see cref="DataRecord"/>
		/// </summary>
		/// <param name="reader">Reader to fetch data</param>
		public DataRecord(IDataReader reader, EntityMapperCollection mappers, DataReflectorCollection reflectors, object bag)
		{
			_reader = reader;
			_reflectors = reflectors;
			_fields = new Dictionary<string, int>();
			_mappers = mappers;
			_cache = new DataCache();
			_bag = bag;
		}

		#endregion

		#region Properties

		public object Bag
		{
			get { return _bag; }
		}

		#endregion

		#region Internal methods

		internal void AddField(string name, int ordinal)
		{
			if (_fields.ContainsKey(name))
				throw new DataLayerException(string.Format(Properties.Resources.DuplicatePartName, name));
			_fields.Add(name, ordinal);
		}

		internal IEnumerable<string> GetFields()
		{
			return _fields.Keys;
		}

		internal void InitCache(Type itemType, Type listType, string path)
		{
			_cache.Register(itemType, listType, path);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		internal void SetPath(string path)
		{            
			_fields.Clear();
			_currentPath = path;
		}

		/// <summary>
		/// Register entity as result entity
		/// </summary>
		/// <param name="entity">Entity</param>
		/// <param name="path">Property path to current entity</param>
		internal void RegisterEntity<TEntity>(TEntity entity, string path)
		{
			_cache.Add<TEntity>(path, entity);
		}

		internal Type GetEntityType(string path)
		{
			return _cache.ItemType(path);
		}

		internal IEnumerable<T> TakeEntities<T>(string rootPath)
		{
			return _cache.Get<T>(rootPath);
		}

		internal IEnumerable TakeEntities(string rootPath)
		{
			return _cache.Get(rootPath);
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Get Boolean value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>bool-value</returns>
		public bool Boolean(string path, string field)
		{
			return DataValueExtractor.Boolean(Value(path, field));
		}

		/// <summary>
		/// Get Byte value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>byte-value</returns>
		public byte Byte(string path, string field)
		{
			return DataValueExtractor.Byte(Value(path, field));
		}

		/// <summary>
		/// Get Byte[] value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>byte[]-value</returns>
		public byte[] ByteArray(string path, string field)
		{
			return DataValueExtractor.ByteArray(Value(path, field));
		}

		/// <summary>
		/// Get class
		/// </summary>
		/// <typeparam name="TClass">Entity type</typeparam>
		/// <param name="path">Current property path</param>
		/// <param name="part">Class partial name</param>
		/// <returns>Class</returns>
		public TClass Class<TClass>(string path, string part, object parameters = null)
		{
			if (path == null)
				throw new DataLayerException("Ошибка доступа к данным. [table alias is null] Не указано имя выходного параметра.");

			if (part == null)
				throw new DataLayerException("Ошибка доступа к данным. [link alias is null] Не указан алиас для подчиненного класса.");           

			string key = GetDataItemKey(path, part);
				   
			if (IsNull(key))
				return default(TClass);

			TClass entity = default(TClass);

			
			var reflector = _reflectors.Get<TClass>();

			// If we have reflector then we will reflect entity
			if (reflector != null)
				entity = reflector.Reflect(Value(path, part), parameters);
			else
			{
				EntityMapper<TClass> mapper = _mappers.Get<TClass>();
				if (mapper == null)
					return default(TClass);

				entity = mapper.TakeEntity(this, key);

				_cache.Add<TClass>(string.Join("#", path, part), entity);
			}

			return entity;
		}

		/// <summary>
		/// Get DateTime value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>DateTime-value</returns>
		public System.DateTime Date(string path, string field)
		{
			return DataValueExtractor.Date(Value(path, field));
		}

		/// <summary>
		/// Get Decimal value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>decimal-value</returns>
		public decimal Decimal(string path, string field)
		{
			return DataValueExtractor.Decimal(Value(path, field));
		}

		/// <summary>
		/// Get T enum value
		/// </summary>
		/// <typeparam name="T">Type of enum</typeparam>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>T-value</returns>
		public T Enum<T>(string path, string field)
			where T : struct
		{
			return DataValueExtractor.Enum<T>(Value(path, field));
		}

		/// <summary>
		/// Get Guid value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>Guid-value</returns>
		public Guid Guid(string path, string field)
		{
			return DataValueExtractor.Guid(Value(path, field));
		}

		/// <summary>
		/// Get Int16 value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>short-value</returns>
		public short Int16(string path, string field)
		{
			return DataValueExtractor.Int16(Value(path, field));
		}

		/// <summary>
		/// Get Int32 value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>int-value</returns>
		public int Int32(string path, string field)
		{
			return DataValueExtractor.Int32(Value(path, field));
		}

		/// <summary>
		/// Get Int64 value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>long-value</returns>
		public long Int64(string path, string field)
		{
			return DataValueExtractor.Int64(Value(path, field));
		}

		/// <summary>
		/// Get Nullable&lt;bool&gt; value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>bool?-value</returns>
		public bool? NBoolean(string path, string field)
		{
			return DataValueExtractor.NBoolean(Value(path, field));
		}
		
		/// <summary>
		/// Get Nullable&lt;DateTime&gt; value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>DateTime?-value</returns>
		public DateTime? NDate(string path, string field)
		{
			return DataValueExtractor.NDate(Value(path, field));
		}

		/// <summary>
		/// Get Nullable&lt;T&gt; enum value
		/// </summary>
		/// <typeparam name="T">Type of enum</typeparam>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>T?-value</returns>
		public T? NEnum<T>(string path, string field)
			where T: struct
		{
			return DataValueExtractor.NEnum<T>(Value(path, field));
		}

		/// <summary>
		/// Get Nullable&lt;Int32&gt; value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>int?-value</returns>
		public int? NInt32(string path, string field)
		{
			return DataValueExtractor.NInt32(Value(path, field));
		}

		/// <summary>
		/// Get Single value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>float value</returns>
		public float Single(string path, string field)
		{
			return DataValueExtractor.Single(Value(path, field));
		}

		/// <summary>
		/// Get String value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>string-value</returns>
		public string String(string path, string field)
		{
			return DataValueExtractor.String(Value(path, field));
		}

		/// <summary>
		/// Get UInt16 value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>ushort-value</returns>
		public ushort UInt16(string path, string field)
		{
			return DataValueExtractor.UInt16(Value(path, field));
		}

		/// <summary>
		/// Get UInt32 value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>uint-value</returns>
		public uint UInt32(string path, string field)
		{
			return DataValueExtractor.UInt32(Value(path, field));
		}

		/// <summary>
		/// Get Int64 value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>long-value</returns>
		public ulong UInt64(string path, string field)
		{
			return DataValueExtractor.UInt64(Value(path, field));
		}

		/// <summary>
		/// Get object-value
		/// </summary>
		/// <param name="table">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>object-value</returns>
		public object Value(string table, string field)
		{            
			if (table == null)
				throw new DataLayerException("Ошибка доступа к данным. [table alias is null] Не указано имя выходного параметра.");
		
			if (!table.StartsWith(_currentPath))
				throw new DataLayerException(string.Format("Ошибка доступа к данным. [table mapping is broken] Неверно указано сопоставление между хранимой процедурой и классом для поля {1} по пути {0}.", field, table));

			string key = string.Concat(table, "#", field).Substring(_currentPath.Length + 1);
			
			if (!this._fields.ContainsKey(key))
				return null;            
			return _reader.GetValue(this._fields[key]);
		}

		/// <summary>
		/// Get XElement-value
		/// </summary>
		/// <param name="path">Current property path</param>
		/// <param name="field">Column name</param>
		/// <returns>XElement-value</returns>
		public XElement XElement(string path, string field)
		{
			return DataValueExtractor.XElement(Value(path, field));
		}

		#endregion

		#region Private methods

		private string GetDataItemKey(string table, string link)
		{
			var builder = new StringBuilder();
			if (table != null)
				builder.Append(table);
			if (table != null && !string.IsNullOrEmpty(link))
				builder.AppendFormat("#{0}", link);
			return builder.ToString();
		}

		/// <summary>
		/// Check if class is NULL
		/// </summary>
		/// <param name="prefix">class prefix</param>
		/// <returns>IsNull flag</returns>
		private bool IsNull(string prefix)
		{
			if (!prefix.StartsWith(_currentPath))
				return true;

			var fieldMatch = prefix.Substring(_currentPath.Length + 1);

			foreach (var field in _fields)
				if (field.Key.StartsWith(fieldMatch))                    
						if (!_reader.IsDBNull(field.Value))
							return false;
			return true;
		}

		#endregion


	}
}
