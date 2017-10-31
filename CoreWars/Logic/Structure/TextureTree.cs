//
//  TextureTree.cs
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

using System.IO;
using System.Collections.Generic;

namespace CoreWars.Logic.Structure
{
    /// <summary>
    /// Árbol de texturas utilizado para dibujar a un Sprite.
    /// </summary>
    public struct TextureTree
    {
        static Texture2D[] GetTextures(string o, DirectoryInfo roth)
        {
            var ret = new List<Texture2D>();
            foreach (var j in roth.GetFiles($"{o}*.png"))
            {
                ret.Add(new Texture2D(j.FullName));
            }
            return ret.ToArray();
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TextureTree"/>
        /// cargando las texturas correspondientes al Sprite.
        /// </summary>
        /// <param name="path">Path.</param>
        public TextureTree(string path)
        {
            DirectoryInfo roth = new DirectoryInfo(path);
            Up = GetTextures(nameof(Up), roth);
            Down = GetTextures(nameof(Down), roth);
            Side = GetTextures(nameof(Side), roth);
            SideUp = GetTextures(nameof(SideUp), roth);
            SideDown = GetTextures(nameof(SideDown), roth);
        }
        /// <summary>
        /// Grupo de texturas de animación con el objeto en dirección hacia
        /// arriba.
        /// </summary>
        public readonly Texture2D[] Up;
        /// <summary>
        /// Grupo de texturas de animación con el objeto en dirección hacia
        /// abajo.
        /// </summary>
        public readonly Texture2D[] Down;
        /// <summary>
        /// Grupo de texturas de animación con el objeto en dirección hacia los
        /// lados (definidas en dirección hacia la derecha).
        /// </summary>
        public readonly Texture2D[] Side;
        /// <summary>
        /// Grupo de texturas de animación con el objeto en dirección diagonal
        /// hacia arriba a la derecha.
        /// </summary>
        public readonly Texture2D[] SideUp;
        /// <summary>
        /// Grupo de texturas de animación con el objeto en dirección diagonal
        /// hacia abajo a la derecha.
        /// </summary>
        public readonly Texture2D[] SideDown;
    }
}