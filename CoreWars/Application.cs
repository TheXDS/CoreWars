//
//  Application.cs
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

namespace CoreWars
{
    /// <summary>
    /// Clase estática que administra el punto de entrada de la aplicación.
    /// </summary>
    public class Application
    {
        static Application instance;
        /// <summary>
        /// Punto de entrada del programa.
        /// </summary>
        public static void Main()
        {
            if (instance == null)
            {
                instance = new Application();
                instance.window.Run();
                instance.window.Dispose();
            }
            else
            {
                instance.window.WindowState = OpenTK.WindowState.Fullscreen;
            }
        }

        readonly MainWindow window;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="Application"/>.
        /// </summary>
        public Application()
        {
            window = new MainWindow
            {
                Title = "OpenGL Test",
                //WindowState = OpenTK.WindowState.Fullscreen
            };

        }
    }
}