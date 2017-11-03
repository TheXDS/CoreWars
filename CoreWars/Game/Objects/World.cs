//
//  World.cs
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
using System.Drawing;
using CoreWars.Game.Objects.Primitives;

namespace CoreWars.Game.Objects
{
    public class World
    {
        public delegate byte WorldGenerator(WorldTile[,] Tiles);

        public readonly WorldTile[,] Tiles;
        public readonly IPointState[,] Points;
        public readonly byte Size;

        public World(byte size)
            : this(size, Ground.Grass, PointState.Unexplored) { }
        public World(byte size, IPointState defaultState)
            : this(size, Ground.Grass, defaultState) { }
        public World(byte size, Ground defaultGround)
            : this(size, defaultGround, PointState.Unexplored) { }
        public World(byte size, Ground defaultGround, IPointState defaultState)
        {
            Size = size;
            Tiles = new WorldTile[size, size];
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    Tiles[j, k] = new WorldTile
                    {
                        GType = defaultGround,
                        VColors = new Color[]
                        {
                            defaultState.Color,
                            defaultState.Color,
                            defaultState.Color,
                            defaultState.Color
                        }
                    };
                }
            }

            // Este arreglo necesita tener un elemento más por dimensión
            Points = new IPointState[++size, size];
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    Points[j, k] = defaultState;
                }
            }
        }

        public World(WorldGenerator generator)
            : this(generator, PointState.Unexplored) { }
        public World(WorldGenerator generator, IPointState defaultState)
        {
            byte size = generator?.Invoke(Tiles) ?? throw new ArgumentNullException(nameof(generator));
            Size = size;
            // Este arreglo necesita tener un elemento más por dimensión
            Points = new IPointState[++size, size];
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    Points[j, k] = defaultState;
                }
            }
        }
    }
}
