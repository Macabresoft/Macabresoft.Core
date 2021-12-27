namespace Macabresoft.Core;

using System.Collections.Generic;

/// <summary>
/// Extensions for anything that implements <see cref="IList{T}" />.
/// </summary>
public static class ListExtensions {
    /// <summary>
    /// Inserts the item at the provided index if valid, otherwise it adds it to the end when the index is greater than the count or inserts it at the start if the provided index is less than 0.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="index">The index.</param>
    /// <param name="item">The item to add.</param>
    /// <typeparam name="T">The type.</typeparam>
    public static void InsertOrAdd<T>(this IList<T> list, int index, T item) {
        if (index >= list.Count) {
            list.Add(item);
        }
        else if (index < 0) {
            list.Insert(0, item);
        }
        else {
            list.Insert(index, item);
        }
    }
}