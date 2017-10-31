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
    public enum TweenType : byte
    {
        Instant,
        Linear,
        Quadratic,
        Cubic,
        Quartic
    }
    public class View
    {
        private Vector2 position;
        public Vector2 Position => position;
        public float rotation;
        public float zoom;
        private Vector2 posGoto, posFrom;
        private TweenType tweenType;
        private int currentStep, tweenSteps;

        public Vector2 ToWorld(Vector2 input)
        {
            input /= zoom;
            Vector2 dx = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            Vector2 dy = new Vector2((float)Math.Cos(rotation + MathHelper.PiOver2), (float)Math.Sin(rotation + MathHelper.PiOver2));
            return (position + dx * input.X + dy * input.Y);
        }

        public View(Vector2 startPosition, float startZoom = 1.0f, float startRotation = 0.0f)
        {
            position = startPosition;
            zoom = startZoom;
            rotation = startRotation;
        }

        public void Update()
        {

            if (currentStep < tweenSteps)
            {
                float t = ((float)currentStep / tweenSteps);
                switch (tweenType)
                {
                    case TweenType.Linear: break;
                    case TweenType.Quadratic:
                        t = (t * t) / (2 * t * t - 2 * t + 1);
                        break;
                    case TweenType.Cubic:
                        t = (t * t * t) / (3 * t * t - 3 * t + 1);
                        break;
                    case TweenType.Quartic:
                        t = -((t - 1) * (t - 1) * (t - 1) * (t - 1)) + 1;
                        break;
                    case TweenType.Instant:
                        t = 1;
                        break;
                    default:
                        System.Diagnostics.Debug.Print($"Unknown tween type: {tweenType}");
                        goto case TweenType.Instant;
                }
                position = posFrom + (posGoto - posFrom) * t;
                currentStep++;
            }
            else
            {
                position = posGoto;
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            posFrom = position;
            posGoto = newPosition;
            position = newPosition;
            tweenType = TweenType.Instant;
            currentStep = 0;
            tweenSteps = 0;
        }

        public void SetPosition(Vector2 newPosition, TweenType tween, int numSteps)
        {
            posFrom = position;
            //position = newPosition;
            posGoto = newPosition;
            tweenType = tween;
            currentStep = 0;
            tweenSteps = numSteps;
        }

        public void ApplyTransform()
        {
            Matrix4 transform = Matrix4.Identity;
            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale(zoom, zoom, 1));
            GL.MultMatrix(ref transform);
        }
    }
}
