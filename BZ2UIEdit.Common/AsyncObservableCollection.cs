using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;

namespace BZ2UIEdit.Common
{
    public class AsyncObservableCollection<T> : ObservableCollection<T>
    {
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        public AsyncObservableCollection() : base()
        {
        }

        public AsyncObservableCollection(IEnumerable<T> items) : base(items)
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (SynchronizationContext.Current == _synchronizationContext)
            {
                // Execute the CollectionChanged event on the current thread
                RaiseCollectionChanged(e);
            }
            else
            {
                // Raises the CollectionChanged event on the creator thread
                _synchronizationContext.Send(RaiseCollectionChanged, e);
            }
        }

        private void RaiseCollectionChanged(object param)
        {
            // We are in the creator thread. Call the base implementation directly.
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }
    }
}
