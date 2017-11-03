//
//  WorldRender.cs
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
using System.Collections.Generic;
using CoreWars.Engine;
using CoreWars.Game.Objects;
using CoreWars.Game.Objects.Primitives;

namespace CoreWars.Game.Helpers
{
    /// <summary>
    /// Motor de dibujado del mundo.
    /// </summary>
    public static class WorldRender
    {
        /// <summary>
        /// Establece el tamaño de un bloque de mundo al ser renderizado.
        /// </summary>
        public const int TileSize = 64;

        static readonly Dictionary<Ground, Texture2D> GTextures = new Dictionary<Ground, Texture2D>();
        static WorldRender()
        {
            //TODO: este ciclo cargará recursos PACK eventualmente.
            foreach (Ground j in typeof(Ground).GetEnumValues())
            {
                GTextures.Add(j, new Texture2D($"Assets/Textures/World/{j.ToString()}.png"));
            }

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

        /// <summary>
        /// Renderiza un <see cref="World"/>.
        /// </summary>
        /// <param name="w"><see cref="World"/> a renderizar.</param>
        public static void Render(World w)
        {
            for (int j = 0; j < w.Size; j++)
            {
                for (int k = 0; k < w.Size; k++)
                {
                    if (GTextures.ContainsKey(w.Tiles[j, k].GType))
                    {
                        Texture2D t = GTextures[w.Tiles[j, k].GType];
                        float sze = (float)TileSize / t.Width;
                        Vector2 position = TranslateOrtho(j, k, TileSize);
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
                        GL.BindTexture(TextureTarget.Texture2D, t.ID);
                        GL.Begin(PrimitiveType.Quads);
                        for (int l = 0; l < 4; l++)
                        {
                            GL.TexCoord2(mapVerts[l]);
                            GL.Color4(w.Tiles[j, k].VColors[l]);
                            GL.Vertex2((((drwVerts[l] * t.Size)) * sze) + position);
                        }
                        GL.End();
                    }
                }
            }
        }







        //public static void Draw(Texture2D texture, Vector2 position, Vector2 scale, Color color, Vector2 origin)
        //{
        //    Vector2[] mapVerts =
        //    {
        //        new Vector2(0,0),
        //        new Vector2(1,0),
        //        new Vector2(1,1),
        //        new Vector2(0,1)
        //    };
        //    Vector2[] drwVerts =
        //    {
        //        new Vector2(0.5f,0),
        //        new Vector2(1,0.25f),
        //        new Vector2(0.5f,0.5f),
        //        new Vector2(0,0.25f)
        //    };
        //    GL.BindTexture(TextureTarget.Texture2D, texture.ID);

        //    GL.Begin(PrimitiveType.Quads);
        //    GL.Color3(color);
        //    for (int j = 0; j < 4; j++)
        //    {
        //        GL.TexCoord2(mapVerts[j]);
        //        GL.Vertex2((((drwVerts[j] * texture.Size) - origin) * scale) + position);
        //    }
        //    GL.End();
        //}


        //public static Vector2 TranslateOrtho(float v, float h)
        //{
        //    return new Vector2(
        //        h / 2f - (v / 2f),
        //        v / 4f + (h / 4f)
        //    );
        //}
        //public static Vector2 TranslateOrtho(Vector2 v)
        //{
        //    return new Vector2(
        //        v.X / 2f - (v.Y / 2f),
        //        v.Y / 4f + (v.X / 4f)
        //    );
        //}
    }
}