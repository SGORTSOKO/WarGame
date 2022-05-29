using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame.States
{
    class RecordState : State
    {
        private Button button;

        private SpriteFont font;

        private int score;

        private ScoreManager scoreManager;

        private float timer;

        public static Random Random;
        private List<Component> components;
        private Texture2D backGround;
        public RecordState(GameWindow inputGame, GraphicsDevice inputGraphicsDevice, ContentManager inputContent, XY inputScreenSize)
        : base(inputGame, inputGraphicsDevice, inputContent, inputScreenSize)
        {
            Texture2D newGameButtonTexture = inputContent.Load<Texture2D>("Buttons/NewGameButton");
            Texture2D continueButtonTexture = inputContent.Load<Texture2D>("Buttons/ContinueButton");
            Texture2D quitButtonTexture = inputContent.Load<Texture2D>("Buttons/ExitButton");
            backGround = inputContent.Load<Texture2D>("BackGrounds/Road");
            SpriteFont buttonFont = inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall");
            Button newGameButton = new Button(newGameButtonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
            };

            newGameButton.Click += NewGameButton_Click;

            var continueButton = new Button(continueButtonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
            };

            continueButton.Click += ContinueButton_Click;

            var quitGameButton = new Button(quitButtonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<Component>()
        {
        newGameButton,
        continueButton,
        quitGameButton,
        };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Vector2(0, 0), new Rectangle(0, 0, thisScreenSize.X, thisScreenSize.Y), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Game");
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            thisGame.ChangeState(new StateSingleplayer(thisGame, thisGraphicsDevice, thisContent, thisScreenSize));
        }


        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            thisGame.Exit();
        }
        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }
    }
}
