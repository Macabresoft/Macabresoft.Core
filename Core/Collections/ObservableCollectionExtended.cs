namespace Macabresoft.Core {
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary>
    /// An observable collection with additional functionality such as the ability to reset or add a range of items without setting off multple events.
    /// </summary>
    /// <typeparam name="T">The type contained within this collection.</typeparam>
    public class ObservableCollectionExtended<T> : ObservableCollection<T> {

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionExtended{T}" /> class.
        /// </summary>
        public ObservableCollectionExtended() : base() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionExtended{T}" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public ObservableCollectionExtended(IEnumerable<T> items) : base(items) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableCollectionExtended{T}" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public ObservableCollectionExtended(List<T> items) : base(items) {
        }

        /// <summary>
        /// Adds a range of items to the collection.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddRange(IEnumerable<T> items) {
            foreach (var item in items) {
                this.Items.Add(item);
            }

            this.RaiseEvents();
        }

        /// <summary>
        /// Replaces the item at the provided index with the provided item.
        /// </summary>
        /// <param name="item">The new item.</param>
        /// <param name="index">The index.</param>
        public void Replace(T item, int index) {
            if (index < this.Items.Count) {
                var originalItem = this.Items[index];
                this.Items.RemoveAt(index);
                this.Items.Insert(index, item);
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, originalItem));
            }
        }

        /// <summary>
        /// Resets this collection to only contain the provided items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Reset(IEnumerable<T> items) {
            this.Items.Clear();
            this.AddRange(items);
            this.RaiseEvents();
        }

        private void RaiseEvents() {
            this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.Count)));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}