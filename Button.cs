using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace WarGame
{
    /// <summary>
    /// Class Button.
    /// Implements the <see cref="WarGame.Component" />
    /// </summary>
    /// <seealso cref="WarGame.Component" />
    public class Button : Component
    {
        #region Fields
        /// <summary>
        /// The current mouse state
        /// </summary>
        private MouseState currentMouse;
        /// <summary>
        /// The font for text in button
        /// </summary>
        private SpriteFont font;
        /// <summary>
        /// Button hovering state
        /// </summary>
        private bool isHovering;
        /// <summary>
        /// The previous mouse state
        /// </summary>
        private MouseState previousMouse;
        /// <summary>
        /// The button texture
        /// </summary>
        private Texture2D texture;
        /// <summary>
        /// shifting the text from the center of the button along the X axis
        /// </summary>
        private float myX = -10;
        /// <summary>
        /// shifting the text from the center of the button along the Y axis
        /// </summary>
        private float myY = -10;
        #endregion

        #region Properties
        public event EventHandler Click;
        /// <summary>
        /// Gets a value indicating whether this <see cref="Button"/> is clicked.
        /// </summary>
        /// <value><c>true</c> if clicked; otherwise, <c>false</c>.</value>
        public bool Clicked { get; private set; }
        /// <summary>
        /// Gets or sets the pen colour.
        /// </summary>
        /// <value>The pen colour.</value>
        public Color PenColour { get; set; }
        /// <summary>
        /// Gets or sets the button position.
        /// </summary>
        /// <value>The position.</value>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets the button rectangle area.
        /// </summary>
        /// <value>The rectangle.</value>
        public Rectangle Rectangle
        {
            get => new Rectangle(
                (int)Position.X, 
                (int)Position.Y, 
                texture.Width, 
                texture.Height);
        }
        /// <summary>
        /// Gets or sets the text in the button.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="inputtexture">The button texture.</param>
        /// <param name="inputfont">The button text font.</param>
        public Button(Texture2D inputtexture, SpriteFont inputfont)
        {
            texture = inputtexture;
            font = inputfont;
            PenColour = Color.Black;
        }
        /// <summary>
        /// Draws the button.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
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
                spriteBatch.DrawString(
                    font, 
                    Text, 
                    new Vector2(myX, myY), 
                    PenColour);
            }
        }
        /// <summary>
        /// Updates button state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(
                currentMouse.X, 
                currentMouse.Y, 
                1, 
                1);

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
