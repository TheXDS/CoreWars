//
//  MainWindow.cs
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
using CoreWars.Engine;
using CoreWars.Game.Helpers;
using CoreWars.Game.Objects;
using CoreWars.Game.Objects.Primitives;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using static CoreWars.Engine.ExtraMath;

namespace CoreWars
{
    public class MainWindow : GameWindow
    {
        readonly Camera currentView;
        readonly World w;
        readonly Input i;
        float xsize, ysize;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            Title = "CoreWars";
            //WindowState = WindowState.Fullscreen;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            w = new World(32, Ground.piso, PointState.Explored);

            xsize = WorldRender.TileSize * w.Size;
            ysize = xsize / 2;

            currentView = new Camera();
            i = new Input(this);


            UpdateFrame += MainWindow_UpdateFrame;
            MouseWheel += MainWindow_MouseWheel;
            Resize += MainWindow_Resize;
            RenderFrame += MainWindow_RenderFrame;


            // Área de pruebas...
            //for (int j = 5; j < 8; j++)
            //{
            //    for (int k = 5; k < 8; k++)
            //    {
            //        Exploration[j, k] = TileState.Active;
            //    }
            //}
            //for (int j = 15; j < 18; j++)
            //{
            //    for (int k = 15; k < 18; k++)
            //    {
            //        Exploration[j, k] = TileState.Explored;
            //    }
            //}

        }

        void MainWindow_Resize(object sender, EventArgs e)
        {
            GL.Viewport(ClientRectangle);
        }

        void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            currentView.RelativeZoom(e.DeltaPrecise * 0.125f, Tween.Quartic, 20);
        }

        void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            if (i.KeyDown(Key.A))
            {
                Vector2 v = currentView.ToAbsolute(new Vector2(-5, 0));
                if (v.Y < ysize) v.Y = v.Y.Clamp(ysize - (1 - (v.X / -xsize)) * ysize, ysize);
                else v.Y = v.Y.Clamp(ysize, ysize + (1 - (v.X / -xsize)) * ysize);
                v.X = v.X.Clamp(-xsize, xsize);
                currentView.Move(v, Tween.Quartic, 20);
            }
            if (i.KeyDown(Key.D))
            {
                Vector2 v = currentView.ToAbsolute(new Vector2(5, 0));
                if (v.Y < ysize) v.Y = v.Y.Clamp(ysize - (1 - (v.X / xsize)) * ysize, ysize);
                else v.Y = v.Y.Clamp(ysize, ysize + (1 - (v.X / xsize)) * ysize);
                v.X = v.X.Clamp(-xsize, xsize);
                currentView.Move(v, Tween.Quartic, 20);
            }
            if (i.KeyDown(Key.W))
            {
                Vector2 v = currentView.ToAbsolute(new Vector2(0, -2.5f));
                if (v.X < 0) v.X = v.X.Clamp(v.Y * -2, 0);
                else v.X = v.X.Clamp(0, v.Y * 2);
                v.Y = v.Y.Clamp(0, xsize);
                currentView.Move(v, Tween.Quartic, 20);
            }
            if (i.KeyDown(Key.S))
            {
                Vector2 v = currentView.ToAbsolute(new Vector2(0, 2.5f));
                if (v.X < 0) v.X = v.X.Clamp(((v.Y * -2) + xsize * 2) * -1, 0);
                else v.X = v.X.Clamp(0, (v.Y * -2) + xsize * 2);
                v.Y = v.Y.Clamp(0, xsize);
                currentView.Move(v, Tween.Quartic, 20);
            }

            // Actualizar terreno activo/explorado
            // TODO: esto debería realizarse sólo al mover unidades.
            for (int j = 1; j < w.Size - 1; j++)
            {
                for (int k = 1; k < w.Size - 1; k++)
                {
                    Color c = w.Points[j, k].Color;
                    w.Tiles[j - 1, k - 1].VColors[2] = c;
                    w.Tiles[j, k - 1].VColors[1] = c;
                    w.Tiles[j - 1, k].VColors[3] = c;
                    w.Tiles[j, k].VColors[0] = c;
                }
            }





            if (i.KeyPress(Key.R))
                currentView.Move(Vector2.Zero, Tween.Linear, 1);

            currentView.Update();
            i.Update();
        }

        void MainWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Black);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-Width / 2, Width / 2, Height / 2, -Height / 2, 0f, 1f);


            currentView.ApplyTransform();

            // Dibujar el mundo...
            WorldRender.Render(w);


            SwapBuffers();
        }
    }
}