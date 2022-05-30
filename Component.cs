using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame
{
    /// <summary>
    /// Class Component.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// Draws the component in screen
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        /// <summary>
        /// Update the components
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void Update(GameTime gameTime);
    }
}
