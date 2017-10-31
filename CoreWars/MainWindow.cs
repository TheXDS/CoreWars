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
using System.Windows;
using System.Drawing;
using System.Diagnostics;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using CoreWars.Logic.Structure;

namespace CoreWars
{
    public class MainWindow : GameWindow
    {
        // Note to self: Las texturas no se pueden cargar aquí, tienen que
        // cargarse en el constructor.
        Texture2D t;
        View view;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            t = new Texture2D("/home/thexds/src/CoreWars/CoreWars/sample.png");
            GL.Enable(EnableCap.Texture2D);
            view = new View(Vector2.Zero);
            Mouse.ButtonDown += Mouse_ButtonDown;
            RenderFrame += MainWindow_RenderFrame;
            UpdateFrame += MainWindow_UpdateFrame;

        }

        void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            Vector2 pos = new Vector2(e.Position.X, e.Position.Y);
            pos -= new Vector2(Width, Height) / 2;
            pos = view.ToWorld((pos));
            view.SetPosition(pos, TweenType.Quartic, 60);
        }

        void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            view.Update();
        }

        void MainWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.SkyBlue);

            Spritebatch.Begin(Width, Height);
            view.ApplyTransform();

            Spritebatch.Draw(t, Vector2.Zero);

            SwapBuffers();
        }
    }
}
