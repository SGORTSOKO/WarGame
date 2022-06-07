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

using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AKSU.ScreenComponent;

namespace AKSU.States
{
    /// <summary>
    /// Класс настроек SettingsState.
    /// Implements the <see cref="AKSU.States.BaseState" />
    /// </summary>
    /// <seealso cref="AKSU.States.BaseState" />
    class SettingsState : BaseState
    {
        /// <summary>
        /// Текстура надписи "размер экрана"
        /// </summary>
        private Texture2D screenSizeTexture;
        /// <summary>
        /// Компоненты экрана
        /// </summary>
        private List<BaseComponent> components;
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
        private SpriteFont buttonFont;
        /// <summary>
        /// Конструктор класса <see cref="MenuState" />.
        /// </summary>
        /// <param name="InputGame">Инициализатор приложения</param>
        /// <param name="InputGraphicsDevice">Графическое устройство.</param>
        /// <param name="InputContentManager">Менеджер контента</param>
        /// <param name="InputScreenSize">Размер экрана</param>
        public SettingsState(
            Initializer InputGame,
            GraphicsDevice InputGraphicsDevice,
            ContentManager InputContentManager,
            CoordinatesXY InputScreenSize)
        : base(
              InputGame,
              InputGraphicsDevice,
              InputContentManager,
              InputScreenSize)
        {
            Texture2D S1280x1024Texture = InputContentManager.Load<Texture2D>("Buttons/1280x1024");
            Texture2D S1366x768Texture = InputContentManager.Load<Texture2D>("Buttons/1366x768");
            Texture2D S1440x900Texture = InputContentManager.Load<Texture2D>("Buttons/1440x900");
            Texture2D S1600x900Texture = InputContentManager.Load<Texture2D>("Buttons/1600x900");
            Texture2D S1920x1080Texture = InputContentManager.Load<Texture2D>("Buttons/1920x1080");
            Texture2D fullscreenTexture = InputContentManager.Load<Texture2D>("Buttons/Fullscreen");
            Texture2D windowedTexture = InputContentManager.Load<Texture2D>("Buttons/Windowed");
            Texture2D newGameButtonTexture = InputContentManager.Load<Texture2D>("Buttons/NewGame");
            screenSizeTexture = InputContentManager.Load<Texture2D>("Buttons/ScreenSize");
            backGround = InputContentManager.Load<Texture2D>("BackGrounds/Road");
            buttonFont = InputContentManager.Load<SpriteFont>("Fonts/TimesNewRoman");

            if (File.Exists("./Content/PlayerData.ini")) //Если существует файл настроек, прочитать имя
                using (StreamReader Reader = new StreamReader("Content/PlayerData.ini"))
                {
                    textName = Reader.ReadLine();
                    if (textName.Length < 1)
                    {
                        textName = "Player";
                    }
                }
            else //иначе принять по умолчанию
            {
                textName = "Player";
            }
            currentGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 100);

            Button S1280x1024Button = new Button(S1280x1024Texture, buttonFont) //Кнопки изменения размера экрана
            {
                Position = new Vector2(200, 150),
            };
            S1280x1024Button.Click += S1280x1024Button_Click;
            
            Button S1366x768Button = new Button(S1366x768Texture, buttonFont) //Кнопки изменения размера экрана
            {
                Position = new Vector2(200, 200),
            };
            S1366x768Button.Click += S1366x768Button_Click;

            Button S1440x900Button = new Button(S1440x900Texture, buttonFont) //Кнопки изменения размера экрана
            {
                Position = new Vector2(200, 250),
            };
            S1440x900Button.Click += S1440x900Button_Click;

            Button S1600x900Button = new Button(S1600x900Texture, buttonFont) //Кнопки изменения размера экрана
            {
                Position = new Vector2(200, 300),
            };
            S1600x900Button.Click += S1600x900Button_Click;

            Button S1920x1080Button = new Button(S1920x1080Texture, buttonFont) //Кнопки изменения размера экрана
            {
                Position = new Vector2(200, 350),
            };
            S1920x1080Button.Click += S1920x1080Button_Click;

            Button FullscreenButton = new Button(fullscreenTexture, buttonFont) //Кнопка полноэкранного режима
            {
                Position = new Vector2(200, 400),
            };
            FullscreenButton.Click += FullscreenButton_Click;

            Button WindowedButton = new Button(windowedTexture, buttonFont) //Кнопка оконного игрока
            {
                Position = new Vector2(200, 450),
            };
            WindowedButton.Click += WindowedButton_Click;

            Button MenuButton = new Button(InputContentManager.Load<Texture2D>("Buttons/Menu"), 
                InputContentManager.Load<SpriteFont>("Fonts/TimesNewRomanSmall")) //Кнопка выхода в меню
            { 
                Position = new Vector2(thisScreenSize.CoordinateIntX * 0.01f, thisScreenSize.CoordinateIntY * 0.1f)
            };
            MenuButton.Click += MenuButton_Click;

            components = new List<BaseComponent>() //Компоненты экрана
            {
                S1280x1024Button,
                S1366x768Button,
                S1440x900Button,
                S1600x900Button,
                S1920x1080Button,
                FullscreenButton,
                WindowedButton,
                MenuButton
            };
        }


