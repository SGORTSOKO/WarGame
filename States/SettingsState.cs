// ***********************************************************************
// Assembly         : WarGame
// Author           : abros
// Created          : 06-03-2022
//
// Last Modified By : abros
// Last Modified On : 06-06-2022
// ***********************************************************************
// <copyright file="SettingsState.cs" company="Kurgan State University">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using WarGame.ScreenComponent;

namespace WarGame.States
{
    /// <summary>
    /// Класс настроек SettingsState.
    /// Implements the <see cref="WarGame.States.State" />
    /// </summary>
    /// <seealso cref="WarGame.States.State" />
    class SettingsState : State
    {
        /// <summary>
        /// Текстура надписи "размер экрана"
        /// </summary>
        private Texture2D screenSizeTexture;
        /// <summary>
        /// Компоненты экрана
        /// </summary>
        private List<Component> components;
        /// <summary>
        /// Фоновое изображение
        /// </summary>
        private Texture2D backGround;
        /// <summary>
        /// Имя игрока
        /// </summary>
        private string textName;
        /// <summary>
        /// Шрифт кнопок
        /// </summary>
        SpriteFont buttonFont;
        /// <summary>
        /// Конструктор класса <see cref="MenuState" />.
        /// </summary>
        /// <param name="inputGame">Инициализатор приложения</param>
        /// <param name="inputGraphicsDevice">Графическое устройство.</param>
        /// <param name="inputContent">Менеджер контента</param>
        /// <param name="inputScreenSize">Размер экрана</param>
        public SettingsState(
            Initializer inputGame,
            GraphicsDevice inputGraphicsDevice,
            ContentManager inputContent,
            CoordinatesXY inputScreenSize)
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

            //Если существует файл настроек, прочитать имя
            if (File.Exists("./Content/PlayerData.ini"))
                using (StreamReader reader = new StreamReader("Content/PlayerData.ini"))
                {
                    textName = reader.ReadLine();
                    if (textName.Length < 1)
                    {
                        textName = "Player";
                    }
                }
            //иначе принять по умолчанию
            else
            {
                textName = "Player";
            }
            thisGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 100);

            //Кнопки изменения размера экрана
            Button s1280x1024Button = new Button(s1280x1024Texture, buttonFont)
            {
                Position = new Vector2(200, 150),
            };
            s1280x1024Button.Click += S1280x1024Button_Click;

            //Кнопки изменения размера экрана
            Button s1366x768Button = new Button(s1366x768Texture, buttonFont)
            {
                Position = new Vector2(200, 200),
            };
            s1366x768Button.Click += S1366x768Button_Click;

            //Кнопки изменения размера экрана
            Button s1440x900Button = new Button(s1440x900Texture, buttonFont)
            {
                Position = new Vector2(200, 250),
            };
            s1440x900Button.Click += S1440x900Button_Click;

            //Кнопки изменения размера экрана
            Button s1600x900Button = new Button(s1600x900Texture, buttonFont)
            {
                Position = new Vector2(200, 300),
            };
            s1600x900Button.Click += S1600x900Button_Click;

            //Кнопки изменения размера экрана
            Button s1920x1080Button = new Button(s1920x1080Texture, buttonFont)
            {
                Position = new Vector2(200, 350),
            };
            s1920x1080Button.Click += S1920x1080Button_Click;

            //Кнопка полноэкранного режима
            Button fullscreenButton = new Button(fullscreenTexture, buttonFont)
            {
                Position = new Vector2(200, 400),
            };
            fullscreenButton.Click += FullscreenButton_Click;

            //Кнопка оконного игрока
            Button windowedButton = new Button(windowedTexture, buttonFont)
            {
                Position = new Vector2(200, 450),
            };
            windowedButton.Click += WindowedButton_Click;

            //Кнопка выхода в меню
            Button menuButton = new Button(inputContent.Load<Texture2D>("Buttons/Menu"), inputContent.Load<SpriteFont>("Fonts/TimesNewRomanSmall"))
            {
                Position = new Vector2(thisScreenSize.X * 0.01f, thisScreenSize.Y * 0.1f)
            };
            menuButton.Click += MenuButton_Click;

            //Компоненты экрана
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
        /// Обновление логики 
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime gameTime)
        {
            //Получить состояние клавиатуры
            KeyboardState keyboardState = Keyboard.GetState();
            string timed;
            //Если имя не стерто поностью
            if (textName.Length > 0)
            {
                //Ползволить стереть символ
                if (keyboardState.IsKeyDown(Keys.Back))
                {
                    textName = textName.Substring(0, textName.Length - 1);
                }
            }
            //Получить нажатые клавиши
            foreach (Keys G in keyboardState.GetPressedKeys())
            {
                //Если однаиз букв, то
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
                    G == Keys.Space
                    )
                {
                    //Если нажата shift, то
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
        /// Отрисовать экран
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="spriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //Отрисовать фон
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

            //Отрисовать надпись "размер экрана"
            spriteBatch.Draw(screenSizeTexture, new Vector2(150, 50), Color.White);

            //Отрисовать имя
            spriteBatch.DrawString(buttonFont, textName, new Vector2(600, 170), Color.Blue);

            //Отрисовать компоненты
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            //Принять изменения экрана
            thisGame.mainGraphics.ApplyChanges();

            spriteBatch.End();
        }


        /// <summary>
        /// Обработчик события нажатия кнопки выхода в меню
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void MenuButton_Click(object sender, System.EventArgs e)
        {
            //Сохранить имя при выходе
            using (StreamWriter writer = new StreamWriter("Content/PlayerData.ini", false))
            {
                if (textName.Length > 0)
                    writer.Write(textName);
                writer.Write("Player");
            }

            //Изменить частоту обновления экрана
            thisGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50);

            //Сменить экран
            thisGame.ChangeState(new MenuState(
                thisGame,
                thisGraphicsDevice,
                thisContent,
                thisScreenSize));
        }

        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1280x1024Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1280, 1024);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1366x768Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1366, 768);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1440x900Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1440, 900);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1600x900Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1600, 900);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1920x1080Button_Click(object sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1920, 1080);
            thisGame.mainGraphics.PreferredBackBufferWidth = thisScreenSize.X;
            thisGame.mainGraphics.PreferredBackBufferHeight = thisScreenSize.Y;
            thisGame.mainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки перехода в полноэкранный режим
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void FullscreenButton_Click(object sender, System.EventArgs e)
        {
            thisGame.mainGraphics.IsFullScreen = true;
            thisGame.mainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки перехода в оконный режим
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void WindowedButton_Click(object sender, System.EventArgs e)
        {
            thisGame.mainGraphics.IsFullScreen = false;
            thisGame.mainGraphics.ApplyChanges();
        }

        /// <summary>
        /// Действие после обновления
        /// </summary>
        /// <param name="gameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void PostUpdate(GameTime gameTime)
        {
        }
    }
}
