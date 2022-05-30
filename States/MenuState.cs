using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame.States
{
    /// <summary>
    /// Class MenuState.
    /// Implements the <see cref="WarGame.States.State" />
    /// </summary>
    /// <seealso cref="WarGame.States.State" />
    public class MenuState : State
    {
        /// <summary>
        /// The components of screen
        /// </summary>
        private List<Component> components;
        /// <summary>
        /// The back ground image
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuState"/> class.
        /// </summary>
        /// <param name="inputGame">The base GameWindow</param>
        /// <param name="inputGraphicsDevice">The input graphics device.</param>
        /// <param name="inputContent">The input ContentManager</param>
        /// <param name="inputScreenSize">Size of the screen.</param>
        public MenuState(GameWindow inputGame, GraphicsDevice inputGraphicsDevice, ContentManager inputContent, XY inputScreenSize)
        : base(inputGame, inputGraphicsDevice, inputContent, inputScreenSize)
        {
        Texture2D newGameButtonTexture = inputContent.Load<Texture2D>("Buttons/NewGame");
        //Texture2D continueButtonTexture = inputContent.Load<Texture2D>("Buttons/Continue");
        Texture2D quitButtonTexture = inputContent.Load<Texture2D>("Buttons/Exit");
        Texture2D scoreButtonTexture = inputContent.Load<Texture2D>("Buttons/Scores");
        backGround = inputContent.Load<Texture2D>("BackGrounds/Road");
        SpriteFont buttonFont = inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall");
        Button newGameButton = new Button(newGameButtonTexture, buttonFont)
        {
            Position = new Vector2(300, 200),
        };

        newGameButton.Click += NewGameButton_Click;
        /*
                var continueButton = new Button(continueButtonTexture, buttonFont)
                {
                    Position = new Vector2(300, 250),
                };

                continueButton.Click += ContinueButton_Click;
        */
        var scoreButton = new Button(scoreButtonTexture, buttonFont)
        {
            Position = new Vector2(300, 250),
        };

        scoreButton.Click += ScoreButton_Click;

        var quitGameButton = new Button(quitButtonTexture, buttonFont)
        {
            Position = new Vector2(300, 300),
        };

        quitGameButton.Click += QuitGameButton_Click;

        components = new List<Component>()
        {
        newGameButton,
        //continueButton,
        scoreButton,
        quitGameButton,
        };
        }
        /// <summary>
        /// Draws the game state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Vector2(0, 0), new Rectangle(0, 0, thisScreenSize.X, thisScreenSize.Y), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
        /// <summary>
        /// Handles the Click event of the ContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Game");
        }

        /// <summary>
        /// Creates new gamebutton_click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            thisGame.ChangeState(new StateSingleplayer(thisGame, thisGraphicsDevice, thisContent, thisScreenSize));
        }

        /// <summary>
        /// Handles the Click event of the ScoreButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ScoreButton_Click(object sender, EventArgs e)
        {
            thisGame.ChangeState(new ScoreState(thisGame, thisGraphicsDevice, thisContent, thisScreenSize));
        }

        /// <summary>
        /// Updates buttons.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }

        /// <summary>
        /// Handles the Click event of the QuitGameButton control to quit the game.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            thisGame.Exit();
        }
        /// <summary>
        /// Action after update
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }
    }
}
