using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WarGame
{
    public class Button : Component
    {
        #region Fields
        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previousMouse;
        private Texture2D texture;
        private float myX = -10;
        private float myY = -10;
        #endregion

        #region Properties
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle
        {
            get => new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }
        public string Text { get; set; }
        #endregion
        #region Methods
        public Button(Texture2D inputtexture, SpriteFont inputfont)
        {
            texture = inputtexture;
            font = inputfont;
            PenColour = Color.Black;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if (isHovering)
                colour = Color.Gray;
            spriteBatch.Draw(texture, Rectangle, colour);
            if (!string.IsNullOrEmpty(Text))
            {
                if (myX <= 0)
                    myX = (Rectangle.X + (Rectangle.Width / 2)) - (font.MeasureString(Text).X / 2);
                if (myY <= 0)
                    myY = (Rectangle.Y + (Rectangle.Height / 2)) - (font.MeasureString(Text).Y / 2);
                spriteBatch.DrawString(font, Text, new Vector2(myX, myY), PenColour);
            }
        }
        public override void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);
            isHovering = false;
            if (mouseRectangle.Intersects(Rectangle))
            {
                isHovering = true;
                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}
