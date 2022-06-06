using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using WarGame.States;
using WarGame.MScore;
using System.IO;


namespace WarGame
{
    /// <summary>
    /// Class StateSingleplayer.
    /// Implements the <see cref="State" />
    /// </summary>
    /// <seealso cref="State" />
    class StateSingleplayer : State
    {
        #region Fields
        /// <summary>
        /// The list of textures
        /// </summary>
        private List<Texture2D> listOfTextures = new List<Texture2D>();
        private Song music;
        private Song soundEnd;
        /// <summary>
        /// The back ground
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// The rand
        /// </summary>
        private Random rand = new Random();
        /// <summary>
        /// The text block2
        /// </summary>
        private SpriteFont textBlock2;
        /// <summary>
        /// The last mouse state
        /// </summary>
        private MouseState lastMouseState;
        /// <summary>
        /// The mouse position
        /// </summary>
        private Vector2 mousePosition;
        /// <summary>
        /// The left
        /// </summary>
        private Player left;
        /// <summary>
        /// The right
        /// </summary>
        private Player right;
        /// <summary>
        /// The winner
        /// </summary>
        private Player winner;
        /// <summary>
        /// The play list
        /// </summary>
        private CreatureList playList;
        /// <summary>
        /// The game components
        /// </summary>
        private List<Component> gameComponents;
        /// <summary>
        /// The score manager
        /// </summary>
        private ScoreManager scoreManager;
        /// <summary>
        /// The timer
        /// </summary>
        private int timer, timer2, timer3;
        /// <summary>
        /// The single
        /// </summary>
        private bool single;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="StateSingleplayer"/> class.
        /// </summary>
        /// <param name="inputGame">The input game.</param>
        /// <param name="inputGraphicsDevice">The input graphics device.</param>
        /// <param name="inputContent">Content of the input.</param>
        /// <param name="inputScreenSize">Size of the input screen.</param>
        public StateSingleplayer(
            Initializer inputGame, 
            GraphicsDevice inputGraphicsDevice, 
            ContentManager inputContent, 
            XY inputScreenSize)
            : base(
                  inputGame, 
                  inputGraphicsDevice, 
                  inputContent, 
                  inputScreenSize)
        {
            timer = 0;
            timer2 = 0;
            timer3 = 0;
            single = true;
            scoreManager = ScoreManager.Load();
            textBlock2 = inputContent.Load<SpriteFont>("Fonts/TimesNewRoman");
            listOfTextures.Add(inputContent.Load<Texture2D>("Creatures/Human"));
            backGround = inputContent.Load<Texture2D>("BackGrounds/Road");
            // загружаем ранее добавленный ресурс audio1
            music = inputContent.Load<Song>("MainMusic");
            soundEnd = inputContent.Load<Song>("MainMusic");
            // начинаем проигрывание мелодии
            MediaPlayer.Play(music);
            // повторять после завершения
            MediaPlayer.IsRepeating = true;

            string textNamePlayer;

            if (File.Exists("Content/PlayerData.ini"))
                using (StreamReader reader = new StreamReader("Content/PlayerData.ini"))
                {
                    textNamePlayer = reader.ReadLine();
                    if (textNamePlayer.Length < 1)
                    {
                        textNamePlayer = "PLAYER";
                    }
                }
            else
                using (StreamWriter writer = new StreamWriter("Content/PlayerData.ini", false))
                {
                    writer.WriteLine("PLAYER");
                    textNamePlayer = "PLAYER";
                }


            left = new Player(
                textNamePlayer, 
                Color.Blue, 
                1000, 
                true, 
                thisScreenSize);

            right = new Player(
                "BOT", 
                Color.Red, 
                1000000, 
                false, 
                thisScreenSize);

            winner = null;
            playList = new CreatureList(left, right);

            Button menuButton = new Button(inputContent.Load<Texture2D>("Buttons/Menu"), inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(thisScreenSize.X * 0.01f, thisScreenSize.Y * 0.9f)
            };
            menuButton.Click += MenuButton_Click;

            gameComponents = new List<Component>()
            {
                menuButton,
            };
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            timer2++;
            timer3++;
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

            if (keyboardState.IsKeyDown(Keys.F3))
            {
                if (thisGame.TargetElapsedTime.Milliseconds < 900)
                {
                    thisGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, thisGame.TargetElapsedTime.Milliseconds + 3);
                }
            }

