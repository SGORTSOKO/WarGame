using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace WarGame
{
    public class GameWindow : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<Texture2D> listOfTextures = new List<Texture2D>();
        private Texture2D backGround;
        private Random rand = new Random();
        private SpriteFont textBlock, textBlock2;
        private MouseState lastMouseState;
        private Vector2 mousePosition;
        private Player left;
        private Player right;
        private Player winner;
        private CreatureList playList;
        private XY screenSize;
        private int random = 0;
        private List<Component> gameComponents;
        public GameWindow()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50); //глобальная частота обновления
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            screenSize = new XY(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            graphics.PreferredBackBufferWidth = screenSize.X;
            graphics.PreferredBackBufferHeight = screenSize.Y;
            graphics.ApplyChanges();
            base.Initialize();

        }
        protected override void LoadContent()
        {
            textBlock = Content.Load<SpriteFont>("Fonts/TimesNewRomanSmall");
            textBlock2 = Content.Load<SpriteFont>("Fonts/TimesNewRoman");
            listOfTextures.Add(Content.Load<Texture2D>("Creatures/Human"));
            backGround = Content.Load<Texture2D>("BackGrounds/Road");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            left = new Player("Left", Color.Blue, 100, true);
            right = new Player("Right", Color.Red, 100, false);
            winner = null;
            playList = new CreatureList(left, right);
            Button randomButton = new Button(Content.Load<Texture2D>("Buttons/NewGameButton"), Content.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(50, 900)
            };
            randomButton.Click += RandomButton_Click;
            var quitButton = new Button(Content.Load<Texture2D>("Buttons/ExitButton"), Content.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(50, 930),
            };
            quitButton.Click += QuitButton_Click;
            gameComponents = new List<Component>()
            {
                randomButton,
                quitButton,
            };
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
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
            }
            else
            {
                playList.RandSort();
                playList.ResetStats();
                if (true)
                {
                    //playList.Add("Human", left, rand.Next(0, 70), listOfTextures[0]);//rand.Next(0, 1079 - 140) //currentMouseState.X
                    playList.Add("Human", right, rand.Next(0, 70), listOfTextures[0]);
                }
                playList.AttackRound();
                winner = playList.StepAll();
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                }
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    playList.DeleteSome(10);
                }
            }
            foreach (Component component in gameComponents)
                component.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(backGround, new Vector2(0, 0), new Rectangle(0, 0, screenSize.X, screenSize.Y), Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            foreach (Component component in gameComponents)
            {
                component.Draw(gameTime, spriteBatch);
            }
            for (int i = 0; i < playList.Count; i++)
            {
                spriteBatch.Draw(playList[i].SelfTexture, playList[i].NowPosition, playList[i].GetRectangleImage(), playList[i].Player.Color, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if (winner != null)
            {
                spriteBatch.DrawString(textBlock2, winner.Name + " Won", new Vector2((screenSize.X) / 2.0f - textBlock.MeasureString(left.Name + " Won").X, screenSize.Y * 0.8f), winner.Color);
            }
            else
            {
                spriteBatch.DrawString(textBlock2, Convert.ToString(left.HP), new Vector2(screenSize.X * 0.1f - textBlock.MeasureString(Convert.ToString(left.HP)).X, screenSize.Y * 0.9f), left.Color);
                spriteBatch.DrawString(textBlock2, Convert.ToString(right.HP), new Vector2(screenSize.X * 0.9f - textBlock.MeasureString(Convert.ToString(right.HP)).X, screenSize.Y * 0.9f), right.Color);
                spriteBatch.DrawString(textBlock2, Convert.ToString(playList.Count), new Vector2(screenSize.X * 0.5f - textBlock.MeasureString(Convert.ToString(playList.Count)).X, screenSize.Y * 0.9f), Color.Black);
                spriteBatch.DrawString(textBlock2, Convert.ToString(random), new Vector2(screenSize.X * 0.5f, screenSize.Y * 0.1f), Color.Black);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }
        private void RandomButton_Click(object sender, System.EventArgs e)
        {
            random = rand.Next();
        }
    }
}