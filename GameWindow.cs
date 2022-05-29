using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace WarGame
{
    public class GameWindow : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Texture2D> listOfTextures = new List<Texture2D>();
        Texture2D backGround, mouseImage;
        Random rand = new Random();
        SpriteFont textBlock, textBlock2;
        MouseState lastMouseState;
        Vector2 mousePosition;
        Player left;
        Player right;
        Player winner;
        CreatureList playList;
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
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ApplyChanges();
            base.Initialize();

        }
        protected override void LoadContent()
        {
            textBlock = Content.Load<SpriteFont>("Arial");
            textBlock2 = Content.Load<SpriteFont>("TimesNewRoman");
            listOfTextures.Add(Content.Load<Texture2D>("textsprite"));
            backGround = Content.Load<Texture2D>("Road");
            mouseImage = Content.Load<Texture2D>("2");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            left = new Player("Left", new Color(255, 0, 0), 10000, true);
            right = new Player("Right", new Color(0, 255, 0), 10000, false);
            winner = null;
            playList = new CreatureList(left, right);
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
                    playList.Add(new Human(listOfTextures[0], -108, rand.Next(0, 1080 - 140), 108, 140, 8, 2, 0, 0, 100, 10, left, left.Color, 1000, 50, 1));//rand.Next(0, 1079 - 140) //currentMouseState.X
                    playList.Add(new Human(listOfTextures[0], 2000, rand.Next(0, 1080 - 140), 108, 140, 8, 2, 0, 0, 100, 10, right, right.Color, 1000, 50, 1));
                    playList.Add(new Human(listOfTextures[0], -108, rand.Next(0, 1080 - 140), 108, 140, 8, 2, 0, 0, 100, 10, left, left.Color, 1000, 50, 1));//rand.Next(0, 1079 - 140) //currentMouseState.X
                    playList.Add(new Human(listOfTextures[0], 2000, rand.Next(0, 1080 - 140), 108, 140, 8, 2, 0, 0, 100, 10, right, right.Color, 1000, 50, 1));
                    playList.Add(new Human(listOfTextures[0], -108, rand.Next(0, 1080 - 140), 108, 140, 8, 2, 0, 0, 100, 10, left, left.Color, 1000, 50, 1));//rand.Next(0, 1079 - 140) //currentMouseState.X
                    playList.Add(new Human(listOfTextures[0], 2000, rand.Next(0, 1080 - 140), 108, 140, 8, 2, 0, 0, 100, 10, right, right.Color, 1000, 50, 1));
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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(backGround, new Vector2(0, 0), new Rectangle(0, 0, 1920, 1080), Color.White, 0f, Vector2.Zero, 1.1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(mouseImage, mousePosition, Color.Black);
            for (int i = 0; i < playList.Count; i++)
            {
                spriteBatch.Draw(playList[i].SelfTexture, playList[i].NowPosition,
                    playList[i].GetRectangleImage(),
                    playList[i].CreatureColor, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if (winner != null)
            {
                if (winner == left)
                {
                    spriteBatch.DrawString(textBlock2, left.Name + " Won", new Vector2(900, 980), left.Color);
                }
                else
                {
                    spriteBatch.DrawString(textBlock2, right.Name + " Won", new Vector2(900, 980), right.Color);
                }
            }
            else
            {
                spriteBatch.DrawString(textBlock2, Convert.ToString(left.HP), new Vector2(100, 980), left.Color);
                spriteBatch.DrawString(textBlock2, Convert.ToString(right.HP), new Vector2(1700, 980), right.Color);
                spriteBatch.DrawString(textBlock2, Convert.ToString(playList.Count), new Vector2(900, 980), Color.Black);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}