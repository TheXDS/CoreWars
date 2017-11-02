//
//  Input.cs
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
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
namespace CoreWars.Logic.Structure
{
    public class Input
    {
        public static List<Key> keysDown;
        public static List<Key> keysDownLast;
        public static List<MouseButton> mouseDown;
        public static List<MouseButton> mouseDownLast;

        public static void Init(GameWindow wnd)
        {
            keysDown = new List<Key>();
            keysDownLast = new List<Key>();
            mouseDown = new List<MouseButton>();
            mouseDownLast = new List<MouseButton>();

            wnd.KeyDown += Wnd_KeyDown;
            wnd.KeyUp += Wnd_KeyUp;
            wnd.MouseDown += Wnd_MouseDown;
            wnd.MouseUp += Wnd_MouseUp;
        }

        static void Wnd_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keysDown.Add(e.Key);
        }

        static void Wnd_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            while (keysDown.Contains(e.Key)) keysDown.Remove(e.Key);
        }

        static void Wnd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown.Add(e.Button);
        }

        static void Wnd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            while (mouseDown.Contains(e.Button)) mouseDown.Remove(e.Button);
        }
        public static void Update()
        {
            keysDownLast = new List<Key>(keysDown);
            mouseDownLast = new List<MouseButton>(mouseDown);
        }
        public static bool KeyPress(Key key) => keysDown.Contains(key) && !keysDownLast.Contains(key);
        public static bool KeyRelease(Key key) => !keysDown.Contains(key) && keysDownLast.Contains(key);
        public static bool KeyDown(Key key) => keysDown.Contains(key);

        public static bool MousePress(MouseButton b) => mouseDown.Contains(b) && !mouseDownLast.Contains(b);
        public static bool MouseRelease(MouseButton b) => !mouseDown.Contains(b) && mouseDownLast.Contains(b);
        public static bool MouseDown(MouseButton b) => mouseDown.Contains(b);
    }
}
