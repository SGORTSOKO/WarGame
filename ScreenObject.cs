using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WarGame
{
    class ScreenObject
    {
        private MouseState currentMouse;
        private MouseState previousMouse;
        private SpriteFont font;
        private bool isHovering;
        private Texture2D texture;
    }
}
