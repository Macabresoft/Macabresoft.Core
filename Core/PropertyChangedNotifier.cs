namespace Macabresoft.Core;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

/// <summary>
/// A base class that implements <see cref="INotifyPropertyChanged" />
/// </summary>
public class PropertyChangedNotifier : INotifyPropertyChanged, IDisposable {
    private bool _isDisposed;

    /// <summary>
    /// Occurs when a property changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <inheritdoc />
    public void Dispose() {
        if (!this._isDisposed) {
            this.OnDisposing();
            this._isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Disposes <see cref="PropertyChanged" />.
    /// </summary>
    protected void DisposePropertyChanged() {
        this.PropertyChanged = null;
    }

    /// <summary>
    /// Called when this <see cref="IDisposable" /> is being disposed.
    /// </summary>
    protected virtual void OnDisposing() {
        this.DisposePropertyChanged();
    }

    /// <summary>
    /// Raises the property changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments.</param>
    protected void RaisePropertyChanged(object? sender, PropertyChangedEventArgs args) {
        this.PropertyChanged?.Invoke(sender, args);
    }

    /// <summary>
    /// Raises the property changed event.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Sets the specified field.
    /// </summary>
    /// <typeparam name="T">The type being set.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="value">The value.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>A value indicating whether or not the value was successfully set.</returns>
    protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = "") {
        var result = false;
        if (!Equals(field, value)) {
            field = value;
            this.RaisePropertyChanged(propertyName);
            result = true;
        }

        return result;
    }
}