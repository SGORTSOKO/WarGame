using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame.States
{
    /// <summary>
    /// Class ScoreState.
    /// Implements the <see cref="WarGame.States.State" />
    /// </summary>
    /// <seealso cref="WarGame.States.State" />
    class ScoreState : State
    {
        /// <summary>
        /// The font
        /// </summary>
        private SpriteFont font;
        /// <summary>
        /// The components
        /// </summary>
        private List<Component> components;
        /// <summary>
        /// The back ground
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// The score manager
        /// </summary>
        private ScoreManager scoreManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreState"/> class.
        /// </summary>
        /// <param name="inputGame">The input game.</param>
        /// <param name="inputGraphicsDevice">The input graphics device.</param>
        /// <param name="inputContent">Content of the input.</param>
        /// <param name="inputScreenSize">Size of the input screen.</param>
        public ScoreState(GameWindow inputGame, GraphicsDevice inputGraphicsDevice, ContentManager inputContent, XY inputScreenSize)
        : base(inputGame, inputGraphicsDevice, inputContent, inputScreenSize)
        {
            backGround = inputContent.Load<Texture2D>("BackGrounds/Road");
            font = inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall");
            scoreManager = ScoreManager.Load();
            Button menuButton = new Button(inputContent.Load<Texture2D>("Buttons/Menu"), inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(thisScreenSize.X * 0.01f, thisScreenSize.Y * 0.1f)
            };
            menuButton.Click += MenuButton_Click;

            components = new List<Component>()
        {
        menuButton,
        };
        }
        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Vector2(0, 0), new Rectangle(0, 0, thisScreenSize.X, thisScreenSize.Y), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "Highscores:\n" + string.Join("\n", scoreManager.Highscores.Select(c => c.PlayerName + ": " + c.Value).ToArray()), new Vector2(300, 10), Color.Black);
            spriteBatch.End();
        }


        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }
        /// <summary>
        /// Posts the update.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }
        /// <summary>
        /// Handles the Click event of the MenuButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            thisGame.ChangeState(new MenuState(thisGame, thisGraphicsDevice, thisContent, thisScreenSize));
        }
    }
}
