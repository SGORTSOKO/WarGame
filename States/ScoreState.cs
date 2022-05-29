using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WarGame.States
{
    class ScoreState : State
    {
        private SpriteFont font;
        private List<Component> components;
        private Texture2D backGround;
        private ScoreManager scoreManager;
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
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, new Vector2(0, 0), new Rectangle(0, 0, thisScreenSize.X, thisScreenSize.Y), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "Highscores:\n" + string.Join("\n", scoreManager.Highscores.Select(c => c.PlayerName + ": " + c.Value).ToArray()), new Vector2(300, 10), Color.Black);
            spriteBatch.End();
        }


        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }
        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            thisGame.ChangeState(new MenuState(thisGame, thisGraphicsDevice, thisContent, thisScreenSize));
        }
    }
}
