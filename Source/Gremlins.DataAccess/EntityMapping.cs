using System;
using System.Collections.Generic;
using System.Linq;

namespace Gremlins.DataAccess
{
    public abstract class EntityMapping
    {
        
        #region Fields

        private readonly string _name;

        private readonly Func<object, object, bool> _join;

        private readonly Action<object, IEnumerable<object>> _setter;

        #endregion

        #region Properties

        public abstract Type ChildType { get; }

        public abstract Type ChildListType { get; }

        public Func<object, object, bool> JoinCondition
        {
            get { return _join; }
        }

        public string Name
        {
            get { return _name; }
        }

        public abstract Type ParentType { get; }

        public Action<object, IEnumerable<object>> Setter
        {
            get { return _setter; }
        } 

        #endregion

        #region Constructors

        public EntityMapping(string name, Action<object, IEnumerable<object>> setter, Func<object, object, bool> join)
        {
            _name = name;
            _join = join;
            _setter = setter;
        }

        #endregion

        #region Internal methods

        internal bool IsMatch(object parent)
        {
            return this.ParentType.IsAssignableFrom(parent.GetType());                
        }

        #endregion
    }

    public abstract class EntityMapping<TParent, TChild>: EntityMapping
    {

        #region Properties

        public override Type ChildType
        {
            get { return typeof(TChild); }
        }

        public override Type ChildListType
        {
            get { return typeof(List<TChild>); }
        }

        public override Type ParentType
        {
            get { return typeof(TParent); }
        }

        #endregion

        #region Constructors

        public EntityMapping(string name, Action<TParent, IEnumerable<TChild>> setter, Func<TParent, TChild, bool> join)
            :base(name, (parent, childs) => setter((TParent)parent, childs.Cast<TChild>()), (parent, child) => join((TParent)parent, (TChild)child))    
        { }

        #endregion

    }

    public class ListEntityMapping<TParent, TChild>: EntityMapping<TParent, TChild>
    {
        public ListEntityMapping(string name, Action<TParent, IList<TChild>> setter, Func<TParent, TChild, bool> join)
            :base(name, (parent, childs) => setter(parent, childs == null ? null : childs.ToList()), join)
        {  }
    }
}
