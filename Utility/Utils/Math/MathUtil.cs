﻿///====================================================================================================
///
///     MathUtil by
///     - CantyCanadian
///
///====================================================================================================

using UnityEngine;

namespace Canty
{
    public static class MathUtil
    {
        /// <summary>
        /// Decrements a value, wrapping to max if it goes under min.
        /// </summary>
        /// <param name="value">Original value.</param>
        /// <param name="minus">Value to decrement by.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>Returns decremented index.</returns>
        public static void DecrementWrap(ref int value, int minus, int min, int max)
        {
            value -= minus;

            if (value < min)
            {
                value = max - 1;
            }
        }

        /// <summary>
        /// Increments a value, wrapping to min if it goes over max.
        /// </summary>
        /// <param name="value">Original value.</param>
        /// <param name="minus">Value to increment by.</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <returns>Returns incremented index.</returns>
        public static void IncrementWrap(ref int value, int add, int min, int max)
        {
            value += add;

            if (value > max - 1)
            {
                value = min;
            }
        }

        /// <summary>
        /// Equivalent to the Mathf.Sign function, but for integers. Returns -1 if negative, 1 if positive, 0 if zero.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>Sign result.</returns>
        public static int Sign(int value)
        {
            return (value > 0 ? 1 : 0) - (value < 0 ? 1 : 0);
        }

        /// <summary>
        /// Calculates the average of a set of integers.
        /// </summary>
        /// <param name="values">Integers to use.</param>
        /// <returns>Set average.</returns>
        public static int Average(params int[] values)
        {
            int result = 0;

            for (int i = 0; i < values.Length; i++)
            {
                result += values[i];
            }

            return result / values.Length;
        }

        /// <summary>
        /// Returns the unit circle position of the given angle (in degree).
        /// </summary>
        /// <param name="angle">Angle in degree.</param>
        /// <returns>The result vector.</returns>
        public static Vector2 UnitVector(float angle)
        {
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }
    }
}