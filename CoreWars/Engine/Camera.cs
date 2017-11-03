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
using OpenTK.Graphics.OpenGL;

namespace CoreWars.Engine
{
    /// <summary>
    /// Esta clase aplica transformaciones de cámara a OpenGL.
    /// </summary>
    public class Camera
    {
        public delegate float TweenMethod(int currentStep, int steps);
        Vector2 position;
        Vector2 posGoto, posFrom;
        TweenMethod moveTween;
        int moveCurrStp, moveStps;
        float zoom;
        float zoomFrom, zoomGoto;
        TweenMethod zoomTween;
        int zoomCurrStp, zoomStps;
        float MaxZoom = 4f;
        float MinZoom = 0.05f;

        /// <summary>
        /// Obtiene o establece la rotación de la cámara. (actualmente, sin
        /// ningún uso)
        /// </summary>
        public float Rotation;
        /// <summary>
        /// Obtiene la posición actual de la cámara.
        /// </summary>
        public Vector2 Position => position;
        /// <summary>
        /// Obtiene el nivel de zoom de la cámara.
        /// </summary>
        /// <value>The zoom.</value>
        public float Zoom => zoom;

        /// <summary>
        /// Traduce un <see cref="Vector2"/> de la pantalla a la posición en el
        /// mundo.
        /// </summary>
        /// <param name="input"><see cref="Vector2"/> de entrada.</param>
        public Vector2 ToWorld(Vector2 input)
        {
            input /= zoom;
            Vector2 dx = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            Vector2 dy = new Vector2((float)Math.Cos(Rotation + MathHelper.PiOver2), (float)Math.Sin(Rotation + MathHelper.PiOver2));
            return (position + dx * input.X + dy * input.Y);
        }
        /// <summary>
        /// Traduce un <see cref="Vector2"/> de posición relativa de movimiento
        /// a una absoluta.
        /// </summary>
        /// <param name="input"><see cref="Vector2"/> de entrada.</param>
        public Vector2 ToAbsolute(Vector2 input)
        {
            return posGoto + input;
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
            zoom = startZoom;
            Rotation = startRotation;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Camera"/>.
        /// </summary>
        public Camera()
        {
            position = Vector2.Zero;
            zoom = 1;
            Rotation = 0;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Camera"/>.
        /// </summary>
        /// <param name="startZoom">Nivel de Zoom.</param>
        /// <param name="startRotation">Rotación.</param>
        public Camera(float startZoom, float startRotation = 0.0f)
        {
            position = Vector2.Zero;
            zoom = startZoom;
            Rotation = startRotation;
        }

        /// <summary>
        /// Mueve la cámara a la posición especificada.
        /// </summary>
        /// <returns>The move.</returns>
        /// <param name="newPosition">Nueva posición de la cámara.</param>
        public void Move(Vector2 newPosition)
        {
            Move(newPosition, null, 1);
        }
        /// <summary>
        /// Mueve la cámara a la posición especificada.
        /// </summary>
        /// <returns>The move.</returns>
        /// <param name="newPosition">Nueva posición de la cámara.</param>
        /// <param name="tween">Tipo de suavizado de movimiento.</param>
        /// <param name="numSteps">Pasos del movimiento.</param>
        public void Move(Vector2 newPosition, TweenMethod tween, int numSteps)
        {
            posFrom = position;
            posGoto = newPosition;
            moveTween = tween;
            moveCurrStp = 0;
            moveStps = numSteps;
        }
        /// <summary>
        /// Establece el nivel de Zoom de la cámara.
        /// </summary>
        /// <param name="targetZoom">Nivel de zoom deseado.</param>
        /// <param name="tween">Tipo de suavizado de movimiento.</param>
        /// <param name="numSteps">Pasos del movimiento.</param>
        public void SetZoom(float targetZoom, TweenMethod tween, int numSteps)
        {
            zoomFrom = zoom;
            zoomGoto = targetZoom.Clamp(MinZoom, MaxZoom);
            zoomTween = tween;
            zoomCurrStp = 0;
            zoomStps = numSteps;
        }
        /// <summary>
        /// Cambia el nivel de zoom en relación al nivel actual.
        /// </summary>
        /// <param name="relativeZoom">Cambio relativo de zoom deseado.</param>
        /// <param name="tween">Tipo de suavizado de movimiento.</param>
        /// <param name="numSteps">Pasos del movimiento.</param>
        public void RelativeZoom(float relativeZoom, TweenMethod tween, int numSteps)
        {
            SetZoom(zoomGoto + relativeZoom, tween, numSteps);
        }

        /// <summary>
        /// Actualiza la cámara en cada ciclo de la aplicación.
        /// </summary>
        /// <remarks>
        /// Esta llamada deber ejecutarse cada vez que se genere el evento
        /// <see cref="GameWindow.UpdateFrame"/>.
        /// </remarks>
        public void Update()
        {
            if (moveCurrStp < moveStps)
            {
                moveCurrStp++;
                position = posFrom + (posGoto - posFrom) *
                    (moveTween?.Invoke(moveCurrStp, moveStps) ?? 1f);
            }
            if (zoomCurrStp < zoomStps)
            {
                zoomCurrStp++;
                zoom = zoomFrom + (zoomGoto - zoomFrom) *
                    (zoomTween?.Invoke(zoomCurrStp, zoomStps) ?? 1f);
            }
        }
        /// <summary>
        /// Aplica las transformaciones de la cámara.
        /// </summary>
        /// <remarks>
        /// Esta llamada debe ejecutarse cada vez que se genere el evento
        /// <see cref="GameWindow.RenderFrame"/>.
        /// </remarks>
        public void ApplyTransform()
        {
            Matrix4 transform = Matrix4.Identity;
            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-Rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale(zoom, zoom, 1));
            GL.MultMatrix(ref transform);
        }
    }
}
