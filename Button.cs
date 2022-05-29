using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WarGame
{
    class Button
    {
        private Vector2 position;
        private Vector2 size;
        private string text;
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }
        public Vector2 Size
        {
            get => size;
            set => size = value;
        }
        public string Text
        {
            get => text;
            set => text = value;
        }
    }
}
