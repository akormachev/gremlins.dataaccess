using System;
namespace Gremlins.DataAccess
{
    static class DataValueExtractor
    {
        /// <summary>
        /// Get boolean
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Boolean value</returns>
        public static bool Boolean(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(bool);
            if (obj is string)
            {
                if (string.IsNullOrEmpty(obj as string) || (obj as string) == "\0")
                    return false;
                return true;
            }

            return Convert.ToBoolean(obj);
        }

        /// <summary>
        /// Get byte
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Byte value</returns>
        public static byte Byte(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(byte);
            return Convert.ToByte(obj);
        }

        /// <summary>
        /// Get byte
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Byte value</returns>
        public static byte[] ByteArray(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(byte[]);
            return (byte[])obj;
        }

        /// <summary>
        /// Get DataTime
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>DateTime value</returns>
        public static DateTime Date(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(DateTime);
            return Convert.ToDateTime(obj);
        }

        /// <summary>
        /// Get decimal
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Decimal value</returns>
        public static decimal Decimal(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(decimal);
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// Get T1
        /// </summary>
        /// <typeparam name="T1">Enum type</typeparam>
        /// <param name="obj">Object</param>
        /// <returns>Enum</returns>
        public static T1 Enum<T1>(object obj)
            where T1 : struct
        {
            if (obj == null || obj == DBNull.Value)
                return default(T1);
            return (T1)System.Enum.ToObject(typeof(T1), Convert.ToInt64(obj));
        }

        /// <summary>
        /// Get guid-value
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Guid value</returns>
        public static Guid Guid(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(Guid);
            return (Guid)obj;
        }

        /// <summary>
        /// Get short
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Int16 value</returns>
        public static short Int16(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(short);
            return Convert.ToInt16(obj);
        }

        /// <summary>
        /// Get int
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Int32 value</returns>
        public static int Int32(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(int);
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// Get long
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Int64 value</returns>
        public static long Int64(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(long);
            return Convert.ToInt64(obj);
        }

		/// <summary>
		/// Get bool?
		/// </summary>
		/// <param name="obj">Object</param>
		/// <returns>Nullable&lt;bool&gt;</returns>
		public static bool? NBoolean(object obj)
		{
			if (obj == null || obj == DBNull.Value)
				return null;
			if (obj is string)
			{
				if (string.IsNullOrEmpty(obj as string) || (obj as string) == "\0")
					return false;
				return true;
			}
			return Convert.ToBoolean(obj);
		}
        
		/// <summary>
        /// Get DateTime?
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Nullable&lt;DateTime&gt;</returns>
        public static DateTime? NDate(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            return Convert.ToDateTime(obj);
        }

        /// <summary>
        /// Get decimal?
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Nullable&lt;decimal&gt;</returns>
        public static decimal? NDecimal(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// Get T1?
        /// </summary>
        /// <typeparam name="T1">Enum type</typeparam>
        /// <param name="obj">Object</param>
        /// <returns>Nullable&lt;Enum&gt;</returns>
        public static T1? NEnum<T1>(object obj)
            where T1 : struct
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            return (T1)System.Enum.ToObject(typeof(T1), Convert.ToInt64(obj));
        }

        /// <summary>
        /// Get ushort?
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Nullable&lt;ushort&gt;</returns>
        public static ushort? NUInt16(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            return Convert.ToUInt16(obj);
        }

        /// <summary>
        /// Get int?
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Nullable&lt;Int32&gt;</returns>
        public static int? NInt32(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// Get float?
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Nullable&lt;Single&gt;</returns>
        public static float Single(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(float);
            return Convert.ToSingle(obj);

        }

        public static string String(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(string);
            return obj.ToString();
        }

        public static ushort UInt16(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(ushort);
            return Convert.ToUInt16(obj);
        }

        public static uint UInt32(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(uint);
            return Convert.ToUInt32(obj);
        }

        public static ulong UInt64(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(ulong);
            return Convert.ToUInt64(obj);
        }

        public static System.Xml.Linq.XElement XElement(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return default(System.Xml.Linq.XElement);
            return System.Xml.Linq.XElement.Parse(obj.ToString());
        }
    }
}
