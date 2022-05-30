using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
