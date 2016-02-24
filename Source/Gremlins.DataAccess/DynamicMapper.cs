using System.Collections.Generic;
using System.Dynamic;

namespace Gremlins.DataAccess
{
    public class DynamicMapper: EntityMapper<dynamic>
    {
        protected override dynamic Read(DataRecord record, string path)
        {
            var dynamic = new ExpandoObject();
            var dictionary = dynamic as IDictionary<string, object>;
            foreach (var field in record.GetFields())                
                dictionary.Add(field, record.Value(path, field));
            return dynamic;
        }
    }
}
