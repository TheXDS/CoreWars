//
//  ITween.cs
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

namespace CoreWars.Logic.Structure
{
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
