using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame.States
{
    /// <summary>
    /// Абстрактный класс. Потомки этого класса - окна приложения (game states):
    /// окно одиночной игры, таблица рекордов, меню
    /// </summary>
    public abstract class State
    {
        #region Fields
        /// <summary>
        /// The this contentManager of this game
        /// </summary>
        protected ContentManager thisContent;
        /// <summary>
        /// The this graphics device that draws the game state
        /// </summary>
        protected GraphicsDevice thisGraphicsDevice;
        /// <summary>
        /// The lint to base Game Window
        /// </summary>
        protected GameWindow thisGame;
        /// <summary>
        /// The game screen size
        /// </summary>
        protected XY thisScreenSize;
        #endregion
        #region Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        /// <param name="inputGame">The base GameWindow.</param>
        /// <param name="inputGraphicsDevice">The input graphics device.</param>
        /// <param name="inputContent">main ContentManager</param>
        /// <param name="inputScreenSize">Size of the screen.</param>
        public State(GameWindow inputGame, GraphicsDevice inputGraphicsDevice, ContentManager inputContent, XY inputScreenSize)
        {
            thisGame = inputGame;
            thisGraphicsDevice = inputGraphicsDevice;
            thisContent = inputContent;
            thisScreenSize = inputScreenSize;
        }
        /// <summary>
        /// Draws the game state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        /// <summary>
        /// Action after update
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void PostUpdate(GameTime gameTime);
        /// <summary>
        /// Updates the game state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void Update(GameTime gameTime);
        #endregion
    }
}
