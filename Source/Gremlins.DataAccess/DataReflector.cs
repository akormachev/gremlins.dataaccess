namespace Gremlins.DataAccess
{
    /// <summary>
    /// Abstract data reflector
    /// </summary>
    public abstract class DataReflector
    {
        internal abstract object ReflectObject(object value, DataRecord record = null);
    }
}
