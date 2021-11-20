namespace Macabresoft.Core {

    using System;

    /// <summary>
    /// Extension methods to handle math.
    /// </summary>
    public static class MathExtensions {

        /// <summary>
        /// Represents 2 * <see cref="Math.PI" />.
        /// </summary>
        public const float TwoPi = (float)Math.PI * 2f;

        /// <summary>
        /// Clamps a <see cref="ushort" /> between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The clamped value.</returns>
        public static ushort Clamp(this ushort value, ushort minimum, ushort maximum) {
            return Math.Max(minimum, Math.Min(maximum, value));
        }

        /// <summary>
        /// Clamps a <see cref="float" /> between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minimum">The minimum.</param>
        /// <param name="maximum">The maximum.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(this float value, float minimum, float maximum) {
            return Math.Max(minimum, Math.Min(maximum, value));
        }

        /// <summary>
        /// Determines whether two floating point values have minimum difference.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="difference">The acceptable difference to be considered equal.</param>
        /// <returns><c>true</c> if there is minimal difference; otherwise, <c>false</c>.</returns>
        public static bool HasMinimalDifference(this float value1, float value2, float difference = 0.00001f) {
            return Math.Abs(value1 - value2) <= difference;
        }

        /// <summary>
        /// Determines whether the value is a power of two.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the value is a power of two; otherwise, <c>false</c>.</returns>
        public static bool IsPowerOfTwo(this int value) {
            return (value != 0) && ((value & (value - 1)) == 0);
        }

        /// <summary>
        /// Normalizes an angle to the radian unit circle.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The normalized angle.</returns>
        public static float NormalizeAngle(this float value) {
            while (value >= TwoPi) {
                value -= TwoPi;
            }

            while (value < 0) {
                value += TwoPi;
            }

            return value;
        }

        /// <summary>
        /// Converts a value to negative one or one.
        /// </summary>
        /// <remarks>Zero is treated as positive in this case.</remarks>
        /// <param name="value">The value.</param>
        /// <returns>The sign.</returns>
        public static float ToSign(this float value) {
            return value < 0 ? -1f : 1f;
        }
    }
}