﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if IPHONE
using OpenTK.Graphics.ES11;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.OpenGLES;

using TextureTarget = OpenTK.Graphics.ES11.All;
using TextureParameterName = OpenTK.Graphics.ES11.All;
using EnableCap = OpenTK.Graphics.ES11.All;
using BlendingFactorSrc = OpenTK.Graphics.ES11.All;
using BlendingFactorDest = OpenTK.Graphics.ES11.All;
using PixelStoreParameter = OpenTK.Graphics.ES11.All;
using VertexPointerType = OpenTK.Graphics.ES11.All;
using ColorPointerType = OpenTK.Graphics.ES11.All;
using ClearBufferMask = OpenTK.Graphics.ES11.All;
using TexCoordPointerType = OpenTK.Graphics.ES11.All;
using BeginMode = OpenTK.Graphics.ES11.All;
using MatrixMode = OpenTK.Graphics.ES11.All;
using PixelInternalFormat = OpenTK.Graphics.ES11.All;
using PixelFormat = OpenTK.Graphics.ES11.All;
using PixelType = OpenTK.Graphics.ES11.All;
using ShaderType = OpenTK.Graphics.ES11.All;
using VertexAttribPointerType = OpenTK.Graphics.ES11.All;
using ProgramParameter = OpenTK.Graphics.ES11.All;
using ShaderParameter = OpenTK.Graphics.ES11.All;
using osu_common.Helpers;
#else
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using osum.Input;
using osum.Helpers;
using osu_common.Helpers;
using OpenTK;
#endif

namespace osum.Graphics.Sprites
{
    internal partial class pSprite : IDrawable, IDisposable
    {
        internal bool IsClickable { get { return onClick != null; } }

        bool inputEventsBound;

        internal event EventHandler onClick;
        internal event EventHandler OnClick
        {
            add { onClick += value; updateInputBindings(); }
            remove { onClick -= value; updateInputBindings(); }
        }

        internal event EventHandler onHover;
        internal event EventHandler OnHover
        {
            add { onHover += value; updateInputBindings(); }
            remove { onHover -= value; updateInputBindings(); }
        }

        private void unbindAllEvents()
        {
            onClick = null;
            onHover = null;
            OnHoverLost = null;
            updateInputBindings();
        }

        private void updateInputBindings()
        {
            bool needEventsBound = onClick != null || onHover != null;

            if (needEventsBound == inputEventsBound) return;

            inputEventsBound = needEventsBound;

            if (needEventsBound)
            {
                InputManager.OnDown += new InputHandler(InputManager_OnDown);
                InputManager.OnMove += new InputHandler(InputManager_OnMove);
                InputManager.OnUp += new InputHandler(InputManager_OnUp);
            }
            else
            {
                InputManager.OnDown -= new InputHandler(InputManager_OnDown);
                InputManager.OnMove -= new InputHandler(InputManager_OnMove);
                InputManager.OnUp -= new InputHandler(InputManager_OnUp);
            }
        }

        void InputManager_OnUp(InputSource source, TrackingPoint trackingPoint)
        {
            
        }

        void InputManager_OnMove(InputSource source, TrackingPoint trackingPoint)
        {

        }

        void InputManager_OnDown(InputSource source, TrackingPoint trackingPoint)
        {
            Vector2 position = trackingPoint.WindowPosition;

            if (Rectangle.Left < position.X &&
                Rectangle.Right >= position.X &&
                Rectangle.Top < position.Y &&
                Rectangle.Bottom >= position.Y)
            {
                    Click();
            }
        }

        internal event EventHandler OnHoverLost;

        internal void Click()
        {
            if (onClick != null)
                onClick(this, null);
        }
    }
}
