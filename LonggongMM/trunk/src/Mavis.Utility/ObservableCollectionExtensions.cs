using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Mavis.Utility
{
    public static class ObservableCollectionExtensions
    {
        public static List<T> FindAll<T> (this ObservableCollection<T> observableCollection, Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            var list = new List<T>();
            for (int i = 0; i < observableCollection.Count; i++)
            {
                if (match(observableCollection[i]))
                {
                    list.Add(observableCollection[i]);
                }
            }
            return list;
        }

        public static T Find<T> (this ObservableCollection<T> observableCollection, Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            for (int i = 0; i < observableCollection.Count; i++)
            {
                if (match(observableCollection[i]))
                {
                    return observableCollection[i];
                }
            }

            return default(T);
        }
    }
}
