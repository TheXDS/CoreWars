//
//  Spritebatch.cs
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
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace CoreWars.Logic.Structure
{
    /// <summary>
    /// Motor de dibujado de Sprites.
    /// </summary>
    public static class Spritebatch
    {
        public static void Draw(Texture2D texture, Vector2 position)
        {
            Draw(texture, position, new Vector2(1f, 1f), Color.White, Vector2.Zero);
        }
        public static void Draw(Texture2D texture, Vector2 position, Vector2 scale, Color color, Vector2 origin)
        {
            Vector2[] mapVerts =
            {
                new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(1,1),
                new Vector2(0,1)
            };
            Vector2[] drwVerts =
            {
                new Vector2(0.5f,0),
                new Vector2(1,0.25f),
                new Vector2(0.5f,0.5f),
                new Vector2(0,0.25f)
            };
            GL.BindTexture(TextureTarget.Texture2D, texture.ID);

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            for (int j = 0; j < 4; j++)
            {
                GL.TexCoord2(mapVerts[j]);
                GL.Vertex2((((drwVerts[j] * texture.Size) - origin) * scale) + position);
            }
            GL.End();
        }
        public static void Begin(int screenWidth, int screenHeight)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-screenWidth / 2, screenWidth / 2, screenHeight / 2, -screenHeight / 2, 0f, 1f);
        }

        /// <summary>
        /// Convierte los índices X y Y en coordenadas ortogonales.
        /// </summary>
        /// <returns>The ortho.</returns>
        /// <param name="h">The x coordinate.</param>
        /// <param name="v">The y coordinate.</param>
        /// <param name="sze">Tamaño del bloque.</param>
        public static Vector2 TranslateOrtho(int v, int h, int sze)
        {
            return new Vector2(
                h * sze / 2f - (sze * v / 2f),
                v * sze / 4f + (sze * h / 4f)
            );
        }
    }
}