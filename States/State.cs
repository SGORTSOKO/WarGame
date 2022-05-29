using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame.States
{
    /// <summary>
    /// Class State.
    /// </summary>
    public abstract class State
    {
        #region Fields
        /// <summary>
        /// The this content
        /// </summary>
        protected ContentManager thisContent;
        /// <summary>
        /// The this graphics device
        /// </summary>
        protected GraphicsDevice thisGraphicsDevice;
        /// <summary>
        /// The this game
        /// </summary>
        protected GameWindow thisGame;
        /// <summary>
        /// The this screen size
        /// </summary>
        protected XY thisScreenSize;
        #endregion
        #region Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="inputGame">The input game.</param>
        /// <param name="inputGraphicsDevice">The input graphics device.</param>
        /// <param name="inputContent">Content of the input.</param>
        /// <param name="inputScreenSize">Size of the input screen.</param>
        public State(GameWindow inputGame, GraphicsDevice inputGraphicsDevice, ContentManager inputContent, XY inputScreenSize)
        {
            thisGame = inputGame;
            thisGraphicsDevice = inputGraphicsDevice;
            thisContent = inputContent;
            thisScreenSize = inputScreenSize;
        }
        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        /// <summary>
        /// Posts the update.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void PostUpdate(GameTime gameTime);
        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void Update(GameTime gameTime);
        #endregion
    }
}
