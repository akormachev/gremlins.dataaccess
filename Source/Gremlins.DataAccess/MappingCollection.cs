using System;
using System.Collections.Generic;

namespace Gremlins.DataAccess
{
    public class MappingCollection : IEnumerable<EntityMapping>
    {
        #region Static field

        private static readonly MappingCollection _empty;

        #endregion

        #region Static properties

        public static MappingCollection Empty { get { return _empty; } }

        #endregion

        #region Static constructors

        static MappingCollection()
        {
            _empty = new MappingCollection();
        }

        #endregion

        #region Static methods

        public static MappingCollection Create()
        {
            return new MappingCollection();
        }

        //public static MappingCollection Create(params EntityMapping[] results)
        //{
        //    return new MappingCollection(results);
        //}

        #endregion

        #region Fields

        private readonly List<EntityMapping> _items;

        #endregion

        private MappingCollection()
        {
            _items = new List<EntityMapping>();
        }

        #region Public methods

        public MappingCollection List<TParent, TChild>(string name, Action<TParent, IList<TChild>> setter, Func<TParent, TChild, bool> join)
        {
            _items.Add(new ListEntityMapping<TParent, TChild>(name, setter, join));
            return this;
        }

        #endregion

        public IEnumerator<EntityMapping> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