            if (keyboardState.IsKeyDown(Keys.F4))
            {
                if (thisGame.TargetElapsedTime.Milliseconds > 10)
                {
                    thisGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, thisGame.TargetElapsedTime.Milliseconds - 3);
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
                if (single)
                {
                    MediaPlayer.Stop();
                    single = false;
                    playList.Clear();
                    scoreManager.Add(new Score()
                    {
                        PlayerName = left.Name,
                        Value = timer + (1000000 - right.HP) * (int)Math.Log10(timer),
                    }); //-------------------------------------------------------------------------------
                    ScoreManager.Save(scoreManager);

                }
            }
            else
            {
                playList.RandSort();
                playList.ResetStats();
                if (timer2 % (10 - (int)Math.Log10(timer2)) == 0)
                {
                    playList.Add(
                        "Human", 
                        right, 
                        rand.Next(0, (int)(thisScreenSize.Y * 0.8)), 
                        listOfTextures[0]);
                }

                playList.AttackRound();

                winner = playList.StepAll();

                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (
                        0 <= currentMouseState.Y && 
                        currentMouseState.Y <= thisScreenSize.Y * 0.8f && 
                        timer3 > 8 - (int)Math.Log10(timer2))
                    {
                        timer3 = 0;
                        playList.Add(
                            "Human", 
                            left, 
                            currentMouseState.Y, 
                            listOfTextures[0]);
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    playList.DeleteSome(10);
                }

                timer++;
            }
            foreach (Component component in gameComponents)
                component.Update(gameTime);
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            spriteBatch.Draw(
                backGround, 
                new Vector2(0, 0), 
                new Rectangle(
                    0, 
                    0, 
                    thisScreenSize.X, 
                    thisScreenSize.Y), 
                Color.White, 
                0f, 
                Vector2.Zero, 
                1.0f, 
                SpriteEffects.None, 
                0f);

            foreach (Component component in gameComponents)
            {
                component.Draw(gameTime, spriteBatch);
            }

            for (int i = 0; i < playList.Count; i++)
            {
                spriteBatch.Draw(
                    playList[i].SelfTexture, 
                    playList[i].NowPosition, 
                    playList[i].GetRectangleImage(), 
                    playList[i].Player.Color, 
                    0, 
                    Vector2.Zero, 
                    thisScreenSize.X / 1920f
                    , SpriteEffects.None, 
                    0);
            }

            if (winner != null)
            {
                spriteBatch.DrawString(
                    textBlock2, 
                    "GAME OVER", 
                    new Vector2((thisScreenSize.X) / 2.0f - textBlock2.MeasureString("GAME OVER").X, thisScreenSize.Y * 0.8f), 
                    winner.Color);
            }

            else
            {
                spriteBatch.DrawString(
                    textBlock2, 
                    Convert.ToString(left.HP), 
                    new Vector2(thisScreenSize.X * 0.1f, thisScreenSize.Y * 0.9f), 
                    left.Color);

                spriteBatch.DrawString(
                    textBlock2, 
                    Convert.ToString(right.HP), 
                    new Vector2(thisScreenSize.X * 0.9f - textBlock2.MeasureString(Convert.ToString(right.HP)).X, thisScreenSize.Y * 0.9f), 
                    right.Color);

                spriteBatch.DrawString(
                    textBlock2, 
                    Convert.ToString(timer), 
                    new Vector2(thisScreenSize.X * 0.5f - textBlock2.MeasureString(Convert.ToString(playList.Count)).X, thisScreenSize.Y * 0.9f), 
                    Color.Black);
            }
            spriteBatch.End();
        }
        /// <summary>
        /// Handles the Click event of the MenuButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            MediaPlayer.Stop();
            thisGame.ChangeState(new MenuState(
                thisGame, 
                thisGraphicsDevice, 
                thisContent, 
                thisScreenSize));
        }
        /// <summary>
        /// Posts the update.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void PostUpdate(GameTime gameTime)
        {

        }
    }
}
