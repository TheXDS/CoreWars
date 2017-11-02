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
using System.Collections.Generic;

namespace CoreWars
{
    public class MainWindow : GameWindow
    {

        readonly Camera currentView;

        // Texturas del suelo.
        readonly Dictionary<Ground, Texture2D> GTextures = new Dictionary<Ground, Texture2D>();

        Ground[,] World = new Ground[32, 32];


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            // Cargar texturas...
            GTextures.Add(Ground.Grass, new Texture2D("Assets/Textures/Arena/Grass.jpg"));
            GTextures.Add(Ground.Aqua, new Texture2D("Assets/Textures/Arena/Aqua.jpg"));
            GTextures.Add(Ground.Bricks, new Texture2D("Assets/Textures/Arena/Bricks.png"));
            GTextures.Add(Ground.Carbon, new Texture2D("Assets/Textures/Arena/Carbon.png"));
            GTextures.Add(Ground.Piso, new Texture2D("Assets/Textures/Arena/p.png"));

            // Inicializar mundo...
            for (int j = 0; j < World.GetLength(1); j++)
            {
                for (int k = 0; k < World.GetLength(0); k++)
                {
                    World[j, k] = Ground.Grass;
                }
            }

            currentView = new Camera(1.5f);
            Input.Init(this);
            RenderFrame += MainWindow_RenderFrame;
            UpdateFrame += MainWindow_UpdateFrame;
            MouseWheel += MainWindow_MouseWheel;
        }

        void MainWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            currentView.Zoom += e.DeltaPrecise * 0.5f;
        }

        void MainWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            //if (Input.MousePress(MouseButton.Left))
            //{
            //    Vector2 pos = new Vector2(Mouse.X, Mouse.Y);
            //    pos -= new Vector2(Width, Height) / 2;
            //    pos = currentView.ToWorld((pos));
            //    currentView.Move(pos, Tween.Quartic, 60);
            //}


            if (Input.KeyDown(Key.W))
            {
                currentView.RelativeMove(new Vector2(0, -5), Tween.Quartic, 20);
            }
            if (Input.KeyDown(Key.S))
            {
                currentView.RelativeMove(new Vector2(0, 5), Tween.Quartic, 20);
            }
            if (Input.KeyDown(Key.A))
            {
                currentView.RelativeMove(new Vector2(-5, 0), Tween.Quartic, 20);
            }
            if (Input.KeyDown(Key.D))
            {
                currentView.RelativeMove(new Vector2(5, 0), Tween.Quartic, 20);
            }
            currentView.Update();
            Input.Update();
        }

        void MainWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Black);

            Spritebatch.Begin(Width, Height);
            currentView.ApplyTransform();

            // Dibujar el mundo...
            for (int j = 0; j < World.GetLength(1); j++)
            {
                for (int k = 0; k < World.GetLength(0); k++)
                {
                    if (GTextures.ContainsKey(World[j, k]))
                    {
                        Texture2D t = GTextures[World[j, k]];
                        Spritebatch.Draw(t, Spritebatch.TranslateOrtho(j, k, t.Height));
                    }
                }
            }

            SwapBuffers();
        }

        /// <summary>
        /// Enumera los distintos tipos de suelo del mundo.
        /// </summary>
        public enum Ground : byte
        {
            /// <summary>
            /// Sin suelo (Usado internamente, no utilizar!)
            /// </summary>
            Nothing,
            /// <summary>
            /// Hierba.
            /// </summary>
            Grass,
            /// <summary>
            /// Agua.
            /// </summary>
            Aqua,
            /// <summary>
            /// Ladrillos
            /// </summary>
            Bricks,
            /// <summary>
            /// Carbono (Cool)
            /// </summary>
            Carbon,
            /// <summary>
            /// Piso cerámico (para pruebas)
            /// </summary>
            Piso
        }
    }
}
