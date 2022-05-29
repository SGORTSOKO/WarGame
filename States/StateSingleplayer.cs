using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using WarGame.States;

namespace WarGame
{
    class StateSingleplayer : State
    {
        private List<Texture2D> listOfTextures = new List<Texture2D>();
        private Texture2D backGround;
        private Random rand = new Random();
        private SpriteFont textBlock2;
        private MouseState lastMouseState;
        private Vector2 mousePosition;
        private Player left;
        private Player right;
        private Player winner;
        private CreatureList playList;
        private List<Component> gameComponents;
        private ScoreManager scoreManager;
        private int timer;
        public StateSingleplayer(GameWindow inputGame, GraphicsDevice inputGraphicsDevice, ContentManager inputContent, XY inputScreenSize)
            : base(inputGame, inputGraphicsDevice, inputContent, inputScreenSize)
        {
            scoreManager = ScoreManager.Load();
            textBlock2 = inputContent.Load<SpriteFont>("Fonts/TimesNewRoman");
            listOfTextures.Add(inputContent.Load<Texture2D>("Creatures/Human"));
            backGround = inputContent.Load<Texture2D>("BackGrounds/Road");
            left = new Player("Left", Color.Blue, 100, true, thisScreenSize);
            right = new Player("Right", Color.Red, 100, false, thisScreenSize);
            winner = null;
            playList = new CreatureList(left, right);
            Button MenuButton = new Button(inputContent.Load<Texture2D>("Buttons/MenuButton"), inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(thisScreenSize.X * 0.01f, thisScreenSize.Y * 0.9f)
            };
            MenuButton.Click += MenuButton_Click;
            var quitButton = new Button(inputContent.Load<Texture2D>("Buttons/ExitButton"), inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(thisScreenSize.X * 0.01f, thisScreenSize.Y * 0.95f),
            };
            quitButton.Click += QuitButton_Click;
            gameComponents = new List<Component>()
            {
                MenuButton,
                quitButton,
            };
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.F1))
            {
                thisGame.mainGraphics.IsFullScreen = false;
                thisGame.mainGraphics.ApplyChanges();
            }
            if (keyboardState.IsKeyDown(Keys.F2))
            {
                thisGame.mainGraphics.IsFullScreen = true;
                thisGame.mainGraphics.ApplyChanges();
            }
            if (keyboardState.IsKeyUp(Keys.F3))
            {
                if (thisGame.TargetElapsedTime.Milliseconds < 900)
                {
                    thisGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, thisGame.TargetElapsedTime.Milliseconds + 1);
                }
            }
            if (keyboardState.IsKeyUp(Keys.F4))
            {
                if (thisGame.TargetElapsedTime.Milliseconds > 10)
                {
                    thisGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, thisGame.TargetElapsedTime.Milliseconds - 1);
                }
            }
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState.X != lastMouseState.X || currentMouseState.Y != lastMouseState.Y)
            {
                mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            }
            lastMouseState = currentMouseState;
            if (winner != null)
            {
                playList.Clear();
                scoreManager.Add(new Score()
                {
                    PlayerName = left.Name,
                    Value = timer,
                });
                ScoreManager.Save(scoreManager);
            }
            else
            {
                playList.RandSort();
                playList.ResetStats();
                if (true)
                {
                    //playList.Add("Human", left, rand.Next(0, (int)(thisScreenSize.Y * 0.8)), listOfTextures[0]);//rand.Next(0, 1079 - 140) //currentMouseState.X
                    playList.Add("Human", right, rand.Next(0, (int)(thisScreenSize.Y * 0.8)), listOfTextures[0]);
                    playList.Add("Human", left, rand.Next(0, (int)(thisScreenSize.Y * 0.8)), listOfTextures[0]);//rand.Next(0, 1079 - 140) //currentMouseState.X
                    playList.Add("Human", right, rand.Next(0, (int)(thisScreenSize.Y * 0.8)), listOfTextures[0]);
                }
                playList.AttackRound();
                winner = playList.StepAll();
                /*
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                }
                */
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    playList.DeleteSome(10);
                }
                timer++;
            }
            foreach (Component component in gameComponents)
                component.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(backGround, new Vector2(0, 0), new Rectangle(0, 0, thisScreenSize.X, thisScreenSize.Y), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            foreach (Component component in gameComponents)
            {
                component.Draw(gameTime, spriteBatch);
            }
            for (int i = 0; i < playList.Count; i++)
            {
                spriteBatch.Draw(playList[i].SelfTexture, playList[i].NowPosition, playList[i].GetRectangleImage(), playList[i].Player.Color, 0, Vector2.Zero, thisScreenSize.X / 1920f, SpriteEffects.None, 0);
            }
            if (winner != null)
            {
                spriteBatch.DrawString(textBlock2, winner.Name + " Won", new Vector2((thisScreenSize.X) / 2.0f - textBlock2.MeasureString(left.Name + " Won").X, thisScreenSize.Y * 0.8f), winner.Color);
            }
            else
            {
                spriteBatch.DrawString(textBlock2, Convert.ToString(left.HP), new Vector2(thisScreenSize.X * 0.1f, thisScreenSize.Y * 0.9f), left.Color); // - textBlock2.MeasureString(Convert.ToString(left.HP)).X
                spriteBatch.DrawString(textBlock2, Convert.ToString(right.HP), new Vector2(thisScreenSize.X * 0.9f - textBlock2.MeasureString(Convert.ToString(right.HP)).X, thisScreenSize.Y * 0.9f), right.Color);
                spriteBatch.DrawString(textBlock2, Convert.ToString(timer), new Vector2(thisScreenSize.X * 0.5f - textBlock2.MeasureString(Convert.ToString(playList.Count)).X, thisScreenSize.Y * 0.9f), Color.Black); //playList.Count
                //spriteBatch.DrawString(textBlock2, Convert.ToString(random), new Vector2(screenSize.X * 0.5f, screenSize.Y * 0.1f), Color.Black);
            }
            spriteBatch.End();
        }
        public void QuitButton_Click(object sender, System.EventArgs e)
        {
            thisGame.Exit();
        }
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            thisGame.ChangeState(new MenuState(thisGame, thisGraphicsDevice, thisContent, thisScreenSize));
        }
        public override void PostUpdate(GameTime gameTime)
        {

        }
    }
}
