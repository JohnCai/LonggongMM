using System;

namespace Mavis.Core
{
    public class Entity: Entity<int>{}

    public class Entity<TId> : EditableObject, IEntity<TId>
    {
        private TId _id;

        private int? _cachedHashcode;

        /// <summary>
        /// To help ensure hashcode uniqueness, a carefully selected random number multiplier 
        /// is used within the calculation.  Goodrich and Tamassia's Data Structures and
        /// Algorithms in Java asserts that 31, 33, 37, 39 and 41 will produce the fewest number
        /// of collissions.  See http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
        /// for more information.
        /// </summary>
        private const int HASH_MULTIPLIER = 31;

        #region Implementation of IEntity<int>

        public virtual TId ID
        {
            get { return _id; }
            protected set
            {
                _id = value;
                NotifyPropertyChanged("ID");
            }
        }



        public virtual bool IsTransient
        {
            get { return Equals(ID, default(TId)) || ID.Equals(default(TId)); }
        }

        #endregion

        #region MyRegion

        public override bool Equals(object obj)
        {
            //return base.Equals(obj);
            var compareTo = obj as Entity<TId>;

            if (compareTo == null || !GetType().Equals(compareTo.GetTypeUnproxied()))
                return false;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (HasSameNonDefaultIdAs(compareTo))
                return true;

            // Since the Ids aren't the same, both of them must be transient to 
            // compare domain signatures; because if one is transient and the 
            // other is a persisted entity, then they cannot be the same object.
            return IsTransient && compareTo.IsTransient &&
                HasSameObjectSignatureAs(compareTo);
        }

        public override int GetHashCode()
        {
            if (_cachedHashcode.HasValue)
                return _cachedHashcode.Value;

            if (IsTransient)
            {
                _cachedHashcode = base.GetHashCode();
            }
            else
            {
                unchecked
                {
                    // It's possible for two objects to return the same hash code based on 
                    // identically valued properties, even if they're of two different types, 
                    // so we include the object's type in the hash calculation
                    int hashCode = GetType().GetHashCode();
                    _cachedHashcode = (hashCode * HASH_MULTIPLIER) ^ ID.GetHashCode();
                }
            }

            return _cachedHashcode.Value;
        }

        protected virtual bool HasSameObjectSignatureAs(Entity<TId> entity)
        {
            //todo
            return false;
        }

        /// <summary>
        /// When NHibernate proxies objects, it masks the type of the actual entity object.
        /// This wrapper burrows into the proxied object to get its actual type.
        /// 
        /// Although this assumes NHibernate is being used, it doesn't require any NHibernate
        /// related dependencies and has no bad side effects if NHibernate isn't being used.
        /// 
        /// Related discussion is at http://groups.google.com/group/sharp-architecture/browse_thread/thread/ddd05f9baede023a ...thanks Jay Oliver!
        /// </summary>
        protected virtual Type GetTypeUnproxied()
        {
            return GetType();
        } 

        private bool HasSameNonDefaultIdAs(Entity<TId> compareTo)
        {
            return !IsTransient && !compareTo.IsTransient && ID.Equals(compareTo.ID);
        }

        #endregion
    }

    
}