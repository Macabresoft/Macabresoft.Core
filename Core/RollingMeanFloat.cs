namespace Macabresoft.Core;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Averages <see cref="float" /> values.
/// </summary>
public sealed class RollingMeanFloat {
    private readonly int _size;
    private readonly List<float> _values = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="RollingMeanFloat" /> class.
    /// </summary>
    /// <param name="size">The size.</param>
    /// <exception cref="ArgumentOutOfRangeException">size</exception>
    public RollingMeanFloat(int size) {
        if (size <= 0) {
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        this._size = size;
    }

    /// <summary>
    /// Gets the mean value.
    /// </summary>
    /// <value>The mean value.</value>
    public float MeanValue { get; private set; }

    /// <summary>
    /// Adds the specified frequency.
    /// </summary>
    /// <param name="value">The frequency.</param>
    public void Add(float value) {
        this._values.Add(value);
        if (this._values.Count > this._size) {
            this._values.RemoveAt(0);
        }

        this.CalculateAverageFrequency();
    }

    /// <summary>
    /// Clears the list of values being averaged and sets the mean value back to 0.
    /// </summary>
    public void Clear() {
        this._values.Clear();
        this.MeanValue = 0f;
    }

    /// <summary>
    /// Removes the first item in the rolling average.
    /// </summary>
    public void Remove() {
        if (this._values.Any()) {
            this._values.RemoveAt(0);
            this.CalculateAverageFrequency();
        }
    }

    private void CalculateAverageFrequency() {
        if (this._values.Any()) {
            this.MeanValue = this._values.Sum() / this._values.Count;
        }
        else {
            this.MeanValue = 0f;
        }
    }
}