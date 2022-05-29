using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame.States
{
    public abstract class State
    {
        #region Fields
        protected ContentManager thisContent;
        protected GraphicsDevice thisGraphicsDevice;
        protected GameWindow thisGame;
        protected XY thisScreenSize;
        #endregion
        #region Methods
        public State(GameWindow inputGame, GraphicsDevice inputGraphicsDevice, ContentManager inputContent, XY inputScreenSize)
        {
            thisGame = inputGame;
            thisGraphicsDevice = inputGraphicsDevice;
            thisContent = inputContent;
            thisScreenSize = inputScreenSize;
        }
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void PostUpdate(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
        #endregion
    }
}
