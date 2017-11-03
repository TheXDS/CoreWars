//
//  Math.cs
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Andrés Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace CoreWars.Engine
{
    /// <summary>
    /// Contiene funciones matemáticas adicionales.
    /// </summary>
    public static class ExtraMath
    {
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        /// Esta implementación se incluye para permitir parámetros de tipo
        /// <see cref="double.NaN"/>, <see cref="double.NegativeInfinity"/> y
        /// <see cref="double.PositiveInfinity"/>.
        /// </remarks>
        public static double Clamp(this double expression, double min = double.NegativeInfinity, double max = double.PositiveInfinity)
        {
            if (!double.IsNaN(expression))
            {
                if (expression > max) return max;
                if (expression < min) return min;
                return expression;
            }
            return double.NaN;
        }
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        /// <remarks>
        /// Esta implementación se incluye para permitir parámetros de tipo
        /// <see cref="float.NaN"/>, <see cref="float.NegativeInfinity"/> y
        /// <see cref="float.PositiveInfinity"/>.
        /// </remarks>
        public static float Clamp(this float expression, float min = float.NegativeInfinity, float max = float.PositiveInfinity)
        {
            if (!float.IsNaN(expression))
            {
                if (expression > max) return max;
                if (expression < min) return min;
                return expression;
            }
            return float.NaN;
        }
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <param name="min">Límite inferior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra dentro del rango especificado.
        /// </returns>
        public static T Clamp<T>(this T expression, T min, T max) where T :
            IComparable,
            IComparable<T>
        {

            if (expression.CompareTo(max) > 0) return max;
            if (expression.CompareTo(min) < 0) return min;
            return expression;
        }
        /// <summary>
        /// Establece límites de sobreflujo para evaluar una expresión.
        /// </summary>
        /// <param name="expression">Expresión a evaluar.</param>
        /// <param name="max">Límite superior de salida, inclusive.</param>
        /// <returns>
        /// El valor evaluado que se encuentra entre 0 y 
        /// <paramref name="max"/>.
        /// </returns>
        public static T Clamp<T>(this T expression, T max) where T :
            IComparable,
            IComparable<T>
        {
            if (expression.CompareTo(max) > 0) return max;
            if (expression.CompareTo(default(T)) < 0) return default(T);
            return expression;
        }

        public static class Tween
        {
            public static float Linear(int step, int total) => (float)step / total;
            public static float Quadratic(int step, int total)
            {
                float t = ((float)step / total);
                return (t * t) / (2 * t * t - 2 * t + 1);
            }
            public static float Cubic(int step, int total)
            {
                float t = ((float)step / total);
                return (t * t * t) / (3 * t * t - 3 * t + 1);
            }
            public static float Quartic(int step, int total)
            {
                float t = ((float)step / total);
                return -((t - 1) * (t - 1) * (t - 1) * (t - 1)) + 1;
            }
        }
    }
}