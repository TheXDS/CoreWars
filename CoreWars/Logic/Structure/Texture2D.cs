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
using OpenTK.Graphics.OpenGL;
using Drw = System.Drawing;
using GL = OpenTK.Graphics.OpenGL.GL;
using Img = System.Drawing.Imaging;

namespace CoreWars.Logic.Structure
{
    /// <summary>
    /// Representa una textura de mapa de bits bidimensional cargada en OpenGL.
    /// </summary>
    public struct Texture2D
    {
        /// <summary>
        /// Identificador de textura asociado en OpenGL.
        /// </summary>
        public readonly int ID;
        /// <summary>
        /// Anchura en pixeles de la textura.
        /// </summary>
        public readonly int Width;
        /// <summary>
        /// Altura en pixeles de la textura.
        /// </summary>
        public readonly int Height;
        /// <summary>
        /// Obtiene el tamaño de la textura como un <see cref="OpenTK.Vector2"/> 
        /// </summary>
        public OpenTK.Vector2 Size => new OpenTK.Vector2(Width, Height);
        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Texture2D"/> con el ID de una textura previamente
        /// cargada en OpenGL.
        /// </summary>
        /// <param name="id">Identificador de la textura.</param>
        /// <param name="width">Anchura en pixeles de la textura.</param>
        /// <param name="height">Altura en pixeles de la textura.</param>
        public Texture2D(int id, int width, int height)
        {
            ID = id;
            Width = width;
            Height = height;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Texture2D"/>, cargando una textura desde la ruta
        /// especificada.
        /// </summary>
        /// <param name="path">Ruta de archivo de la textura.</param>
        public Texture2D(string path)
        {
            ID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, ID);
            Drw.Bitmap bmp;
            if (File.Exists(path))
            {
                bmp = new Drw.Bitmap(path);
            }
            else
            {
                bmp = new Drw.Bitmap(64, 64, Img.PixelFormat.Format32bppArgb);
                var g = Drw.Graphics.FromImage(bmp);
                var p = new Drw.Pen(Drw.Color.White, 2);
                g.DrawRectangle(p, 1, 1, 63, 63);
                g.DrawLine(p, 1, 1, 63, 63);
                g.DrawLine(p, 1, 63, 63, 1);
                g.Dispose();
            }
            Width = bmp.Width;
            Height = bmp.Height;

            Img.BitmapData data = bmp.LockBits(
                new Drw.Rectangle(0, 0, Width, Height),
                Img.ImageLockMode.ReadOnly,
                Img.PixelFormat.Format32bppArgb);
            GL.TexImage2D(
                TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                data.Width, data.Height, 0,
                PixelFormat.Bgra, PixelType.UnsignedByte,
                data.Scan0);
            bmp.UnlockBits(data);
            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
    }
}