﻿//
//  Base.cs
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
using CoreWars.Logic.Structure;

namespace CoreWars.Logic.Factions.Unit
{
    /// <summary>
    /// Clase base que representa a todas las unidades interactivas del juego.
    /// </summary>
    public abstract class UnitDefinition
    {
        /// <summary>
        /// Nombre de la unidad.
        /// </summary>
        public string Name;
        /// <summary>
        /// Testuras que definen al Sprite
        /// </summary>
        public TextureTree IdleSprite;






    }
}
