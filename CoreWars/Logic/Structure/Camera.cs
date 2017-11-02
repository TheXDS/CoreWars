//
//  View.cs
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
    /// Esta clase aplica transformaciones de cámara a OpenGL.
    /// </summary>
    public class Camera
    {
        Vector2 position;
        Vector2 posGoto, posFrom;
        Func<int, int, float> tweenType;
        int currentStep, tweenSteps;

        public float Rotation;
        public float Zoom;
        public Vector2 Position => position;
        public Vector2 PositionGoto => posGoto;

        /// <summary>
        /// Traduce un <see cref="Vector2"/> de la pantalla a la posición en el
        /// mundo.
        /// </summary>
        /// <param name="input"><see cref="Vector2"/> de entrada.</param>
        public Vector2 ToWorld(Vector2 input)
        {
            input /= Zoom;
            Vector2 dx = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            Vector2 dy = new Vector2((float)Math.Cos(Rotation + MathHelper.PiOver2), (float)Math.Sin(Rotation + MathHelper.PiOver2));
            return (position + dx * input.X + dy * input.Y);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Camera"/>.
        /// </summary>
        /// <param name="startPosition">Posición inicial.</param>
        /// <param name="startZoom">Nivel de Zoom.</param>
        /// <param name="startRotation">Rotación.</param>
        public Camera(Vector2 startPosition, float startZoom = 1.0f, float startRotation = 0.0f)
        {
            position = startPosition;
            Zoom = startZoom;
            Rotation = startRotation;
        }
        public Camera()
        {
            position = Vector2.Zero;
            Zoom = 1;
            Rotation = 0;
        }
        public Camera(float startZoom, float startRotation = 0.0f)
        {
            position = Vector2.Zero;
            Zoom = startZoom;
            Rotation = startRotation;
        }

        /// <summary>
        /// Actualiza la cámara en cada ciclo de la aplicación.
        /// </summary>
        /// <remarks>
        /// Esta llamada debe ejecutarse cada vez que se genere el evento <see cref="GameWindow.UpdateFrame"/> 
        /// </remarks>
        public void Update()
        {
            if (currentStep < tweenSteps)
            {
                currentStep++;
                position = posFrom + (posGoto - posFrom) *
                    (tweenType?.Invoke(currentStep, tweenSteps) ?? 1f);
            }
            else { position = posGoto; }
        }

        /// <summary>
        /// Mueve la cámara a la posición especificada.
        /// </summary>
        /// <returns>The move.</returns>
        /// <param name="newPosition">Nueva posición de la cámara.</param>
        public void Move(Vector2 newPosition)
        {
            Move(newPosition, null, 0);
        }

        /// <summary>
        /// Mueve la cámara a la posición especificada.
        /// </summary>
        /// <returns>The move.</returns>
        /// <param name="newPosition">Nueva posición de la cámara.</param>
        /// <param name="tween">Tipo de movimiento.</param>
        /// <param name="numSteps">Pasos del movimiento.</param>
        public void Move(Vector2 newPosition, Func<int, int, float> tween, int numSteps)
        {
            posFrom = position;
            posGoto = newPosition;
            tweenType = tween;
            currentStep = 0;
            tweenSteps = numSteps;
        }

        public void RelativeMove(Vector2 newPosition, Func<int, int, float> tween, int numSteps)
        {
            Move(posGoto + newPosition, tween, numSteps);
        }
        public void RelativeMove(Vector2 newPosition)
        {
            Move(posGoto + newPosition, null, 0);
        }

        /// <summary>
        /// Aplica las transformaciones de la cámara.
        /// </summary>
        /// <remarks>
        /// Esta llamada debe ejecutarse cada vez que se genere el evento <see cref="GameWindow.RenderFrame"/> 
        /// </remarks>
        public void ApplyTransform()
        {
            Matrix4 transform = Matrix4.Identity;
            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-Rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale(Zoom, Zoom, 1));
            GL.MultMatrix(ref transform);
        }
    }
}
