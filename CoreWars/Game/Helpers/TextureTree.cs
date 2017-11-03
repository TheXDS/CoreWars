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

#define LoadOnUse

using System.IO;
using System.Collections.Generic;
using CoreWars.Engine;

#if !LoadOnUse
using System.Linq;
#endif

namespace CoreWars.Logic.Structure
{
    /// <summary>
    /// Árbol de texturas utilizado para dibujar a un Sprite.
    /// </summary>
    public struct TextureTree
    {
        /// <summary>
        /// Carga las texturas de un directorio.
        /// </summary>
        /// <returns>The textures.</returns>
        /// <param name="g">Grupo a cargar.</param>
        /// <param name="roth">Raíz en dónde buscar las texturas.</param>
        static IEnumerable<Texture2D> GetTextures(string g, DirectoryInfo roth)
        {
            foreach (var j in roth.GetFiles($"{g}*.png"))
                yield return new Texture2D(j.FullName);
        }

#if LoadOnUse
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
        public readonly IEnumerable<Texture2D> Up;
        /// <summary>
        /// Grupo de texturas de animación con el objeto en dirección hacia
        /// abajo.
        /// </summary>
        public readonly IEnumerable<Texture2D> Down;
        /// <summary>
        /// Grupo de texturas de animación con el objeto en dirección hacia los
        /// lados (definidas en dirección hacia la derecha).
        /// </summary>
        public readonly IEnumerable<Texture2D> Side;
        /// <summary>
        /// Grupo de texturas de animación con el objeto en dirección diagonal
        /// hacia arriba a la derecha.
        /// </summary>
        public readonly IEnumerable<Texture2D> SideUp;
        /// <summary>
        /// Grupo de texturas de animación con el objeto en dirección diagonal
        /// hacia abajo a la derecha.
        /// </summary>
        public readonly IEnumerable<Texture2D> SideDown;
#else
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TextureTree"/>
        /// cargando las texturas correspondientes al Sprite.
        /// </summary>
        /// <param name="path">Path.</param>
        public TextureTree(string path)
        {
            DirectoryInfo roth = new DirectoryInfo(path);
            Up = GetTextures(nameof(Up), roth).ToArray();
            Down = GetTextures(nameof(Down), roth).ToArray();
            Side = GetTextures(nameof(Side), roth).ToArray();
            SideUp = GetTextures(nameof(SideUp), roth).ToArray();
            SideDown = GetTextures(nameof(SideDown), roth).ToArray();
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
#endif
    }
}