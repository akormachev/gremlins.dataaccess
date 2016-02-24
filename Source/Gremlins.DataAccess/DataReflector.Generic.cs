namespace Gremlins.DataAccess
{
    /// <summary>
    /// Abstract generic data reflector. Root of any data reflector object.
    /// </summary>
    /// <typeparam name="T">Type of entity read by DataReflector</typeparam>
    public abstract class DataReflector<T> : DataReflector
    {

        #region Protected methods

        /// <summary>
        /// Reflect entity by data reflector
        /// </summary>
        /// <param name="value">Value to reflect</param>    
        /// <returns>Entity</returns>
        protected abstract T Reflect(object value);

        /// <summary>
        /// Reflect entity by data reflector
        /// </summary>
        /// <param name="value">Value to reflect</param>
        /// <param name="record">Data source image</param>        
        /// <returns>Entity</returns>
        protected internal virtual T Reflect(object value, object parameters)
        {
            return Reflect(value);
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Read entity as object from data source
        /// </summary>
        /// <param name="record">Data source image</param>
        /// <param name="path">Current relative path</param>
        /// <returns>Entity</returns>
        internal override object ReflectObject(object value, DataRecord record = null)
        {
            return Reflect(value, record);                        
        }        

        #endregion
    }
}
