//
//  PointState.cs
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

using System.Drawing;

namespace CoreWars.Game.Objects.Primitives
{
    public static class PointState
    {
        public static IPointState Unexplored = new Unexplored();
        public static IPointState Explored = new Explored();
        public static IPointState Active = new Active();
    }

    public interface IPointState
    {
        Color Color { get; }
    }

    public class Unexplored : IPointState
    {
        public Color Color => Color.Black;
    }

    public class Explored : IPointState
    {
        public Color Color => Color.Gray;
    }

    public class Active : IPointState
    {
        public Color Color => Color.White;
    }
}