        /// <summary>
        /// Обновление логики 
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void Update(GameTime CurrentGameTime)
        {
            KeyboardState CurrentKeyboardState = Keyboard.GetState(); //Получить состояние клавиатуры
            string Timed;
            
            if (textName.Length > 0) //Если имя не стерто поностью
            {
                if (CurrentKeyboardState.IsKeyDown(Keys.Back)) //Ползволить стереть символ
                {
                    textName = textName.Substring(0, textName.Length - 1);
                }
            }

            foreach (Keys G in CurrentKeyboardState.GetPressedKeys()) //Получить нажатые клавиши
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
                    G == Keys.Space
                    ) //Если однаиз букв, то
                {
                    if (CurrentKeyboardState.IsKeyDown(Keys.LeftShift)) //Если нажата shift, то
                    {
                        textName += G;
                    }
                    else
                    {
                        Timed = "" + G;
                        textName += Timed.ToLower();
                    }
                }
            }
            foreach (var Component in components)
                Component.Update(CurrentGameTime);
        }

        /// <summary>
        /// Отрисовать экран
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        /// <param name="CurrentSpriteBatch">Объект оптимизатора графической отрисовки</param>
        public override void Draw(GameTime CurrentGameTime, SpriteBatch CurrentSpriteBatch)
        {
            CurrentSpriteBatch.Begin();

            CurrentSpriteBatch.Draw(
                backGround,
                new Vector2(0, 0),
                new Rectangle(0, 0, thisScreenSize.CoordinateIntX, thisScreenSize.CoordinateIntY),
                Color.White,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0f); //Отрисовать фон

            CurrentSpriteBatch.Draw(
                screenSizeTexture, 
                new Vector2(150, 50), 
                Color.White); //Отрисовать надпись "размер экрана"

            
            CurrentSpriteBatch.DrawString(
                buttonFont, 
                textName, 
                new Vector2(600, 170), 
                Color.Blue); //Отрисовать имя

            foreach (var Component in components) //Отрисовать компоненты
                Component.Draw(CurrentGameTime, CurrentSpriteBatch);

            currentGame.MainGraphics.ApplyChanges(); //Принять изменения экрана

            CurrentSpriteBatch.End();
        }


        /// <summary>
        /// Обработчик события нажатия кнопки выхода в меню
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void MenuButton_Click(object Sender, System.EventArgs e)
        {
            using (StreamWriter Writer = new StreamWriter("Content/PlayerData.ini", false)) //Сохранить имя при выходе
            {
                if (textName.Length > 0)
                {
                    Writer.Write(textName);
                }
                else
                {
                    Writer.Write("Player");
                }
            }

            currentGame.TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50); //Изменить частоту обновления экрана

            currentGame.ChangeState(new MenuState(
                currentGame,
                currentGraphicsDevice,
                currentContentManager,
                thisScreenSize)); //Сменить экран
        }

        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1280x1024Button_Click(object Sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1280, 1024);
            currentGame.MainGraphics.PreferredBackBufferWidth = thisScreenSize.CoordinateIntX;
            currentGame.MainGraphics.PreferredBackBufferHeight = thisScreenSize.CoordinateIntY;
            currentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1366x768Button_Click(object Sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1366, 768);
            currentGame.MainGraphics.PreferredBackBufferWidth = thisScreenSize.CoordinateIntX;
            currentGame.MainGraphics.PreferredBackBufferHeight = thisScreenSize.CoordinateIntY;
            currentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1440x900Button_Click(object Sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1440, 900);
            currentGame.MainGraphics.PreferredBackBufferWidth = thisScreenSize.CoordinateIntX;
            currentGame.MainGraphics.PreferredBackBufferHeight = thisScreenSize.CoordinateIntY;
            currentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1600x900Button_Click(object Sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1600, 900);
            currentGame.MainGraphics.PreferredBackBufferWidth = thisScreenSize.CoordinateIntX;
            currentGame.MainGraphics.PreferredBackBufferHeight = thisScreenSize.CoordinateIntY;
            currentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки смены размера экрана
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void S1920x1080Button_Click(object Sender, System.EventArgs e)
        {
            thisScreenSize = new CoordinatesXY(1920, 1080);
            currentGame.MainGraphics.PreferredBackBufferWidth = thisScreenSize.CoordinateIntX;
            currentGame.MainGraphics.PreferredBackBufferHeight = thisScreenSize.CoordinateIntY;
            currentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки перехода в полноэкранный режим
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void FullscreenButton_Click(object Sender, System.EventArgs e)
        {
            currentGame.MainGraphics.IsFullScreen = true;
            currentGame.MainGraphics.ApplyChanges();
        }
        /// <summary>
        /// Обработчик события нажатия кнопки перехода в оконный режим
        /// </summary>
        /// <param name="Sender">Источник события</param>
        /// <param name="e"><see cref="EventArgs" /> содержащий данные события</param>
        public void WindowedButton_Click(object Sender, System.EventArgs e)
        {
            currentGame.MainGraphics.IsFullScreen = false;
            currentGame.MainGraphics.ApplyChanges();
        }

        /// <summary>
        /// Действие после обновления
        /// </summary>
        /// <param name="CurrentGameTime">Время с последнего вызова <see cref="M:Microsoft.Xna.Framework.Game.Update(Microsoft.Xna.Framework.GameTime)" />.</param>
        public override void PostUpdate(GameTime CurrentGameTime)
        {
        }
    }
}
