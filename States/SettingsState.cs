using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace WarGame.States
{
    class SettingsState : State
    {
        private Texture2D screenSizeTexture;
        /// <summary>
        /// The components of screen
        /// </summary>
        private List<Component> components;
        /// <summary>
        /// The back ground image
        /// </summary>
        private Texture2D backGround;
        private string textName;
        SpriteFont buttonFont;
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuState"/> class.
        /// </summary>
        /// <param name="inputGame">The base GameWindow</param>
        /// <param name="inputGraphicsDevice">The input graphics device.</param>
        /// <param name="inputContent">The input ContentManager</param>
        /// <param name="inputScreenSize">Size of the screen.</param>
        public SettingsState(
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
            Texture2D s1280x1024Texture = inputContent.Load<Texture2D>("Buttons/1280x1024");
            Texture2D s1366x768Texture = inputContent.Load<Texture2D>("Buttons/1366x768");
            Texture2D s1440x900Texture = inputContent.Load<Texture2D>("Buttons/1440x900");
            Texture2D s1600x900Texture = inputContent.Load<Texture2D>("Buttons/1600x900");
            Texture2D s1920x1080Texture = inputContent.Load<Texture2D>("Buttons/1920x1080");
            Texture2D fullscreenTexture = inputContent.Load<Texture2D>("Buttons/Fullscreen");
            Texture2D windowedTexture = inputContent.Load<Texture2D>("Buttons/Windowed");
            Texture2D newGameButtonTexture = inputContent.Load<Texture2D>("Buttons/NewGame");
            screenSizeTexture = inputContent.Load<Texture2D>("Buttons/ScreenSize");
            backGround = inputContent.Load<Texture2D>("BackGrounds/Road");
            buttonFont = inputContent.Load<SpriteFont>("Fonts/TimesNewRoman");
            if (File.Exists("./Content/PlayerData.ini"))
                using (StreamReader reader = new StreamReader("Content/PlayerData.ini"))
                {
                    textName = reader.ReadLine();
                    if (textName.Length < 1)
                    {
                        textName = "Player";
                    }
                }
            else
            {
                textName = "Player";
            }
            thisGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 100);

            Button s1280x1024Button = new Button(s1280x1024Texture, buttonFont)
            {
                Position = new Vector2(200, 150),
            };

            s1280x1024Button.Click += S1280x1024Button_Click;

            
            Button s1366x768Button = new Button(s1366x768Texture, buttonFont)
            {
                Position = new Vector2(200, 200),
            };

            s1366x768Button.Click += S1366x768Button_Click;


            Button s1440x900Button = new Button(s1440x900Texture, buttonFont)
            {
                Position = new Vector2(200, 250),
            };

            s1440x900Button.Click += S1440x900Button_Click;


            Button s1600x900Button = new Button(s1600x900Texture, buttonFont)
            {
                Position = new Vector2(200, 300),
            };

            s1600x900Button.Click += S1600x900Button_Click;


            Button s1920x1080Button = new Button(s1920x1080Texture, buttonFont)
            {
                Position = new Vector2(200, 350),
            };

            s1920x1080Button.Click += S1920x1080Button_Click;


            Button fullscreenButton = new Button(fullscreenTexture, buttonFont)
            {
                Position = new Vector2(200, 400),
            };

            fullscreenButton.Click += FullscreenButton_Click;


            Button windowedButton = new Button(windowedTexture, buttonFont)
            {
                Position = new Vector2(200, 450),
            };

            windowedButton.Click += WindowedButton_Click;
            
            Button menuButton = new Button(inputContent.Load<Texture2D>("Buttons/Menu"), inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(thisScreenSize.X * 0.01f, thisScreenSize.Y * 0.1f)
            };
            menuButton.Click += MenuButton_Click;

            components = new List<Component>()
        {
                s1280x1024Button,
                s1366x768Button,
                s1440x900Button,
                s1600x900Button,
                s1920x1080Button,
                fullscreenButton,
                windowedButton,
                menuButton
        };
        }


        /// <summary>
        /// Updates buttons.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            string timed;
            if (textName.Length > 0)
            {
                if (keyboardState.IsKeyDown(Keys.Back))
                {
                    textName = textName.Substring(0, textName.Length - 1);
                }
            }
            foreach (Keys G in keyboardState.GetPressedKeys())
            {
                if (
                    G == Keys.A || 
                    G == Keys.B ||
                    G == Keys.C ||
                    G == Keys.D ||
                    G == Keys.E ||
                    G == Keys.F ||
                    G == Keys.G ||
                    G == Keys.H ||
                    G == Keys.I ||
                    G == Keys.J ||
                    G == Keys.K ||
                    G == Keys.L ||
                    G == Keys.M ||
                    G == Keys.N ||
                    G == Keys.O ||
                    G == Keys.P ||
                    G == Keys.Q ||
                    G == Keys.R ||
                    G == Keys.S ||
                    G == Keys.T ||
                    G == Keys.U ||
                    G == Keys.V ||
                    G == Keys.W ||
                    G == Keys.X ||
                    G == Keys.Y ||
                    G == Keys.Z ||
                    G == Keys.Space)
                {
                    if (keyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        textName += G;
                    }
                    else
                    {
                        timed = "" + G;
                        textName += timed.ToLower();
                    }
                }
            }
            foreach (var component in components)
                component.Update(gameTime);
        }

        /// <summary>
        /// Draws the game state.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(
                backGround,
                new Vector2(0, 0),
                new Rectangle(0, 0, thisScreenSize.X, thisScreenSize.Y),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f);

            spriteBatch.Draw(screenSizeTexture, new Vector2(150, 50), Color.White);

            spriteBatch.DrawString(buttonFont, textName, new Vector2(600, 170), Color.Blue);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            thisGame.mainGraphics.ApplyChanges();
            spriteBatch.End();
        }


        /// <summary>
        /// Handles the Click event of the MenuButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("Content/PlayerData.ini", false))
            {
                writer.Write(textName);
            }
            thisGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50);
            thisGame.ChangeState(new MenuState(
                thisGame,
                thisGraphicsDevice,
                thisContent,
                thisScreenSize));
        }

        public void S1280x1024Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new XY(1280, 1024);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        public void S1366x768Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new XY(1366, 768);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        public void S1440x900Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new XY(1440, 900);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        public void S1600x900Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new XY(1600, 900);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        public void S1920x1080Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new XY(1920, 1080);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        public void FullscreenButton_Click(object sender, System.EventArgs e)
        {
            thisGame.mainGraphics.IsFullScreen = true;
            thisGame.mainGraphics.ApplyChanges();
        }
        public void WindowedButton_Click(object sender, System.EventArgs e)
        {
            thisGame.mainGraphics.IsFullScreen = false;
            thisGame.mainGraphics.ApplyChanges();
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
